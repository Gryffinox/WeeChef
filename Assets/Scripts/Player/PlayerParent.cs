using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour {
    //Players
    public static Player[] Players;
    public static int ActivePlayerIndex;

    //
    private int TurnState;

    //Indicator
    public GameObject Cursor;
    
    //Enums
    private enum Directions { None = 0, Up = 1, Right = 2, Down = 3, Left = 4 }
    private enum TurnStates { SelectMovement = 0, SelectAction = 1 };

    private Animator[] mAnimator;

    private void Awake() {
        //keep the player information when showing the ingredient hand
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start() {
        Players = GetComponentsInChildren<Player>();
        ActivePlayerIndex = 0;
        TurnState = (int)TurnStates.SelectMovement;
        Cursor.transform.position = Players[ActivePlayerIndex].transform.position;

        mAnimator = GetComponentsInChildren<Animator>();
        Animate();
    }

    void Update() {
        switch (TurnState) {
            case (int)TurnStates.SelectMovement:
                MoveCursor();
                if (Input.GetButtonDown("Submit") && Players[ActivePlayerIndex].transform.position != Cursor.transform.position) {
                    MovePlayer();
                    TurnState = (int)TurnStates.SelectAction;
                }
                break;
            case (int)TurnStates.SelectAction:
                break;
            default: break;
        }
        Animate();
    }

    public static Player GetActivePlayer() {
        return Players[ActivePlayerIndex];
    }

    private int GetDirectionInput() {
        if (Input.GetButtonDown("Up")) {
            return (int)Directions.Up;
        }
        else if (Input.GetButtonDown("Right")) {
            return (int)Directions.Right;
        }
        else if (Input.GetButtonDown("Down")) {
            return (int)Directions.Down;
        }
        else if (Input.GetButtonDown("Left")) {
            return (int)Directions.Left;
        }
        return (int)Directions.None;
    }

    private void MoveCursor() {
        switch (GetDirectionInput()) {
            case (int)Directions.Up:
                if (Players[ActivePlayerIndex].transform.position.y < MainGame.MapSize - 1) {
                    Cursor.transform.position = new Vector3(Players[ActivePlayerIndex].transform.position.x, Players[ActivePlayerIndex].transform.position.y + 1, Players[ActivePlayerIndex].transform.position.z);
                }
                break;
            case (int)Directions.Right:
                if (Players[ActivePlayerIndex].transform.position.x < MainGame.MapSize - 1) {
                    Cursor.transform.position = new Vector3(Players[ActivePlayerIndex].transform.position.x + 1, Players[ActivePlayerIndex].transform.position.y, Players[ActivePlayerIndex].transform.position.z);
                }
                break;
            case (int)Directions.Down:
                if (Players[ActivePlayerIndex].transform.position.y > 0) {
                    Cursor.transform.position = new Vector3(Players[ActivePlayerIndex].transform.position.x, Players[ActivePlayerIndex].transform.position.y - 1, Players[ActivePlayerIndex].transform.position.z);
                }
                break;
            case (int)Directions.Left:
                if (Players[ActivePlayerIndex].transform.position.x > 0) {
                    Cursor.transform.position = new Vector3(Players[ActivePlayerIndex].transform.position.x - 1, Players[ActivePlayerIndex].transform.position.y, Players[ActivePlayerIndex].transform.position.z);
                }
                break;
            default: break;
        }
    }

    private void MovePlayer() {
        Players[ActivePlayerIndex].transform.position = Cursor.transform.position;
    }

    public void AcceptCard() {
        // Adding Ingredient Card to the active player's Ingredient Hand
        //Ingredient ingredientToAdd = MainGame.Tiles[(int)Players[ActivePlayerIndex].transform.position.x, (int)Players[ActivePlayerIndex].transform.position.y];
        //Players[ActivePlayerIndex].AddCardToIngredientHand(ingredientToAdd);
        //MainGame.Tiles[(int)Players[ActivePlayerIndex].transform.position.x, (int)Players[ActivePlayerIndex].transform.position.y].gameObject.SetActive(false);
        EndTurn();
    }

    public void DeclineCard() {
        EndTurn();
    }

    private void EndTurn() {
        ActivePlayerIndex++;
        if (ActivePlayerIndex >= Players.Length) {
            ActivePlayerIndex = 0;
        }
        TurnState = (int)TurnStates.SelectMovement;
        Cursor.transform.position = Players[ActivePlayerIndex].transform.position;
    }

    private void Animate() {
        for (int i = 0; i < Players.Length; i++) {
            mAnimator[i].ResetTrigger("isMoving");
        }
        mAnimator[ActivePlayerIndex].SetTrigger("isMoving");
    }
}
