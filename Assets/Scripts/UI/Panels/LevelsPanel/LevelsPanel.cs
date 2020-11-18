using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Orchard.GameSpace;
using System.IO;
using Orchard.Game;

namespace Orchard.UI
{
    public class LevelsPanel : MonoBehaviour
    {
        [SerializeField] private SoundButton _soundButton;
        [SerializeField] private ItemLevel _prefItemLevel;
        [SerializeField] private Transform _parentLevels;
        [Space(10)]
        [SerializeField] private TextMeshProUGUI _tmpLevels;
        [SerializeField] private Button _previousPageButton;
        [SerializeField] private Button _nextPageButton;

        private const int _countItemsLevel = 50;

        private List<ItemLevel> _listLevels;

        private int _page = 1;
        private int _countFiles;

        private void Awake()
        {
            _listLevels = new List<ItemLevel>();

            _previousPageButton.onClick.AddListener(() =>
            {
                if (_page == 1)
                    return;

                _page--;
                ChangePage();
            });

            _nextPageButton.onClick.AddListener(() =>
            {
                _page++;
                ChangePage();
            });

            Init();
        }

        private void Init()
        {
            _countFiles = GetCountLevels();

            if (_countFiles == 0)
            {
                _tmpLevels.text = "null";
                return;
            }

            int numberLevel = GameManager.GameInfo.NumberLevel;

            _page = numberLevel / _countItemsLevel;

            if (numberLevel % _countItemsLevel != 0)
                _page++;

            CreateLevels();
            ChangePage();
        }

        private void CreateLevels()
        {
            for (int i = 0; i < _countItemsLevel; i++)
            {
                ItemLevel itemLevel = Instantiate(_prefItemLevel, _parentLevels);
                itemLevel.LoadLevelButton.onClick.AddListener(_soundButton.ButtonClick);

                _listLevels.Add(itemLevel);
            }
        }

        private void ChangePage()
        {
            int startNumberLevel = (_page - 1) * _countItemsLevel;

            if (startNumberLevel >= _countFiles)
            {
                _page--;
                return;
            }

            _tmpLevels.text = $"{startNumberLevel + 1} - {startNumberLevel + _countItemsLevel}";
            startNumberLevel++;

            int currentLevel = GameManager.GameInfo.NumberLevel;

            for (int i = 0; i < _listLevels.Count; i++)
            {
                bool isHaveLevel = startNumberLevel <= _countFiles;

                _listLevels[i].gameObject.SetActive(isHaveLevel);

                if (isHaveLevel)
                {
                    if (currentLevel >= startNumberLevel)
                        _listLevels[i].UnlockedLevel(startNumberLevel);
                    else
                        _listLevels[i].LockedLevel();
                }

                startNumberLevel++;
            }
        }

        private int GetCountLevels()
        {
#if UNITY_ANDROID
            string pathFiles = $"Levels/";

            var txtLevel = Resources.LoadAll<TextAsset>(pathFiles) as TextAsset[];
            return txtLevel.Length;
#else
            string pathFiles = $"{Application.streamingAssetsPath}/Levels/";
            return new DirectoryInfo(pathFiles).GetFiles("*json").Length;
#endif
        }
    }
}
