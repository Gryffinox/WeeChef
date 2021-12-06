using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RecipeList : MonoBehaviour {

    [SerializeField] TextAsset RecipesFile;

    private static List<Recipe> RecipesDeck;

    void Start() {
        // Recipe Loading straight to deck
        RecipesDeck = new List<Recipe>();
        LoadRecipesToDeck();
    }

    // Displays a random recipe in the Recipe Bank from the Recipe List
    // The same recipe can appear more than once
    public void DisplayRecipe() {
        //Text textObject = gameObject.GetComponentInChildren<Text>();
        //textObject.text = recipes[rand.Next(0, recipes.Count)].ToString();
    }

    public void LoadRecipesToDeck() {
        Recipes recipes = JsonUtility.FromJson<Recipes>(RecipesFile.text);
        foreach (Recipe recipe in recipes.recipes) {
            RecipesDeck.Add(recipe);
        }
        Shuffle();
    }

    private void Shuffle() {
        int i = RecipesDeck.Count;
        while (i > 1) {
            i--;
            int s = Random.Range(0, i + 1);
            Recipe swap = RecipesDeck[s];
            RecipesDeck[s] = RecipesDeck[i];
            RecipesDeck[i] = swap;
        }
    }

    public Recipe DrawRecipeCard() {
        if(RecipesDeck.Count == 0) {
            return null;    //no cards left to return deck is empty
        }
        Recipe drawnCard = RecipesDeck[0];
        RecipesDeck.RemoveAt(0);
        return drawnCard;
    }


}