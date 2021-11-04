using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientHand : MonoBehaviour
{
    Text ingredientHandText;

    void Start()
    {
        ingredientHandText = gameObject.GetComponentInChildren<Text>();
        DisplayIngredientHand();
    }

    private void DisplayIngredientHand()
    {
        ingredientHandText.text = "Player Ingredients:\nLemon (Garden Vegetable)" +
            "\nCheese (Dairy)" +
            "\nWine (Liquid Goods)" +
            "\nBacon (Pork)" +
            "\nEggs (Dairy)";
    }

}
