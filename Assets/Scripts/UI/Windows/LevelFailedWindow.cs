using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using Orchard.GameSpace;
using UnityEngine.UI;
using Orchard.Game;

namespace Orchard.UI
{
    public class LevelFailedWindow : Window
    {
        [SerializeField] private TaskDisplay _prefTaskDisplayer;
        [SerializeField] private Transform _parentItemsTaskInfoRender;

        [SerializeField] private TextMeshProUGUI _tmpNumberLevel;
        [SerializeField] private TextMeshProUGUI _tmpPriceMoves;
        [SerializeField] private TextMeshProUGUI _tmpCountMoves;

        [SerializeField] private SceneTransitionWindow _prefSceneTransitionWindow;
        [SerializeField] private SelectBoosterWindow _prefSelectBoosterWindow;
        [Space(10)]
        [SerializeField] private Button _buyMovesButton;
        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _closeWindowButton;
        [Space(10)]
        [SerializeField] private int _countMoves;
        [SerializeField] private int _priceMoves;

        private MovesMatch _movesMatch;

        private UnityAction OnHideBoard;
        private Transform _levelInformation;

        private const string _keyLocalization = "Level";

        public void Init(LevelInformation levelInformation, UnityAction onHideBoard)
        {
            _buyMovesButton.onClick.AddListener(ContinueLevel);
            _replayButton.onClick.AddListener(Hide);
            _closeWindowButton.onClick.AddListener(CloseWindow);

            _levelInformation = levelInformation.gameObject.transform;

            List<DataTask> listDataTask = levelInformation.GetDataTask();

            OnHideBoard = onHideBoard;

            foreach (var dataTask in listDataTask)
            {
                TaskDisplay item = Instantiate(_prefTaskDisplayer, _parentItemsTaskInfoRender);
                item.SetInfo(dataTask, true);
            }

            _movesMatch = levelInformation.MovesMatch;

            _tmpNumberLevel.text = $"{GameManager.Localization.GetValue(_keyLocalization)} {GameManager.LevelLoadingData.NumberLevel}";
            _tmpCountMoves.text = $"+{_countMoves.ToString()}";
            _tmpPriceMoves.text = _priceMoves.ToString();

            AnimationShowWindow();
        }

        private void ContinueLevel()
        {
            if (GameManager.GameInfo.Coins.TryBuy(_priceMoves))
            {
                _movesMatch.Init(_countMoves, true);
                AnimationCloseWindow();
                return;
            }
            _movesMatch.Init(_countMoves, true);
            AnimationCloseWindow();
        }

        public override void Hide()
        {
            TryShowAds();

            if (GameManager.GameInfo.Health.Value == 0)
            {
                CloseWindow();
                return;
            }

            OnHideBoard();
            _levelInformation.DOLocalMoveY(_levelInformation.localPosition.y + 600, 0.75f);

            AnimationCloseWindow(() =>
            {
                var selectBoosterPanel = Instantiate(_prefSelectBoosterWindow, FindObjectOfType<Canvas>().transform);
                selectBoosterPanel.Init();
            });
        }

        public void ChangeLocalization(string value)
        {
            _tmpNumberLevel.text = $"{value} {GameManager.GameInfo.NumberLevel}";
        }

        private void CloseWindow()
        {
            TryShowAds();

            var sceneTransitionPanel = Instantiate(_prefSceneTransitionWindow, FindObjectOfType<Canvas>().transform);
            sceneTransitionPanel.Show(1);

            base.Hide();
        }

        private void TryShowAds()
        {
            GameManager.LevelLoadingData.CountAdFreeLevels++;

            if (GameManager.LevelLoadingData.CountAdFreeLevels >= 3)
            {
                if (GameManager.Ads.TryShowInterstitial())
                {
                    GameManager.LevelLoadingData.CountAdFreeLevels = 0;
                }
            }
        }
    }
}
