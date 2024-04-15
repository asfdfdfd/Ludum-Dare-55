using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

public class FightSceneController : MonoBehaviour
{
    enum FightStateItem
    {
        Idle,
        PrepareAttack,
        Attack,
        Defend,
        Stunned,
        Heal,
        Talk
    }

    [SerializeField]
    private float actionCooldown;

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
    private PlayerController _playerController;

    [SerializeField]
    private GameObject attackButtonGameObject;

    [SerializeField]
    private GameObject defendButtonGameObject;

    [SerializeField]
    private GameObject healButtonGameObject;

    [SerializeField]
    private GameObject talkButtonGameObject;    
    
    [SerializeField]
    private TextMeshProUGUI monsterStateText;

    [SerializeField]
    private DialogController dialogController;

    private readonly List<GameObject> _fightControlButtonsGameObjectsList = new();

    private List<Button> _fightControlButtonsList = new();

    private MonsterScriptableObject _activeMonster;

    private int _activeMonsterHealth;

    private FightStateItem _playerFightState;

    private FightStateItem _monsterFightState;

    private bool _isFightReallyStarted = false;

    private readonly float _monsterAwaitNextAttackTime = 3.0f;
    private float _monsterAwaitNextAttackTimer;

    private readonly float _monsterPrepareAttackTime = 1.0f;
    private float _monsterPrepareAttackTimer;

    private readonly float _monsterAttackTime = 1.0f;
    private float _monsterAttackTimer;    

    private readonly float _monsterDefendTime = 1.0f;
    private float _monsterDefendTimer; 

    private readonly float _monsterStunnedTime = 3.0f;
    private float _monsterStunnedTimer;        

    private readonly float _playerAttackTime = 1.0f;
    private float _playerAttackTimer;       

    private readonly float _playerDefendTime = 1.0f;
    private float _playerDefendTimer;       

    private void Awake()
    {
        Assert.IsNotNull(attackButtonGameObject);
        Assert.IsNotNull(defendButtonGameObject);
        Assert.IsNotNull(healButtonGameObject);
        Assert.IsNotNull(talkButtonGameObject);

        _fightControlButtonsGameObjectsList.Add(attackButtonGameObject);
        _fightControlButtonsGameObjectsList.Add(defendButtonGameObject);
        _fightControlButtonsGameObjectsList.Add(healButtonGameObject);
        _fightControlButtonsGameObjectsList.Add(talkButtonGameObject);

        _fightControlButtonsList = _fightControlButtonsGameObjectsList.Select(gameObject => gameObject.GetComponent<Button>()).ToList();
    }

    private void Update()
    {    
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MoveToSummonScene();
            }
        }

        if (_isFightReallyStarted)
        {
            HandlePlayerState();
            HandleMonsterState();
        }
    }

    private void HandlePlayerState()
    {
        _fightControlButtonsList.ForEach(button => button.interactable = _playerFightState == FightStateItem.Idle);

        if (_playerFightState == FightStateItem.Attack)
        {
            _playerAttackTimer -= Time.deltaTime;

            if (_playerAttackTimer <= 0)
            {
                _playerFightState = FightStateItem.Idle;
            }
        }
        else if (_playerFightState == FightStateItem.Defend)
        {
            _playerDefendTimer -= Time.deltaTime;

            if (_playerDefendTimer <= 0)
            {
                _playerFightState = FightStateItem.Idle;
            }
        }
    }

    private void HandleMonsterState()
    {
        _monsterHealthPanelController.SetHealth(_activeMonsterHealth);
        monsterStateText.SetText(_monsterFightState.ToShortString());

        if (_monsterFightState == FightStateItem.Idle)
        {
            _monsterAwaitNextAttackTimer -= Time.deltaTime;

            if (_monsterAwaitNextAttackTimer <= 0)
            {
                _monsterFightState = FightStateItem.PrepareAttack;
                _monsterPrepareAttackTimer = _monsterPrepareAttackTime;
            }
        }
        else if (_monsterFightState == FightStateItem.PrepareAttack)
        {
            _monsterPrepareAttackTimer -= Time.deltaTime;

            if (_monsterPrepareAttackTimer <= 0)
            {
                if (_playerFightState == FightStateItem.Defend)
                {
                    _monsterFightState = FightStateItem.Stunned;
                    _monsterStunnedTimer = _monsterStunnedTime;                    
                }
                else
                {
                    _monsterFightState = FightStateItem.Attack;
                    _monsterAttackTimer = _monsterAttackTime;
                }
            }    
        }
        else if (_monsterFightState == FightStateItem.Attack)
        {
            _monsterAttackTimer -= Time.deltaTime;

            if (_monsterAttackTimer <= 0)
            {
                _playerController.Damage(_activeMonster.damage);

                if (_playerController.Health == 0)
                {
                    MoveToSummonScene();
                    return;
                }

                _monsterFightState = FightStateItem.Idle;
                _monsterAwaitNextAttackTimer = _monsterAwaitNextAttackTime;
            }
        }
        else if (_monsterFightState == FightStateItem.Defend)
        {
            _monsterDefendTimer -= Time.deltaTime;

            if (_monsterDefendTimer <= 0)
            {
                _monsterFightState = FightStateItem.Idle;
            }
        }
        else if (_monsterFightState == FightStateItem.Stunned)
        {
            _monsterStunnedTimer -= Time.deltaTime;

            if (_monsterStunnedTimer <= 0)
            {
                _monsterFightState = FightStateItem.Idle; 
                _monsterAwaitNextAttackTimer = _monsterAwaitNextAttackTime;
            }
        }
    }

    public void OnAttackButtonClick()
    {
        if (_monsterFightState == FightStateItem.Idle)
        {
            _monsterFightState = FightStateItem.Defend;
            _monsterDefendTimer = _monsterDefendTime;
        }
        else if (_playerFightState == FightStateItem.Idle)
        {
            _playerFightState = FightStateItem.Attack;
            _playerAttackTimer = _playerAttackTime;

            if (_monsterFightState == FightStateItem.Stunned || _monsterFightState == FightStateItem.PrepareAttack)            
            {
                _activeMonsterHealth -= _playerController.AttackPower;

                if (_activeMonsterHealth <= 0) 
                {
                    foreach (var item in _activeMonster.itemsProduced)
                    {
                        _ingredientsItemsStorageController.AddItem(item.id);
                    }
                    
                    _isFightReallyStarted = false;

                    StartCoroutine(OnPlayerWinCoroutine());
                }
            }
        }
    }

    private IEnumerator OnPlayerWinCoroutine()
    {
        yield return dialogController.ShowDialogAfterFight(_activeMonster.id);

        MoveToSummonScene();
    }

    public void OnShieldButtonClick()
    {
        if (_playerFightState == FightStateItem.Idle)
        {
            _playerFightState = FightStateItem.Defend;
            _playerDefendTimer = _playerDefendTime;
        }
    }

    public void OnTalkButtonClick()
    {

    }

    public void OnHealButtonClick()
    {

    }

    private void MoveToSummonScene()
    {
        _isFightReallyStarted = false;

        gameObjectFight.SetActive(false);
        gameObjectSummon.SetActive(true);

        _activeMonster = null;
        _activeMonsterHealth = 0;

        _playerController.ResetHealth();
    }

    public void StartNewFight(MonsterScriptableObject monsterScriptableObject)
    {
        SetActiveMonster(monsterScriptableObject);

        _playerHealthPanelController.SetHealth(_playerController.Health);

        _playerFightState = FightStateItem.Idle;

        _isFightReallyStarted = false;

        StartCoroutine(StartReallyNewFight());
    }

    private IEnumerator StartReallyNewFight()
    {
        yield return dialogController.ShowDialogBeforeFight(_activeMonster.id);

        _isFightReallyStarted = true;
    }

    private void SetActiveMonster(MonsterScriptableObject monsterScriptableObject)
    {
        _activeMonster = monsterScriptableObject;
        _activeMonsterHealth = _activeMonster.health;

        _monsterHealthPanelController.SetHealth(_activeMonsterHealth);

        _monsterFightState = FightStateItem.Idle;

        _monsterAwaitNextAttackTimer = _monsterAwaitNextAttackTime;
    }
}
