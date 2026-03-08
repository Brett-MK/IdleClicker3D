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
        foreach (ShopItemSO item in shopitems)
        {
            UIManager.Instance.AddToShopContent(item);
        }
    }
}
