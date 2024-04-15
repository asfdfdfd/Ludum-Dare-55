using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class MonsterControllerBase : MonoBehaviour
{
    public UnityEvent<FightStateItem> onFightStateChanged;

    private FightStateItem _fightState = FightStateItem.Idle;

    public FightStateItem FightState
    {
        get
        {
            return _fightState;
        }
    }

    private int _health = -1;

    public int Health
    {
        get
        {
            return _health;
        }
    }

    private readonly float _monsterIdleTime = 3.0f;
    private float _monsterIdleTimer = 0.0f;

    public void Setup(int health)
    {
        _health = health;
    }

    private void SetState(FightStateItem fightState)
    {
        if (fightState == _fightState)
        {
            return;
        }

        if (fightState == FightStateItem.Idle)
        {        
            _monsterIdleTimer = _monsterIdleTime;
        }

        _fightState = fightState;
        onFightStateChanged.Invoke(fightState);
    }

    protected void Awake() 
    {
        _monsterIdleTimer = _monsterIdleTime;
    }

    public void OnIdleStarted()
    {
        SetState(FightStateItem.Idle);
    }

    public void OnAttackStarted()
    {
        SetState(FightStateItem.Attack);
    }

    public void OnAttackFinished()
    {
        SetState(FightStateItem.Idle);
    }        

    protected void Update()
    {
        if (_fightState == FightStateItem.Idle)
        {
            _monsterIdleTimer -= Time.deltaTime;

            if (_monsterIdleTimer <= 0)
            {
                _fightState = FightStateItem.PrepareAttack;

                _monsterIdleTimer = 0.0f;

                Attack();
            }
        }
    }

    public void Damage(int attackPower)
    {
        if (_fightState == FightStateItem.Idle)
        {
            SetState(FightStateItem.Defend);
            Defend();
        }
        else if (_fightState == FightStateItem.Stunned)
        {
            _health -= attackPower;
        }
    }

    public abstract void Attack();

    public abstract void Defend();

    public virtual void Stun()
    {
        SetState(FightStateItem.Stunned);
    }
}
