using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectSummon;

    [SerializeField]
    private FightSceneController fightSceneController;

    [SerializeField]
    private MoneyController moneyController;    

    [SerializeField]
    private GameObject gameObjectShop;

    [SerializeField]
    private GameObject gameObjectBook;    

    [SerializeField]
    private GameObject gameObjectFight;     

    [SerializeField] 
    private List<MonsterScriptableObject> _availableMonsters;

    
    public void OnOpenShopButtonClick()
    {
        gameObjectShop.SetActive(true);
    }

    public void OnOpenBookButtonClick()
    {
        gameObjectBook.SetActive(true);
    }    

    public void OnSummonButtonClick()
    {
        gameObjectSummon.SetActive(false);
        gameObjectFight.SetActive(true);

        fightSceneController.StartNewFight(_availableMonsters[0]);
    }
}
