using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    //Fudge c#, we write it like Java
    private int Id; // { get; private set; }
    private string IngredientName; // { get; set; }
    private IngredientTypeEnum IngredientType; //{ get; private set; }
    private float IngredientCost;
    //Text ingredientInfo;
    public enum IngredientTypeEnum { Dry_Goods, Dairy, Pork, Poultry, Allium, Garden_Vegetables, Liquid_Goods, Root_Vegetables }

    // Parameter Constructor
    public Ingredient(string name, IngredientTypeEnum type, float cost)
    {
        IngredientName = name;
        IngredientType = type;
        IngredientCost = cost;
    }

    public void Start() {
        Id = 0;
        IngredientName = GetComponent<SpriteRenderer>().sprite.name;
        IngredientType = 0; // dry goods by default
        IngredientCost = 0f;
        //ingredientInfo = gameObject.GetComponentInChildren<Text>();
    }

    public void setId(int id) {
        Id = id;
    }

    public void setIngredientName(string name) {
        IngredientName = name;
    }

    public string getIngredientName() {
        return IngredientName;
    }

}
