using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientGatheringUI : MonoBehaviour {

    [SerializeField] private Text TxtBox;
    [SerializeField] private GameObject ConfirmButton;
    [SerializeField] private GameObject BuyButton;
    [SerializeField] private GameObject PlayerParentHandler;

    void Start() {
        TxtBox.text = "";
        PlayerParent pp = PlayerParentHandler.GetComponent<PlayerParent>();
        ConfirmButton.GetComponent<Button>().onClick.AddListener(pp.MoveAction);
        BuyButton.GetComponent<Button>().onClick.AddListener(pp.BuyAction);
    }

    //functions that hide and show elements and info
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

    public void DisplayIngredientInfo(Ingredient ingredient) {
        TxtBox.text = ingredient.Name + "\n" + "Cost: " + ingredient.Cost;
    }
}
