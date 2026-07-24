using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float _detectRange = 10f;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Transform _eyePoint;

    private Transform _playerTransform;

    public bool CanSeePlayer { get; private set; }
    public Transform PlayerTransform => _playerTransform;

    void Update()
    {
        CanSeePlayer = CheckDetection();
    }

    private bool CheckDetection()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _detectRange, _playerLayer);

        if (hits.Length == 0)
        {
            return false;
        }
        else
        {
            _playerTransform = hits[0].transform;
            Vector3 directionToPlayer = (_playerTransform.position - _eyePoint.position).normalized;
            float distanceToPlayer = (_playerTransform.position - _eyePoint.position).magnitude;

            if (Physics.Raycast(_eyePoint.position, directionToPlayer, out RaycastHit hit, distanceToPlayer, ~0, QueryTriggerInteraction.Ignore))
            {
                return hit.transform == _playerTransform;
            }
            return false;
        }
    }
}