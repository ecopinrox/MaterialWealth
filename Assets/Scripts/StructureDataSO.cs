using UnityEngine;

[CreateAssetMenu(fileName = "StructureDataSO", menuName = "Scriptable Objects/StructureDataSO")]
public class StructureDataSO : ScriptableObject
{
    public float[] sectionThresholdArray;
    public int[] moneyGainArray;
    public float moneyGainInterval = 3f;
    public float lifetime = 12f;
    public int investmentCost = 5;
    public float investmentLifeBoost = 0.2f;
}
