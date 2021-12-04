using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveClick : MonoBehaviour {

    [SerializeField] GameObject UIParent;
    [SerializeField] GameObject Cursor;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip click;

    private MainGame GameHandler;               //access the game
    private IngredientGatheringUI UIHandler;    //access UI
    private bool MovementTile = true;           //when you click the tile, if its not the center tile, its a movement tile

    void Start() {
        //link handlers
        GameHandler = Camera.main.GetComponent<MainGame>();
        UIHandler = UIParent.GetComponent<IngredientGatheringUI>();
        //if within the parent cursor container, this click object is the center one
        if(transform.localPosition.x == 0 && transform.localPosition.y == 0) {
            MovementTile = false;
        }
    }

    private void OnMouseDown() {
        //if the clicked box is within the map area
        if (transform.position.x < MainGame.MapSize && transform.position.x >= 0 &&
            transform.position.y < MainGame.MapSize && transform.position.y >= 0) {
            //move cursor to where the player clicked
            Cursor.transform.position = transform.position;
            //If clicked on an empty tile, just hide the buttons
            if (!GameHandler.ValidCoords((int)transform.position.x, (int)transform.position.y)) {
                UIHandler.HideAllButtons();
                audioSource.PlayOneShot(click, 1);
            }
            //if a valid tile clicked
            else {
                //if this is a movement tile and not the center buy tile
                if (MovementTile) {
                    //display the confirm movement button
                    UIHandler.ShowConfirmButton();
                    audioSource.PlayOneShot(click, 1);
                }
                else {
                    //display the buy button
                    UIHandler.ShowBuyButton();
                    audioSource.PlayOneShot(click, 1);
                }
                //tell ui we'd like to display the ingredient clicked
                UIHandler.DisplayIngredientInfo(GameHandler.GetTileIngredient((int)transform.position.x, (int)transform.position.y));    //get the ingredient from the map
            }
        }
    }

}
