using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoneyTracker : MonoBehaviour
{
    public BigInteger Money { get; private set; } = 0;

    public static event Action<BigInteger> OnMoneyChanged;
    public static event Action<StructureDataSO> OnStructureBought;

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

    public void BuyStructure(StructureDataSO structureData)
    {
        if(Money < structureData.cost)
        {
            return;
        }

        Debug.Log("Bought structure");
        SubtractMoney(structureData.cost);
        OnStructureBought(structureData);
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
}
