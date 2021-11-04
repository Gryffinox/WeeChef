using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeList : MonoBehaviour
{
    Text recipeText;
    [SerializeField] int recipeIndex;
    private List<Recipe> recipeList = new List<Recipe>();
    Recipe Croissant;
    Recipe Souffle;
    Recipe Cassoulet;
    Recipe Ratatouille;
    Recipe CoqAuVin;

    // ALL this should be managed in Recipe class by getIngredientsOfRecipe()
    private List<string> croissantIngredients;
    private List<string> souffleIngredients;
    private List<string> cassouletIngredients;
    private List<string> ratatouilleIngredients;
    private List<string> coqauvinIngredients;

    /* 5 french recipes
     * croissant: flour (dry goods), butter (dairy)
     * soufflé: flour (dry goods), butter (dairy), eggs (dairy)
     * cassoulet: beans (dry goods), sausages (pork/charcuterie), duck (poultry)
     * ratatouille: garlic (allium), tomatoes (garden vegetables), zucchini (garden vegetables), eggplants (garden vegetables)
     * coq-au-vin: chicken (poultry), bacon (pork/charcuterie), wine (liquid goods), mushrooms (root vegetables)
     */

    void Start()
    {
        recipeText = gameObject.GetComponentInChildren<Text>();
        CreateRecipes();

        recipeList.Add(Croissant);
        recipeList.Add(Souffle);
        recipeList.Add(Cassoulet);
        recipeList.Add(Ratatouille);
        recipeList.Add(CoqAuVin);

        //DisplayRecipeList();
        DisplayRecipe();
    }

    // Creates recipes (only 5 for now)
    // Will use a text reading code later, for now it's all hardcoded
    public void CreateRecipes()
    {
        croissantIngredients = new List<string>() { "dry goods", "dairy" };
        souffleIngredients = new List<string>() { "dry goods", "dairy x2" };
        cassouletIngredients = new List<string>() { "dry goods", "pork/charcuterie", "poultry" };
        ratatouilleIngredients = new List<string>() { "allium", "garden vegetables x3" };
        coqauvinIngredients = new List<string>() { "poultry", "pork / charcuterie", "liquid goods", "root vegetables" };

        Croissant = new Recipe("Croissant", Recipe.CuisineTypes.French, croissantIngredients);
        Souffle = new Recipe("Soufflé", Recipe.CuisineTypes.French, souffleIngredients);
        Cassoulet = new Recipe("Cassoulet", Recipe.CuisineTypes.French, cassouletIngredients);
        Ratatouille = new Recipe("Ratatouille", Recipe.CuisineTypes.French, ratatouilleIngredients);
        CoqAuVin = new Recipe("Coq-Au-Vin", Recipe.CuisineTypes.French, coqauvinIngredients);
    }

    public void DisplayRecipeList()
    {
        //recipeText.text = "";
        for (int i = 0; i < recipeList.Count; i++)
        {
            // get the recipe name + ingredients
            recipeText.text += "\n" + recipeList[i].getRecipeName() + ": "
                + recipeList[i].getIngredientsOfRecipeString();
        }
    }

    public void DisplayRecipe()
    {
        Text textObject = gameObject.GetComponentInChildren<Text>();
        for (int i = 0; i < recipeList.Count; i++)
        {
            if (i == recipeIndex)
                textObject.text = recipeList[i].ToString();
        }
    }

}
