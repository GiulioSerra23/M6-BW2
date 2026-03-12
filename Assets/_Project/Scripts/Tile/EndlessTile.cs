using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTile : PoolableObject
{
    private bool _hasTriggered;    

    public override void OnSpawned()
    {
        _hasTriggered = false;
    }

    public override void OnDespawned()
    {
        _hasTriggered = false;
        TileSpawner.Instance.RemoveTile(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasTriggered) return;
        if (!other.CompareTag(Tags.Player)) return;

        _hasTriggered = true;

        TileSpawner.Instance.SpawnTile();

        Release();
    }
}
