// PlayerLaneMovement.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerLaneMovement : MonoBehaviour
{
    [SerializeField] private float _laneDistance = 2f;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _laneCount = 3;

    private Rigidbody _rb;
    private int _currentLane = 1;
    private float _targetX; // Solo X: non congelare Y e Z

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _targetX = transform.position.x;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) ChangeLane(1);
        else if (Input.GetKeyDown(KeyCode.A)) ChangeLane(-1);
    }

    private void FixedUpdate()
    {
        // MovePosition rispetta il Rigidbody e il motore fisico
        float newX = Mathf.MoveTowards(transform.position.x, _targetX, _moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(new Vector3(newX, _rb.position.y, _rb.position.z));
    }

    private void ChangeLane(int direction)
    {
        int newLane = Mathf.Clamp(_currentLane + direction, 0, _laneCount - 1);
        if (newLane == _currentLane) return;

        _currentLane = newLane;
        _targetX = (_currentLane - 1) * _laneDistance;
    }
}
