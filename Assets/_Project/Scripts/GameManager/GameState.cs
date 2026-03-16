using UnityEngine;

public class GameState : GenericSingleton<GameState>
{
    [SerializeField] private LifeController _playerLife;

    protected override void Awake()
    {
        base.Awake();

        //LoadGame();
    }

    private void Start()
    {
        _playerLife.OnDie += SaveState;
        PowerUpManager.Instance.OnPowerUpsUpgraded += SaveState;
    }

    private void SaveState()
    {
        SavingSystem.Save();
    }

    public SaveData GetSaveData()
    {
        SaveData data = new SaveData();

        data.LastRunTime = TimerManager.Instance.CurrentTime;
        data.BestTimes = LeaderboardManager.Instance.GetAllBestTimes();
        data.TotalCoins = CoinsManager.Instance.TotalCoins;
        data.PowerUps = PowerUpManager.Instance.GetAllPowerUpLevels();

        return data;
    }

    private void LoadGame()
    {
        SaveData data = SavingSystem.Load();

        if (data.BestTimes != null) LeaderboardManager.Instance.SetTimes(data.BestTimes);
        if (data.PowerUps != null) PowerUpManager.Instance.SetLevels(data.PowerUps);
        CoinsManager.Instance.SetCoins(data.TotalCoins);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (_playerLife != null) _playerLife.OnDie -= SaveState;
        if (PowerUpManager.Instance != null) PowerUpManager.Instance.OnPowerUpsUpgraded -= SaveState;
    }
}
