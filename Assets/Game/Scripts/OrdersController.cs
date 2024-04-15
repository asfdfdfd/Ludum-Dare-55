using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OrdersController : MonoBehaviour
{
    class Order 
    {
        public List<IngredientItemScriptableObject> ingredients;
        public List<int> amountOfIngredients;
    }

    [SerializeField]
    private Button sellOrderButton;

    [SerializeField]
    private IngredientItemsStorageController ingredientItemsStorageController;

    [SerializeField]
    private SummonSceneController summonSceneController;

    [SerializeField]
    private List<IngredientItemScriptableObject> ingredientsTier1;

    [SerializeField]
    private List<IngredientItemScriptableObject> ingredientsTier2;

    [SerializeField]
    private List<IngredientItemScriptableObject> ingredientsTier3;  

    [SerializeField]  
    private GameObject orderIconPrefab;

    [SerializeField]
    private GameObject orderIconsPanel;

    [SerializeField]
    private MoneyController moneyController;

    private List<Order> _mandatoryOrders = new List<Order>();

    private Order _activeOrder;

    private void Awake()
    {
        _mandatoryOrders.Add(new Order { ingredients = ingredientsTier1, amountOfIngredients = ingredientsTier1.Select(i => 1).ToList() });
        _mandatoryOrders.Add(new Order { ingredients = ingredientsTier1, amountOfIngredients = ingredientsTier1.Select(i => 1).ToList() });
        _mandatoryOrders.Add(new Order { ingredients = ingredientsTier2, amountOfIngredients = ingredientsTier2.Select(i => 2).ToList() });

        sellOrderButton.onClick.AddListener(() => SellOrder());

        GenerateNextOrder();
    }

    private void GenerateNextOrder()
    {
        if (_mandatoryOrders.Count > 0)
        {
            _activeOrder = _mandatoryOrders[0];
            _mandatoryOrders.RemoveAt(0);
        }
        else
        {
            _activeOrder = null;
        }

        if (_activeOrder != null)
        {
            RemoveAllItemsFromPanel();

            for (int i = 0; i < _activeOrder.ingredients.Count; i++)
            {
                IngredientItemScriptableObject ingredient = _activeOrder.ingredients[i];

                GameObject ingredientIconPrefab = Instantiate(orderIconPrefab);
                ingredientIconPrefab.transform.SetParent(orderIconsPanel.transform);

                OrderIngredientPanelItemController controller = ingredientIconPrefab.GetComponent<OrderIngredientPanelItemController>();
                controller.SetAmount(_activeOrder.amountOfIngredients[i]);
                controller.SetIcon(ingredient.icon);
            }
        }
    }

    private void RemoveAllItemsFromPanel()
    {
        for (int i = orderIconsPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(orderIconsPanel.transform.GetChild(i).gameObject);
        }
    }

    private void SellOrder()
    {
        int amountOfMoney = 0;

        for (int i = 0; i < _activeOrder.ingredients.Count; i++)
        {
            IngredientItemScriptableObject ingredient = _activeOrder.ingredients[i];   

            ingredientItemsStorageController.RemoveItem(ingredient.id, _activeOrder.amountOfIngredients[i]);

            amountOfMoney += ingredient.price * _activeOrder.amountOfIngredients[i];     
        }

        moneyController.EarnMoney(amountOfMoney);

        RemoveAllItemsFromPanel();
        
        GenerateNextOrder();
    }

    private void Update()
    {
        bool isSellingAllowed = true;

        if (_activeOrder != null)
        {
            for (int i = 0; i < _activeOrder.ingredients.Count; i++)
            {
                IngredientItemScriptableObject ingredient = _activeOrder.ingredients[i];

                if (ingredientItemsStorageController.GetItemCount(ingredient.id) < _activeOrder.amountOfIngredients[i])
                {
                    isSellingAllowed = false;
                    break;
                }
            }
        }
        else
        {
            isSellingAllowed = false;
        }
    
        sellOrderButton.interactable = isSellingAllowed;
    }
}
