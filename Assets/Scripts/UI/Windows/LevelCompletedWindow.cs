using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Orchard.GameSpace;

namespace Orchard.UI
{
    public class LevelCompletedWindow : Window
    {
        [Space(10)]
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _watchAdsButton;
        [SerializeField] private Button _closeWindowButton;
        [Space(10)]
        [SerializeField] private TextMeshProUGUI _tmpNumberLevel;
        [SerializeField] private TextMeshProUGUI _tmpCountCoins;

        [SerializeField] private SceneTransitionWindow _prefSceneTransitionWindow;

        private SecureInt _countCoinsReward;

        private const string _keyLocalization = "Level";

        public void Init(int numberLevel, int countCoins)
        {
            _continueButton.onClick.AddListener(Hide);
            _watchAdsButton.onClick.AddListener(ShowAds);
            _closeWindowButton.onClick.AddListener(Hide);

            _tmpNumberLevel.text = $"{GameManager.Localization.GetValue(_keyLocalization)} {numberLevel}";
            _tmpCountCoins.text = $"+{countCoins}";
            _countCoinsReward = countCoins;

            AnimationShowWindow();
        }

        private void ShowAds()
        {
            GameManager.Ads.ShowRewardAds(OnGiveReward);
        }

        private void OnGiveReward()
        {
            _tmpCountCoins.text = $"+{_countCoinsReward * 2}";
            GameManager.GameInfo.Coins.AddCoins(_countCoinsReward);
            _watchAdsButton.gameObject.SetActive(false);
        }

        public override void Hide()
        {
            GameManager.LevelLoadingData.CountAdFreeLevels++;

            if (GameManager.LevelLoadingData.CountAdFreeLevels >= 3)
            {
                if (GameManager.Ads.TryShowInterstitial())
                {
                    GameManager.LevelLoadingData.CountAdFreeLevels = 0;
                }
            }

            var sceneTransitionPanel = Instantiate(_prefSceneTransitionWindow, FindObjectOfType<Canvas>().transform);
            sceneTransitionPanel.Show(1);

            AnimationCloseWindow();
        }
    }
}
