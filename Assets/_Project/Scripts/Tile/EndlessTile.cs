using System;
using UnityEngine;

public class EndlessTile : PoolableObject
{
    [SerializeField] private PickupSpawnPoint[] _pickupSpawns;

    private bool _hasTriggered;

    public event Action<EndlessTile> OnTilePassed;

    public override void OnSpawned()
    {
        _hasTriggered = false;

        foreach (var pickup in _pickupSpawns)
        {
            pickup.Spawn();
        }
    }

    public override void OnDespawned()
    {
        _hasTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasTriggered) return;
        if (!other.CompareTag(Tags.Player)) return;

        _hasTriggered = true;

        OnTilePassed?.Invoke(this);
    }
}
