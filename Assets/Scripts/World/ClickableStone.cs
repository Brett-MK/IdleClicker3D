using DG.Tweening;
using Interfaces;
using Manager;
using UnityEngine;

namespace World
{
    public class ClickableStone : MonoBehaviour, IClickable
    {
        [SerializeField] private int scale;
        [SerializeField] private float clickScale;

        [SerializeField] private float scaleSpeed;

        private void Awake()
        {
            transform.localScale = Vector3.one * scale;
        }

        public void OnClick()
        {
            PlayerManager.Instance.CurrentCash += 1;
            DOTween.Kill(this, true);
            transform.DOScale(clickScale, scaleSpeed).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
                transform.localScale = Vector3.one * scale);
        }
    }
}
