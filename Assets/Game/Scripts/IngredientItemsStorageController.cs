using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;

public class IngredientItemsStorageController : MonoBehaviour
{
    private readonly Dictionary<string, int> _itemsDict = new();

    private readonly List<string> _itemsList = new();

    public UnityEvent onItemsUpdated;

    // First item is a special item — it has infinite amount.
    [SerializeField]
    private IngredientItemScriptableObject firstIngredientItem;

    private void Awake()
    {
        _itemsDict[firstIngredientItem.id] = int.MaxValue;
        _itemsList.Add(firstIngredientItem.id);
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
        if (id == firstIngredientItem.id)
        {
            return;
        }

        if (_itemsDict.ContainsKey(id))
        {
            _itemsDict[id] = _itemsDict[id] - 1;

            if (_itemsDict[id] == 0)
            {
                _itemsDict.Remove(id);
                _itemsList.Remove(id);
            }

            onItemsUpdated?.Invoke();
        }
    }
}
