using System.Threading;
using UnityEngine;

public class PlayerMovementForward : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float _startSpeed = 10f;
    [SerializeField] private float _maxSpeed = 20f;
    [SerializeField] private float _increseMaxSpeedMultiplier = 1.2f;
    [SerializeField] private float _acceleration = 0.2f;

    private float _currentSpeed;

    private void OnEnable()
    {
        if (TileSpawner.Instance != null) TileSpawner.Instance.OnZoneChanged += IncreseMaxSpeed;
    }

    private void Start()
    {
        _currentSpeed = _startSpeed;
    }

    public float GetForwardSpeed()
    {
        return _currentSpeed;
    }

    public void IncreseMaxSpeed()
    {
        _maxSpeed *= _increseMaxSpeedMultiplier; 
    }

    private void IncreseSpeed()
    {
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, _maxSpeed, _acceleration * Time.deltaTime);
    }

    private void Update()
    {
        IncreseSpeed();
    }

    private void OnDisable()
    {
        if (TileSpawner.Instance != null) TileSpawner.Instance.OnZoneChanged -= IncreseMaxSpeed;        
    }
}

