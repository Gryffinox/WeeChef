using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public const int MapSize = 5;
    public static GameObject[,] Tiles;
    private int GamePhase;
    List<GameObject> ingredientList;

    private enum GamePhases { IngredientGathering = 0, RecipeBuilding = 1};
    // Start is called before the first frame update
    void Start()
    {
        GamePhase = 0;      //0 tile ingredient gathering, 1 recipe building phase
        //Initialize tiles
        Tiles = new GameObject[MapSize, MapSize];
        //Get ingredientlist from ingredient list script (both scripts attached to main camera)
        ingredientList = GetComponent<IngredientList>().getIngredientList();
        //fill out the tiles
        for (int column = 0; column < MapSize; column++) {
            for (int row = 0; row < MapSize; row++) {
                int i = Random.Range(0, ingredientList.Count);
                MainGame.Tiles[column, row] = Instantiate(ingredientList[i], new Vector3(column, row, -1), Quaternion.identity);
                MainGame.Tiles[column, row].transform.localScale = new Vector3(0.8f, 0.8f, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (GamePhase) {
            case (int)GamePhases.IngredientGathering:
                //Do ingredient gathering here
                break;
            case (int)GamePhases.RecipeBuilding:
                //Do recipe building here
                break;
        }
    }
}
