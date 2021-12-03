using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientGatheringUI : MonoBehaviour {

    //UI elements
    [SerializeField] private Text TxtBox;
    [SerializeField] private GameObject ConfirmButton;
    [SerializeField] private GameObject BuyButton;
    [SerializeField] private Text TurnText;
    //Needed for the buttons. Buttons are used by players
    [SerializeField] private GameObject Players;
    private PlayerParent PlayerHandler;

    void Start() {
        TxtBox.text = "";
        PlayerHandler = Players.GetComponent<PlayerParent>();
        ConfirmButton.GetComponent<Button>().onClick.AddListener(PlayerHandler.MoveAction);
        BuyButton.GetComponent<Button>().onClick.AddListener(PlayerHandler.BuyAction);
    }

    public void ShowConfirmButton() {
        HideAllButtons();
        ConfirmButton.SetActive(true);
    }

    public void ShowBuyButton() {
        HideAllButtons();
        BuyButton.SetActive(true);
    }

    public void HideAllButtons() {
        ConfirmButton.SetActive(false);
        BuyButton.SetActive(false);
    }

    public void SetBuyButtonInteractable(bool state) {
        BuyButton.GetComponent<Button>().interactable = state;
    }

    public void DisplayIngredientInfo(Ingredient ingredient) {
        TxtBox.text = ingredient.Name + "\n" + "Cost: " + ingredient.Cost;
    }

    public void DisplayText(string txt) {
        TxtBox.text = txt;
    }
    public void UpdateTurnCount() {
        TurnText.text = "Turns Left: " + PlayerHandler.GetTurnCount().ToString();
    }

}
