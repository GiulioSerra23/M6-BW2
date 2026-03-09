// GroundCheck.cs — nessuna modifica strutturale necessaria, solo pulizia
using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<bool> _onIsGroundedChange;

    [Header("Settings")]
    [SerializeField] private float _radius = 0.45f;
    [SerializeField] private LayerMask _groundMask;

    [Header("Debug")]
    [SerializeField] private bool _isGrounded = true;

    public bool IsGrounded => _isGrounded;

    private void Update()
    {
        bool wasGrounded = _isGrounded;
        _isGrounded = Physics.CheckSphere(transform.position, _radius, _groundMask);

        if (wasGrounded != _isGrounded)
            _onIsGroundedChange.Invoke(_isGrounded);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}