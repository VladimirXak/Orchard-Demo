using UnityEngine;
using TMPro;
using System;
using Orchard.GameSpace;
using UnityEngine.UI;

namespace Orchard.UI
{
    public class LackOfHealthWindow : Window
    {
        [SerializeField] private TextMeshProUGUI _tmpPrice;
        [SerializeField] private TextMeshProUGUI _tmpHealthCount;
        [SerializeField] private TextMeshProUGUI _tmpHealthCountRewardAds;

        [SerializeField] private Button _buyHealthButton;
        [SerializeField] private Button _watchAdsButton;
        [SerializeField] private Button _closeWindowButton;
        [Space(10)]
        [SerializeField] private int _price;
        [SerializeField] private int _countHealth;
        [SerializeField] private int _countHealthRewardAds;

        private void Awake()
        {
            _buyHealthButton.onClick.AddListener(BuyHealth);
            _watchAdsButton.onClick.AddListener(ShowAds);
            _closeWindowButton.onClick.AddListener(Hide);
        }

        public void Init()
        {
            ShowInfo();
            AnimationShowWindow();
        }

        private void BuyHealth()
        {
            if (GameManager.GameInfo.Coins.TryBuy(_price, false))
            {
                GameManager.GameInfo.Health.AddHealth(5, DateTime.Now, true);
                AnimationCloseWindow();
            }
            else
            {
                GameManager.GameInfo.Coins.AddCoins(900, true);
                BuyHealth();
            }
        }

        private void ShowAds()
        {
            GameManager.Ads.ShowRewardAds(OnGiveReward);
        }

        private void OnGiveReward()
        {
            GameManager.GameInfo.Health.AddHealth(_countHealthRewardAds, GameManager.GameInfo.Health.DateUpdate);
            AnimationCloseWindow();
        }

        private void ShowInfo()
        {
            _tmpPrice.text = _price.ToString();
            _tmpHealthCount.text = $"x{_countHealth}";
            _tmpHealthCountRewardAds.text = $"x{_countHealthRewardAds}";
        }
    }
}
