using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour  //gestione di tutte le monete
{
    public static CoinManager Instance;

    public int coins = 0;
    public TextMeshProUGUI coinText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        coinText.text = coins.ToString();
    }
}
