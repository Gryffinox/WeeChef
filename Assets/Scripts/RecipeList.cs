using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeList : MonoBehaviour
{
    [SerializeField] Text recipeText;
    private List<Recipe> recipeList = new List<Recipe>();
    Recipe Croissant;
    Recipe Souffle;
    Recipe Cassoulet;
    Recipe Ratatouille;

    // ALL this should be managed in Recipe class by getIngredientsOfRecipe()
    private List<string> croissantIngredients;
    private List<string> souffleIngredients;
    private List<string> cassouletIngredients;
    private List<string> ratatouilleIngredients;

    /* 4 french recipes
     * croissant: flour, butter
     * soufflé: flour, butter, eggs
     * cassoulet: beans, sausages, duck
     * ratatouille: garlic, tomatoes, zucchini eggplants
     */

    void Start()
    {
        recipeText = gameObject.GetComponentInChildren<Text>();
        CreateRecipes();

        recipeList.Add(Croissant);
        recipeList.Add(Souffle);
        recipeList.Add(Cassoulet);
        recipeList.Add(Ratatouille);

        DisplayRecipeList();
    }

    // Creates recipes (only 4 for now)
    // Will use a text reading code later, for now it's all hardcoded
    public void CreateRecipes()
    {
        croissantIngredients = new List<string>() { "flour", "butter" };
        souffleIngredients = new List<string>() { "flour", "butter", "eggs" };
        cassouletIngredients = new List<string>() { "beans", "sausages", "duck" };
        ratatouilleIngredients = new List<string>() { "garlic", "tomatoes", "zucchini", "eggplants" };

        Croissant = new Recipe("Croissant", "French", croissantIngredients);
        Souffle = new Recipe("Soufflé", "French", souffleIngredients);
        Cassoulet = new Recipe("Cassoulet", "French", cassouletIngredients);
        Ratatouille = new Recipe("Ratatouille", "French", ratatouilleIngredients);
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

}
