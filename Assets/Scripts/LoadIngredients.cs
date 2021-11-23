using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadIngredients : MonoBehaviour
{
    string filePathCards = "Assets/TextFiles/IngredientCards.txt";
    string filePathIngredients = "Assets/TextFiles/Ingredients.txt";
    public static List<Ingredient> allIngredients = new List<Ingredient>();
    public static List<Ingredient> allIngredientCards = new List<Ingredient>();
    private static System.Random rand = new System.Random();

    void Start()
    {
        OrganizeAllIngredients(filePathIngredients);

        // Test to check
        //string s1 = "";
        //foreach (Ingredient i in allIngredients)
        //    s1 += i.ToString();
        //print("All Ingredients:\n" + s1);

        OrganizeAllIngredientCards(filePathCards);

        // Test to check
        //string s2 = "";
        //foreach (Ingredient i in allIngredientCards)
        //    s2 += i.ToString();
        //print("Ingredient Cards:\n" + s2);

    }

    // This should not be used frenquently
    public static List<Ingredient> getAllIngredients()
    {
        return allIngredients;
    }

    // Returns the List of Ingredient Cards
    public static List<Ingredient> getAllIngredientCards()
    {
        return allIngredientCards;
    }

    // Reads a text file of every ingredient and its type and
    // creates one Ingredient object for each
    private static void OrganizeAllIngredients(string filePath)
    {
        string[] allLines = System.IO.File.ReadAllLines(filePath);
        string ingredientType = "";

        foreach (string line in allLines)
        {
            // Skip empty lines
            if (line.Length == 0)
                continue;

            // The start of a new ingredient type
            if (line.Contains("+"))
            {
                ingredientType = line.Split('+')[1];
                continue;
            }
            allIngredients.Add(new Ingredient(line, ingredientType)); // line = ingredient name
        }
    }

    // Reads a text file of every ingredient type and its quantity and associates
    // a random Ingredient object with the same type. The resulting Ingredient List
    // will be used in the grid during the Ingredient Gathering phase.
    private static void OrganizeAllIngredientCards(string filePath)
    {
        string[] allLines = System.IO.File.ReadAllLines(filePath);
        string ingredientType;
        int typeQuantity;
        List<Ingredient> specificIngredients;

        foreach (string line in allLines)
        {
            // Ex. line = Allium x7
            ingredientType = line.Split('x')[0].TrimEnd();
            typeQuantity = int.Parse(line.Split('x')[1]);

            // Find a random concrete Ingredient to associate with the type
            // Ex. Allium is: Garlic, Onions, Chives, Spring Onions, Pearl Onions

            // Isolate all ingredients of the specific type from allIngredients
            specificIngredients = new List<Ingredient>();
            for (int i = 0; i < allIngredients.Count; i++)
            {
                if (allIngredients[i].GetIngredientType().Equals(ingredientType))
                    specificIngredients.Add(allIngredients[i]);
            }

            for (int i = 0; i < typeQuantity; i++)
            {
                // What's important for recipe-making is the ingredient type, not the actual ingredient
                // We create a copy of the Ingredient (instead of making a reference to the same Ingredient
                // object many times) and we add it to the Ingredient Cards List
                // --> deleting one ingredient won't cause all the others to delete as well
                int index = rand.Next(0, specificIngredients.Count);
                allIngredientCards.Add(new Ingredient(specificIngredients[index]));
            }
        }
    }

}
