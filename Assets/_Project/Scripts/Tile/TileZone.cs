using UnityEngine;

[System.Serializable]
public class TileZone
{
    [SerializeField] private int _startTile;
    [SerializeField] private PoolType[] _tiles;

    public int StartTile => _startTile;
    public PoolType[] Tiles => _tiles;
}
