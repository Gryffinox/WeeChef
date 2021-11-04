using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] GameObject ingredientPanel;

    void Start()
    {
        ingredientPanel.SetActive(false);
    }

    // Toggles the panel that shows the player's gathered ingredients
    public void TogglePlayerIngredients()
    {
        // We need to show the player ingredients
        if (!ingredientPanel.activeInHierarchy)
        {
            ingredientPanel.SetActive(true);
        }
        // We need to hide the player ingredients
        else
        {
            ingredientPanel.SetActive(false);
        }
    }

}
