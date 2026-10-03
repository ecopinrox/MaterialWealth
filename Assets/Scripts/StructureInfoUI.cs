using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureInfoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Slider lifeSlider;
    [SerializeField] TextMeshProUGUI gainText;
    [SerializeField] Image gainIntervalFill;
    [SerializeField] TextMeshProUGUI investCostText;
    [SerializeField] TextMeshProUGUI levelText;

    public void UpdateInfo(Structure structure)
    {
        SetName(structure.name);
        UpdateLifeSlider(structure.LifeFraction);
        UpdateGainText(structure.GetMoneyGain());
        UpdateIntervalFill(structure.MoneyGainIntervalFraction);
        SetInvestCostText(structure.investmentCost);
    }

    void SetName(string name)
    {
        nameText.text = name;
    }

    void UpdateLifeSlider(float value)
    {
        lifeSlider.value = value;
    }

    void UpdateGainText(int gain)
    {
        gainText.text = gain.ToString();
    }

    void UpdateIntervalFill(float value)
    {
        gainIntervalFill.fillAmount = value;
    }

    void SetInvestCostText(int cost)
    {
        investCostText.text = $"Invest ({cost})";
    }
}

