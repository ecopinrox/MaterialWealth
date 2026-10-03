using System;
using System.Numerics;
using UnityEngine;

public class MoneyTracker : MonoBehaviour
{
    public BigInteger Money { get; private set; } = 0;

    public static event Action<BigInteger> OnMoneyChanged;

    private void OnEnable()
    {
        InputHandler.OnMakeMoney += IncrementMoney;
        MoneyDecayManager.OnMoneyDecay += SubtractMoney;
        StructureHandler.OnMoneyGained += AddMoney;
    }

    private void OnDisable()
    {
        InputHandler.OnMakeMoney -= IncrementMoney;
        MoneyDecayManager.OnMoneyDecay -= SubtractMoney;
        StructureHandler.OnMoneyGained -= AddMoney;
    }

    void IncrementMoney()
    {
        AddMoney(1);
    }

    void AddMoney(int money)
    {
        Money += money;
        OnMoneyChanged?.Invoke(Money);
    }

    void SubtractMoney(int money)
    {
        Money -= money;
        OnMoneyChanged?.Invoke(Money);
    }

    public bool TrySubtractMoney(int money)
    {
        if(Money < money)
        {
            return false;
        }

        SubtractMoney(money);
        return true;
    }
}
