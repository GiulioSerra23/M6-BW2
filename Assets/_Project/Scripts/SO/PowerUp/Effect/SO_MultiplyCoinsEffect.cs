
using UnityEngine;

[CreateAssetMenu(fileName = "Multiply Coin Effect", menuName = "Data/PowerUp Effect/Multiply Coins")]
public class SO_MultiplyCoinsEffect : SO_PowerUpEffect
{
    [Header ("Base Settings")]
    [SerializeField] private float _baseMultiplier = 1.5f;
    [SerializeField] private float _baseDuration = 5f;

    [Header("Scaling per Level")]
    [SerializeField] private float _multiplierPerLevel = 0.5f;
    [SerializeField] private float _durationPerLevel = 1f;

    public override void Apply(GameObject user, int level)
    {
        float multiplier = _baseMultiplier + _multiplierPerLevel * (level - 1);
        float duration = _baseDuration + _durationPerLevel * (level - 1);

        CoinsManager.Instance.ActivateMultiplier(multiplier, duration);
    }
}
