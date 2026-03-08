using System.Runtime.CompilerServices;
using Manager;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ShopItemSO[] shopitems;

    public ShopItemSO[] ShopItems { get => shopitems; }

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        shopitems = Resources.LoadAll<ShopItemSO>("ShopItems");
    }

    private void Start()
    {
        UpgradeManager.Instance.Initialize(shopitems);

        foreach (ShopItemSO item in shopitems)
        {
            UIManager.Instance.AddToShopContent(item);
        }
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveGamestate();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveSystem.SaveGamestate();
        }
    }
}
