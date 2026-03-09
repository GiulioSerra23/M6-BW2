using UnityEngine;

// ScriptableObject: i dati vivono come asset nel progetto,
// non su un GameObject. Questo significa che PlayerController,
// UIManager e ShopManager possono tutti leggere/scrivere
// le stesse stats senza conoscersi.
[CreateAssetMenu(fileName = "PlayerStats", menuName = "RunnerRoguelite/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Base Values — modifica questi in Inspector")]
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _baseSpeed = 8f;

    // Stato runtime - NON serializzato, si resetta a ogni run
    public int CurrentHealth { get; private set; }
    public float CurrentSpeed { get; private set; }
    public int Coins { get; private set; }
    public float Distance { get; private set; }

    // Events: chi vuole sapere che qualcosa è cambiato si iscrive,
    // senza che PlayerStats conosca UIManager o chiunque altro.
    // Health, Coins, Distance, Player
    public event System.Action<int> OnHealthChanged;
    public event System.Action<int> OnCoinsChanged;
    public event System.Action<float> OnDistanceChanged;
    public event System.Action OnPlayerDied;

    // Chiamato all'inizio di ogni run (non in Awake : è un SO)
    public void InitializeRun()
    {
        CurrentHealth = _maxHealth;
        CurrentSpeed = _baseSpeed;
        Coins = 0;
        Distance = 0f;
    }

    public void TakeDamage(int amount)
    {
        // Clamp evita health negativo senza if sparsi nel codice
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth == 0)
           OnPlayerDied?.Invoke();
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        OnCoinsChanged?.Invoke(Coins);
    }

    public void AddDistance(float delta)
    {
        Distance += delta;
        OnDistanceChanged?.Invoke(Distance);
    }

    public void SetSpeed(float newSpeed)
    {
        CurrentSpeed = Mathf.Max(0f, newSpeed);
    }
}


/*
PlayerStats: Il "Cervello" dei Dati
La classe funge da unica fonte di verità per tutte le informazioni del giocatore (Salute, Monete, Velocità), 
risolvendo il problema della frammentazione dei dati tra i vari sistemi (UI, Player, Shop)
*/