using UnityEngine;
using TMPro;

public class UpdateCoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    void Update()
    {
        if (InventoryManager2.instance != null)
        {
            coinText.text = "Coins: " + InventoryManager2.instance.coins;
        }
    }
}
