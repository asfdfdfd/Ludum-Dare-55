using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScriptableObject : ScriptableObject
{
    public string id;
    public int health;
    // i - idle, wait.
    // a - attack.
    public string pattern;
    public List<IngredientItemScriptableObject> itemsRequired;
    public List<IngredientItemScriptableObject> itemsProduced;
}
