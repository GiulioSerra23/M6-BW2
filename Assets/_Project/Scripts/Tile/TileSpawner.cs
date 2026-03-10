using UnityEngine;

public class TileSpawner : GenericSingleton<TileSpawner>
{
    [Header("Tile Pools")]
    [SerializeField] private TileZone[] _zones;

    [Header ("Tile Settings")]
    [SerializeField] private int _tilesOnScreen = 6;
    [SerializeField] private float _tileLenght = 20f;

    private int _tileCount;
    private float _spawnZ;

    private void Start()
    {
        for (int i = 0; i < _tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    private PoolType ChoosTilePool()
    {
        TileZone currentZone = _zones[0];

        for (int i = 0; i < _zones.Length; i++)
        {
            if (_tileCount >= _zones[i].StartTile)
            {
                currentZone = _zones[i];
            }
        }

        PoolType[] tiles = currentZone.Tiles;

        int randomIndex = Random.Range(0, tiles.Length);

        return tiles[randomIndex];
    }

    public void SpawnTile()
    {
        PoolType poolType = ChoosTilePool();

        PoolableObject tile = PoolManager.Instance.GetPool(poolType).Get();

        tile.transform.position = new Vector3(0, 0, _spawnZ);
        _spawnZ += _tileLenght;
        _tileCount++;
    }
}
