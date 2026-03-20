
using System;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : GenericSingleton<LifeController>
{
    [Header("Sound ID")]
    [SerializeField] protected SoundID _hitAudio;

    [Header("Life Settings")]
    [SerializeField] private int _maxHp;
    [SerializeField] private int _currentHp;
    [SerializeField] private bool _startFullHp = true;
    [SerializeField] private bool _canTakeDamage = true;

    public event Action<int> OnHpChanged;
    public event Action OnDie;

    public int CurrentHp { get => _currentHp; private set => SetHp(value); }
    public bool CanTakeDamage { get => _canTakeDamage; set => _canTakeDamage = value; }

    private void Start()
    {
        if (_startFullHp)
        {
            RestoreFullHp();
        }
    }

    public void RestoreFullHp() => SetHp(_maxHp);

    public void SetMaxHp(int maxHp) => _maxHp = maxHp;

    private void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHp);

        if (hp != _currentHp)
        {
            _currentHp = hp;
            OnHpChanged?.Invoke(_currentHp);

            if (_currentHp <= 0)
            {
                OnDie?.Invoke();
            }
        }
    }

    public void AddHp(int amount) => SetHp(_currentHp + amount);

    public void TakeDamage(int amount)
    {
        if (!_canTakeDamage) return;

        SetHp(_currentHp - amount);
        AudioManager.Instance.Play(_hitAudio);
    }
}
