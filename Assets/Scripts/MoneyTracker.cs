using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoneyTracker : MonoBehaviour
{
    public BigInteger Money { get; private set; } = 0;

    private void OnEnable()
    {
        InputHandler.OnMakeMoney += IncrementMoney;
    }

    private void OnDisable()
    {
        InputHandler.OnMakeMoney -= IncrementMoney;
    }

    void IncrementMoney()
    {
        AddMoney(1);
    }

    void AddMoney(int money)
    {
        Money += money;
        Debug.Log("Money = " + Money);
    }
}
