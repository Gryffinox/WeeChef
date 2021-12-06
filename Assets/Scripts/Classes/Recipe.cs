using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Recipe
{
    public string RecipeName;
    public string RecipeType;
    public List<int> IngredientIds;
    public List<Ingredient> Ingredients;

    public Recipe(string name, string type, List<int> ids) {
        RecipeName = name;
        RecipeType = type;
        IngredientIds = new List<int>();
        IngredientIds.AddRange(ids);
    }

    public override string ToString()
    {
        return RecipeName + " (Type: " + RecipeType + ")";

    }
}

[Serializable]
public class Recipes
{
    public Recipe[] recipes;
}
