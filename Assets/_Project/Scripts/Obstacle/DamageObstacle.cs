
using UnityEngine;

public class DamageObstacle : MonoBehaviour
{
    [SerializeField] private int _damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tags.Player)) return;
        if (!other.TryGetComponent<LifeController>(out var lifeController)) return;

        lifeController.TakeDamage(_damageAmount);
        ShieldController.Instance.BreakShield(lifeController);
    }
}
