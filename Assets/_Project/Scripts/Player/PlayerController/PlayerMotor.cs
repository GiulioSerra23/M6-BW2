using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    public CharacterController Controller { get; private set; }
    public PlayerJumpController JumpController { get; private set; }
    public PlayerLaneMovement LaneMovement { get; private set; }
    public PlayerMovementForward ForwardMovement { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        JumpController = GetComponent<PlayerJumpController>();
        LaneMovement = GetComponent<PlayerLaneMovement>();
        ForwardMovement = GetComponent<PlayerMovementForward>();
    }

    public bool IsGrounded()
    {
        return Controller.isGrounded;
    }

    private void Move()
    {
        float moveX = LaneMovement.GetMovementX(transform.position.x);
        float moveY = JumpController.GetVerticalVelocity() * Time.deltaTime;
        float moveZ = ForwardMovement.GetForwardSpeed() * Time.deltaTime;

        Vector3 move = new Vector3(moveX, moveY, moveZ);

        Controller.Move(move);
    }

    private void Update()
    {
        Move();
    }
}
