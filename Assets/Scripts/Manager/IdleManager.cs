using System;
using UnityEngine;

namespace Manager
{
    public class IdleManager : MonoBehaviour
    {
        private float currentTime;
        private double cashPerSecond;

        public double CashPerSecond => cashPerSecond;

        public static IdleManager Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 1f)
            {
                double minerAmount = GetProductionFromShopItemType(ShopItemType.MINER_AMOUNT);
                double minerlevel = GetProductionFromShopItemType(ShopItemType.MINER_LEVEL);
                double stoneLevel = GetProductionFromShopItemType(ShopItemType.STONE_LEVEL);

                cashPerSecond = minerAmount * minerlevel * stoneLevel;
                PlayerManager.Instance.CurrentCash += cashPerSecond;

                currentTime = 0f;
            }
        }

        private double GetProductionFromShopItemType(ShopItemType itemType)
        {
            ShopItemSO upgrade = UpgradeManager.Instance.GetShopItemByType(itemType);
            int upgradeLevel = UpgradeManager.Instance.GetUpgradeLevel(upgrade);
            return upgrade.GetCurrentProduction(upgradeLevel);
        }
    }
}