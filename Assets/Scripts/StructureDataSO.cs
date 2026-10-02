using UnityEngine;

[CreateAssetMenu(fileName = "StructureDataSO", menuName = "Scriptable Objects/StructureDataSO")]
public class StructureDataSO : ScriptableObject
{
    public int cost = 20;
    public float moneyGainInterval = 3f;
    public int moneyGainAmount = 8;
    public float lifetime = 12f;
}
