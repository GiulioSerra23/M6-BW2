using UnityEngine;

public class PlayerLaneMovement : MonoBehaviour
{
    [SerializeField] private float _laneDistance = 2f;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _laneCount = 3;

    private int _currentLane = 1;
    private Vector3 _targetPosition;


    private void Start()
    {
        _targetPosition = transform.position;
    }

    private void ChangeLane(int direction)
    {
        int newLane = Mathf.Clamp(_currentLane + direction, 0, _laneCount - 1);

        if (newLane != _currentLane)
        {
            _currentLane = newLane;
            _targetPosition = new Vector3((_currentLane - 1) * _laneDistance, transform.position.y, transform.position.z);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane(1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeLane(-1);
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
    }
}
