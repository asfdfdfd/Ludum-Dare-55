using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class IngredientItemsStorageController : MonoBehaviour
{
    private readonly Dictionary<string, int> _itemsDict = new();

    private readonly List<string> _itemsList = new();

    [SerializeField]
    private List<IngredientItemScriptableObject> _ingredientsAll;

    private readonly Dictionary<string, IngredientItemScriptableObject>  _ingredientsAllDict = new();

    public UnityEvent onItemsUpdated;

    // First item is a special item — it has infinite amount.
    [SerializeField]
    private IngredientItemScriptableObject firstIngredientItem;

    private void Awake()
    {
        _itemsDict[firstIngredientItem.id] = int.MaxValue;
        _itemsList.Add(firstIngredientItem.id);     

        foreach (IngredientItemScriptableObject item in _ingredientsAll)
        {
            _ingredientsAllDict[item.id] = item;
        }
    }

    private void Update()
    {
        if (Application.isEditor)        
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _itemsDict["IngredientItem1"] = 10;
                _itemsList.Add("IngredientItem1");

                _itemsDict["IngredientItem2"] = 10;
                _itemsList.Add("IngredientItem2");
                
                _itemsDict["IngredientItem3"] = 10;
                _itemsList.Add("IngredientItem3");        

                _itemsDict["IngredientItem4"] = 10;
                _itemsList.Add("IngredientItem4");        

                _itemsDict["IngredientItem5"] = 10;
                _itemsList.Add("IngredientItem5");  
            }
        }
    }

    public List<string> Items
    {
        get { return _itemsList; }
    }

    public void AddItem(string id)
    {
        if (id == firstIngredientItem.id)
        {
            return;
        }

        if (_itemsDict.ContainsKey(id))
        {
            _itemsDict[id] = _itemsDict[id] + 1;
        }
        else
        {
            _itemsDict[id] = 1;
            _itemsList.Add(id);
        }

        onItemsUpdated?.Invoke();
    }

    public void RemoveItem(string id)
    {
        RemoveItem(id, 1);
    }

    internal void RemoveItem(string id, int amount)
    {
        if (id == firstIngredientItem.id)
        {
            return;
        }

        if (_itemsDict.ContainsKey(id))
        {
            _itemsDict[id] = _itemsDict[id] - amount;

            if (_itemsDict[id] <= 0)
            {
                if (_itemsDict[id] < 0)
                {
                    Debug.LogWarning("Amount of item after remove is negative!");                    
                }
                
                _itemsDict.Remove(id);
                _itemsList.Remove(id);
            }

            onItemsUpdated?.Invoke();
        }
    }    

    internal int GetItemCount(string id)
    {
        if (_itemsDict.ContainsKey(id))
        {
            return _itemsDict[id];
        }
        else
        {
            return 0;
        }
    }

    internal Sprite GetSprite(string id)
    {
        return _ingredientsAllDict[id].icon;
    }
}
