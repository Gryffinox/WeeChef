using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour {
    
    //Players
    private static Player[] Players;            //list of players by their player script component
    private static int ActivePlayerIndex;       //Current player
    private static List<int> PlayerTurnOrder;   //the order in which the players play which goes back and forth
    
    List<GameObject> cards = new List<GameObject>();

    //Gameobject
    [SerializeField] private GameObject CursorContainer;    //moves the whole cursor container which includes the click actions
    [SerializeField] private GameObject Cursor;             //just the visual cursor object
    [SerializeField] private GameObject IngredientGatheringUI;
    [SerializeField] GameObject onionCard;
    [SerializeField] GameObject porkCard;
    [SerializeField] GameObject cheeseCard;
    [SerializeField] GameObject ribsCard;
    [SerializeField] GameObject chickenCard;
    [SerializeField] GameObject potatoCard;
    [SerializeField] GameObject breadCard;
    [SerializeField] GameObject tomatoCard;
    [SerializeField] GameObject appleCard;
    [SerializeField] GameObject oliveCard;
    [SerializeField] GameObject fishCard;
    [SerializeField] GameObject wineCard;
    [SerializeField] GameObject brownieCard;
    [SerializeField] GameObject pepperCard;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip click;
    [SerializeField] AudioClip money;
    [SerializeField] GameObject hand;

    [SerializeField] GameObject turn1;
    [SerializeField] GameObject turn2;
    [SerializeField] GameObject turn3;
    [SerializeField] GameObject turn4;


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
        /*//fisher yates shuffle
        i = PlayerTurnOrder.Count;
        while(i > 1) {
            i--;
            int k = Random.Range(0, i + 1);
            int swap = PlayerTurnOrder[k];
            PlayerTurnOrder[k] = PlayerTurnOrder[i];
            PlayerTurnOrder[i] = swap;
        }*/
        ActivePlayerIndex = 0;  //first player
        //cursor to first player
        CursorContainer.transform.position = Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position;

        //handlers
        MainGameHandler = Camera.main.GetComponent<MainGame>();
        UIHandler = IngredientGatheringUI.GetComponent<IngredientGatheringUI>();
        //setup animators
        mAnimator = GetComponentsInChildren<Animator>();
    }

    private void Update() {
        //Set animator for the current player
        mAnimator[PlayerTurnOrder[ActivePlayerIndex]].SetTrigger("isMoving");
        showCards();
    }

    public void ResetPosition()
    {
        GameObject[] temp;
        temp = GameObject.FindGameObjectsWithTag("p1");
        foreach (GameObject go in temp)
        {
            go.GetComponent<CardMove>().goShuffle();
        }
        temp = GameObject.FindGameObjectsWithTag("p2");
        foreach (GameObject go in temp)
        {
            go.GetComponent<CardMove>().goShuffle();
        }
        temp = GameObject.FindGameObjectsWithTag("p3");
        foreach (GameObject go in temp)
        {
            go.GetComponent<CardMove>().goShuffle();
        }
        temp = GameObject.FindGameObjectsWithTag("p4");
        foreach (GameObject go in temp)
        {
            go.GetComponent<CardMove>().goShuffle();
        }
    }
    private void showCards()
    {
        int i = returnPlayer();
        GameObject[] temp;

        if (i == 0)
        {
            turn1.SetActive(true);
            turn2.SetActive(false);
            turn3.SetActive(false);
            turn4.SetActive(false);
            temp = GameObject.FindGameObjectsWithTag("p2");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p3");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p4");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
        }
        

        if (i == 1)
        {
            turn1.SetActive(false);
            turn2.SetActive(true);
            turn3.SetActive(false);
            turn4.SetActive(false);
            temp = GameObject.FindGameObjectsWithTag("p1");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p3");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p4");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
        }
        
        if (i == 2)
        {
            turn1.SetActive(false);
            turn2.SetActive(false);
            turn3.SetActive(true);
            turn4.SetActive(false);
            temp = GameObject.FindGameObjectsWithTag("p2");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p1");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p4");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
        }
        
        if (i == 3)
        {
            turn1.SetActive(false);
            turn2.SetActive(false);
            turn3.SetActive(false);
            turn4.SetActive(true);
            temp = GameObject.FindGameObjectsWithTag("p2");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p3");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
            temp = GameObject.FindGameObjectsWithTag("p1");
            foreach (GameObject go in temp)
            {
                go.SetActive(false);
            }
        }
    }

    //for recipe building and anytime any player needs things
    public static Player GetActivePlayer() {
        return Players[PlayerTurnOrder[ActivePlayerIndex]];
    }

    //move player ez pz
    public void MoveAction() {
        Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position = Cursor.transform.position;
        audioSource.PlayOneShot(click, 1);
        EndTurn();
    }

    //buy ingredient at player coordinate
    public void BuyAction() {
        //Get coords of where the player is (coord of ingredient to be bought)
        int x = (int)Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position.x;
        int y = (int)Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position.y;
        //Get the ingredient
        Ingredient ingredientToAdd = MainGameHandler.GetTileIngredient(x, y);
        //add it to player hand
        if (ingredientToAdd.returnName() == "Allium")
        {
            GameObject temp = Instantiate(onionCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Dairy")
        {
            GameObject temp = Instantiate(cheeseCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Beef")
        {
            GameObject temp = Instantiate(ribsCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Poultry")
        {
            GameObject temp = Instantiate(chickenCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Pork")
        {
            GameObject temp = Instantiate(porkCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Root Vegetables")
        {
            GameObject temp = Instantiate(potatoCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Dry Goods")
        {
            GameObject temp = Instantiate(breadCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Garden Vegetable")
        {
            GameObject temp = Instantiate(tomatoCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Fruits")
        {
            GameObject temp = Instantiate(appleCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Herbs")
        {
            GameObject temp = Instantiate(oliveCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Fish")
        {
            GameObject temp = Instantiate(fishCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Liquid Goods")
        {
            GameObject temp = Instantiate(wineCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Confectionery")
        {
            GameObject temp = Instantiate(brownieCard);
            setTag(temp);
            cards.Add(temp);
        }
        if (ingredientToAdd.returnName() == "Spices")
        {
            GameObject temp = Instantiate(pepperCard);
            setTag(temp);
            cards.Add(temp);
        }
        MainGameHandler.RemoveIngredient(x, y); //remove ingredient from map
        audioSource.PlayOneShot(money, 1);
        EndTurn();
    }

    private void setTag(GameObject gameobject)
    {
        int i = returnPlayer();
       // Debug.Log(returnPlayer());

        if (i == 0)
        {
            gameobject.tag = "p1";
        }
        if (i == 1)
        {
            gameobject.tag = "p2";
        }
        if (i == 2)
        {
            gameobject.tag = "p3";
        }
        if (i == 3)
        {
            gameobject.tag = "p4";
        }

    }
    public void EndTurn() {
        ActivePlayerIndex++;    //next player
        //if index exceeds our number of players, loop back to player 1 (index 0)
        if (ActivePlayerIndex >= Players.Length) {
            ActivePlayerIndex = 0;
        }
        //reset animators
        for (int i = 0; i < Players.Length; i++) {
            mAnimator[i].ResetTrigger("isMoving");
        }
        //move container to the next player
        CursorContainer.transform.position = Players[PlayerTurnOrder[ActivePlayerIndex]].transform.position;
        //reset cursor position to center
        Cursor.transform.localPosition = new Vector3(0, 0, 0);
        //reset ui
        UIHandler.HideAllButtons();
        foreach (GameObject c in cards)
        {
            c.SetActive(true);
        }
    }

    public static void BuyRecipe(RecipeCard recipeCard)
    {
        Players[PlayerTurnOrder[ActivePlayerIndex]].AddCardToRecipeHand(recipeCard);
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

    private int returnPlayer()
    {
        return ActivePlayerIndex;
    }
}
