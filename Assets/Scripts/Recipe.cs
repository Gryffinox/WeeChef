using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    private List<string> ingredientsOfRecipe;
    private string recipeName;
    public enum CuisineTypes { French, Italian, Japanese, Indian, Mediterranean, SouthEastAsian, Caribbean }
    private CuisineTypes recipeType; // french, italian, mediterranean, etc.

    public Recipe(string name, CuisineTypes type, List<string> ingredients)
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
            ingredients += "\n- " + ingredientsOfRecipe[i];
        ingredients += "\n- " + ingredientsOfRecipe[ingredientsOfRecipe.Count - 1];
        return ingredients;
    }

    // Returns as a list of strings
    public List<string> getIngredientsOfRecipeList()
    {
        return ingredientsOfRecipe;
    }

    public override string ToString()
    {
        return getRecipeName() + ":" + getIngredientsOfRecipeString();
    }

}
