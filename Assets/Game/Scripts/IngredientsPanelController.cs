using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsPanelController : MonoBehaviour
{
    [SerializeField]
    private IngredientItemsStorageController _ingredientItemsStorageController;

    [SerializeField]
    private GameObject _ingredientItemsRoot;

    [SerializeField]
    private GameObject _ingredientIconPanelItemPrefab;

    private void Start()
    {
        _ingredientItemsStorageController.onItemsUpdated.AddListener(OnItemsUpdated);

        RebuildList();
    }

    private void OnItemsUpdated()
    {
        RebuildList();
    }

    private void RebuildList()
    {
        for (int i = _ingredientItemsRoot.transform.childCount - 1; i >= 0; i--) 
        {
            Destroy(_ingredientItemsRoot.transform.GetChild(i).gameObject);
        }

        foreach (string id in _ingredientItemsStorageController.Items)
        {
            GameObject ingredientIconPanelItem = Instantiate(_ingredientIconPanelItemPrefab);
            ingredientIconPanelItem.transform.SetParent(_ingredientItemsRoot.transform);
        }
    }
}
