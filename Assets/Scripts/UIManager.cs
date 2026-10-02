using System.Numerics;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        MoneyTracker.OnMoneyChanged += UpdateMoneyText;
    }

    private void OnDisable()
    {
        MoneyTracker.OnMoneyChanged -= UpdateMoneyText;
    }

    private void Start()
    {
        UpdateMoneyText(0);
    }

    void UpdateMoneyText(BigInteger money)
    {
        moneyText.text = money.ToString();
    }
}
