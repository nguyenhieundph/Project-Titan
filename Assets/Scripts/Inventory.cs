    using UnityEngine;
    using System.Collections.Generic;
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

        public void AddItem(Item item) 
        {
            if(item.isStackable == true)
            {
                foreach(var slot in _inventorySlots)
                {
                    if(slot.item == item && slot.quantity < item.maxStackSize)
                    {
                        slot.quantity += 1;
                        PrintInventory();
                        return;
                    }
                }
            }
            _inventorySlots.Add(new InventorySlot(item, 1));
            PrintInventory();
        }

        private void PrintInventory()
        {
            Debug.Log("Current Inventory:");
            foreach (var slot in _inventorySlots)
            {
                Debug.Log($"Item: {slot.item.itemName}, Quantity: {slot.quantity}");
            }
        }

        public List<SaveData.InventorySlotData> GetSaveData()
        {
            List<SaveData.InventorySlotData> result = new List<SaveData.InventorySlotData>();

            foreach (var slot in _inventorySlots)
            {
                SaveData.InventorySlotData slotData = new SaveData.InventorySlotData();
                slotData.itemId = slot.item.itemId;
                slotData.quantity = slot.quantity;
                result.Add(slotData);
            }
            return result;
        }

        public void LoadSaveData(List<SaveData.InventorySlotData> savedSlots, ItemDatabase database)
        {
            _inventorySlots.Clear();

            foreach (var slotData in savedSlots)
            {
                Item item = database.GetItemById(slotData.itemId);
                if(item != null)
            {
                _inventorySlots.Add(new InventorySlot(item, slotData.quantity));
            }

            }
        }
    }
