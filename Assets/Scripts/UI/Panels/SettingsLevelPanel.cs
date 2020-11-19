using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Orchard.UI
{
    public class SettingsLevelPanel : MonoBehaviour
    {
        [SerializeField] private SceneTransitionWindow _prefSceneTransitionWindow;
        [SerializeField] private Transform _mainPanel;
        [SerializeField] private Button _moveSettingsPanelButton;
        [SerializeField] private Button _returnMainMenuButton;

        private TypeMovePanel _typeMovePanel;

        private float _startPositionY;

        private void Awake()
        {
            _moveSettingsPanelButton.onClick.AddListener(MovePanel);
            _returnMainMenuButton.onClick.AddListener(ReturnMainMenu);
        }

        private void MovePanel()
        {
            if (_typeMovePanel == TypeMovePanel.Move)
                return;

            if (_typeMovePanel == TypeMovePanel.Hide)
                Show();
            else
                Hide();
        }

        private void Show()
        {
            _typeMovePanel = TypeMovePanel.Move;
            _startPositionY = _mainPanel.localPosition.y;

            _mainPanel.DOLocalMoveY(_mainPanel.localPosition.y - 440, 0.5f).OnComplete(delegate {
                _typeMovePanel = TypeMovePanel.Show;
            });
        }

        private void Hide()
        {
            _typeMovePanel = TypeMovePanel.Move;

            _mainPanel.DOLocalMoveY(_startPositionY, 0.5f).OnComplete(delegate {
                _typeMovePanel = TypeMovePanel.Hide;
            });
        }

        private void ReturnMainMenu()
        {
            Instantiate(_prefSceneTransitionWindow, FindObjectOfType<Canvas>().transform).Show(1);
        }

        public enum TypeMovePanel
        {
            Hide,
            Show,
            Move,
        }
    }
}
