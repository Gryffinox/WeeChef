using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawn : MonoBehaviour {
    //Size of the map
    private const int MapSize = 5;
    //Array of the tiles/map
    private GameObject[,] Tiles;

    // Start is called before the first frame update
    void Start() {
        Tiles = new GameObject[MapSize, MapSize];
        //Get ingredientlist from ingredient list script (both scripts attached to main camera)
        List<GameObject> ingredientList = GetComponent<IngredientList>().getIngredientList(); ;
        //fill out the tiles
        for (int column = 0; column < MapSize; column++) {
            for (int row = 0; row < MapSize; row++) {
                int i = Random.Range(0, ingredientList.Count);
                //SpawnTileCard(column, row, ingredientList[i], ingredientList[i].GetComponent<Ingredient>().getIngredientName());
                //GameObject newTile = new GameObject();
                //newTile.AddComponent<SpriteRenderer>();
                //newTile = ingredientList[i];
                //newTile.transform.position = new Vector3(column, row, -1);
                Tiles[column, row] = Instantiate(ingredientList[i], new Vector3(column, row, -1), Quaternion.identity);
                Tiles[column, row].transform.localScale = new Vector3(0.8f, 0.8f, 1);
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }

    //Adds a new card to the ingredient map
    void SpawnTileCard(int x, int y, GameObject ingredient, string name) {
        GameObject newTile = new GameObject();
        //newTile.AddComponent<SpriteRenderer>();
        newTile = ingredient;
        newTile.transform.position = new Vector3(x, y, -1);
        newTile.name = name;
    }
}
