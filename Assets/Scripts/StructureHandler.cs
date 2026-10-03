using System.Collections.Generic;
using System;
using UnityEngine;

public class StructureHandler : MonoBehaviour
{
    [SerializeField] StructureDataSO[] structureTypes;
    public Structure[] structures;

    public static event Action<int> OnMoneyGained;
    public static event Action<int, float> OnStructureLifetimeUpdated;

    MoneyTracker moneyTracker;

    private void Awake()
    {
        moneyTracker = GetComponent<MoneyTracker>();

        structures = new Structure[structureTypes.Length];
        for (int i = 0; i < structures.Length; i++)
        {
            structures[i] = new(structureTypes[i]);

            structures[i].OnMoneyGained += RegisterMoneyGained;
            structures[i].OnLifetimeChanged += (lifetime) => UpdateStructureLifetime(i, lifetime);
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Update()
    {
        foreach(Structure structure in structures)
        {
            structure.Tick(Time.deltaTime);
        }
    }

    public void TryInvestInStructure(int structureIndex)
    {
        Structure structure = structures[structureIndex];

        if(!moneyTracker.TrySubtractMoney(structure.investmentCost))
        {
            return;
        }

        structure.Invest();
    }

    public void RegisterMoneyGained(int money)
    {
        Debug.Log($"Earned {money} from structure");
        OnMoneyGained?.Invoke(money);
    }

    public void UpdateStructureLifetime(int index, float lifetime)
    {
        Debug.Log($"Structure {index} lifetime fraction is {lifetime}");
        OnStructureLifetimeUpdated?.Invoke(index, lifetime);
    }
}
