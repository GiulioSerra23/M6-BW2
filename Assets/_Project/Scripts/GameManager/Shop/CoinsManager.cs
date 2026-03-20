using System;
using System.Collections;
using UnityEngine;

public class CoinsManager : GenericSingleton<CoinsManager>
{
    [Header ("Coins")]
    [SerializeField] private int _totalCoins;
    [SerializeField] private int _runCoins;


    private Coroutine _muliplierCoroutine;
    private float _coinMultiplier = 1f;

    public event Action<int> OnCoinsChanged;
    public event Action<int> OnRunCoinsChanged;

    public int TotalCoins => _totalCoins;
    public int RunCoins => _runCoins;

    public void SetCoins(int coins)
    {
        coins = Mathf.Max(coins, 0);
        _totalCoins = coins;
        OnCoinsChanged?.Invoke(_totalCoins);
    }

    public void AddRunCoins(int amount)
    {
        int finalAmount = Mathf.RoundToInt(amount * _coinMultiplier);
        _runCoins += finalAmount;

        OnRunCoinsChanged?.Invoke(_runCoins);
    }

    public void ResetRunCoins()
    {
        _runCoins = 0;
        OnRunCoinsChanged?.Invoke(_runCoins);
    }

    public void CommitRunCoins()
    {
        SetCoins(_totalCoins + _runCoins);
        ResetRunCoins();
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

    public bool HasReachedCoins(int requiredCoins) => _totalCoins >= requiredCoins;
}
