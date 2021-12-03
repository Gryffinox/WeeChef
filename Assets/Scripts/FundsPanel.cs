using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FundsPanel : MonoBehaviour
{
    private Text fundsText;

    // Start is called before the first frame update
    void Start()
    {
        fundsText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        fundsText.text = "$" + PlayerParent.GetActivePlayer().GetFunds().ToString();
    }
}
