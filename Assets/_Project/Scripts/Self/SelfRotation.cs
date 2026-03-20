
using UnityEngine;

public class SelfRotation : MonoBehaviour
{
    public enum RotationType { Perpetual, Swing, CopyTarget, ToTarget }

    [Header ("Rotation Type")]
    [SerializeField] private RotationType _rotationType;

    [Header ("Rotation Settings")]
    [SerializeField]private Vector3 _rotationAngles = Vector3.up;

    [Header ("Perpetual Settings")]    
    [SerializeField] private float _rotationSpeed = 5f;

    [Header ("Swing Settings")]
    [SerializeField] private float _swingAngle = 30f;
    [SerializeField] private float _swingSpeed = 2f;

    [Header ("Target Settings")]
    [SerializeField] private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void PerpetualRotation()
    {
        transform.Rotate(_rotationAngles * _rotationSpeed * Time.deltaTime);
    }

    private void SwingRotation()
    {
        float angleOffset = Mathf.Sin(Time.time * _swingSpeed) * _swingAngle;
        Vector3 currentRotation = _rotationAngles * angleOffset;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    private void CopyTargetRotation()
    {
        transform.rotation = _target.rotation;
    }

    private void ToTargetRotation()
    {
        Vector3 direction = transform.position - _target.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Update()
    {
        switch (_rotationType)
        {
            case RotationType.Perpetual:
                PerpetualRotation();
                break;
            case RotationType.Swing:
                SwingRotation();
                break;
            case RotationType.CopyTarget:
                CopyTargetRotation();
                break;
            case RotationType.ToTarget:
                ToTargetRotation();
                break;
        }
    }    
}
