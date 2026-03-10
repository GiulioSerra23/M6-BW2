using UnityEngine;

public class PlayerLaneMovement : PlayerController
{
    [SerializeField] private float _laneDistance = 2f;
    [SerializeField] private float _laneChangeSpeed = 10f;
    [SerializeField] private int _laneCount = 3;

    private int _currentLane;
    private float _targetX;

    private void Start()
    {
        _currentLane = _laneCount / 2;
        _targetX = 0f;
    }

    private void ChangeLane(int direction)
    {
        _currentLane = Mathf.Clamp(_currentLane + direction, 0, _laneCount - 1);

        _targetX = (_currentLane - (_laneCount / 2)) * _laneDistance;
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
    }

    private void FixedUpdate()
    {
        Vector3 position = _rb.position;

        position.x = Mathf.Lerp(position.x, _targetX, _laneChangeSpeed * Time.fixedDeltaTime);

        _rb.MovePosition(position);
    }
}
