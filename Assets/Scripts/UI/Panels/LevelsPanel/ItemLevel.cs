using Orchard.GameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Orchard.UI
{
    public class ItemLevel : MonoBehaviour
    {
        [SerializeField] private SelectBoosterWindow _prefSelectBoosterWindow;
        [SerializeField] private LackOfHealthWindow _prefLackOfHealthWindow;
        [SerializeField] private TextMeshProUGUI _tmpNumberLevel;
        [Space(10)]
        [SerializeField] private GameObject _activeLevelPanel;
        [SerializeField] private GameObject _inActiveLevelPanel;
        [Space(10)]
        [SerializeField] private Button _loadLevelButton;

        public Button LoadLevelButton => _loadLevelButton;

        private SecureInt _numberLevel;

        private Transform _canvas;

        private void Awake()
        {
            _canvas = FindObjectOfType<Canvas>().transform;

            _loadLevelButton.onClick.AddListener(LoadLevel);
        }

        public void LockedLevel()
        {
            IsActiveLevel(false);
        }

        public void UnlockedLevel(int numberLevel)
        {
            IsActiveLevel(true);

            _numberLevel = numberLevel;
            _tmpNumberLevel.text = _numberLevel.ToString();
        }

        private void IsActiveLevel(bool isActive)
        {
            _activeLevelPanel.SetActive(isActive);
            _inActiveLevelPanel.SetActive(!isActive);
        }

        private void LoadLevel()
        {
            if (GameManager.GameInfo.Health.Value == 0)
            {
                Instantiate(_prefLackOfHealthWindow, _canvas).Init();
                return;
            }

            GameManager.LevelLoadingData.NumberLevel = _numberLevel;

            Instantiate(_prefSelectBoosterWindow, _canvas).Init();
        }
    }
}
