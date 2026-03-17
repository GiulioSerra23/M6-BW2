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
        CoinsManager.Instance.OnCoinsChanged += AddScore;
        AddScore(CoinsManager.Instance.TotalCoins);
    }

    public void AddScore(int value)
    {
        _scoreText.SetText(value.ToString());
    }

    private void OnDisable()
    {
        if (CoinsManager.Instance != null) CoinsManager.Instance.OnCoinsChanged -= AddScore;
    }
}
