using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    [Header("Quest Info")]
    public string questName;
    [TextArea] public string description;

    [Header("Objective")]
    public int requiredKillCount;

    [Header("Rewards")]
    public Item rewardItem;
    public int rewardQuantity;
}
