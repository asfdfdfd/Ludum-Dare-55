using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterAnimatedAnimationEventsController : MonoBehaviour
{
    public UnityEvent onIdleStarted;
    public UnityEvent onAttackStarted;
    public UnityEvent onAttackFinished;

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
}
