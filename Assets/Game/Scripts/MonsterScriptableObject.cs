using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterScriptableObject : ScriptableObject
{
    public string id;
    public int health;
    public int damage;
    public List<IngredientItemScriptableObject> itemsRequired;
    public List<IngredientItemScriptableObject> itemsProduced;
    public List<string> dialogsBeforeFight;
    public List<string> dialogsAfterFight;

    public int Price
    {
        get
        {
            return itemsProduced.Select(item => item.price).Aggregate(0, (sum, cur) => sum + cur);
        }
    }
}
