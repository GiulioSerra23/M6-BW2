using UnityEngine;
using System;

// ScriptableObject: i dati vivono come asset nel progetto,
// non su un GameObject. Questo significa che PlayerController,
// UIManager e ShopManager possono tutti leggere/scrivere
// le stesse stats senza conoscersi.
[CreateAssetMenu(fileName = "PlayerStats", menuName = "RunnerRoguelite/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    #region Configuration Fields
    [Header("Base Values — modifica questi in Inspector")]
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _baseSpeed = 8f;
    #endregion

    #region Runtime State
    // Stato runtime - NON serializzato, si resetta a ogni run
    // Setter privati: l'unico modo di modificare lo stato è passare dai metodi pubblici.
    // Questo garantisce che gli eventi vengano SEMPRE invocati correttamente.
    public int CurrentHealth { get; private set; }
    public float CurrentSpeed { get; private set; }
    public int Coins { get; private set; }
    public float Distance { get; private set; }
    public bool isDead => CurrentHealth <= 0; // Computed property: nessuno stato extra, zero costo
    #endregion

    #region Events
    // Events: chi vuole sapere che qualcosa è cambiato si iscrive,
    // senza che PlayerStats conosca UIManager o chiunque altro.
    // Health, Coins, Distance, Player
    public event Action<int> OnHealthChanged;
    public event Action<int> OnCoinsChanged;
    public event Action<float> OnDistanceChanged;
    public event Action OnPlayerDied;
    #endregion

    #region Initialization
    // Chiamato all'inizio di ogni run (non in Awake : è un SO)
    public void InitializeRun()
    {
        CurrentHealth = _maxHealth;
        CurrentSpeed = _baseSpeed;
        Coins = 0;
        Distance = 0f;
    }
    #endregion

    #region Public API (Methods)
    public void TakeDamage(int amount)
    {
        if (isDead) return; // Guard clause: evita double-death

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        OnHealthChanged?.Invoke(CurrentHealth);

        if (isDead)
            OnPlayerDied?.Invoke();
    }

    public void AddCoins(int amount)
    {
        Coins += Mathf.Max(0, amount);
        OnCoinsChanged?.Invoke(Coins);
    }

    public void AddDistance(float delta)
    {
        Distance += Mathf.Max(0, delta);
        OnDistanceChanged?.Invoke(Distance);
    }

    public void SetSpeed(float newSpeed)
    {
        CurrentSpeed = Mathf.Max(0f, newSpeed);
    }
    #endregion
}

/*
PlayerStats: Il "Cervello" dei Dati
La classe funge da unica fonte di verità per tutte le informazioni del giocatore (Salute, Monete, Velocità), 
risolvendo il problema della frammentazione dei dati tra i vari sistemi (UI, Player, Shop)
*/