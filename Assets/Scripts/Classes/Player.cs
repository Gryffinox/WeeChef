using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int playerID;
    private List<GameObject> ingredientHand; // change it to list of Ingredient
    private List<Recipe> recipeHand;
    private int funds;

    //private void Awake()
    //{
    //    // Hopefully will allow us to access the player information across scenes
    //    DontDestroyOnLoad(transform.gameObject);
    //}

    private void Start()
    {
        ingredientHand = new List<GameObject>(); // change it to list of Ingredient
        recipeHand = new List<Recipe>();
        funds = 0;
    }

    public void AddCardToIngredientHand(GameObject ingredient)
    {
        //print("Player -> Add Card to Ingredient Hand: " + ingredient.name);
        ingredientHand.Add(ingredient);
        print("Player has " + ingredientHand.Count + " ingredients.");
    }

    public int GetIngredientCount()
    {
        return ingredientHand.Count;
    }

    // change to list of Ingredient
    public List<GameObject> GetIngredientHand()
    {
        // Test to make sure it returns the right stuff.
        string ing = "";
        for (int i = 0; i < ingredientHand.Count; i++)
            ing += (ingredientHand[i].name + "; ");
        print(ing);
        // end test

        return ingredientHand;
    }
}
