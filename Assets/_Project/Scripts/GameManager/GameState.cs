using UnityEngine;

public class GameState : GenericSingleton<GameState>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        //LoadGame();
    }

    private void Start()
    {
        if (LifeController.Instance != null) LifeController.Instance.OnDie += SaveState;
        if (PowerUpManager.Instance != null) PowerUpManager.Instance.OnPowerUpsUpgraded += SaveState;
    }

    private void SaveState()
    {
        SavingSystem.Save();
    }

    public SaveData GetSaveData()
    {
        SaveData data = new SaveData();

        data.BestTimes = LeaderboardManager.Instance.GetAllBestTimes();
        data.TotalCoins = CoinsManager.Instance.TotalCoins;
        data.PowerUps = PowerUpManager.Instance.GetAllPowerUpLevels();

        return data;
    }

    private void LoadGame()
    {
        SaveData data = SavingSystem.Load();

        if (data == null) return;

        if (data.BestTimes != null) LeaderboardManager.Instance.SetTimes(data.BestTimes);
        if (data.PowerUps != null) PowerUpManager.Instance.SetLevels(data.PowerUps);
        CoinsManager.Instance.SetCoins(data.TotalCoins);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (LifeController.Instance != null) LifeController.Instance.OnDie -= SaveState;
        if (PowerUpManager.Instance != null) PowerUpManager.Instance.OnPowerUpsUpgraded -= SaveState;
    }
}
