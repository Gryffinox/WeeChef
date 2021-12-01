using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    //Overall game variables
    private int GamePhase;
    private enum GamePhases { IngredientGathering = 0, RecipeBuilding = 1};

    //Ingredient Gathering
    public const int MapSize = 5;
    public GameObject[,] Tiles;

    // --- my adds
    private IngredientList IngredientHandler;

    void Start()
    {
        GamePhase = 0;      //0 tile ingredient gathering, 1 recipe building phase

        // Initialize tiles with Ingredient objects
        Tiles = new GameObject[MapSize, MapSize];

        // Get the IngredientList script from the PlayerCamera to access IngredientList methods
        IngredientHandler = GetComponent<IngredientList>();

        // Fill every tile with a random Ingredient object from the list of Ingredient cards
        for (int column = 0; column < MapSize; column++) {
            for (int row = 0; row < MapSize; row++) {
                Ingredient newIng = IngredientHandler.DrawCard();
                IngredientHandler.assignSprite(newIng.Id);
                GameObject newCard = IngredientHandler.getIngredientPrefab();

                Tiles[column, row] = Instantiate(newCard, new Vector3(column, row, -1), Quaternion.identity);
                Tiles[column, row].transform.localScale = new Vector3(0.8f, 0.8f, 1);

                //Ingredient ing = Tiles[column, row].GetComponent<Ingredient>();
                //ing.SetIngredientName(ingredientList_[i].GetIngredientName());
                //ing.SetIngredientType(ingredientList_[i].GetIngredientType());
                //ing.SetId(ingredientList_[i].GetId());
                //ing.SetCost(ingredientList_[i].GetCost());

                //print("Tiles[x,y] = " + Tiles[column, row].ToString());
            }
        }
    }

    void Update()
    {
        switch (GamePhase) {
            case (int)GamePhases.IngredientGathering:
                // Do ingredient gathering here
                //RefillMarket();
                break;
            case (int)GamePhases.RecipeBuilding:
                // Do recipe building here
                break;
        }
    }

    private void RefillMarket() {

    }
}
