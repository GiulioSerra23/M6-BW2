using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldOriginShifter : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private CinemachineVirtualCamera _cam;

    [Header ("Shift Settings")]
    [SerializeField] private float _resetDistance = 500f;

    private void ShiftWorld()
    {
        float offset = _player.position.z;
        _player.position -= new Vector3(0f, 0f, offset);

        TileSpawner.Instance.ShiftWorld(offset);
        _cam.OnTargetObjectWarped(_player, new Vector3(0f, 0f, -offset));
    }

    private void Update()
    {
        if (_player.position.z > _resetDistance)
        {
            ShiftWorld();
        }
    }
}
