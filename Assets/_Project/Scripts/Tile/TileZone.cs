using UnityEngine;

[System.Serializable]
public class TileZone
{
    [SerializeField] private int _tileCount;
    [SerializeField] private PoolType[] _tiles;

    public int TileCount => _tileCount;
    public PoolType[] Tiles => _tiles;
}
