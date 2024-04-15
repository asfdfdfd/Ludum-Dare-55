using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IngredientIconPanelItemController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amountText;

    [SerializeField]
    public Toggle toggle;

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
