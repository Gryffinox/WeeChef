using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientHand : MonoBehaviour
{
    Player player;
    Text ingredientHandText;
    List<GameObject> ingredientHand; // needs to become a list of Ingredient, not gameobject
    string strToDisplay = "";

    void Start()
    {
        player = PlayerParent.GetActivePlayer();
        ingredientHandText = gameObject.GetComponentInChildren<Text>();
        ingredientHand = new List<GameObject>();
        DisplayIngredientHand();
    }

    private void DisplayIngredientHand()
    {
        strToDisplay = "";
        strToDisplay += player.name + ": ";
        for (int i = 0; i < player.GetIngredientCount(); i++)
            strToDisplay += player.GetIngredientHand()[i].name + "\n";
        ingredientHandText.text = strToDisplay;
    }

    private void Update()
    {
        // The active player needs to change
        player = PlayerParent.GetActivePlayer();

        if (gameObject.activeInHierarchy)
        {
            DisplayIngredientHand();
        }
            
    }

}
