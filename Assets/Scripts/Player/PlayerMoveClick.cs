using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveClick : MonoBehaviour {

    [SerializeField] GameObject UIParent;
    [SerializeField] GameObject Cursor;
    [SerializeField] GameObject Players;

    private MainGame GameHandler;               //access the game
    private IngredientGatheringUI UIHandler;    //access UI
    private PlayerParent PlayerHandler;         //access players
    private bool MovementTile = true;           //when you click the tile, if its not the center tile, its a movement tile

    void Start() {
        //link handlers
        GameHandler = Camera.main.GetComponent<MainGame>();
        UIHandler = UIParent.GetComponent<IngredientGatheringUI>();
        PlayerHandler = Players.GetComponent<PlayerParent>();
        //if within the parent cursor container, this click object is the center one
        if(transform.localPosition.x == 0 && transform.localPosition.y == 0) {
            MovementTile = false;
        }
    }

    private void OnMouseDown() {
        //if the clicked box is within the map area
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        if (x < MainGame.MapSize && x >= 0 &&
            y < MainGame.MapSize && y >= 0) {
            //move cursor to where the player clicked
            Cursor.transform.position = transform.position;
            //If clicked on an empty tile or an already occupied tile, just hide the buttons
            if (!GameHandler.ValidCoords(x, y) || PlayerHandler.Overlap(x, y)) {
                UIHandler.HideAllButtons();
                UIHandler.DisplayText("");
            }
            //if a valid tile clicked with an ingredient
            else {
                //if this is a movement tile and not the center buy tile
                if (MovementTile) {
                    UIHandler.ShowConfirmButton();
                }
                else {
                    UIHandler.ShowBuyButton();
                    //if the player has enough funds to buy the ingredient, enable button
                    Debug.Log("Cost: " + GameHandler.GetTileIngredient(x, y).Cost);
                    Debug.Log("Funds" + PlayerParent.GetActivePlayer().GetFunds());
                    UIHandler.SetBuyButtonInteractable(GameHandler.GetTileIngredient(x, y).Cost <= PlayerParent.GetActivePlayer().GetFunds());
                }
                //display the ingredient of the info clicked
                UIHandler.DisplayIngredientInfo(GameHandler.GetTileIngredient((int)transform.position.x, (int)transform.position.y));    //get the ingredient from the map
            }
        }
    }

}
