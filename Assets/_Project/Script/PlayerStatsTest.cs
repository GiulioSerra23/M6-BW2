using UnityEngine;

public class PlayerStatsTest : MonoBehaviour
{
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private int _damageValue = 5;

    void Start()
    {
        _stats.InitializeRun();
        Debug.Log($"Run started — Health: {_stats.CurrentHealth}, Coins: {_stats.Coins}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _stats.TakeDamage(_damageValue);
        }
    }

    private void OnEnable()
    {
        // Mi iscrivo
        _stats.OnHealthChanged += UpdateHealthUI;
        _stats.OnCoinsChanged += UpdateCoinUI;
        _stats.OnDistanceChanged += UpdateDistanceUI;
        _stats.OnPlayerDied += Died;
    }

    private void OnDisable()
    {
        // Mi Disiscrivo
        _stats.OnHealthChanged -= UpdateHealthUI;
        _stats.OnCoinsChanged -= UpdateCoinUI;
        _stats.OnDistanceChanged -= UpdateDistanceUI;
        _stats.OnPlayerDied -= Died;
    }

    // I callback si fidano di chi li invoca: ricevono il dato, lo usano, basta.
    private void UpdateHealthUI(int health) => Debug.Log($"Health : {health}");
    private void UpdateCoinUI(int currentCoins) => Debug.Log($"Coins: {currentCoins}");
    private void UpdateDistanceUI(float dist) => Debug.Log($"Distance: {dist:F1}");

    private void Died()
    {
        Debug.Log($"Is Dead");
    }

}
