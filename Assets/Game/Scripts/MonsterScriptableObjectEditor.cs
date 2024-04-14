using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterScriptableObjectEditor
{
    [MenuItem("Assets/Create/Monster")]
    public static void CreateMonster()
    {
        MonsterScriptableObject so = ScriptableObject.CreateInstance<MonsterScriptableObject>();

        AssetDatabase.CreateAsset(so, "Assets/Game/Database/Monsters/NewMonster.asset");
        AssetDatabase.SaveAssets();
    }
}
