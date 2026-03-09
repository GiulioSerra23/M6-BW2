using UnityEngine;

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
        _forwardSpeed = speed;
    }

    private void FixedUpdate()
    {
        Vector3 forwardVelocity = Vector3.forward * _forwardSpeed;
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, forwardVelocity.z);
    }
}

