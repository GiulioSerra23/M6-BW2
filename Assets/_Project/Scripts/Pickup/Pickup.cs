
using UnityEngine;

public class Pickup : MonoBehaviour, IPickable
{
    [Header ("Sound ID")]
    [SerializeField] private SoundID _pickupSound;

    private bool _isPicked = false;

    public virtual void OnPick(GameObject picker)
    {
        _isPicked = true;
        AudioManager.Instance.Play(_pickupSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tags.Player)) return;

        if (_isPicked) return;
        
        OnPick(other.gameObject);
        Destroy(gameObject);
    }
}
