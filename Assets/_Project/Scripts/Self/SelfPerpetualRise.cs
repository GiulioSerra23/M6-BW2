
using UnityEngine;

public class SelfPerpetualRise : MonoBehaviour
{
    [Header ("Rise Settings")]
    [SerializeField] private float _altitude = 1f;
    [SerializeField] private float _altitudeSpeed = 2f;

    private float _startY;

    private void Start()
    {
        _startY = transform.position.y;
    }

    private void Rise()
    {
        float offSetY = Mathf.Sin(Time.time * _altitudeSpeed) * _altitude;
        Vector3 position = transform.position;
        position.y = _startY + offSetY;
        transform.position = position;
    }

    private void Update()
    {
        Rise();
    }
}
