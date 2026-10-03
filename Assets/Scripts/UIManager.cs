using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] List<StructureInfoUI> structureInfoElements;

    Structure[] structures;

    private void OnEnable()
    {
        MoneyTracker.OnMoneyChanged += UpdateMoneyText;
        StructureHandler.OnStructuresCreated += GetStructures;
        StructureHandler.OnStructuresTicked += UpdateStructureInfo;
    }

    private void OnDisable()
    {
        MoneyTracker.OnMoneyChanged -= UpdateMoneyText;
        StructureHandler.OnStructuresCreated -= GetStructures;
        StructureHandler.OnStructuresTicked -= UpdateStructureInfo;
    }

    private void Start()
    {
        UpdateMoneyText(0);
    }

    void UpdateMoneyText(BigInteger money)
    {
        moneyText.text = money.ToString();
    }

    void GetStructures(Structure[] structures)
    {
        this.structures = structures;
    }

    void UpdateStructureInfo()
    {
        for (int i = 0; i < structures.Length; i++)
        {
            structureInfoElements[i].UpdateInfo(structures[i]);
        }
    }
}
