using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PowerUpSlot : MonoBehaviour
{
    [Header ("Item")]
    [SerializeField] private SO_PowerUpItem _powerUp;

    [Header("UI References")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Button _buyButton;

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        _icon.sprite = _powerUp.Icon;
        _nameText.text = _powerUp.Name;

        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(OnBuyClicked);

        UpdateUI();

        PowerUpManager.Instance.OnPowerUpsUpgraded += UpdateUI;
        CoinsManager.Instance.OnCoinsChanged += OnCoinsChanged;
        ShopManager.Instance.OnItemPurchased += UpdateUI;
    }

    private void OnCoinsChanged(int coins)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        int level = PowerUpManager.Instance.GetLevel(_powerUp);
        int maxLevel = _powerUp.MaxLevel;

        _levelText.SetText($"Level : {level} of {maxLevel}");

        if (level >= maxLevel)
        {
            _costText.SetText("Max");
            _buyButton.interactable = false;
        }
        else
        {
            int cost = _powerUp.GetCost(level);
            _costText.SetText(cost.ToString());
            _buyButton.interactable = CoinsManager.Instance.HasReachedCoins(cost);
        }
    }

    private void OnBuyClicked()
    {
        ShopManager.Instance.Buy(_powerUp);
    }

    private void OnDestroy()
    {
        if (PowerUpManager.Instance != null) PowerUpManager.Instance.OnPowerUpsUpgraded -= UpdateUI;
        if (CoinsManager.Instance != null) CoinsManager.Instance.OnCoinsChanged -= OnCoinsChanged;
        if (ShopManager.Instance != null) ShopManager.Instance.OnItemPurchased -= UpdateUI;
    }
}