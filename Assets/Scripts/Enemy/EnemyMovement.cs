using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _repathThreshold = 0.5f; // khoảng cách tối thiểu để gọi lại SetDestination

    private NavMeshAgent _agent;
    private EnemyDetection _detection;
    private Vector3 _lastKnownPlayerPosition;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _detection = GetComponent<EnemyDetection>();
    }
    // Update is called once per frame
    void Update()
    {
       
        if (_detection.CanSeePlayer == false)
        {
            StopMoving();

            return;
        }
        else
        {
            Vector3 currentPlayerPos = _detection.PlayerTransform.position;
            float distanceMoved = Vector3.Distance(_lastKnownPlayerPosition,currentPlayerPos);

            if(distanceMoved > _repathThreshold)
            {
                _agent.SetDestination(currentPlayerPos);
                _lastKnownPlayerPosition = currentPlayerPos;
            }
        }
    }

    private void StopMoving()
    {
        _agent.SetDestination(transform.position);
    }
}
