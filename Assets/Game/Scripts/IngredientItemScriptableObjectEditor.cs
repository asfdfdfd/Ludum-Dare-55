using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IngredientItemScriptableObjectEditor
{
    [MenuItem("Assets/Create/Ingredient Item")]
    public static void CreateIngredientItem()
    {
        IngredientItemScriptableObject so = ScriptableObject.CreateInstance<IngredientItemScriptableObject>();

        AssetDatabase.CreateAsset(so, "Assets/Game/Database/IngredientItems/NewIngredientItem.asset");
        AssetDatabase.SaveAssets();
    }
}
