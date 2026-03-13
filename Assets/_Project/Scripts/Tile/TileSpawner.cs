using System;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : GenericSingleton<TileSpawner>
{
    [Header("Tile Pools")]
    [SerializeField] private TileZone[] _zones;

    [Header ("Tile Settings")]
    [SerializeField] private int _tilesOnScreen = 6;
    [SerializeField] private float _tileLenght = 20f;

    private int _currentZoneIndex = 0;
    private int _tileCount;
    private float _spawnZ;

    private List<EndlessTile> _activeTiles;

    public event Action OnZoneChanged;


    private void Start()
    {
        _activeTiles = new List<EndlessTile>(_tilesOnScreen);

        for (int i = 0; i < _tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    private PoolType ChooseTilePool()
    {
        int zoneIndex = 0;

        for (int i = 0; i < _zones.Length; i++)
        {
            if (_tileCount >= _zones[i].StartTile)
            {
                zoneIndex = i;
            }
        }

        if (zoneIndex != _currentZoneIndex)
        {
            _currentZoneIndex = zoneIndex;
            OnZoneChanged?.Invoke();
        }

        TileZone currentZone = _zones[zoneIndex];

        PoolType[] tiles = currentZone.Tiles;

        int randomIndex = UnityEngine.Random.Range(0, tiles.Length);

        return tiles[randomIndex];
    }

    public void SpawnTile()
    {
        PoolType poolType = ChooseTilePool();

        EndlessTile tile = (EndlessTile)PoolManager.Instance.GetPool(poolType).GetObject();

        Vector3 position = tile.transform.position;
        position.z = _spawnZ;
        tile.transform.position = position;

        tile.OnTilePassed += HandleTilePassed;

        _activeTiles.Add(tile);

        _spawnZ += _tileLenght;
        _tileCount++;
    }

    private void HandleTilePassed(EndlessTile tile)
    {
        SpawnTile();

        tile.OnTilePassed -= HandleTilePassed;

        RemoveTile(tile);
        tile.Release();
    }

    public void RemoveTile(EndlessTile tile)
    {
        _activeTiles.Remove(tile);
    }

    public void ShiftWorld(float offset)
    {
        foreach (var tile in _activeTiles)
        {
            tile.transform.position -= new Vector3(0f, 0f, offset);
        }

        _spawnZ -= offset;
    }
}
