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


        public static PlayerManager Instance;
        public UnityEvent<double> OnCashChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    }
}