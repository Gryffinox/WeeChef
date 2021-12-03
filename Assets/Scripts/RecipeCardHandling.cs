using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCardHandling : MonoBehaviour
{
    private Player player;
    private Player nextPlayer;

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

        if (player != nextPlayer)
        {
            for (int i = 0; i < 4; i++)
            {
                recipeCardsInMenu[i].SetRecipeName("no recipe");
                recipeCardsInMenu[i].SetRecipeType("no recipe");
                recipeCardsInMenu[i].SetIngredients(null);
                recipeCardsInMenu[i].SetIngredientIds(null);
                recipeCardText[i].text = "No Recipe";
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (playerRecipeCards.Count == 0)
            {
                recipeCardsInMenu[i].SetRecipeName("no recipe");
                recipeCardsInMenu[i].SetRecipeType("no recipe");
                recipeCardsInMenu[i].SetIngredients(null);
                recipeCardsInMenu[i].SetIngredientIds(null);
                recipeCardText[i].text = "No Recipe";
            }
            else
            {
                recipeCardsInMenu[i].SetRecipeName(playerRecipeCards[i].getRecipeName());
                recipeCardsInMenu[i].SetRecipeType(playerRecipeCards[i].getRecipeType());
                recipeCardsInMenu[i].SetIngredients(playerRecipeCards[i].getIngredientsOfRecipeList());
                recipeCardsInMenu[i].SetIngredientIds(playerRecipeCards[i].getIngredientIds());
                recipeCardText[i].text = playerRecipeCards[i].ToString();
            }
        }
        nextPlayer = PlayerParent.GetActivePlayer();
    }   
}
