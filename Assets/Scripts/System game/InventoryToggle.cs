using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.ToggleInventory.performed += OnTogglePerformed;
    }

    private void OnDisable()
    {
        _inputActions.Player.ToggleInventory.performed -= OnTogglePerformed;
        _inputActions.Player.Disable();
    }
    private void OnTogglePerformed(InputAction.CallbackContext context)
    {
        _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
    }

    private void Start()
    {
        _inventoryPanel.SetActive(false);
    }
}
