using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    private List<string> ingredientsOfRecipe;
    private string recipeName;
    private string recipeType;

    public Recipe(string name, string type, List<string> ingredients)
    {
        recipeName = name;
        recipeType = type;
        ingredientsOfRecipe = ingredients;
    }

    public string getRecipeName()
    {
        return recipeName;
    }

    // Returns it as a string
    public string getIngredientsOfRecipeString()
    {
        string ingredients = "";
        for (int i = 0; i < ingredientsOfRecipe.Count - 1; i++)
            ingredients += ingredientsOfRecipe[i] + ", ";
        ingredients += ingredientsOfRecipe[ingredientsOfRecipe.Count - 1];
        return ingredients;
    }

    // Returns as a list of strings
    public List<string> getIngredientsOfRecipeList()
    {
        return ingredientsOfRecipe;
    }

}
