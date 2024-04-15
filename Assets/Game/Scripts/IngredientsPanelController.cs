using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IngredientsPanelController : MonoBehaviour
{
    [SerializeField]
    private IngredientItemsStorageController _ingredientItemsStorageController;

    [SerializeField]
    private GameObject _ingredientItemsRoot;

    [SerializeField]
    private GameObject _ingredientIconPanelItemPrefab;

    private readonly int itemsOnPage = 4;

    private int pageIndex = 0;

    private readonly HashSet<string> _selectedIngredients = new HashSet<string>();

    public HashSet<string> SelectedIngredients
    {
        get
        {
            return _selectedIngredients;
        }
    }

    public void DepleteSelectedIngredients()
    {
        foreach(string id in _selectedIngredients)
        {
            _ingredientItemsStorageController.RemoveItem(id);
        }

        _selectedIngredients.Clear();

        RebuildList();
    }

    private void Start()
    {
        _ingredientItemsStorageController.onItemsUpdated.AddListener(OnItemsUpdated);

        RebuildList();
    }

    private int PagesCount
    {
        get
        {
            var itemsCountNew = _ingredientItemsStorageController.Items.Count;

            return (int)Math.Ceiling(itemsCountNew / (float)itemsOnPage);
        }
    }

    public void PageUp()
    {
        pageIndex--;

        if (pageIndex < 0)
        {
            pageIndex = PagesCount - 1;
        }

        RebuildList();
    }

    public void PageDown()
    {
        pageIndex++;

        if (pageIndex == PagesCount)
        {
            pageIndex = 0;
        }

        RebuildList();
    }

    private void OnItemsUpdated()
    {
        RebuildList();
    }

    private void RebuildList()
    {
        var itemsCountNew = _ingredientItemsStorageController.Items.Count;

        if (pageIndex >= PagesCount)
        {
            pageIndex = PagesCount - 1;
        }

        var pageStartItemIndex = pageIndex * itemsOnPage;
        
        var pageEndItemIndex = pageStartItemIndex + itemsOnPage - 1;
        if (pageEndItemIndex >= itemsCountNew)
        {
            pageEndItemIndex = itemsCountNew - 1;
        }

        for (int i = _ingredientItemsRoot.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(_ingredientItemsRoot.transform.GetChild(i).gameObject);
        }

         for (int i = pageStartItemIndex; i <= pageEndItemIndex; i++)
         {
            GameObject ingredientIconPanelItem = Instantiate(_ingredientIconPanelItemPrefab);
            ingredientIconPanelItem.transform.SetParent(_ingredientItemsRoot.transform);            

            var id = _ingredientItemsStorageController.Items[i];
            var ingredientIconPanelItemController = ingredientIconPanelItem.GetComponent<IngredientIconPanelItemController>();
            ingredientIconPanelItemController.SetAmount(_ingredientItemsStorageController.GetItemCount(id));            
            ingredientIconPanelItemController.SetSprite(_ingredientItemsStorageController.GetSprite(id));
            ingredientIconPanelItemController.toggle.isOn = _selectedIngredients.Contains(id);
            ingredientIconPanelItemController.toggle.onValueChanged.AddListener((isSelected) => {
                if (isSelected)
                {
                    _selectedIngredients.Add(id);
                }
                else
                {
                    _selectedIngredients.Remove(id);
                }
            });
         }
    }
}
