using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private List<Ingredient> ingredientHand; // changed it from GameObject to list of Ingredient
    private List<Recipe> recipeHand;
    [SerializeField] int funds = 15;

    //Player's income per round;
    [SerializeField] private string name;
    [SerializeField] private int Income = 5;

    private void Start() {
        ingredientHand = new List<Ingredient>();    //changed it from GameObject to list of Ingredient
        //recipeHand = new List<RecipeCard>();
    }

    // parameter changed from GameObject to Ingredient
    public void AddCardToIngredientHand(Ingredient ingredient) {
        funds -= ingredient.Cost;
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

    public int GetFunds()
    {
        return funds;
    }

    public void decreaseFunds(int cost)
    {
        funds -= cost;
    }
    public void increaseFunds(int cost)
    {
        funds += cost;
    }

    public void AddToIncome(int recipeWorth)
    {
        Income += recipeWorth;
    }

    public void CalculateFundsAtEndOfRound()
    {
        funds += Income;
    }

    public void AddRecipe(Recipe aRecipe) {
        recipeHand.Add(aRecipe);
    }

    public List<Recipe> GetRecipeHand() {
        return recipeHand;
    }

    public string GetName() {
        return name;
    }
}
