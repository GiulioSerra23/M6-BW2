
using UnityEngine;

public class SurfaceImpactAudioController : MonoBehaviour
{
    [Header ("RayCast Settings")]
    [SerializeField] private float _maxDistance = 1.5f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _origin;

    private void TryPlaySurfaceSound(bool isFootStep)
    {
        Vector3 origin = _origin != null ? _origin.position : transform.position;

        Debug.DrawRay(_origin.position, Vector3.down * _maxDistance, Color.yellow, 0.1f);
        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, _maxDistance, _layerMask))
        {
            if (!hit.collider.TryGetComponent<SurfaceType>(out var surfaceType)) return;

            SoundID sound = ConvertSurfaceToSound(surfaceType.IDSurface, isFootStep);
            AudioManager.Instance.Play(sound);
        }
    }

    public void OnFootStep()
    {
        TryPlaySurfaceSound(true);
    }

    public void OnLanding()
    {
        TryPlaySurfaceSound(false);
    }

    private SoundID ConvertSurfaceToSound(SurfaceID surface, bool isFootStep)
    {
        switch (surface)
        {
            case SurfaceID.GRASS:
                return isFootStep ? SoundID.FOOTSTEPS_GRASS : SoundID.LANDING_GRASS;
            case SurfaceID.ROCK:
                return isFootStep ? SoundID.FOOTSTEPS_ROCK : SoundID.LANDING_ROCK;
            case SurfaceID.WOOD:
                return isFootStep ? SoundID.FOOTSTEPS_WOOD : SoundID.LANDING_WOOD;
            case SurfaceID.WATER:
                return SoundID.LANDING_WATER;
            default:
                return isFootStep ? SoundID.FOOTSTEPS_GRASS : SoundID.LANDING_GRASS;
        }
    }
}
