using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveClick : MonoBehaviour {

    [SerializeField] GameObject Cursor;

    private bool Move = true;

    void Start() {
        if(transform.position.x == 0 && transform.position.y == 0) {
            Move = false;
        }
    }

    
    void Update() {

    }

    private void OnMouseDown() {
        //if the clicked box is within the map area
        if (transform.position.x < MainGame.MapSize && transform.position.x >= 0 &&
            transform.position.y < MainGame.MapSize && transform.position.y >= 0) {
            //move cursor to where the player clicked
            Cursor.transform.position = transform.position;
            //if this is a movement tile and not the center buy tile
            if (Move) {

            }
        }
    }

}
