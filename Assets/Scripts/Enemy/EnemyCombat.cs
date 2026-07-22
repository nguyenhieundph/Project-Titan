using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private float _attackRange = 2f; //nên >= _stopDistance của EnemyMovement
    [SerializeField] private float _attackDamage = 10f;
    [SerializeField] private float _attackCooldown = 1f;

    private EnemyDetection _detection;
    private Health _playerHealth;
    private float _cooldownTimer;

    private void Awake()
    {
        _detection = GetComponent<EnemyDetection>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        if(_cooldownTimer > 0) 
        {
            _cooldownTimer -= deltaTime;

            if( _cooldownTimer < 0)
            {
                _cooldownTimer = 0;
            }
        }

        if (_detection.CanSeePlayer == false)
        {
            return;
        }


        if (_playerHealth == null)
        {
            _playerHealth = _detection.PlayerTransform.GetComponent<Health>();
        }

        if (_playerHealth == null || _playerHealth.IsDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _detection.PlayerTransform.position);
        if (distanceToPlayer > _attackRange) 
        {
            return;
        }

        if(_cooldownTimer > 0)
        {
            return;
        }

        _playerHealth.TakeDamage(_attackDamage);
        _cooldownTimer = _attackCooldown;
    }
}
