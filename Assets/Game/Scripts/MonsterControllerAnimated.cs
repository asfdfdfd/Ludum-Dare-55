using UnityEngine;

public class MonsterControllerAnimated : MonsterControllerBase
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private MonsterAnimatedAnimationEventsController _animationEvents;

    override public void Attack() 
    {
        _animator.SetTrigger("TriggerAttack");
    }

    override public void Defend() 
    {
        _animator.SetTrigger("TriggerBlock");
    }    

    override public void Stun() 
    {
        base.Stun();
        
        _animator.SetTrigger("TriggerStun");
    }       
}
