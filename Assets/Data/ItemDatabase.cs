using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> AllItems = new List<Item>();

    public Item GetItemById(string itemId)
    {
        foreach (var item in AllItems)
        {
            if (item.itemId == itemId)
            {
                return item;
            }
        }
        return null;
    }

}
