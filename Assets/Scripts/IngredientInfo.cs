using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientInfo : MonoBehaviour
{
    Text ingredientInfo;

    void Start()
    {
        ingredientInfo = gameObject.GetComponentInChildren<Text>();
        DisplayIngredientInfo();
    }

    private float GenerateRandomCost()
    {
        float min = 3f;
        float max = 20f;
        System.Random rand = new System.Random();
        return (float)rand.NextDouble() * (max - min) + min;
    }

    public void DisplayIngredientInfo()
    {
        ingredientInfo.text += "Name: " + "Cheese"
            + "\nType: " + "Dairy"
            + "\nCost: " + GenerateRandomCost().ToString("F2") + "$";
    }

}
