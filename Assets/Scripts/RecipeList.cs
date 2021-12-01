using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeList : MonoBehaviour
{
    [SerializeField] TextAsset RecipesFile;
    [SerializeField] TextAsset IngredientsFile;


    private static List<RecipeCard> recipes;
    static System.Random rand = new System.Random();

    void Start()
    {
        //recipes = LoadRecipes.getAllRecipes();
        //DisplayRecipe();
        LoadRecipes();
    }

    // Displays a random recipe in the Recipe Bank from the Recipe List
    // The same recipe can appear more than once
    public void DisplayRecipe()
    {
        //Text textObject = gameObject.GetComponentInChildren<Text>();
        //textObject.text = recipes[rand.Next(0, recipes.Count)].ToString();
    }

    public void LoadRecipes()
    {
        Recipes recipes = JsonUtility.FromJson<Recipes>(RecipesFile.text);
        foreach (Recipe recipe in recipes.recipes)
        {
            RecipeCard aRecipeCard = new RecipeCard(
                recipe.RecipeName,
                recipe.RecipeType,
                recipe.IngredientIds,
                IngredientsFile);
        }
    }

}
