using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float _maxHealth = 100f;

    public float CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    public event Action OnDeath;
    public event Action<float> OnDamaged;

    public float MaxHealth => _maxHealth;
    void Awake()
    {
        CurrentHealth = _maxHealth;
        IsDead = false;
    }

    public void TakeDamage(float amount)
    {
        if(IsDead == true)
        {
            return;
        }

        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
      
        OnDamaged?.Invoke(amount);

        if (CurrentHealth <= 0) { 
            IsDead = true;
            OnDeath?.Invoke();
        }
    }

    public float GetSaveData()
    {
        return CurrentHealth;
    }

    public void LoadSaveData(float savedHealth)
    {
        CurrentHealth = savedHealth;
    }
}
