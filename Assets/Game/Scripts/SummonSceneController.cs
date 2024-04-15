using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField]
    private IngredientsPanelController _ingredientsPanelController;

    [SerializeField]
    private UnityEvent<MonsterScriptableObject> onMonsterSummoned;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Jukebox jukebox;

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
        MonsterScriptableObject selectedMonster = null;
        foreach(MonsterScriptableObject monster in _availableMonsters)
        {
            List<IngredientItemScriptableObject> itemsRequired = monster.itemsRequired;
            HashSet<string> itemsRequiredSet = new HashSet<string>(itemsRequired.Select(ingredient => ingredient.id));

            if (itemsRequiredSet.SetEquals(_ingredientsPanelController.SelectedIngredients))
            {
                selectedMonster = monster;
                break;
            }
        }

        selectedMonster = _availableMonsters[2];

        _ingredientsPanelController.DepleteSelectedIngredients();

        if (selectedMonster != null)
        {
            playerController.ResetHealth();

            onMonsterSummoned.Invoke(selectedMonster);
            
            gameObjectSummon.SetActive(false);
            gameObjectFight.SetActive(true);

            jukebox.PlayFightMusic();

            fightSceneController.StartNewFight(selectedMonster);
        }
    }
}
