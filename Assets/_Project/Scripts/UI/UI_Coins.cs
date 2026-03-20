using TMPro;
using UnityEngine;

public class UI_Coins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        if (CoinsManager.Instance != null)
        {
            Init();
        }
        else
        {
            CoinsManager.OnSingletonReady += Init;
        }        
    }

    private void Init()
    {
        CoinsManager.Instance.OnCoinsChanged += UpdateCoins;
        CoinsManager.Instance.OnRunCoinsChanged += UpdateCoins;
        UpdateCoins(0);
    }

    private void UpdateCoins(int value)
    {
        int beforeRunCoins = CoinsManager.Instance.TotalCoins;
        int runCoins = CoinsManager.Instance.RunCoins;

        int total = beforeRunCoins + runCoins;
        _scoreText.SetText(total.ToString());
    }

    private void OnDisable()
    {
        if (CoinsManager.Instance != null)
        {
            CoinsManager.Instance.OnCoinsChanged -= UpdateCoins;
            CoinsManager.Instance.OnRunCoinsChanged -= UpdateCoins;
        }

        CoinsManager.OnSingletonReady -= Init;
    }
}
