using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour {
    //Overall game variables
    private enum GamePhases { IngredientGathering = 0, RecipeBuilding = 1 };
    private int GamePhase;

    //Players
    [SerializeField] private GameObject Players;
    private PlayerParent PlayerHandler;

    //Ingredient Gathering
    public const int MapSize = 5;
    private GameObject[,] Tiles;
    private IngredientList IngredientHandler;

    void Start() {
        GamePhase = 0;      //0 tile ingredient gathering, 1 recipe building phase
        // Initialize tiles with Ingredient objects
        Tiles = new GameObject[MapSize, MapSize];

        PlayerHandler = Players.GetComponent<PlayerParent>();

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
    }

    void Update() {
        switch (GamePhase) {
            case (int)GamePhases.IngredientGathering:
                // Do ingredient gathering here
                if (PlayerHandler.NoMovesLeft()) {
                    GamePhase = (int)GamePhases.RecipeBuilding;
                    Debug.Log("No valid moves left. Recipe building time.");
                }
                break;
            case (int)GamePhases.RecipeBuilding:
                // Do recipe building here
                //TODO: if recipe building done. refill market
                break;
        }
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
    private void RefillMarket() {

    }
}
