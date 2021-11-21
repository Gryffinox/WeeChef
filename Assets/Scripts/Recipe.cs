using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe
{
    private List<Ingredient> ingredients;
    private string recipeName;
    //public enum CuisineTypes { French, Italian, Japanese, Indian, Mediterranean, SouthEastAsian, Caribbean }
    //private CuisineTypes recipeType; // french, italian, mediterranean, etc.
    private string recipeType;

    public Recipe(string name, string type, List<Ingredient> ingredients)
    {
        recipeName = name;
        recipeType = type;
        this.ingredients = ingredients;
    }

    public string getRecipeName()
    {
        return recipeName;
    }

    // Returns it as a string
    public string getIngredientsOfRecipeString()
    {
        string ingredients_ = "";
        for (int i = 0; i < ingredients.Count; i++)
            ingredients_ += "\n - " + ingredients[i].ToString();
        //ingredients_ += "\n - " + ingredients[ingredients.Count - 1].ToString();
        return ingredients_;
    }

    // Returns as a list of strings UNUSED
    public List<Ingredient> getIngredientsOfRecipeList()
    {
        return ingredients;
    }

    public override string ToString()
    {
        return getRecipeName() + " (" + recipeType + ")" + ":" + getIngredientsOfRecipeString();
    }

}
