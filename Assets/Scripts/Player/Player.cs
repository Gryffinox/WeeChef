using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private int playerID;   //TODO: Could be removed? PlayerParent handles ids
    private List<Ingredient> ingredientHand; // changed it from GameObject to list of Ingredient
    private List<Recipe> recipeHand;
    private int funds;

    private void Start() {
        ingredientHand = new List<Ingredient>();    //changed it from GameObject to list of Ingredient
        recipeHand = new List<Recipe>();
        funds = 10;                                 //player starts with 10
    }

    // parameter changed from GameObject to Ingredient
    public void AddCardToIngredientHand(Ingredient ingredient) {
        ingredientHand.Add(ingredient);
    }

    public int GetIngredientCount() {
        return ingredientHand.Count;
    }

    // changed from GameObject to list of Ingredient
    public List<Ingredient> GetIngredientHand() {
        // TEST to make sure it returns the right stuff.
        if (ingredientHand.Count == 0)
            print("in Player: ingredientHand has 0 elements");

        string ing = "Hand: ";
        for (int i = 0; i < ingredientHand.Count; i++)
            ing += (ingredientHand[i].ToString() + "; ");
        print(ing);
        // end test

        return ingredientHand;
    }
}
