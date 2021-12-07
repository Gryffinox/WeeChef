using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerParent : MonoBehaviour {

    //Players
    private static Player[] Players;            //list of players by their player script component
    private static int ActivePlayerIndex;       //Current player
    private static List<int> PlayerTurnOrder;   //the order in which the players play which goes back and forth

    List<GameObject> cards = new List<GameObject>();

    //Gameobject
    [SerializeField] IngredientList ingredientList;
    [SerializeField] private GameObject CursorContainer;    //moves the whole cursor container which includes the click actions
    [SerializeField] private GameObject Cursor;             //just the visual cursor object
    [SerializeField] private GameObject IngredientGatheringUI;
    [SerializeField] GameObject onionCard;
    [SerializeField] GameObject porkCard;
    [SerializeField] GameObject cheeseCard;
    [SerializeField] GameObject ribsCard;
    [SerializeField] GameObject chickenCard;
    [SerializeField] GameObject potatoCard;
    [SerializeField] GameObject breadCard;
    [SerializeField] GameObject tomatoCard;
    [SerializeField] GameObject appleCard;
    [SerializeField] GameObject oliveCard;
    [SerializeField] GameObject fishCard;
    [SerializeField] GameObject wineCard;
    [SerializeField] GameObject brownieCard;
    [SerializeField] GameObject pepperCard;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip click;
    [SerializeField] AudioClip money;
    [SerializeField] GameObject hand;

    [SerializeField] GameObject turn1;
    [SerializeField] GameObject turn2;
    [SerializeField] GameObject turn3;
    [SerializeField] GameObject turn4;

    [SerializeField] TextMeshProUGUI scoreText;

    //Handler
    private MainGame GameHandler;
    private IngredientGatheringUI UIHandler;
    //Player parent handles all animators since only one hat can be moving at a time
    private Animator[] mAnimator;
    private bool HasValidAction = true;

    private bool skip1 = false;
    private bool skip2 = false;
    private bool skip3 = false;
    private bool skip4 = false;
    private bool allskip = false;
    [SerializeField] GameObject Uiskip1;
    [SerializeField] GameObject Uiskip2;
    [SerializeField] GameObject Uiskip3;
    [SerializeField] GameObject Uiskip4;

    void Start() {
        Players = GetComponentsInChildren<Player>();
        PlayerTurnOrder = new List<int>();
        //determine random play order
        int i = 0;  //index counter
        for (i = 0; i < Players.Length; i++) {
            PlayerTurnOrder.Add(i);
        }
        ActivePlayerIndex = 0;  //first player
                                //cursor to first player
        CursorContainer.transform.position = Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position;

        //handlers
        GameHandler = Camera.main.GetComponent<MainGame>();
        UIHandler = IngredientGatheringUI.GetComponent<IngredientGatheringUI>();
        //setup animators
        mAnimator = GetComponentsInChildren<Animator>();
        //show buy button for valid ingredient
        int x = (int)Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position.x;
        int y = (int)Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position.y;
        if (GameHandler.ValidIngredientInMap(x, y)) {
            UIHandler.ShowBuyButton();
            UIHandler.DisplayIngredientInfo(GameHandler.GetTileIngredient(x, y));    //get the ingredient from the map
            //enable buy button only if they can afford it
            UIHandler.SetBuyButtonInteractable(GameHandler.GetTileIngredient(x, y).Cost <= PlayerParent.GetActivePlayer().GetFunds());
        }
    }

    private void Update() {
        //Set animator for the current player
        mAnimator[PlayerTurnOrder[ActivePlayerIndex]].SetTrigger("isMoving");
        showCards();
        DisplayCoin();
        DisplaySkip();
        if (skip1 == true && skip2 == true && skip3 == true && skip4 == true) {
            allskip = true;
            skip1 = false;
            skip2 = false;
            skip3 = false;
            skip4 = false;

        }
        else {
            allskip = false;
        }
    }

    private void DisplaySkip() {
        if (skip1 == true) {
            Uiskip1.SetActive(true);
        }
        else {
            Uiskip1.SetActive(false);
        }
        if (skip2 == true) {
            Uiskip2.SetActive(true);
        }
        else {
            Uiskip2.SetActive(false);
        }
        if (skip3 == true) {
            Uiskip3.SetActive(true);
        }
        else {
            Uiskip3.SetActive(false);
        }
        if (skip4 == true) {
            Uiskip4.SetActive(true);
        }
        else {
            Uiskip4.SetActive(false);
        }
    }
    private void DisplayCoin() {
        scoreText.text = GetActivePlayer().GetFunds().ToString();
    }
    public void ResetPosition() {
        GameObject[] temp;
        temp = GameObject.FindGameObjectsWithTag("p1");
        foreach (GameObject go in temp) {
            if (go.GetComponent<CardMove>() != null) {
                go.GetComponent<CardMove>().goShuffle();
            }
            if (go.GetComponent<RecipeMove>() != null) {
                go.GetComponent<RecipeMove>().goShuffle();
            }
        }
        temp = GameObject.FindGameObjectsWithTag("p2");
        foreach (GameObject go in temp) {
            if (go.GetComponent<CardMove>() != null) {
                go.GetComponent<CardMove>().goShuffle();
            }
            if (go.GetComponent<RecipeMove>() != null) {
                go.GetComponent<RecipeMove>().goShuffle();
            }
        }
        temp = GameObject.FindGameObjectsWithTag("p3");
        foreach (GameObject go in temp) {
            if (go.GetComponent<CardMove>() != null) {
                go.GetComponent<CardMove>().goShuffle();
            }
            if (go.GetComponent<RecipeMove>() != null) {
                go.GetComponent<RecipeMove>().goShuffle();
            }
        }
        temp = GameObject.FindGameObjectsWithTag("p4");
        foreach (GameObject go in temp) {
            if (go.GetComponent<CardMove>() != null) {
                go.GetComponent<CardMove>().goShuffle();
            }
            if (go.GetComponent<RecipeMove>() != null) {
                go.GetComponent<RecipeMove>().goShuffle();
            }
        }
    }
    private void showCards() {
        int i = returnPlayer();
        GameObject[] temp;

        if (i == 0) {
            turn1.SetActive(true);
            turn2.SetActive(false);
            turn3.SetActive(false);
            turn4.SetActive(false);
            temp = GameObject.FindGameObjectsWithTag("p2");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p3");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p4");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
        }


        if (i == 1) {
            turn1.SetActive(false);
            turn2.SetActive(true);
            turn3.SetActive(false);
            turn4.SetActive(false);
            temp = GameObject.FindGameObjectsWithTag("p1");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p3");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p4");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
        }

        if (i == 2) {
            turn1.SetActive(false);
            turn2.SetActive(false);
            turn3.SetActive(true);
            turn4.SetActive(false);
            temp = GameObject.FindGameObjectsWithTag("p2");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p1");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p4");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
        }

        if (i == 3) {
            turn1.SetActive(false);
            turn2.SetActive(false);
            turn3.SetActive(false);
            turn4.SetActive(true);
            temp = GameObject.FindGameObjectsWithTag("p2");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p3");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p1");
            foreach (GameObject go in temp) {
                go.SetActive(false);
            }
        }
    }

    //for recipe building and anytime any player needs things
    public static Player GetActivePlayer() {
        return Players[PlayerTurnOrder[ActivePlayerIndex]];
    }

    //move player ez pz
    public void MoveAction() {
        Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position = Cursor.transform.position;
        audioSource.PlayOneShot(click, 1);
        EndTurn();
        UpdateValidAction();
    }

    public void playClick() {
        int i = returnPlayer();
        if (i == 0) {
            skip1 = true;
        }
        if (i == 1) {
            skip2 = true;
        }
        if (i == 2) {
            skip3 = true;
        }
        if (i == 3) {
            skip4 = true;
        }
        audioSource.PlayOneShot(click, 1);
    }
    public void clickSound() {
        audioSource.PlayOneShot(click, 1);
    }

    //buy ingredient at player coordinate
    public void BuyAction() {
        //Get coords of where the player is (coord of ingredient to be bought)
        int x = (int)Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position.x;
        int y = (int)Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position.y;
        //Get the ingredient
        Ingredient ingredientToAdd = GameHandler.GetTileIngredient(x, y);
        //add it to player hand
        GameObject temp;
        switch (ingredientToAdd.Id) {
            case 1: temp = Instantiate(onionCard); break; //allium
            case 2: temp = Instantiate(cheeseCard); break;
            case 3: temp = Instantiate(ribsCard); break;
            case 4: temp = Instantiate(chickenCard); break;
            case 5: temp = Instantiate(porkCard); break;
            case 6: temp = Instantiate(potatoCard); break;
            case 7: temp = Instantiate(breadCard); break;
            case 8: temp = Instantiate(tomatoCard); break;
            case 9: temp = Instantiate(appleCard); break;
            case 10: temp = Instantiate(oliveCard); break;
            case 11: temp = Instantiate(fishCard); break;
            case 12: temp = Instantiate(wineCard); break;
            case 13: temp = Instantiate(brownieCard); break;
            case 14: temp = Instantiate(pepperCard); break;
            default: temp = new GameObject(); break;
        }
        //
        setTag(temp);
        cards.Add(temp);
        Players[PlayerTurnOrder[ActivePlayerIndex]].decreaseFunds(ingredientToAdd.Cost);
        //
        GameHandler.RemoveIngredient(x, y); //remove ingredient from map
        ingredientList.DiscardCard(ingredientToAdd);
        audioSource.PlayOneShot(money, 1);
        EndTurn();
        UpdateValidAction();
    }

    public void removeCard(GameObject card) {
        cards.Remove(card);
        Destroy(card);
    }

    public void addCard(GameObject card) {
        cards.Add(card);
    }

    public void setTag(GameObject gameobject) {
        int i = returnPlayer();
        // Debug.Log(returnPlayer());

        if (i == 0) {
            gameobject.tag = "p1";
        }
        if (i == 1) {
            gameobject.tag = "p2";
        }
        if (i == 2) {
            gameobject.tag = "p3";
        }
        if (i == 3) {
            gameobject.tag = "p4";
        }

    }
    public void EndTurn() {
        ActivePlayerIndex++;    //next player
                                //if index exceeds our number of players, loop back to player 1 (index 0)
                                //decrease number of turns left
        if (ActivePlayerIndex >= Players.Length) {
            ActivePlayerIndex = 0;
        }
        //reset animators
        for (int i = 0; i < Players.Length; i++) {
            mAnimator[i].ResetTrigger("isMoving");
        }
        mAnimator[PlayerTurnOrder[ActivePlayerIndex]].SetTrigger("isMoving");
        //Set animator for the current player

        //move container to the next player
        CursorContainer.transform.position = Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position;
        //reset cursor position to center
        Cursor.transform.localPosition = new Vector3(0, 0, 0);
        //reset ui, default to available buy if possible. if no buy available, hide all buttons
        int x = (int)CursorContainer.transform.position.x;
        int y = (int)CursorContainer.transform.position.y;
        //if player on ingredient, display buy button
        if (GameHandler.ValidIngredientInMap(x, y)) {
            UIHandler.ShowBuyButton();
            UIHandler.DisplayIngredientInfo(GameHandler.GetTileIngredient(x, y));    //get the ingredient from the map
            //enable buy button only if they can afford it
            UIHandler.SetBuyButtonInteractable(GameHandler.GetTileIngredient(x, y).Cost <= PlayerParent.GetActivePlayer().GetFunds());
        }
        else {
            UIHandler.HideAllButtons();
            UIHandler.DisplayText("");
        }
        foreach (GameObject c in cards) {
            c.SetActive(true);
        }
    }

    public void UpdateValidAction() {
        int x = (int)CursorContainer.transform.position.x;
        int y = (int)CursorContainer.transform.position.y;
        //check if the new player has any valid moves. if not set a flag for NoMovesLeft
        //validate in all 4 directions
        HasValidAction = ValidateMove(x, y + 1) || ValidateMove(x, y - 1) || ValidateMove(x + 1, y) || ValidateMove(x - 1, y);
        //validate if theres an ingredient under the player currently
        if (GameHandler.ValidIngredientInMap(x, y)) {
            //if there is, can the player afford it
            if (GameHandler.GetTileIngredient(x, y).Cost > Players[PlayerTurnOrder[ActivePlayerIndex]].GetFunds()) {
                Debug.Log("Player can't afford ingredient");
                HasValidAction = HasValidAction || false;   //could be true because move allowed
            }
            else {
                HasValidAction = HasValidAction || true;    //could be false, but still buy the one ingredient
            }
        }
        else {
            HasValidAction = HasValidAction || false;
        }
    }

    //Returns true if the selected coordinates is already occupied
    public bool Overlap(int x, int y) {
        //check against all other player
        for (int i = 0; i < Players.Length; i++) {
            if (PlayerTurnOrder[i] != PlayerTurnOrder[ActivePlayerIndex]) {  //dont cross reference against the own player
                //if the tile selected is occupied by another player
                if (x == Players[PlayerTurnOrder[i]].transform.position.x && y == Players[PlayerTurnOrder[i]].transform.position.y) {
                    return true;
                }
            }
        }
        return false;
    }

    public bool NoMovesLeft() {
        //check each surrounding valid tile if theres at least an ingredient, no player, and within bounds
        return !HasValidAction;
    }

    public bool allSkip() {
        return allskip;
    }

    //wrapper class that validates whether theres an ingredient and/or player overlap on a given tile
    //returns true if no player and theres and ingredient to be moved on to
    public bool ValidateMove(int x, int y) {
        if (GameHandler.ValidIngredientInMap(x, y) && !Overlap(x, y)) {
            return true;
        }
        else {
            return false;
        }
    }

    private int returnPlayer() {
        return ActivePlayerIndex;
    }

    //returns the player with the most money
    public List<Player> GetPlayerWithHighestIncome() {
        //store the indeces of the players with highest income
        List<int> WinnerIndeces = new List<int> { 0 };    //automatically add player 1 to the list
        //for each player
        for (int i = 1; i < Players.Length; i++) {
            //if the next player has a higher income, replace the list with a new list of winners
            if (Players[PlayerTurnOrder[i]].GetFunds() > Players[WinnerIndeces[0]].GetFunds()) {
                WinnerIndeces.Clear();
                WinnerIndeces.Add(PlayerTurnOrder[i]);
            }
            //equal to highest income, add this player to the list
            else if (Players[PlayerTurnOrder[i]].GetFunds() == Players[WinnerIndeces[0]].GetFunds()) {
                WinnerIndeces.Add(PlayerTurnOrder[i]);
            }
        }
        List<Player> Winners = new List<Player>();
        for (int i = 0; i < WinnerIndeces.Count; i++) {
            Winners.Add(Players[WinnerIndeces[i]]);
        }
        return Winners;
    }

    //returns list on winners based on playerindex
    public List<Player> CheckWinner() {
        //list of winners
        List<Player> winners = new List<Player>();
        for (int i = 0; i < Players.Length; i++) {
            List<Recipe> hand = Players[PlayerTurnOrder[i]].GetRecipeHand();
            if(hand.Count > 0) {
                //dictionary
                Dictionary<string, int> counter = new Dictionary<string, int>();
                //for each recipe, increment counter
                foreach (Recipe recipe in hand) {
                    if (counter.ContainsKey(recipe.GetRecipeType())) {
                        counter[recipe.GetRecipeType()]++;
                    }
                    else {
                        counter.Add(recipe.GetRecipeType(), 1);
                    }
                }
                //now check for wincon
                foreach (int val in counter.Values) {
                    //if collected more than 4 recipes of a type
                    if (val >= 4) {
                        winners.Add(Players[PlayerTurnOrder[i]]);
                    }
                }
            }
        }
        return winners;
    }

    public Player[] GetPlayers() {
        return Players;
    }
    public void AddToPlayerIncome(int recipeWorth) {
        Players[PlayerTurnOrder[ActivePlayerIndex]].AddToIncome(recipeWorth);
    }

    public void skipReset() {
        skip1 = false;
        skip2 = false;
        skip3 = false;
        skip4 = false;
    }

    public void AddRecipeToPlayer(Recipe aRecipe) {
        Players[PlayerTurnOrder[ActivePlayerIndex]].AddRecipe(aRecipe);
    }

}
