using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private RecipeList RecipeHandler;
    [SerializeField] private const float RecipeX = 9;
    [SerializeField] private const float RecipeDistance = 2.25f;

    //Players
    [SerializeField] private GameObject Players;
    [SerializeField] private PlayerParent pParent;
    private PlayerParent PlayerHandler;
    //UI
    [SerializeField] private GameObject IngredientUI;
    [SerializeField] private GameObject phase1Text;
    [SerializeField] private GameObject phase2Text;
    [SerializeField] private GameObject phase1Stuck;
    private IngredientGatheringUI UIHandler;

    void Start() {
        //handlers
        PlayerHandler = Players.GetComponent<PlayerParent>();
        UIHandler = IngredientUI.GetComponent<IngredientGatheringUI>();

        GamePhase = 0;      //0 tile ingredient gathering, 1 recipe building phase
        // Initialize tiles with Ingredient objects
        Tiles = new GameObject[MapSize, MapSize];

        // Get the IngredientList script from the PlayerCamera to access IngredientList methods
        IngredientHandler = GetComponent<IngredientList>();

        // Fill every tile with a random Ingredient object from the list of Ingredient cards
        for (int column = 0; column < MapSize; column++) {
            for (int row = 0; row < MapSize; row++) {
                //Draw ingredient card from the deck
                Ingredient newIng = IngredientHandler.DrawCard();
                //Set sprite for gameobject
                IngredientHandler.assignSprite(newIng.Id);
                //Create it's instance and spawn it
                Tiles[column, row] = Instantiate(IngredientHandler.getIngredientPrefab(), new Vector3(column, row, -1), Quaternion.identity);
                //scale it down
                Tiles[column, row].GetComponent<IngredientCard>().SetIngredient(newIng.Id, newIng.Name, newIng.Cost);
                Tiles[column, row].transform.localScale = new Vector3(0.8f, 0.8f, 1);
                //name it
                Tiles[column, row].name = newIng.Name;
            }
        }

        //Recipe
        RecipeHandler = GetComponent<RecipeList>();
        RecipesListed = new Recipe[MapSize];
        RefillRecipeMenu();

    }

    void Update() {
        switch (GamePhase) {
            case (int)GamePhases.IngredientGathering:
                //Debug.Log("currently phase 1");
                // Do ingredient gathering here
                if (PlayerHandler.allSkip()) {
                    GamePhase = 1;
                    IngredientUI.SetActive(false);
                    phase1Text.SetActive(false);
                    phase2Text.SetActive(true);
                    RefillRecipeMenu();
                }
                else
                {
                    if (PlayerHandler.NoMovesLeft())
                    {
                        IngredientUI.SetActive(false);
                        phase1Text.SetActive(false);
                        phase2Text.SetActive(true);
                        phase1Stuck.SetActive(true);
                        RefillRecipeMenu();
                    }
                }
                break;
            case (int)GamePhases.RecipeBuilding:
                // Do recipe building here
                if (PlayerHandler.allSkip())
                {
                    GamePhase = 0;
                    IngredientUI.SetActive(true);
                    phase1Text.SetActive(true);
                    phase2Text.SetActive(false);
                }
                break;
        }
    }

    public void hideStuck()
    {
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

    public void goPhase1()
    {
        GamePhase = 0;
    }

    public void goPhase2()
    {
        GamePhase = 1;
    }
    public void RefillMarket()
    {
        // for each tile in map, check if it's empty
        for (int column = 0; column < MapSize; column++)
        {
            for (int row = 0; row < MapSize; row++)
            {
                if (Tiles[column, row] == null)
                {
                    //Draw ingredient card from the deck
                    Ingredient newIng = IngredientHandler.DrawCard();
                    IngredientHandler.assignSprite(newIng.Id);
                    //Create it's instance and spawn it
                    Tiles[column, row] = Instantiate(IngredientHandler.getIngredientPrefab(), new Vector3(column, row, -1), Quaternion.identity);
                    Tiles[column, row].GetComponent<IngredientCard>().SetIngredient(newIng.Id, newIng.Name, newIng.Cost);
                    Tiles[column, row].transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    Tiles[column, row].name = newIng.Name;
                }
            }
        }
    }

    public void RefillRecipeMenu() {
        //refill recipe list
        for(int i = 0; i < MapSize; i++) {
            //if theres a blank in the list
            if (RecipesListed[i] == null) {
                RecipesListed[i]  = RecipeHandler.DrawRecipeCard();
                RecipeHandler.ToggleRecipeCard(RecipesListed[i].RecipeName, true, (i - 2) * RecipeDistance);
            }
        }
    }
}
