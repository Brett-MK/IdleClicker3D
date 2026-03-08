using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemSO", menuName = "Scriptable Objects/ShopItemSO")]
public class ShopItemSO : ScriptableObject
{
    [Header("General Details")]
    public int id;
    public string itemName;
    public Sprite itemSprite;

    [Header("Shop Details")]
    public int basePrice;
    public float rateGrowth;

    [Header("Production Details")]
    public int baseProduction;

    public int GetCurrentPrice(int currentLevel)
    {
        return (int)(basePrice * Math.Pow(rateGrowth, currentLevel));
    }

    public int GetCurrentProduction(int currentLevel)
    {
        return baseProduction * currentLevel;
    }
}
