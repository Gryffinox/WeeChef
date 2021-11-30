//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LoadRecipes : MonoBehaviour
//{
//    string filePath = "Assets/TextFiles/Recipes.txt";
//    public static List<Recipe> allRecipes = new List<Recipe>();

//    void Start()
//    {
//        OrganizeAllRecipes(filePath);
//    }

//    public static List<Recipe> getAllRecipes()
//    {
//        return allRecipes;
//    }

//    // This method will read through the recipes text file
//    // and organize every recipe with it's name, cuisine type and ingredients.
//    private static void OrganizeAllRecipes(string filePath)
//    {
//        string[] allLines = System.IO.File.ReadAllLines(filePath);
//        string cuisineType = "";
//        string[] recipeElements;
//        string recipeName;
//        string ingredients;
//        string optionalIngredient;
//        string[] ingredientsArray;
//        List<Ingredient> actualIngredients; 

//        // Will repeat on every line
//        foreach (string line in allLines)
//        {
//            actualIngredients = new List<Ingredient>();

//            // The start of a new cuisine type
//            if (line.Contains("+"))
//            {
//                cuisineType = line.Split('+')[1]; // assuming an empty string isnt in the split
//                continue;
//            }

//            // Skip empty lines
//            if (line.Length == 0)
//                continue;

//            // The recipe name, needed ingredients, and optional ingredients
//            recipeElements = line.Split(':', '.');

//            recipeName = recipeElements[0];
//            ingredients = recipeElements[1];
//            optionalIngredient = recipeElements[3]; // idk if we will use this

//            // Organize the ingredients of the recipe
//            ingredientsArray = ingredients.Split(',');

//            // Separate every ingredient string to isolate the name and the type
//            string ingredientName;
//            string ingredientType;
//            for (int i = 0; i < ingredientsArray.Length; i++)
//            {
//                // ex. Cream (Dairy)
//                ingredientName = ingredientsArray[i].Split('(', ')')[0];
//                ingredientType = ingredientsArray[i].Split('(', ')')[1];
//                actualIngredients.Add(new Ingredient(ingredientName, ingredientType));
//            }

//            allRecipes.Add(new Recipe(recipeName, cuisineType, actualIngredients));

//        }
//    }
//}
