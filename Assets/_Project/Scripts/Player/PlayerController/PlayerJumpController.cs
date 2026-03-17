using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    [Header ("Jump Settings")]
    [SerializeField] private float _jumpForce = 30f;

    [Header("Gravity Settings")]
    [SerializeField] private float _gravity = -0.5f;

    private float _verticalVelocity;
    private PlayerMotor _controller;

    public bool OverrideVertical { get; set; } = false;

    private void Awake()
    {
        _controller = GetComponent<PlayerMotor>();
    }

    public float GetVerticalVelocity()
    {
        if (OverrideVertical) return 0f;
        return _verticalVelocity;
    }

    private void HandleJump()
    {
        if (_controller.IsGrounded())
        {
            if (_verticalVelocity < 0)
            {
                _verticalVelocity = -2f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalVelocity = _jumpForce;
            }
        }

        _verticalVelocity += _gravity;
    }

    private void Update()
    {
        HandleJump();
    }
}

