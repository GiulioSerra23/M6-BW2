
using UnityEngine;

[CreateAssetMenu(fileName = "Flight Effect", menuName = "Data/PowerUp Effect/Flight")]
public class SO_FlightEffect : SO_PowerUpEffect
{
    public override void Apply(GameObject user, int level)
    {
        float duration = _baseDuration + (_durationPerLevel * (level));

        if (!user.TryGetComponent<PlayerMotor>(out var player)) return;

        FlightController.Instance.ActiveFlight(player, duration);
    }
}