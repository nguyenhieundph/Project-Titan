using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _quantityText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Setup(InventorySlot slot)
    {
        _iconImage.sprite = slot.item.icon;
        _quantityText.text = slot.quantity.ToString();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
