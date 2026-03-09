using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Manager
{
    public class UpgradeManager : MonoBehaviour
    {
        private static readonly int DEFAULT_UPGRADE_LEVEL = 1;
        private Dictionary<ShopItemSO, int> upgradeLevels = new Dictionary<ShopItemSO, int>();
        public Dictionary<ShopItemSO, int> UpgradeLevels => upgradeLevels;

        public static UpgradeManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }


            foreach (ShopItemSO item in GameManager.Instance.ShopItems)
            {
                upgradeLevels.Add(item, DEFAULT_UPGRADE_LEVEL);
            }

            GameManager.Instance.OnLoadData.AddListener(LoadData);
        }

        public int GetUpgradeLevel(ShopItemSO item)
        {
            return upgradeLevels[item];
        }

        private void IncreaseUpgradeLevel(ShopItemSO item)
        {
            upgradeLevels[item]++;
        }

        public void UpgradeShopItem(ShopItemSO item)
        {
            Debug.Log("Upgrade shop item" + item.name);
            int upgradeLevel = GetUpgradeLevel(item);
            int upgradePrice = item.GetCurrentPrice(upgradeLevel);
            if (PlayerManager.Instance.CurrentCash >= upgradePrice)
            {
                IncreaseUpgradeLevel(item);
                PlayerManager.Instance.CurrentCash -= upgradePrice;

                if (item.itemType == ShopItemType.MINER_AMOUNT)
                {
                    SpawnManager.instance.SpawnHuman();
                }
            }
        }

        public ShopItemSO GetShopItemByType(ShopItemType type)
        {
            foreach (ShopItemSO item in GameManager.Instance.ShopItems)
            {
                if (item.itemType == type)
                {
                    return item;
                }
            }

            return null;
        }

        private void LoadData(SaveData saveData)
        {
            if (saveData == null) return;

            foreach (UpgradeLevel upgradeLevel in saveData.upgradeSaveData.upgradeLevels)
            {
                ShopItemSO item = GameManager.Instance.GetShopItemById(upgradeLevel.id);
                this.upgradeLevels[item] = upgradeLevel.level;
            }
        }
    }
}