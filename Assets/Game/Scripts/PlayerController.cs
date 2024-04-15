using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _maxHealth = 3;

    private int _health;

    public int AttackPower = 1;

    private void Awake()
    {
        ResetHealth();
    }

    public int Health
    {
        get { return _health; }
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        _health = _maxHealth;
    }

    public void ResetHealth()
    {
        _health = _maxHealth;
    }

    public void Damage(int value)
    {
        _health -= value;

        if (_health < 0)
        {
            _health = 0;
        }
    }
}
