using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumpController : PlayerController
{
    [Header ("Renfereces")]
    [SerializeField] private GroundCheck _groundCheck;

    [Header ("Jump Settings")]
    [SerializeField] private float _jumpForce = 5f;

    [Header("Gravity Settings")]
    [SerializeField] private float _fallMultiplier = 2.5f;

    private bool _isJumping;

    private void CheckIsJumping()
    {
        if (_groundCheck.IsGrounded && _rb.velocity.y <= 0)
        {
            _isJumping = false;
        }
    }

    private void HandleJump()
    {
        if (_groundCheck.IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 velocity = _rb.velocity;
            velocity.y = _jumpForce;
            _rb.velocity = velocity;

            _isJumping = true;
        }
    }

    private void HandleBetterGravity()
    {
        if (_rb.velocity.y < _jumpForce)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void StickToGround()
    {
        if (_groundCheck.IsGrounded && !_isJumping && _rb.velocity.y > 0)
        {
            Vector3 velocity = _rb.velocity;
            velocity.y = 0f;
            _rb.velocity = velocity;
        }
    }

    private void Update()
    {
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleBetterGravity();
        CheckIsJumping();
        StickToGround();
    }
}

