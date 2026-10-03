using UnityEngine;
using System;

public class Structure
{
    public readonly float[] sectionThresholdArray;
    public readonly int[] moneyGainArray;
    public readonly float moneyGainInterval;
    public readonly float lifetime;
    public readonly int investmentCost;
    public readonly float investmentLifeBoostFraction;

    float currentLife;

    public float TimeTillMoneyGain { get; private set; }
    float LifeFraction { get { return currentLife / lifetime; } }

    public event Action<int> OnMoneyGained;
    public event Action<float> OnLifetimeChanged;

    public Structure(StructureDataSO structureDataSO)
    {
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
        TimeTillMoneyGain -= deltaTime;
        if(TimeTillMoneyGain <= 0)
        {
            TimeTillMoneyGain = moneyGainInterval;

            int section = GetCurrentSection();
            int gain = (section < 0) ? 0 : moneyGainArray[section];

            OnMoneyGained?.Invoke(gain);
        }
    }

    int GetCurrentSection()
    {
        int section;
        for(section = 0; section < sectionThresholdArray.Length; section++)
        {
            if (LifeFraction <= sectionThresholdArray[section]) break;
        }

        return section - 1;
    }
}

