using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrderIngredientPanelItemController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amountText;

    public void SetAmount(int amount)
    {
        if (amount == int.MaxValue)
        {
            amountText.SetText("∞");
        }
        else
        {
            amountText.SetText(amount.ToString());
        }
    }
}
