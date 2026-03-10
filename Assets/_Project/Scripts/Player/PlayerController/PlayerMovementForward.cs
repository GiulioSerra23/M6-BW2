using UnityEngine;

public class PlayerMovementForward : PlayerController
{
    [Header("Speed Settings")]
    [SerializeField] private float _startSpeed = 10f;
    [SerializeField] private float _maxSpeed = 20f;
    [SerializeField] private float _acceleration = 0.5f;

    private float _currentSpeed;

    private void Start()
    {
        _currentSpeed = _startSpeed;
    }

    private void IncreseSpeed()
    {
        _currentSpeed = Mathf.MoveTowards(_currentSpeed,_maxSpeed, _acceleration * Time.fixedDeltaTime);
    }

    private void MoveForward()
    {
        Vector3 velocity = _rb.velocity;

        velocity.x = 0f;
        velocity.z = _currentSpeed;

        _rb.velocity = velocity;
    }

    private void FixedUpdate()
    {
        IncreseSpeed();
        MoveForward();
    }
}

