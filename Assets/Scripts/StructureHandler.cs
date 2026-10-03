using System.Collections.Generic;
using System;
using UnityEngine;

public class StructureHandler : MonoBehaviour
{
    [SerializeField] StructureDataSO[] structureTypes;
    public Structure[] structures;

    public static event Action<int> OnMoneyGained;
    public static event Action<Structure[]> OnStructuresCreated;
    public static event Action OnStructuresTicked;

    MoneyTracker moneyTracker;

    private void Awake()
    {
        moneyTracker = GetComponent<MoneyTracker>();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
    {
        structures = new Structure[structureTypes.Length];
        for (int i = 0; i < structures.Length; i++)
        {
            structures[i] = new(structureTypes[i]);

            structures[i].OnMoneyGained += RegisterMoneyGained;
            structures[i].OnLifetimeChanged += (lifetime) => UpdateStructureLifetime(i, lifetime);
        }

        OnStructuresCreated(structures);
    }

    private void Update()
    {
        foreach(Structure structure in structures)
        {
            structure.Tick(Time.deltaTime);
        }

        OnStructuresTicked?.Invoke();
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

    }
}
