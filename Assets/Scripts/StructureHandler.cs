using System.Collections.Generic;
using System;
using UnityEngine;

public class StructureHandler : MonoBehaviour
{
    public List<Structure> activeStructures = new();

    public static event Action<int> OnMoneyGained;

    private void OnEnable()
    {
        MoneyTracker.OnStructureBought += AddStructure;
    }

    private void OnDisable()
    {
        MoneyTracker.OnStructureBought -= AddStructure;
    }

    private void Update()
    {
        for(int i = 0; i < activeStructures.Count; i++)
        {
                activeStructures[i].Tick(Time.deltaTime);
        }
    }

    void AddStructure(StructureDataSO structureData)
    {
        Structure newStructure = new(structureData);

        newStructure.OnDestroyed += RemoveStructure;
        newStructure.OnMoneyGained += RegisterMoneyGained;

        activeStructures.Add(newStructure);
    }

    public void RemoveStructure(Structure structure) 
    {
        Debug.Log($"Removing structure");
        activeStructures.Remove(structure);
    }

    public void RegisterMoneyGained(int money)
    {
        Debug.Log($"Earned {money} from structure");
        OnMoneyGained?.Invoke(money);
    }
}
