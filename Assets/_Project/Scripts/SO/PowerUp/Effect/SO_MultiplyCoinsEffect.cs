
using UnityEngine;

[CreateAssetMenu(fileName = "Multiply Coin Effect", menuName = "Data/PowerUp Effect/Multiply Coins")]
public class SO_MultiplyCoinsEffect : SO_PowerUpEffect
{
    [Header ("Multiply Settings")]
    [SerializeField] private float _baseMultiplier = 2f;

    public override void Apply(GameObject user, int level)
    {
        float duration = _baseDuration + (_durationPerLevel * (level));

        CoinsManager.Instance.ActivateMultiplier(_baseMultiplier, duration);
    }
}