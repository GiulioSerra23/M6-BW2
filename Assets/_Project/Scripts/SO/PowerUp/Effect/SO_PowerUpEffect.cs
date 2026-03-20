using UnityEngine;

public abstract class SO_PowerUpEffect : ScriptableObject
{
    [Header("Duration Settings")]
    [SerializeField] protected float _baseDuration = 5f;

    [Header("Scaling per Level")]
    [SerializeField] protected float _durationPerLevel = 1f;

    public abstract void Apply(GameObject user, int level);
}