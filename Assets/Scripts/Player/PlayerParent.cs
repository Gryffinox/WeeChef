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

    //for turn countdown
    private int TurnCount = 10;

    //Enums
    private enum Directions { None = 0, Up = 1, Right = 2, Down = 3, Left = 4 }

    //Handler
    private MainGame GameHandler;
    private IngredientGatheringUI UIHandler;
    //Player parent handles all animators since only one hat can be moving at a time
    private Animator[] mAnimator;

    //To end Ingredient Gathering phase
    private bool HasValidAction;

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
        while (i > 1) {
            i--;
            int k = Random.Range(0, i + 1);
            int swap = PlayerTurnOrder[k];
            PlayerTurnOrder[k] = PlayerTurnOrder[i];
            PlayerTurnOrder[i] = swap;
        }
        ActivePlayerIndex = 0;  //first player
        //cursor to first player
        CursorContainer.transform.position = Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position;
        HasValidAction = true;

        //handlers
        GameHandler = Camera.main.GetComponent<MainGame>();
        UIHandler = IngredientGatheringUI.GetComponent<IngredientGatheringUI>();
        //setup animators
        mAnimator = GetComponentsInChildren<Animator>();
        //animate first player
        mAnimator[PlayerTurnOrder[ActivePlayerIndex]].SetTrigger("isMoving");
    }

    //for recipe building and anytime any player needs things
    public static Player GetActivePlayer() {
        return Players[PlayerTurnOrder[ActivePlayerIndex]];
    }

    //move player ez pz
    public void MoveAction() {
        Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position = Cursor.transform.position;
        EndIngredientTurn();
    }

    //buy ingredient at player coordinate
    public void BuyAction() {
        //Get coords of where the player is (coord of ingredient to be bought)
        int x = (int)Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position.x;
        int y = (int)Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position.y;
        //Get the ingredient
        Ingredient ingredientToAdd = GameHandler.GetTileIngredient(x, y);
        //add it to player hand
        Players[PlayerTurnOrder[ActivePlayerIndex]].AddCardToIngredientHand(ingredientToAdd);
        //deduct balance from player
        Players[PlayerTurnOrder[ActivePlayerIndex]].DeductBalance(ingredientToAdd.Cost);
        GameHandler.RemoveIngredient(x, y); //remove ingredient from map
        EndIngredientTurn();
    }

    public void EndIngredientTurn() {
        ActivePlayerIndex++;    //next player
        //if index exceeds our number of players, loop back to player 1 (index 0)
        //decrease number of turns left
        if (ActivePlayerIndex >= Players.Length) {
            ActivePlayerIndex = 0;
            TurnCount--;
        }
        //reset animators
        ResetAnimators();
        //Set animator for the current player
        mAnimator[PlayerTurnOrder[ActivePlayerIndex]].SetTrigger("isMoving");

        //move container to the next player
        CursorContainer.transform.position = Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position;
        //reset cursor position to center
        Cursor.transform.localPosition = new Vector3(0, 0, 0);
        //reset ui, default to available buy if possible. if no buy available, hide all buttons
        int x = (int)CursorContainer.transform.position.x;
        int y = (int)CursorContainer.transform.position.y;
        if (GameHandler.ValidIngredientInMap(x, y)) {
            UIHandler.ShowBuyButton();
            UIHandler.DisplayIngredientInfo(GameHandler.GetTileIngredient(x, y));    //get the ingredient from the map
        }
        else {
            UIHandler.HideAllButtons();
            UIHandler.DisplayText("");
        }
        //check if the new player has any valid moves. if not set a flag for NoMovesLeft
        //validate in all 4 directions
        HasValidAction = ValidateMove(x, y + 1) || ValidateMove(x, y - 1) || ValidateMove(x + 1, y) || ValidateMove(x - 1, y);
        //validate if theres an ingredient under the player currently
        if (GameHandler.ValidIngredientInMap(x, y)) {
            //if there is, can the player afford it
            if (GameHandler.GetTileIngredient(x, y).Cost > Players[PlayerTurnOrder[ActivePlayerIndex]].GetFunds()) {
                HasValidAction = false;
            }
        }
        else {
            HasValidAction = false;
        }
    }

    //Returns true if the selected coordinates is already occupied
    public bool Overlap(int x, int y) {
        //check against all other player
        for (int i = 0; i < Players.Length; i++) {
            if (PlayerTurnOrder[i] != PlayerTurnOrder[ActivePlayerIndex]) {  //dont cross reference against the own player
                //if the tile selected is occupied by another player
                if (x == Players[PlayerTurnOrder[i]].transform.position.x && y == Players[PlayerTurnOrder[i]].transform.position.y) {
                    return true;
                }
            }
        }
        return false;
    }

    public bool NoMovesLeft() {
        //check each surrounding valid tile if theres at least an ingredient, no player, and within bounds
        return !HasValidAction;
    }

    //wrapper class that validates whether theres an ingredient and/or player overlap on a given tile
    //returns true if no player and theres and ingredient to be moved on to
    public bool ValidateMove(int x, int y) {
        if (GameHandler.ValidIngredientInMap(x, y) && !Overlap(x, y)) {
            return true;
        }
        else {
            return false;
        }
    }

    public void ResetAnimators() {
        for (int i = 0; i < Players.Length; i++) {
            mAnimator[i].ResetTrigger("isMoving");
        }
    }

    public int GetTurnCount() {
        return TurnCount;
    }
}
