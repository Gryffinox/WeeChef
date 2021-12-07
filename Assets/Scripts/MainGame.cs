using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainGame : MonoBehaviour {
    //Overall game variables
    private enum GamePhases { IngredientGathering = 0, RecipeBuilding = 1 };
    private int GamePhase;

    //Ingredient Gathering
    public const int MapSize = 5;
    private GameObject[,] Tiles;
    private IngredientList IngredientHandler;

    //Recipes
    private Recipe[] RecipesListed;
    [SerializeField] RecipeParent recipeParent;
    private RecipeList RecipeHandler;
    [SerializeField] private const float RecipeDistance = 2.25f;

    //Players
    [SerializeField] private GameObject Players;
    [SerializeField] private PlayerParent pParent;
    private PlayerParent PlayerHandler;

    //RoundsCounter
    private float RoundsCounter = 1;
    [SerializeField] private float MaxRounds = 16;

    //UI
    [SerializeField] private GameObject IngredientUI;
    [SerializeField] private GameObject phase1Text;
    [SerializeField] private GameObject phase2Text;
    [SerializeField] private GameObject phase1Stuck;
    [SerializeField] private GameObject RoundsDisplay;
    [SerializeField] private GameObject EndGameMessage;

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip cash;

    void Start() {
        //handlers
        PlayerHandler = Players.GetComponent<PlayerParent>();

        GamePhase = 0;      //0 tile ingredient gathering, 1 recipe building phase
        // Initialize tiles with Ingredient objects
        Tiles = new GameObject[MapSize, MapSize];

        // Get the IngredientList script from the PlayerCamera to access IngredientList methods
        IngredientHandler = GetComponent<IngredientList>();

        RefillMarket();

        //Recipe
        RecipeHandler = GetComponent<RecipeList>();
        RecipesListed = new Recipe[MapSize];
        RefillRecipeMenu();
        recipeParent.triggerOff();

    }

    void Update() {
        switch (GamePhase) {
            case (int)GamePhases.IngredientGathering:
                //Debug.Log("currently phase 1");
                // Do ingredient gathering here
                if (PlayerHandler.allSkip()) {
                    GamePhase = (int)GamePhases.RecipeBuilding;
                    IngredientUI.SetActive(false);
                    phase1Text.SetActive(false);
                    phase2Text.SetActive(true);
                    recipeParent.triggerOn();
                }
                else {

                    if (PlayerHandler.NoMovesLeft()) {

                        IngredientUI.SetActive(false);

                        phase1Text.SetActive(false);

                        phase2Text.SetActive(true);

                        phase1Stuck.SetActive(true);

                        pParent.skipReset();

                        recipeParent.triggerOn();

                    }

                }
                break;
            case (int)GamePhases.RecipeBuilding:

                // Do recipe building here

                if (PlayerHandler.allSkip()) {

                    GamePhase = (int)GamePhases.IngredientGathering;
                    IngredientUI.SetActive(true);
                    phase1Text.SetActive(true);
                    phase2Text.SetActive(false);

                    RefillMarket();

                    RefillRecipeMenu();

                    audioSource.PlayOneShot(cash, 1);

                    //needs to be called after market refilled

                    PlayerHandler.UpdateValidAction();

                    recipeParent.triggerOff();

                    foreach (Player player in PlayerHandler.GetPlayers()) {
                        player.CalculateFundsAtEndOfRound();
                    }

                    RoundsCounter++;

                    List<Player> winners = PlayerHandler.CheckWinner();
                    if(winners.Count > 0 || RoundsCounter > MaxRounds) {
                        if(winners.Count > 1) {
                            //same a gethighestincome but on list of winners
                            //store the indeces of the players with highest income
                            List<int> WinnerIndeces = new List<int> { 0 };    //automatically add player 1 to the list
                            //for each player
                            for (int i = 1; i < winners.Count; i++) {
                                //if the next player has a higher income, replace the list with a new list of winners
                                if (winners[i].GetFunds() > winners[WinnerIndeces[0]].GetIncome()) {
                                    WinnerIndeces.Clear();
                                    WinnerIndeces.Add(i);
                                }
                                //equal to highest income, add this player to the list
                                else if (winners[i].GetIncome() == winners[WinnerIndeces[0]].GetIncome()) {
                                    WinnerIndeces.Add(i);
                                }
                            }
                            List<Player> Winners = new List<Player>();
                            for (int i = 0; i < WinnerIndeces.Count; i++) {
                                Winners.Add(winners[WinnerIndeces[i]]);
                            }
                            winners = Winners;
                        }
                        EndGame(FormatWinMsg(winners));
                    }
                    if(RoundsCounter > MaxRounds) {
                        winners = PlayerHandler.GetPlayerWithHighestIncome();
                        EndGame(FormatWinMsg(winners));
                    }
                }

                break;
        }
        RoundsDisplay.GetComponent<TextMeshProUGUI>().text = "Rounds: " + RoundsCounter;
    }

    public void hideStuck() {

        phase1Stuck.SetActive(false);

    }

    //Getting and removing ingredients
    public Ingredient GetTileIngredient(int x, int y) {
        if (ValidIngredientInMap(x, y)) {
            //returns the ingredient at coords if it exists
            return Tiles[x, y].GetComponent<IngredientCard>().GetIngredient();
        }
        else {
            //game ded if game no give ingredient when asked
            throw new System.Exception("Empty tile at requested address: " + x + ", " + y);
        }
    }

    public void RemoveIngredient(int x, int y) {
        if (ValidIngredientInMap(x, y)) {
            //UNALIVE INGREDIENT
            Destroy(Tiles[x, y]);
        }
        else {
            //game ded if game no remove ingredient when asked
            throw new System.Exception("Trying to remove ingredient at: " + x + ", " + y);
        }
    }

    //validate coords
    public bool ValidIngredientInMap(int x, int y) {
        //if outside bounds of map
        if (x < 0 || x >= MapSize || y < 0 || y >= MapSize) {
            //Debug.Log("Address (" + x + ", " + y + ") out of bounds");
            return false;
        }
        //if no ingredient at coords
        if (Tiles[x, y] == null) {
            //Debug.Log("No ingredient at (" + x + ", " + y + ")");
            return false;
        }
        return true;
    }

    public void goPhase1() {

        GamePhase = 0;

    }

    public void goPhase2() {

        GamePhase = 1;

    }
    public void RefillMarket() {
        // for each tile in map, check if it's empty
        for (int column = 0; column < MapSize; column++) {
            for (int row = 0; row < MapSize; row++) {
                if (Tiles[column, row] == null) {

                    //Draw ingredient card from the deck

                    Ingredient newIng = IngredientHandler.DrawCard();

                    IngredientHandler.assignSprite(newIng.Id);

                    //Create it's instance and spawn it

                    Tiles[column, row] = Instantiate(IngredientHandler.getIngredientPrefab(), new Vector3(column, row, -2), Quaternion.identity);

                    Tiles[column, row].GetComponent<IngredientCard>().SetIngredient(newIng.Id, newIng.Name, newIng.Cost);

                    Tiles[column, row].transform.localScale = new Vector3(0.8f, 0.8f, 1);

                    Tiles[column, row].name = newIng.Name;

                }

            }

        }

    }

    public void RefillRecipeMenu() {

        //refill recipe list

        for (int i = 0; i < MapSize; i++) {

            //if theres a blank in the list

            if (RecipesListed[i] == null) {

                RecipesListed[i] = RecipeHandler.DrawRecipeCard();

                RecipeHandler.ToggleRecipeCard(RecipesListed[i].RecipeName, RecipesListed[i].RecipeType, true, (i - 2) * RecipeDistance);

                //Debug.Log("just added" + RecipesListed[i].RecipeName);

            }

        }

    }

    public void removeRecipe(string recipename) {

        for (int i = 0; i < MapSize; i++) {

            if (recipename == RecipesListed[i].RecipeName) {

                RecipesListed[i] = null;

            }

        }

    }

    private string FormatWinMsg(List<Player> winners) {
        switch (winners.Count) {
            case 1:
                return winners[0].GetName() + " wins!";
            case 2:
                return winners[0].GetName() + " and " + winners[1].GetName() + " win!";
            case 3:
                return winners[0].GetName() + ", " + winners[1].GetName() + " and " + winners[2].GetName() + " win!";
            case 4:
                return "Wow either everyone is really bad or everyone is really good because everyone wins";
            default:
                return "No one won somehow which should be impossible";
        }
    }

    private void EndGame(string winMsg) {
        //display winner
        IngredientUI.SetActive(false);
        phase1Text.SetActive(false);
        phase2Text.SetActive(false);
        Time.timeScale = 0;
        EndGameMessage.SetActive(true);
        EndGameMessage.GetComponentInChildren<TextMeshProUGUI>().text = winMsg;
    }
}
