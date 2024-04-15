using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterAnimatedAnimationEventsController : MonoBehaviour
{
    public UnityEvent onIdleStarted;
    public UnityEvent onAttackStarted;
    public UnityEvent onAttackFinished;
    public UnityEvent onDefendStarted;
    public UnityEvent onDefendFinished;

    public void OnIdleStarted()
    {
        onIdleStarted.Invoke();
    }

    public void OnAttackStarted()
    {
        onAttackStarted.Invoke();
    }

    public void OnAttackFinished()
    {
        onAttackFinished.Invoke();
    }    

    public void OnDefendStarted()
    {
        onDefendStarted.Invoke();
    }

    public void OnDefendFinished()
    {
        onDefendFinished.Invoke();
    }
}
