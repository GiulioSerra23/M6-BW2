using System.Threading;
using UnityEngine;

public class PlayerMovementForward : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float _startSpeed = 10f;
    [SerializeField] private float _maxSpeed = 20f;
    [SerializeField] private float _increaseMaxSpeedAmount = 10f;
    [SerializeField] private float _acceleration = 0.2f;

    private float _currentSpeed;

    private void OnEnable()
    {
        if (TileSpawner.Instance != null) TileSpawner.Instance.OnZoneChanged += IncreaseMaxSpeed;
    }

    private void Start()
    {
        _currentSpeed = _startSpeed;
    }

    public float GetForwardSpeed()
    {
        return _currentSpeed;
    }

    public void IncreaseMaxSpeed()
    {
        _maxSpeed += _increaseMaxSpeedAmount; 
    }

    private void IncreaseSpeed()
    {
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, _maxSpeed, _acceleration * Time.deltaTime);
    }

    private void Update()
    {
        IncreaseSpeed();
    }

    private void OnDisable()
    {
        if (TileSpawner.Instance != null) TileSpawner.Instance.OnZoneChanged -= IncreaseMaxSpeed;        
    }
}

