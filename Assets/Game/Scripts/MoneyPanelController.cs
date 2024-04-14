using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPanelController : MonoBehaviour
{
    [SerializeField]
    private MoneyController moneyController;

    [SerializeField]
    private TextMeshProUGUI moneyTextBox;

    private void Update()
    {
        moneyTextBox.text = moneyController.Money.ToString();
    }
}
