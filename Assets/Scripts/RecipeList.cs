using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RecipeList : MonoBehaviour
{
    [SerializeField] TextAsset RecipesFile;
    [SerializeField] TextAsset IngredientsFile;


    private static List<RecipeCard> RecipesDeck;
    static System.Random rand = new System.Random();

    void Start()
    {
        // Recipe Loading straight to deck
        RecipesDeck = new List<RecipeCard>();
        LoadRecipesToDeck();
    }

    // Displays a random recipe in the Recipe Bank from the Recipe List
    // The same recipe can appear more than once
    public void DisplayRecipe()
    {
        //Text textObject = gameObject.GetComponentInChildren<Text>();
        //textObject.text = recipes[rand.Next(0, recipes.Count)].ToString();
    }

    public void LoadRecipesToDeck()
    {
        Recipes recipes = JsonUtility.FromJson<Recipes>(RecipesFile.text);
        foreach (Recipe recipe in recipes.recipes)
        {
            RecipeCard aRecipeCard = new RecipeCard(
                recipe.RecipeName,
                recipe.RecipeType,
                recipe.IngredientIds,
                IngredientsFile);
            RecipesDeck.Add(aRecipeCard);
        }
        Shuffle(RecipesDeck);
    }

    private void Shuffle(List<RecipeCard> recipeDeck)
    {
        int i = recipeDeck.Count;
        while (i > 1)
        {
            i--;
            int s = Random.Range(0, i + 1);
            RecipeCard swap = recipeDeck[s];
            recipeDeck[s] = recipeDeck[i];
            recipeDeck[i] = swap;
        }
    }

    public RecipeCard DrawRecipeCard()
    {
        RecipeCard drawnCard = RecipesDeck[0];
        RecipesDeck.RemoveAt(0);
        return drawnCard;
    }


}
