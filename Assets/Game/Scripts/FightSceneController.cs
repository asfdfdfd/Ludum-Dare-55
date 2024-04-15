using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightSceneController : MonoBehaviour
{
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

    [SerializeField]
    private GameObject monsterSpawnPoint;

    [SerializeField]
    private GameObject gameObjectFightLose;

    [SerializeField]
    private Jukebox jukebox;

    private readonly List<GameObject> _fightControlButtonsGameObjectsList = new();

    private List<Button> _fightControlButtonsList = new();

    private MonsterScriptableObject _activeMonster;

    private GameObject _activeMonsterGameObject;

    private MonsterControllerBase _monsterController;

    private FightStateItem _playerFightState;

    private bool _isFightReallyStarted = false;

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
        _monsterHealthPanelController.SetHealth(_monsterController.Health);
    }

    public void OnAttackButtonClick()
    {
        if (_playerFightState == FightStateItem.Idle)
        {
            _playerFightState = FightStateItem.Attack;
            _playerAttackTimer = _playerAttackTime;

            _monsterController.Damage(_playerController.AttackPower);

            if (_monsterController.Health <= 0) 
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
        if (_activeMonster.id == "SlimeMonster")
        {
            SceneManager.LoadScene("Game/Scenes/FinishScene");
            
            return;
        }

        _isFightReallyStarted = false;

        jukebox.PlaySummonMusic();

        gameObjectFight.SetActive(false);
        gameObjectSummon.SetActive(true);

        _activeMonster = null;

        Destroy(_activeMonsterGameObject);
        _activeMonsterGameObject = null;

        _playerController.ResetHealth();
    }

    private void MoveToGameLoseScene()
    {
        _isFightReallyStarted = false;

        gameObjectFight.SetActive(false);
        gameObjectFightLose.SetActive(true);

        _activeMonster = null;

        Destroy(_activeMonsterGameObject);
        _activeMonsterGameObject = null;

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

        _monsterController.ReallyStart();
    }

    private void SetActiveMonster(MonsterScriptableObject monsterScriptableObject)
    {
        _activeMonster = monsterScriptableObject;

        _activeMonsterGameObject = Instantiate(_activeMonster.prefab, monsterSpawnPoint.transform);
        _monsterController = _activeMonsterGameObject.GetComponent<MonsterControllerBase>();
        _monsterController.Setup(_activeMonster.health);

        _monsterController.onFightStateChanged.AddListener((fightState) => 
        {
            monsterStateText.SetText(fightState.ToShortString());

            if (fightState == FightStateItem.Attack)
            {
                if (_playerFightState == FightStateItem.Defend)
                {
                    _monsterController.Stun();
                }
                else
                {
                    DamagePlayer();
                }
            }
        });

        monsterStateText.SetText(_monsterController.FightState.ToShortString());

        _monsterHealthPanelController.SetHealth(_monsterController.Health);
    }

    private void DamagePlayer()
    {
        _playerController.Damage(_activeMonster.damage);

        if (_playerController.Health == 0)
        {
            MoveToGameLoseScene();
            return;
        }        
    }
}
