
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Effect", menuName = "Data/PowerUp Effect/Shield")]
public class SO_ShieldEffect : SO_PowerUpEffect
{
    public override void Apply(GameObject user, int level)
    {
        float duration = _baseDuration + (_durationPerLevel * (level));

        if (!user.TryGetComponent<LifeController>(out var lifeController)) return;

        ShieldController.Instance.ActivateShield(lifeController, duration);
    }
}