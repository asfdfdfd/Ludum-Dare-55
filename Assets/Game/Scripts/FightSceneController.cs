using System;
using UnityEngine;

public class FightSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectFight;  

    [SerializeField]
    private GameObject gameObjectSummon;  

    [SerializeField]
    private IngredientItemsStorageController _ingredientsItemsStorageController;

    [SerializeField]
    private HealthPanelController _playerHealthPanelController;

    [SerializeField]
    private HealthPanelController _monsterHealthPanelController;   

    [SerializeField]
    private PlayerHealthController _playerHealthController;

    private MonsterScriptableObject _activeMonster;

    private int _activeMonsterHealth;
    private int _activeMonsterPatternIndex;

    public void OnAttackButtonClick()
    {
        // HandlePlayerAction("a");

        MoveToSummonScene();
    }

    public void OnShieldButtonClick()
    {
        // HandlePlayerAction("d");
    }

    public void OnTalkButtonClick()
    {
        //HandlePlayerAction("t");
    }

    public void OnHealButtonClick()
    {
        // HandlePlayerAction("h");
    }

    private void HandlePlayerAction(string playerAction)
    {
        // var monsterAction = _activeMonster.pattern.Substring(_activeMonsterPatternIndex, 1);

        // if (monsterAction == "a" && playerAction == "a")
        // {

        // }
        // else if (monsterAction == "a" && playerAction != "d")
        // {
        //     _playerHealth -= 1;

        //     if (_playerHealth == 0)
        //     {
        //         // TODO: Move to game over?
        //     }
        // }
        // else if (playerAction == "a" && monsterAction != "d")
        // {
        //     _activeMonsterHealth -= 1;

        //     if (_activeMonsterHealth == 0)
        //     {
        //         MoveToSummonScene();
        //     }
        // }
    }

    private void MoveToSummonScene()
    {
        gameObjectFight.SetActive(false);
        gameObjectSummon.SetActive(true);

        foreach (var item in _activeMonster.itemsProduced)
        {
            _ingredientsItemsStorageController.AddItem(item.id);
        }

        _activeMonster = null;
        _activeMonsterHealth = 0;
    }

    public void StartNewFight(MonsterScriptableObject monsterScriptableObject)
    {
        _activeMonster = monsterScriptableObject;
        _activeMonsterHealth = _activeMonster.health;
        _activeMonsterPatternIndex = 0;

        _monsterHealthPanelController.SetHealth(_activeMonsterHealth);
        _playerHealthPanelController.SetHealth(_playerHealthController.Health);
    }
}
