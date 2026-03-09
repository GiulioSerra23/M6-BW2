// PlayerMovementForward.cs — nessun cambiamento logico, aggiunto RequireComponent
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementForward : MonoBehaviour
{
    [Header("Forward Settings")]
    [SerializeField] private float _forwardSpeed = 10f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetSpeed(float speed)
    {
        _forwardSpeed = Mathf.Max(0f, speed); // Guard: no velocità negative
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _forwardSpeed);
    }
}

