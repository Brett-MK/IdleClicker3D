using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Image itemimage;
        [SerializeField] private TextMeshProUGUI itemLabel;
        [SerializeField] private Button itemBuyButton;
        [SerializeField] private TextMeshProUGUI itemBuyButtonText;
        private ShopItemSO shopItemSO;

        public void Initialize(ShopItemSO shopItemSO)
        {
            this.shopItemSO = shopItemSO;

            itemimage.sprite = shopItemSO.itemSprite;
            itemLabel.text = shopItemSO.itemName;

            CheckInteractable(PlayerManager.Instance.CurrentCash);

            itemBuyButton.onClick.AddListener(delegate { UpgradeManager.Instance.UpgradeShopItem(shopItemSO); });
            PlayerManager.Instance.OnCashChanged.AddListener(OnChangeCashCallback);
        }

        private void OnChangeCashCallback(double newCash)
        {
            CheckInteractable(newCash);
        }

        private void CheckInteractable(double cash)
        {
            int currentLevel = UpgradeManager.Instance.GetUpgradeLevel(shopItemSO);
            int currentPrice = shopItemSO.GetCurrentPrice(currentLevel);

            itemBuyButton.interactable = cash >= currentPrice;
            itemBuyButtonText.color = cash >= currentPrice ? Color.white : Color.gray;

            itemBuyButtonText.text = FormatNumbers.Format(currentPrice);
        }
    }
}
