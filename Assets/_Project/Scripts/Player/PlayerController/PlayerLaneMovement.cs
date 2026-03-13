using UnityEngine;

public class PlayerLaneMovement : MonoBehaviour
{
    [Header ("Lane Settings")]
    [SerializeField] private float _laneDistance = 6f;
    [SerializeField] private float _laneChangeSpeed = 30f;
    [SerializeField] private int _laneCount = 3;

    [Header ("Raycast Settings")]
    [SerializeField] private float _raycastHeight = 0.5f;
    [SerializeField] private LayerMask _obstacleMask;

    private PlayerMovementForward _movementForward;
    private int _currentLane;
    private float _targetX;

    private void Awake()
    {
        _movementForward = GetComponent<PlayerMovementForward>();
    }

    private void Start()
    {
        _currentLane = _laneCount / 2;
        _targetX = 0f;
    }

    private bool CanChangeLane(int direction)
    {
        int newLane = Mathf.Clamp(_currentLane + direction, 0, _laneCount - 1);
        float targetX = (newLane - (_laneCount / 2)) * _laneDistance;

        Vector3 origin = transform.position + Vector3.up * _raycastHeight;

        origin += transform.forward * (_movementForward.GetForwardSpeed() * 0.1f);

        Vector3 directionVector = new Vector3(targetX - transform.position.x, 0, 0).normalized;
        float distance = Mathf.Abs(targetX - transform.position.x) + _laneDistance * 0.2f;

        Debug.DrawRay(origin, directionVector);
        return !Physics.Raycast(origin, directionVector, distance, _obstacleMask);
    }


    private void ChangeLane(int direction)
    {
        _currentLane = Mathf.Clamp(_currentLane + direction, 0, _laneCount - 1);

        _targetX = (_currentLane - (_laneCount / 2)) * _laneDistance;
    }

    public float GetMovementX(float currentX)
    {
        float difference = _targetX - currentX;

        float movement = Mathf.Clamp(difference, -_laneChangeSpeed * Time.deltaTime, _laneChangeSpeed * Time.deltaTime);
        return movement;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && CanChangeLane(1))
        {
            ChangeLane(1);
        }
        else if (Input.GetKeyDown(KeyCode.A) && CanChangeLane(-1))
        {
            ChangeLane(-1);
        }        
    }
}
