using UnityEngine;

namespace Manager
{
    public class UIPrefabManager : MonoBehaviour
    {

        [Header("Shop Prefabs")]
        public GameObject shopItem;

        public static UIPrefabManager Instance;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    }
}