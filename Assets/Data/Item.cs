using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [Header("Info")]
    public string itemId;
    public string itemName;
    public string description;
    public Sprite icon;

    [Header("Stacking")]
    public bool isStackable = true;
    public int maxStackSize = 99;
}
