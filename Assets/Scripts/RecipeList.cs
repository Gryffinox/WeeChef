using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeList : MonoBehaviour
{
    private static List<Recipe> recipes;
    System.Random rand = new System.Random();

    void Start()
    {
        recipes = LoadRecipes.getAllRecipes();
        DisplayRecipe();
    }

    // Displays a random recipe in the Recipe Bank from the Recipe List 
    public void DisplayRecipe()
    {
        Text textObject = gameObject.GetComponentInChildren<Text>();
        textObject.text = recipes[rand.Next(0, recipes.Count)].ToString();

    }

}
