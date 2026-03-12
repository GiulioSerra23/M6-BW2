
using UnityEngine;

public enum SurfaceID { GRASS, ROCK, WOOD, WATER };

public class SurfaceType : MonoBehaviour
{
    [Header ("Surface Type")]
    [SerializeField] private SurfaceID _iDSurface;

    public SurfaceID IDSurface => _iDSurface;
}
