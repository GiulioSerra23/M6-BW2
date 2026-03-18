
public class GameState : GenericSingleton<GameState>
{
    private bool _lifeReady;
    private bool _powerUpReady;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        //LoadGame();
    }

    public void SetupRun()
    {
        InventoryManager.Instance.ClearInvetory();

        var levels = PowerUpManager.Instance.GetAllPowerUpLevels();

        foreach (var pair in levels)
        {
            if (pair.Value > 0)
            {
                SO_PowerUpItem item = PowerUpManager.Instance.GetItem(pair.Key);

                if (item != null)
                {
                    InventoryManager.Instance.AddItem(item);
                }
            }
        }
    }

    #region INITIALIZE

    private void OnEnable()
    {
        TryInit();

        if (!_lifeReady) LifeController.OnSingletonReady += OnLifeReady;
        if (!_powerUpReady) PowerUpManager.OnSingletonReady += OnPowerUpReady;
    }

    private void TryInit()
    {
        if (LifeController.Instance != null) _lifeReady = true;
        if (PowerUpManager.Instance != null) _powerUpReady = true;

        if (_lifeReady && _powerUpReady) Init();
    }

    private void Init()
    {
        LifeController.Instance.OnDie += SaveState;
        LifeController.Instance.OnDie += CoinsManager.Instance.CommitRunCoins;
        PowerUpManager.Instance.OnPowerUpsUpgraded += SaveState;

        SaveState();
    }

    private void OnLifeReady()
    {
        _lifeReady = true;
        TryInit();
    }

    private void OnPowerUpReady()
    {
        _powerUpReady = true;
        TryInit();
    }

    private void OnDisable()
    {
        if (LifeController.Instance != null) LifeController.Instance.OnDie -= SaveState;
        if (LifeController.Instance != null) LifeController.Instance.OnDie -= CoinsManager.Instance.CommitRunCoins;
        if (PowerUpManager.Instance != null) PowerUpManager.Instance.OnPowerUpsUpgraded -= SaveState;

        LifeController.OnSingletonReady -= OnLifeReady;
        PowerUpManager.OnSingletonReady -= OnPowerUpReady;
    }

    #endregion
    
    #region SAVING
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
    #endregion
}