using DG.Tweening;
using Orchard.GameSpace;
using Orchard.UI;
using System.Collections;
using UnityEngine;

namespace Orchard.Game
{
    public class StartLevel : MonoBehaviour
    {
        [SerializeField] private SceneTransitionWindow _prefSceneTransitionWindow;
        [SerializeField] private Transform _startTaskPanel;
        [SerializeField] private BoardTapController _boardTapController;

        private void Awake()
        {
            var sceneTransitionPanel = Instantiate(_prefSceneTransitionWindow, FindObjectOfType<Canvas>().transform);
            sceneTransitionPanel.Hide();

            GameManager.GameInfo.Health.DecreaseHealth(true);

            StartCoroutine(MovePanels());
        }

        private IEnumerator MovePanels()
        {
            yield return new WaitForSeconds(1f);

            _startTaskPanel.DOLocalMoveY(-70, 0.5f).SetEase(Ease.InSine).OnComplete(delegate
            {
                _startTaskPanel.DOLocalMoveY(0, 0.1f).SetEase(Ease.OutSine);
            });

            yield return new WaitForSeconds(2.5f);

            _startTaskPanel.DOLocalMoveY(-70, 0.1f).SetEase(Ease.InSine).OnComplete(delegate
            {
                _startTaskPanel.DOLocalMoveY(1500, 0.5f).SetEase(Ease.OutSine).OnComplete(delegate
                {
                    Destroy(_startTaskPanel.gameObject);
                    _boardTapController.IsCanTap = true;
                });
            });
        }
    }
}
