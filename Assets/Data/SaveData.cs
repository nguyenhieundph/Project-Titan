using UnityEngine;
using System;
using System.Collections.Generic;
[Serializable]
public class SaveData
{
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    public float playerHealth;

    public List<InventorySlotData> inventorySlotDatas = new List<InventorySlotData>();

    public string activeQuestId;
    public int QuestKillCount;
    public bool isQuestCompleted;

    [Serializable]
    public class InventorySlotData
    {
        public string itemId;
        public int quantity;
    }

}
