using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrderIngredientPanelItemController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI amountText;

    [SerializeField]
    private Image icon;

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

    public void SetIcon(Sprite iconSprite)
    {
        icon.sprite = iconSprite;
    }
}
