using UnityEngine;

public abstract class SO_PowerUpEffect : ScriptableObject
{
    public abstract void Apply(GameObject user, int level);
}