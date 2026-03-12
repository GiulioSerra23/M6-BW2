using UnityEngine;

public enum SoundID
{
    FOOTSTEPS_GRASS = 0,
    FOOTSTEPS_ROCK = 1,
    FOOTSTEPS_WOOD = 2,

    LANDING_GRASS = 10,
    LANDING_ROCK = 11,
    LANDING_WOOD = 12,
    LANDING_WATER = 13,

    PICKUP_COIN = 20,

    NONE = 100,
}

[System.Serializable]
public class SoundData
{
    public SoundID ID;
    public AudioClip[] Clips;
}
