using UnityEngine;
using System;
using UnityEngine.Rendering;

public class Structure
{
    public readonly string name;
    public readonly float[] sectionThresholdArray;
    public readonly int[] moneyGainArray;
    public readonly float moneyGainInterval;
    public readonly float lifetime;
    public readonly int investmentCost;
    public readonly float investmentLifeBoostFraction;

    float currentLife;
    float timeTillMoneyGain;

    public float LifeFraction { get { return currentLife / lifetime; } }
    public float MoneyGainIntervalFraction { get { return timeTillMoneyGain / moneyGainInterval; } } 

    public event Action<int> OnMoneyGained;
    public event Action<float> OnLifetimeChanged;

    public Structure(StructureDataSO structureDataSO)
    {
        name = structureDataSO.structureName;
        sectionThresholdArray = structureDataSO.sectionThresholdArray;
        moneyGainArray = structureDataSO.moneyGainArray;
        moneyGainInterval = structureDataSO.moneyGainInterval;
        lifetime = structureDataSO.lifetime;
        investmentCost = structureDataSO.investmentCost;
        investmentLifeBoostFraction = structureDataSO.investmentLifeBoost;

        currentLife = 0;
    }

    public void Tick(float deltaTime)
    {
        TickLife(deltaTime);
        TickMoney(deltaTime);
    }

    public void Invest()
    {
        currentLife = Math.Clamp(
            currentLife + investmentLifeBoostFraction * lifetime, 
            0, 
            lifetime
        );

        OnLifetimeChanged?.Invoke(LifeFraction);
    }

    void TickLife(float deltaTime)
    {
        currentLife -= deltaTime;
        if (currentLife <= 0)
        {
            currentLife = 0;
        }

        OnLifetimeChanged?.Invoke(LifeFraction);
    }

    void TickMoney(float deltaTime)
    {
        timeTillMoneyGain -= deltaTime;
        if(timeTillMoneyGain <= 0)
        {
            timeTillMoneyGain = moneyGainInterval;

            int section = GetCurrentSection();
            int gain = (section < 0) ? 0 : moneyGainArray[section];

            OnMoneyGained?.Invoke(gain);
        }
    }

    public int GetCurrentSection()
    {
        int section;
        for(section = 0; section < sectionThresholdArray.Length; section++)
        {
            if (LifeFraction <= sectionThresholdArray[section]) break;
        }

        return section - 1;
    }

    public int GetMoneyGain()
    {
        int section = GetCurrentSection();
        if (section < 0) return 0;
        return moneyGainArray[section];
    }
}

