﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCard : MonoBehaviour
{
    private List<Ingredient> ingredients;
    private string recipeName;
    private string recipeType;
    private List<int> ingredientIds;

    public RecipeCard()
    {

    }

    public RecipeCard(string name, string type, List<int> ingredientIdsList, TextAsset IngredientsTxtFile)
    {
        recipeName = name;
        recipeType = type;
        ingredientIds = ingredientIdsList;
        ingredients = new List<Ingredient>();

        Ingredients listOfIngredients = JsonUtility.FromJson<Ingredients>(IngredientsTxtFile.text);

        foreach (int ingredientId in ingredientIds)
        {
            foreach (Ingredient ingredient in listOfIngredients.ingredients)
            {
                if (ingredientId == ingredient.Id)
                {
                    ingredients.Add(ingredient);
                    break;
                }
            }
        }
    }

    public string getRecipeName()
    {
        return recipeName;
    }

    // Returns it as a string
    //public string getIngredientsOfRecipeString()
    //{
    //    string ingredients_ = "";
    //    for (int i = 0; i < ingredients.Count; i++)
    //        ingredients_ += "\n - " + ingredients[i].ToString();
    //    //ingredients_ += "\n - " + ingredients[ingredients.Count - 1].ToString();
    //    return ingredients_;
    //}

    // Returns as a list of strings
    public List<Ingredient> getIngredientsOfRecipeList()
    {
        return ingredients;
    }

    public string getIngredientsOfRecipe()
    {
        string allIngredients = "";
        foreach(Ingredient ingredient in ingredients)
        {
            allIngredients += ingredient.Name + ", ";
        }

        return allIngredients;
    }

    public string getRecipeType()
    {
        return recipeType;
    }

    public List<int> getIngredientIds()
    {
        return ingredientIds;
    }

    public void SetRecipeName(string recipeName)
    {
        this.recipeName = recipeName;
    }

    public void SetRecipeType(string recipeType)
    {
        this.recipeType = recipeType;
    }

    public void SetIngredients(List<Ingredient> ingredients)
    {
        this.ingredients = ingredients;
    }

    public void SetIngredientIds(List<int> ingredientIds)
    {
        this.ingredientIds = ingredientIds;
    }

    public void MoveRecipeCardToHand()
    {
        PlayerParent.BuyRecipe(this);
    }

    public override string ToString()
    {
        return getRecipeName() + " (" + recipeType + ")" + ":" + getIngredientsOfRecipe();
    }
}
