using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientGatheringUI : MonoBehaviour {

    //UI elements
    [SerializeField] private TextMeshProUGUI TxtBox;
    [SerializeField] private GameObject ConfirmButton;
    [SerializeField] private GameObject BuyButton;
    //Needed for the buttons. Buttons are used by players
    [SerializeField] private GameObject PlayerParentHandler;

    void Start() {
        TxtBox.text = "";
        PlayerParent pp = PlayerParentHandler.GetComponent<PlayerParent>();
        ConfirmButton.GetComponent<Button>().onClick.AddListener(pp.MoveAction);
        BuyButton.GetComponent<Button>().onClick.AddListener(pp.BuyAction);
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
}
