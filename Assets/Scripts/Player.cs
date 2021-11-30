using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int playerID;
    private List<IngredientCard> ingredientHand; // changed it from GameObject to list of Ingredient
    private List<Recipe> recipeHand;
    private int funds;

    private void Start()
    {
        ingredientHand = new List<IngredientCard>(); // changed it from GameObject to list of Ingredient
        recipeHand = new List<Recipe>();
        funds = 0;
    }

    // parameter changed from GameObject to Ingredient
    public void AddCardToIngredientHand(IngredientCard ingredient)
    {
        ingredientHand.Add(ingredient);
    }

    public int GetIngredientCount()
    {
        return ingredientHand.Count;
    }

    // changed from GameObject to list of Ingredient
    public List<IngredientCard> GetIngredientHand()
    {
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
