using UnityEngine;
using System;

public class Structure
{
    public readonly float moneyGainInterval;
    public readonly int moneyGainAmount;
    public readonly float lifetime;

    public float CurrentLife { get; private set; }
    public float TimeTillMoneyGain { get; private set; }

    public event Action<int> OnMoneyGained;
    public event Action<Structure> OnDestroyed;

    public Structure(StructureDataSO structureDataSO)
    {
        moneyGainInterval = structureDataSO.moneyGainInterval;
        moneyGainAmount = structureDataSO.moneyGainAmount;
        lifetime = structureDataSO.lifetime;
        CurrentLife = lifetime;
    }

    public void Tick(float deltaTime)
    {
        TickLife(deltaTime);
        TickMoney(deltaTime);
    }

    void TickLife(float deltaTime)
    {
        CurrentLife -= deltaTime;
        if (CurrentLife <= 0)
        {
            CurrentLife = 0;
            OnDestroyed?.Invoke(this);
        }
    }

    void TickMoney(float deltaTime)
    {
        TimeTillMoneyGain -= deltaTime;
        if(TimeTillMoneyGain <= 0)
        {
            TimeTillMoneyGain = moneyGainInterval;
            OnMoneyGained?.Invoke(moneyGainAmount);
        }
    }
}

