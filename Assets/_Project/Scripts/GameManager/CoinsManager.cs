using System;
using System.Collections;
using UnityEngine;

public class CoinsManager : GenericSingleton<CoinsManager>
{
    [Header ("Coins")]
    [SerializeField] private int _coins;

    private Coroutine _muliplierCoroutine;
    private float _coinMultiplier = 1f;

    public event Action<int> OnCoinsChanged;

    private void SetCoins(int coins)
    {
        coins = Mathf.Max(coins, 0);
        _coins = coins;
        OnCoinsChanged?.Invoke(_coins);
    }

    public void AddCoins(int amount)
    {
        int finalAmount = Mathf.RoundToInt(amount * _coinMultiplier);

        SetCoins(_coins + finalAmount);
    }

    public void ActivateMultiplier(float multiplier, float duration)
    {
        if (_muliplierCoroutine != null)
        {
            StopCoroutine(MultiplierRoutine(multiplier, duration));
        }

        _muliplierCoroutine = StartCoroutine(MultiplierRoutine(multiplier, duration));
    }

    private IEnumerator MultiplierRoutine(float multiplier, float duration)
    {
        _coinMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        _coinMultiplier = 1f;
    }

    public bool HasReachedCoins(int requiredCoins) => _coins >= requiredCoins;
}
