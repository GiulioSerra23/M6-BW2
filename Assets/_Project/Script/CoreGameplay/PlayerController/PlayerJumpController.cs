using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumpController : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private GroundCheck _groundCheck;

    private Rigidbody _rb;
    private bool _jumpRequested; // Flag: input letto in Update, applicato in FixedUpdate

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_groundCheck.IsGrounded && Input.GetKeyDown(KeyCode.Space))
           _jumpRequested = true;
    }

    private void FixedUpdate()
    {
        // Fisica SOLO in Update
        if (!_jumpRequested) return;

        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _jumpRequested = false;
    }
}

