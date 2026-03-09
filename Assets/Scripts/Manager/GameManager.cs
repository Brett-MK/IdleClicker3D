using System.Runtime.CompilerServices;
using Manager;
using UI;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private ShopItemSO[] shopitems;

    public ShopItemSO[] ShopItems { get => shopitems; }

    public static GameManager Instance;

    public UnityEvent<SaveData> OnLoadData = new UnityEvent<SaveData>();

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
        foreach (ShopItemSO item in shopitems)
        {
            UIManager.Instance.AddToShopContent(item);
        }

        SaveData saveData = LoadSystem.LoadGameState();
        if (saveData != null)
        {
            OnLoadData.Invoke(saveData);
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

    public ShopItemSO GetShopItemById(int id)
    {
        foreach (ShopItemSO item in shopitems)
        {
            if (item.id == id)
            {
                return item;
            }
        }

        return null;
    }
}
