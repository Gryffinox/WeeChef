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

    public override string ToString()
    {
        return RecipeName;
    }
}

[Serializable]
public class Recipes
{
    public Recipe[] recipes;
}
