using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    private CharacterController _controller;

    private PlayerJumpController _jumpController;
    private PlayerLaneMovement _laneMovement;
    private PlayerMovementForward _forwardMovement;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _jumpController = GetComponent<PlayerJumpController>();
        _laneMovement = GetComponent<PlayerLaneMovement>();
        _forwardMovement = GetComponent<PlayerMovementForward>();
    }

    public bool IsGrounded()
    {
        return _controller.isGrounded;
    }

    private void Move()
    {
        float moveX = _laneMovement.GetMovementX(transform.position.x);
        float moveY = _jumpController.GetVerticalVelocity() * Time.deltaTime;
        float moveZ = _forwardMovement.GetForwardSpeed() * Time.deltaTime;

        Vector3 move = new Vector3(moveX, moveY, moveZ);

        _controller.Move(move);
    }

    private void Update()
    {
        Move();
    }
}
