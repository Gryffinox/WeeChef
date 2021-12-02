using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour {
    
    //Players
    private static Player[] Players;            //list of players by their player script component
    private static int ActivePlayerIndex;       //Current player
    private static List<int> PlayerTurnOrder;   //the order in which the players play which goes back and forth

    //Gameobject
    [SerializeField] private GameObject CursorContainer;    //moves the whole cursor container which includes the click actions
    [SerializeField] private GameObject Cursor;             //just the visual cursor object
    [SerializeField] private GameObject IngredientGatheringUI;
    
    //Enums
    private enum Directions { None = 0, Up = 1, Right = 2, Down = 3, Left = 4 }

    //Handler
    private MainGame MainGameHandler;
    private IngredientGatheringUI UIHandler;
    //Player parent handles all animators since only one hat can be moving at a time
    private Animator[] mAnimator;

    private void Awake() {
        //keep the player information when showing the ingredient hand
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start() {
        Players = GetComponentsInChildren<Player>();
        PlayerTurnOrder = new List<int>();
        //determine random play order
        int i = 0;  //index counter
        for (i = 0; i < Players.Length; i++) {
            PlayerTurnOrder.Add(i);
        }
        //fisher yates shuffle
        i = PlayerTurnOrder.Count;
        while(i > 1) {
            i--;
            int k = Random.Range(0, i + 1);
            int swap = PlayerTurnOrder[k];
            PlayerTurnOrder[k] = PlayerTurnOrder[i];
            PlayerTurnOrder[i] = swap;
        }
        ActivePlayerIndex = 0;  //first player
        //cursor to first player
        CursorContainer.transform.position = Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position;

        //handlers
        MainGameHandler = Camera.main.GetComponent<MainGame>();
        UIHandler = IngredientGatheringUI.GetComponent<IngredientGatheringUI>();
        //setup animators
        mAnimator = GetComponentsInChildren<Animator>();
    }

    void Update() {
        Animate();
    }

    public static Player GetActivePlayer() {
        return Players[PlayerTurnOrder[ActivePlayerIndex]];
    }

    public void MoveAction() {
        Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position = Cursor.transform.position;
        EndTurn();
    }

    public void BuyAction() {
        //Get coords of where the player is (coord of ingredient to be bought)
        int x = (int)Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position.x;
        int y = (int)Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position.y;
        //Get the ingredient
        Ingredient ingredientToAdd = MainGameHandler.GetTileIngredient(x, y);
        //add it to player hand
        Players[PlayerTurnOrder[ActivePlayerIndex]].AddCardToIngredientHand(ingredientToAdd);
        MainGameHandler.RemoveIngredient(x, y);
        EndTurn();
    }

    public void EndTurn() {
        ActivePlayerIndex++;
        //if index exceeds our number of players, loop back to player 1 (index 0)
        if (ActivePlayerIndex >= Players.Length) {
            ActivePlayerIndex = 0;
        }
        //move container to the next player
        CursorContainer.transform.position = Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position;
        //reset cursor position to center
        Cursor.transform.localPosition = new Vector3(0, 0, 0);
        //reset ui
        UIHandler.HideAllButtons();
    }

    private void Animate() {
        for (int i = 0; i < Players.Length; i++) {
            mAnimator[i].ResetTrigger("isMoving");
        }
        mAnimator[PlayerTurnOrder[ActivePlayerIndex]].SetTrigger("isMoving");
    }


    //not used for now
    //-----------------
    //KEYBOARD CONTROLS
    //-----------------
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
}
