using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldOriginShifter : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private CinemachineVirtualCamera _cam;

    [Header ("Shift Settings")]
    [SerializeField] private float _resetDistance = 500f;

    private Transform _player;

    private void Start()
    {        
        _player = PlayerManager.Instance.CurrentPlayer.transform;
    }

    private void ShiftWorld()
    {        
        CharacterController controller = PlayerManager.Instance.CurrentPlayer.Controller;

        float offset = _player.position.z;
        Vector3 shift = new Vector3(0f, 0f, -offset);

        controller.enabled = false;

        _player.position += shift;

        controller.enabled = true;

        TileSpawner.Instance.ShiftWorld(offset);
        _cam.OnTargetObjectWarped(_player, shift);
    }

    private void Update()
    {
        if (_player.position.z > _resetDistance)
        {
            ShiftWorld();
        }
    }
}
