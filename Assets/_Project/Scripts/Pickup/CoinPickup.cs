
using UnityEngine;

public class CoinPickup : Pickup
{
    [Header ("Coins Settings")]
    [SerializeField] private int _coinsAmount;

    public override void OnPick(GameObject picker)
    {
        base.OnPick(picker);

        CoinsManager.Instance.AddCoins(_coinsAmount);
    }
}
