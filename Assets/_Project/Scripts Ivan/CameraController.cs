using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _gameplayCam;
    [SerializeField] private float duration = 3f;

    private void Start()
    {
        if (_gameplayCam != null) SetFollow();
    }

    // Questa funzione ora accetta un "punto di arrivo" completo
    public void MoveToAnchor(Transform anchor)
    {
        // Muove la camera alla posizione del segnaposto
        transform.DOMove(anchor.position, duration).SetEase(Ease.InOutSine);

        // Ruota la camera alla rotazione del segnaposto
        transform.DORotate(anchor.eulerAngles, duration).SetEase(Ease.InOutSine);
    }

    private void SetFollow()
    {
        _gameplayCam.Follow = PlayerManager.Instance.CurrentPlayer.transform;        
    }
}