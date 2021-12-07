using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Recipe
{
    public List<Ingredient> Ingredients;
    public string RecipeName;
    public string RecipeType;
    public List<int> IngredientIds;

    //only needed for checking endgame who has what recipe
    public Recipe(string name, string type) {
        RecipeName = name;
        RecipeType = type;
    }

    public override string ToString()
    {
        return RecipeName + " (Type: " + RecipeType + ")";

    }

    public String GetRecipeType() {
        return RecipeType;
    }
}

[Serializable]
public class Recipes
{
    public Recipe[] recipes;
}
