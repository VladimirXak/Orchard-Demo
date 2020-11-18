using DG.Tweening;
using System;
using UnityEngine;

namespace Orchard.UI
{
    public class Window : MonoBehaviour
    {
        [SerializeField] private Transform _mainPanel;
        [SerializeField] private GameObject _blockPanel;
        [SerializeField] private CanvasGroup _canvasGroup;

        public virtual void Hide()
        {
            AnimationCloseWindow();
        }

        protected void AnimationShowWindow(Action action = null)
        {
            _blockPanel.SetActive(true);

            _mainPanel.localScale = Vector3.zero;
            _canvasGroup.alpha = 0;

            _canvasGroup.DOFade(1, 0.4f);

            gameObject.SetActive(true);

            _mainPanel.DOScale(1.1f, 0.3f).OnComplete(delegate
            {
                _mainPanel.DOScale(1f, 0.1f).OnComplete(delegate
                {
                    _blockPanel.SetActive(false);
                    action?.Invoke();
                });
            });
        }

        protected void AnimationCloseWindow(Action action = null, bool isDestroyEnd = true)
        {
            _blockPanel.SetActive(true);

            _canvasGroup.DOFade(0, 0.4f);

            _mainPanel.DOScale(1.1f, 0.1f).OnComplete(delegate
            {
                _mainPanel.DOScale(0, 0.3f).OnComplete(delegate
                {
                    if (isDestroyEnd)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        _blockPanel.SetActive(false);
                    }

                    action?.Invoke();
                });
            });
        }
    }
}
