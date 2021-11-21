using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    private int Id; // { get; private set; }
    private string IngredientName; // { get; set; }
    //private IngredientTypeEnum IngredientType; //{ get; private set; }
    private int IngredientCost;
    //public enum IngredientTypeEnum { Dry_Goods, Dairy, Pork, Poultry, Allium, Garden_Vegetables, Liquid_Goods, Root_Vegetables }
    private string IngredientType;
    private static int idCounter;

    // Parameter Constructor
    public Ingredient(string name, string type)
    {
        Id = setIdConstructor();
        IngredientName = name;
        IngredientType = type;
        IngredientCost = setRandomCost();
    }

    public void setId(int id)
    {
        Id = id;
    }

    public int setIdConstructor()
    {
        //print("TEST: Ingredient ID Counter is at: " + idCounter);
        return idCounter++;
    }

    public void setIngredientName(string name) {
        IngredientName = name;
    }

    public string getIngredientName() {
        return IngredientName;
    }

    public string getIngredientType()
    {
        return IngredientType;
    }

    public override string ToString()
    {
        return getIngredientName() + " (" + getIngredientType() + ")";
    }

    private int setRandomCost()
    {
        // random cost between 5 and 32 dollars
        return (new System.Random().Next(5, 33));
    }

}
