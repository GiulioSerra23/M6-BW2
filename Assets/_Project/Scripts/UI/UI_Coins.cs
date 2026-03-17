using TMPro;
using UnityEngine;

public class UI_Coins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        CoinsManager.Instance.OnCoinsChanged += AddScore;
    }

    private void OnDestroy()
    {
        CoinsManager.Instance.OnCoinsChanged -= AddScore;
    }


    public void AddScore(int value)
    {
        _scoreText.text = value.ToString();
    }
}
