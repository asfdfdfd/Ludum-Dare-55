using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private int _health = 3;

    public int Health
    {
        get { return _health; }
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
