using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Orchard.GameSpace;

namespace Orchard.UI
{
    public class SelectBoosterWindow : Window
    {
        [Space(10)]
        [SerializeField] private Button _closeWindowButton;
        [SerializeField] private Button _playGameButton;
        [Space(10)]
        [SerializeField] private SceneTransitionWindow _prefSceneTransitionWindow;
        [SerializeField] private TextMeshProUGUI _tmpNumberLevel;

        public const string _keyLocalization = "Level";

        public void Init()
        {
            _closeWindowButton.onClick.AddListener(CloseWindow);
            _playGameButton.onClick.AddListener(PlayGame);

            _tmpNumberLevel.text = $"{GameManager.Localization.GetValue(_keyLocalization)} {GameManager.LevelLoadingData.NumberLevel}";

            AnimationShowWindow();
        }

        private void PlayGame()
        {
            var sceneTransitionPanel = Instantiate(_prefSceneTransitionWindow, FindObjectOfType<Canvas>().transform);
            sceneTransitionPanel.Show(2);

            AnimationCloseWindow();
        }

        private void CloseWindow()
        {
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                var sceneTransitionPanel = Instantiate(_prefSceneTransitionWindow, FindObjectOfType<Canvas>().transform);
                sceneTransitionPanel.Show(1);
            }

            AnimationCloseWindow();
        }
    }
}
