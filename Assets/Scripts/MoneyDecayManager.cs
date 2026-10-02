using UnityEngine;
using System;

public class MoneyDecayManager : MonoBehaviour
{
    public static event Action<int> OnMoneyDecay;

    [SerializeField] float decayInterval = 1;
    [SerializeField] int decayAmount = 5;

    private void Start()
    {
        _ = DecayMoney();
    }

    async Awaitable DecayMoney()
    {
        while(true)
        {
            await Awaitable.WaitForSecondsAsync(decayInterval);
            OnMoneyDecay?.Invoke(decayAmount);
        }
    }
}
