using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Manager
{
    public class PlayerManager : MonoBehaviour
    {
        private double currentCash;
        private double currentPremiumCurrency;

        public double CurrentCash
        {
            get { return currentCash; }
            set
            {
                currentCash = value;
                UIManager.Instance.SetCurrentCashLabel((int)currentCash);
                OnCashChanged.Invoke(currentCash);
            }
        }

        public double CurrentPremiumCurrency
        {
            get { return currentPremiumCurrency; }
            set
            {
                currentPremiumCurrency = value;
            }
        }


        public static PlayerManager Instance;
        public UnityEvent<double> OnCashChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            GameManager.Instance.OnLoadData.AddListener(LoadData);
        }

        private void LoadData(SaveData saveData)
        {
            this.currentCash = saveData.playerSaveData.currentCash;
            this.currentPremiumCurrency = saveData.playerSaveData.currentPremiumCurrency;
        }
    }
}