using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientList : MonoBehaviour {

    private int DeckSize = 50;

    //The 14 different ingredient types aka cards possible
    private List<Ingredient> IngredientTypes;

    //Decks
    private List<Ingredient> IngredientDeck;
    private List<Ingredient> DiscardPile;

    //text file with the ingredient list
    [SerializeField] TextAsset IngredientsFile;
    //Prefab object itself for an ingredient card
    [SerializeField] GameObject ingredientPrefab;
    //Individual sprites for each ingredient type
    [SerializeField] Sprite onionSprite;
    [SerializeField] Sprite cheeseSprite;
    [SerializeField] Sprite ribsSprite;
    [SerializeField] Sprite chickenSprite;
    [SerializeField] Sprite porkSprite;
    [SerializeField] Sprite potatoSprite;
    [SerializeField] Sprite breadSprite;
    [SerializeField] Sprite tomatoSprite;
    [SerializeField] Sprite appleSprite;
    [SerializeField] Sprite oliveSprite;
    [SerializeField] Sprite fishSprite;
    [SerializeField] Sprite wineSprite;
    [SerializeField] Sprite brownieSprite;
    [SerializeField] Sprite pepperRedSprite;

    Dictionary<int, Sprite> spriteList;

    public void Start() {

        //Ingredient Loading
        IngredientTypes = new List<Ingredient>();
        LoadIngredients();

        //Deck building
        IngredientDeck = new List<Ingredient>();
        DiscardPile = new List<Ingredient>();
        PopulateDeck();

        //Sprites
        spriteList = new Dictionary<int, Sprite>();
        //Manually added. Order matters so that the right sprite is assigned to each ingredient
        spriteList.Add(IngredientTypes[0].Id, onionSprite);
        spriteList.Add(IngredientTypes[1].Id, cheeseSprite);
        spriteList.Add(IngredientTypes[2].Id, ribsSprite);
        spriteList.Add(IngredientTypes[3].Id, chickenSprite);
        spriteList.Add(IngredientTypes[4].Id, porkSprite);
        spriteList.Add(IngredientTypes[5].Id, potatoSprite);
        spriteList.Add(IngredientTypes[6].Id, breadSprite);
        spriteList.Add(IngredientTypes[7].Id, tomatoSprite);
        spriteList.Add(IngredientTypes[8].Id, appleSprite);
        spriteList.Add(IngredientTypes[9].Id, oliveSprite);
        spriteList.Add(IngredientTypes[10].Id, fishSprite);
        spriteList.Add(IngredientTypes[11].Id, wineSprite);
        spriteList.Add(IngredientTypes[12].Id, brownieSprite);
        spriteList.Add(IngredientTypes[13].Id, pepperRedSprite);

    }

    // Returns the Ingredient prefab to spawn
    // it should already have its sprite assigned
    public GameObject getIngredientPrefab() {
        return ingredientPrefab;
    }

    // Will receive the type of ingredient
    // and assign the right sprite to the Ingredient prefab
    public void assignSprite(int id) {
        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[id];
    }

    private void LoadIngredients() {
        Ingredients ingredients = JsonUtility.FromJson<Ingredients>(IngredientsFile.text);
        foreach (Ingredient ingredient in ingredients.ingredients) {
            IngredientTypes.Add(ingredient);
        }
    }

    private void PopulateDeck() {
        DeckSize = 0;
        foreach(Ingredient ingredient in IngredientTypes) {
            DeckSize += ingredient.Count;
            for(int i = 0; i < ingredient.Count; i++) {
                IngredientDeck.Add(ingredient);
            }
        }
        Shuffle(ref IngredientDeck);
    }
    private void Shuffle(ref List<Ingredient> deck) {
        //fisher yates shuffle algorithm
        //in place swap O(n)
        int i = deck.Count;
        while (i > 1) {
            i--;
            int s = Random.Range(0, i + 1);
            Ingredient swap = deck[s];
            deck[s] = deck[i];
            deck[i] = swap;
        }
    }

    public Ingredient DrawCard() {
        if(IngredientDeck.Count == 0) {
            Shuffle(ref DiscardPile);
            IngredientDeck.AddRange(DiscardPile);
        }
        Ingredient drawn = IngredientDeck[0];
        IngredientDeck.RemoveAt(0);
        return drawn;
    }

    public void DiscardCard(Ingredient card) {
        DiscardPile.Add(card);
    }

}
