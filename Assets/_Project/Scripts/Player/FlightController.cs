using System.Collections;
using UnityEngine;

public class FlightController : GenericSingleton<FlightController>
{
    [SerializeField] private float _height = 7f;
    [SerializeField] private float _verticalSpeed = 5f;

    private Coroutine _flightRoutine;

    public void ActiveFlight(PlayerMotor playerMotor, float duration)
    {
        if (_flightRoutine != null) StopCoroutine(_flightRoutine);

        _flightRoutine = StartCoroutine(FlightRoutine(playerMotor, duration));
    }

    private IEnumerator FlightRoutine(PlayerMotor playerMotor, float duration)
    {
        Transform playerTransform = playerMotor.transform;

        playerMotor.LaneMovement.enabled = false;
        playerMotor.JumpController.OverrideVertical = true;

        Vector3 startPosition = playerTransform.position;
        float targetY = startPosition.y + _height;

        while (playerTransform.position.y < targetY)
        {
            Vector3 move = new Vector3( 0f, _verticalSpeed * Time.deltaTime, 0f);
            playerMotor.Controller.Move(move);

            yield return null;
        }            

        yield return new WaitForSeconds(duration);

        while (playerTransform.position.y > startPosition.y)
        {
            Vector3 move = new Vector3(0f, -_verticalSpeed * Time.deltaTime, 0f);
            playerMotor.Controller.Move(move);

            yield return null;
        }

        playerMotor.JumpController.OverrideVertical = false;
        playerMotor.LaneMovement.enabled = true;

        _flightRoutine = null;
    }
}