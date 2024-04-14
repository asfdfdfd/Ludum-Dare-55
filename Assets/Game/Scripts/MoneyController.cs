using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    private int _money;

    public int Money
    {
        get { return _money; }
    }

    public void EarnMoney(int value)
    {
        _money += value;
    }

    public void SpendMoney(int value)
    {
        _money -= value;
    }    
}
