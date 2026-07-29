using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private Transform _slotContainer;      // chính là InventoryPanel
    [SerializeField] private GameObject _slotPrefab;         // prefab InventorySlotUI

    private List<GameObject> _spawnedSlots = new List<GameObject>();

    private void OnEnable()
    {
        _playerInventory.OnInventoryChanged += RefreshUI;
    }

    private void OnDisable()
    {
        _playerInventory.OnInventoryChanged -= RefreshUI;
    }

    private void Start()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        // Bước 1: xoá sạch mọi slot UI cũ đang hiển thị
        foreach (var slotObj in _spawnedSlots)
        {
            Destroy(slotObj);
        }
        _spawnedSlots.Clear();

        // Bước 2: tạo lại từ đầu, đúng theo dữ liệu Inventory hiện tại
        foreach (var slot in _playerInventory.GetAllSlots())
        {
            GameObject newSlotObj = Instantiate(_slotPrefab, _slotContainer);
            InventorySlotUI slotUI = newSlotObj.GetComponent<InventorySlotUI>();
            slotUI.Setup(slot);
            _spawnedSlots.Add(newSlotObj);
        }
    }
}
