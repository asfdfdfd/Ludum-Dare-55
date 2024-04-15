using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectShop;

    [SerializeField]
    private MoneyController moneyController;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Button healthUpgradeButton;

    [SerializeField]
    private TextMeshProUGUI healthUpgradePriceText;

    [SerializeField]
    private Button swordUpgradeButton;

    [SerializeField]
    private TextMeshProUGUI swordUpgradePriceText;

    private readonly List<int> _healthUpgrade = new List<int> { 5, 8 };
    private readonly List<int> _healthPrice = new List<int> { 400, 4000 };

    private readonly List<int> _attackUpgrade = new List<int> { 2, 4, 8 };
    private readonly List<int> _attackUpgradePrice = new List<int> { 400, 4000, 8000 };

    public void OnCloseButtonClick()
    {
        gameObjectShop.SetActive(false);
    }

    private void Update()
    {
        if (_healthUpgrade.Count > 0)
        {
            healthUpgradePriceText.SetText(_healthPrice[0].ToString());
            healthUpgradeButton.interactable = moneyController.Money >= _healthPrice[0];
        }
        else
        {
            healthUpgradePriceText.SetText("MAX");
            healthUpgradeButton.interactable = false;
        }

        if (_attackUpgrade.Count > 0)
        {
            swordUpgradePriceText.SetText(_attackUpgradePrice[0].ToString());
            swordUpgradeButton.interactable = moneyController.Money >= _attackUpgradePrice[0];
        }
        else
        {
            swordUpgradePriceText.SetText("MAX");
            swordUpgradeButton.interactable = false;
        }        
    }

    public void UpgradeHeart()
    {
        playerController.SetMaxHealth(_healthUpgrade[0]);
        moneyController.SpendMoney(_healthPrice[0]);
        _healthUpgrade.RemoveAt(0);
        _healthPrice.RemoveAt(0);
    }

    public void UpgradeSword()
    {
        playerController.AttackPower = _attackUpgrade[0];
        moneyController.SpendMoney(_attackUpgradePrice[0]);
        _attackUpgrade.RemoveAt(0);
        _attackUpgradePrice.RemoveAt(0);
    }
}
