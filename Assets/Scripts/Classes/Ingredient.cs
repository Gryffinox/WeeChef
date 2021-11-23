using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    private int Id; // { get; private set; }
    private string IngredientName; // { get; set; }
    private int Cost;
    private string Type;
    private static int idCounter;

    // Parameter Constructor
    public Ingredient(string name, string type)
    {
        Id = SetIdConstructor();
        IngredientName = name;
        Type = type;
        Cost = SetRandomCost();
    }

    // Copy Constructor
    public Ingredient(Ingredient ingredientToCopy)
    {
        this.Id = SetIdConstructor(); // because every ingredient needs a unique ID
        this.IngredientName = ingredientToCopy.IngredientName;
        this.Type = ingredientToCopy.Type;
        this.Cost = ingredientToCopy.Cost;
    }

    // Setters
    public void SetId(int id)
    {
        Id = id;
    }

    public int SetIdConstructor()
    {
        return idCounter++;
    }

    public void SetIngredientName(string name) {
        IngredientName = name;
    }

    public void SetIngredientType(string type)
    {
        Type = type;
    }

    // To use only when referring to an already existing Ingredient
    public void SetCost(int cost)
    {
        Cost = cost;
    }

    private int SetRandomCost()
    {
        // random cost between 5 and 32 dollars
        return (new System.Random().Next(5, 33));
    }

    // Getters
    public int GetId()
    {
        return Id;
    }

    public string GetIngredientName() {
        return IngredientName;
    }

    public string GetIngredientType()
    {
        return Type;
    }

    public int GetCost()
    {
        return Cost;
    }

    public override string ToString()
    {
        return GetIngredientName() + " (" + GetIngredientType() + ")";
    }

}
