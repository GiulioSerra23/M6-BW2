using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected Rigidbody _rb;

    protected void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
}
