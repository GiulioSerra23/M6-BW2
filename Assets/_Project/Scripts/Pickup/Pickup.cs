
using UnityEngine;

public class Pickup : PoolableObject, IPickable
{
    [Header ("Sound ID")]
    [SerializeField] private SoundID _pickupSound;

    private bool _isPicked = false;

    public override void OnSpawned()
    {
        _isPicked = false;
        gameObject.SetActive(true);
    }

    public override void OnDespawned()
    {
        _isPicked = false;
    }

    public virtual void OnPick(GameObject picker)
    {
        if (_isPicked) return;

        _isPicked = true;
        AudioManager.Instance.Play(_pickupSound);

        Release();
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tags.Player)) return;
        
        OnPick(other.gameObject);
    }
}