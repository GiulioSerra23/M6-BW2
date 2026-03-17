
using UnityEngine;

public class PickupSpawnPoint : MonoBehaviour
{
    [SerializeField] private PoolType poolType;

    private PoolableObject _spawnedPickup;

    public void Spawn()
    {
        if (_spawnedPickup != null && _spawnedPickup.transform.parent == transform) return;

        _spawnedPickup = PoolManager.Instance.GetPool(poolType).GetObject();

        _spawnedPickup.transform.SetParent(transform, false);
        _spawnedPickup.transform.localPosition = Vector3.zero;
        _spawnedPickup.transform.localRotation = Quaternion.identity;
    }

    public void Clear()
    {
        if (_spawnedPickup != null && _spawnedPickup.transform.parent == transform)
        {
            _spawnedPickup.Release();            
        }

        _spawnedPickup = null;
    }
}