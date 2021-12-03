using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCardHandling : MonoBehaviour
{
    private Player player;

    private List<RecipeCard> playerRecipeCards;

    private RecipeCard[] recipeCardsInMenu;

    private Text[] recipeCardText;

    private void Start()
    {
        recipeCardsInMenu = GetComponentsInChildren<RecipeCard>();
        recipeCardText = GetComponentsInChildren<Text>();
    }

    private void Update()
    {
        player = PlayerParent.GetActivePlayer();
        playerRecipeCards = player.GetRecipeHand();

        for (int i = 0; i < player.GetRecipeHand().Count; i++)
        {
            if (recipeCardsInMenu[i].getRecipeName() == null)
            {
                recipeCardText[i].text = playerRecipeCards[i].ToString();
            }
        }
    }   
}
