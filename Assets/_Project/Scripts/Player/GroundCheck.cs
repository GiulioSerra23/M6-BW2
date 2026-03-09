
using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    [Header ("Events")]
    [SerializeField] private UnityEvent<bool> _onIsGroundedChange;

    [Header("Ground Check Settings")]
    [SerializeField] private float _radius = 0.45f;
    [SerializeField] private LayerMask _groundMask;

    [Header ("Debug")]
    [SerializeField] private bool _isGrounded = true;

    public bool IsGrounded => _isGrounded;

    public void CheckIsGrounded()
    {
        bool wasGrounded = _isGrounded;

        _isGrounded = Physics.CheckSphere(transform.position, _radius, _groundMask);

        if (wasGrounded != _isGrounded) _onIsGroundedChange.Invoke(_isGrounded);
    }

    private void Update()
    {
        CheckIsGrounded();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded? Color.yellow : Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
