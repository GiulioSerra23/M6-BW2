using System.Collections;
using UnityEngine;

public class ShieldController : GenericSingleton<ShieldController>
{
    private Coroutine _shieldRoutine;

    public void ActivateShield(LifeController life, float duration)
    {
        if (_shieldRoutine != null) StopCoroutine(_shieldRoutine);

        _shieldRoutine = StartCoroutine(ShieldRoutine(life, duration));
    }

    private IEnumerator ShieldRoutine(LifeController life, float duration)
    {
        life.CanTakeDamage = false;

        yield return new WaitForSeconds(duration);

        life.CanTakeDamage = true;
        _shieldRoutine = null;
    }

    public void BreakShield(LifeController life)
    {
        if (_shieldRoutine != null)
        {
            StopCoroutine(_shieldRoutine);
            life.CanTakeDamage = true;
            _shieldRoutine = null;
        }
    }
}
