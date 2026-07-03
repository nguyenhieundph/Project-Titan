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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       CanSeePlayer = CheckDetection();
    }

    private bool CheckDetection()
    {
      
        Collider[] hits = Physics.OverlapSphere(transform.position, _detectRange, _playerLayer);

        if(hits.Length == null)
        {
            return false;
        }
        else
        {
            _playerTransform = hits[0].transform;
        }
    }
}