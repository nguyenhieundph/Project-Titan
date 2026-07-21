using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackDamage = 20f;
    [SerializeField] private LayerMask _enemyLayer;

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.Attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Attack.performed -= OnAttackPerformed;
        _inputActions.Disable();
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        PerformAttack();
    }
    private void PerformAttack()
    {        
        Vector3 attackCenter = transform.position + transform.forward * _attackRange;
        Collider[] hits = Physics.OverlapSphere(attackCenter, _attackRange, _enemyLayer);
       
        if (hits.Length == 0) return;
        
        Collider nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        for(int i = 0; i < hits.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, hits[i].transform.position);
            if (distance < nearestDistance) { 
                nearestDistance = distance;
                nearestEnemy = hits[i];
            }
        }
       
        Health enemyHealth = nearestEnemy.GetComponent<Health>();
        if(enemyHealth != null){
            enemyHealth.TakeDamage(_attackDamage);
        }

        Debug.Log(enemyHealth.CurrentHealth);
    }
}
