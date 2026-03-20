
using UnityEngine;

public enum PoolType
{
    TILE_ZONE1_V1 = 0,
    TILE_ZONE1_V2 = 1,
    TILE_ZONE1_V3 = 2,

    TILE_ZONE2_V1 = 5,
    TILE_ZONE2_V2 = 6,
    TILE_ZONE2_V3 = 7,

    PICKUP_COIN_POOL = 20,
}

[System.Serializable]
public class PoolEntry
{
    [SerializeField] private PoolType _poolType;
    [SerializeField] private ObjectPool _pool;

    public PoolType PoolType => _poolType;
    public ObjectPool Pool => _pool;
}
