using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    private Player[] Players;

    public int ActivePlayerIndex;
    private int TurnState;
    public GameObject Cursor;

    private UIElements UI;

    private enum Directions { None = 0, Up = 1, Right = 2, Down = 3, Left = 4}
    private enum TurnStates { SelectMovement = 0, SelectAction = 1};

    // Start is called before the first frame update
    void Start()
    {
        Players = GetComponentsInChildren<Player>();
        ActivePlayerIndex = 0;
        TurnState = (int)TurnStates.SelectMovement;
        UI = GameObject.Find("PlayerUI").GetComponent<UIElements>();
        Cursor.transform.position = Players[ActivePlayerIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (TurnState) {
            case (int)TurnStates.SelectMovement:
                UI.ActivateMovementConfirmDialog();
                MoveCursor();
                if (Input.GetButtonDown("Submit") && Players[ActivePlayerIndex].transform.position != Cursor.transform.position) {
                    MovePlayer();
                    TurnState = (int)TurnStates.SelectAction;
                }
                break;
            case (int)TurnStates.SelectAction:
                UI.ActivateCardPickupDialog();
                break;
            default: break;
        }
    }

    private int GetDirectionInput() {
        if (Input.GetButtonDown("Up")){
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
                if(Players[ActivePlayerIndex].transform.position.y < MainGame.MapSize - 1) {
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

    private void MovePlayer()
    {
        Players[ActivePlayerIndex].transform.position = Cursor.transform.position;
    }

    public void AcceptCard() {
        MainGame.Tiles[(int)Players[ActivePlayerIndex].transform.position.x, (int)Players[ActivePlayerIndex].transform.position.y].SetActive(false);
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
}
