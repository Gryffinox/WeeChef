using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientList : MonoBehaviour {

    private const int DeckSize = 50;

    //The 14 different ingredient types aka cards possible
    private List<IngredientCard> IngredientTypes;

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

    List<Sprite> spriteList;

    public void Start() {
        LoadIngredients();

        //ingredientCards = LoadIngredients.getAllIngredientCards();
        spriteList = new List<Sprite>();

        //Manually added. Order matters so that the right sprite is assigned to each ingredient
        spriteList.Add(onionSprite);
        spriteList.Add(cheeseSprite);
        spriteList.Add(ribsSprite);
        spriteList.Add(chickenSprite);
        spriteList.Add(porkSprite);
        spriteList.Add(potatoSprite);
        spriteList.Add(breadSprite);
        spriteList.Add(tomatoSprite);
        spriteList.Add(appleSprite);
        spriteList.Add(oliveSprite);
        spriteList.Add(fishSprite);
        spriteList.Add(wineSprite);
        spriteList.Add(brownieSprite);
        spriteList.Add(pepperRedSprite);

    }

    // Returns the Ingredient prefab to spawn
    // it should already have its sprite assigned
    public GameObject getIngredientPrefab() {
        return ingredientPrefab;
    }

    // Will receive the type of ingredient
    // and assign the right sprite to the Ingredient prefab
    public void assignSprite(int id) {
        //switch (type) {
        //    case "Allium":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[0];
        //        break;
        //    case "Dairy":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[1];
        //        break;
        //    case "Beef/Veal":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[2];
        //        break;
        //    case "Poultry":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[3];
        //        break;
        //    case "Pork/Charcuterie":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[4];
        //        break;
        //    case "Root Vegetables/Mushrooms":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[5];
        //        break;
        //    case "Dry Goods":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[6];
        //        break;
        //    case "Garden Vegetables":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[7];
        //        break;
        //    case "Fruits":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[8];
        //        break;
        //    case "Herbs/Leafy Greens":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[9];
        //        break;
        //    case "Fish/Shellfish":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[10];
        //        break;
        //    case "Liquid Goods":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[11];
        //        break;
        //    case "Confectionery":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[12];
        //        break;
        //    case "Spices":
        //        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[13];
        //        break;
        //    default:
        //        Debug.LogError("Unknown ingredient type passed. Could not assign sprite");
        //        Debug.Log(type + ": " + ingredientPrefab.ToString());
        //        break;
        //}
        ingredientPrefab.GetComponent<SpriteRenderer>().sprite = spriteList[id];
    }

    // Returns the list of Ingredient Cards
    public List<IngredientCard> getIngredientCards() {
        return IngredientTypes;
    }

    private void LoadIngredients() {
        Ingredients ingredients = JsonUtility.FromJson<Ingredients>(IngredientsFile.text);
        foreach (Ingredient ingredient in ingredients.ingredients) {
            //Debug.Log(ingredient);
            IngredientCard aCard = new IngredientCard(
                ingredient.Id,
                ingredient.Name,
                ingredient.Count,
                ingredient.Cost);
            IngredientTypes.Add(aCard);
        }
    }
}
