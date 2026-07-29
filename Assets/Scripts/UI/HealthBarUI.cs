using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _playerHealth.OnDamaged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _playerHealth.OnDamaged -= UpdateHealthBar;
    }

    private void Start()
    {
        UpdateHealthBar(0);
    }

    private void UpdateHealthBar(float damageAmount)
    {
        _slider.value = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
    }
}
