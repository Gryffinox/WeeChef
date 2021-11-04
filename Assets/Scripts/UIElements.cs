using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{
    public GameObject CardPickupDialog;
    public GameObject MovementConfirmDialog;
    public Button YesBtn;
    public Button NoBtn;
    // Start is called before the first frame update
    void Start()
    {
        CardPickupDialog.SetActive(false);
        MovementConfirmDialog.SetActive(false);
        PlayerParent playerParent = GameObject.Find("Players").GetComponent<PlayerParent>();
        YesBtn.GetComponent<Button>().onClick.AddListener(playerParent.AcceptCard);
        NoBtn.GetComponent<Button>().onClick.AddListener(playerParent.DeclineCard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMovementConfirmDialog() {
        MovementConfirmDialog.SetActive(true);
        CardPickupDialog.SetActive(false);
    }

    public void ActivateCardPickupDialog() {
        CardPickupDialog.SetActive(true);
        MovementConfirmDialog.SetActive(false);
    }



}
