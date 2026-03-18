using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Data/PowerUp")]
public class SO_PowerUpItem : SO_GenericItem
{
    [SerializeField] private SO_PowerUpEffect _effect;
    [SerializeField] private int _maxLevel = 5;

    [Header("Cost Settings")]
    [SerializeField] private int _baseCost = 100;
    [SerializeField] private int _costPerLevel = 50;

    public int MaxLevel => _maxLevel;

    public int GetCost(int currentLevel)
    {
        return _baseCost + (_costPerLevel * currentLevel);
    }

    public override void Use(GameObject user)
    {
        int level = PowerUpManager.Instance.GetLevel(this);

        _effect.Apply(user, level);
    }    
}