using TMPro;
using UnityEngine;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("Main Game - Top Panel")]
        [SerializeField] private TextMeshProUGUI currentCashLabel;
        [SerializeField] private TextMeshProUGUI currentPremiumCurrencyLabel;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void SetCurrentCashLabel(int value)
        {
            string labelValue = FormatNumbers.Format(value);
            currentCashLabel.text = labelValue;
        }

        public void SetCurrentPremiumCurrencyLabel(int value)
        {
            string labelValue = FormatNumbers.Format(value);
            currentPremiumCurrencyLabel.text = labelValue;
        }
    }
}