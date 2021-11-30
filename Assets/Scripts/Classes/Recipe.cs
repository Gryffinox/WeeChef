using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe
{
    private List<IngredientCard> ingredients;
    private string recipeName;
    private string recipeType;

    public Recipe(string name, string type, List<IngredientCard> ingredients)
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
    public List<IngredientCard> getIngredientsOfRecipeList()
    {
        return ingredients;
    }

    public override string ToString()
    {
        return getRecipeName() + " (" + recipeType + ")" + ":" + getIngredientsOfRecipeString();
    }

}
