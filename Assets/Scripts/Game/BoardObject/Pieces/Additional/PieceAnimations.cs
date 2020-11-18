using DG.Tweening;
using Orchard.GameSpace;
using System;
using UnityEngine;

namespace Orchard.Game
{
    public class PieceAnimations : MonoBehaviour
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void Destroy(Action action = null)
        {
            _transform.DOScale(new Vector2(1.20f, 1.20f), GameManager.Config.TIME_PIECE_DESTROY_ONE).SetEase(Ease.InOutCubic).OnComplete(delegate
            {
                _transform.DOScale(Vector2.zero, GameManager.Config.TIME_PIECE_DESTROY_TWO).SetEase(Ease.InOutCubic).OnComplete(delegate
                {
                    action?.Invoke();
                });
            });
        }

        public void AnimHint()
        {
            _transform.DOScale(new Vector2(1.15f, 1.15f), 0.10f).SetEase(Ease.InQuad).OnComplete(delegate
            {
                _transform.DOScale(new Vector2(0.85f, 0.85f), 0.15f).SetEase(Ease.OutQuad).OnComplete(delegate
                {
                    _transform.DOScale(new Vector2(1.10f, 1.10f), 0.15f).SetEase(Ease.InQuad).OnComplete(delegate
                    {
                        _transform.DOScale(new Vector2(1f, 1f), 0.15f).SetEase(Ease.InOutQuad);
                    });
                });
            });
        }
    }
}
