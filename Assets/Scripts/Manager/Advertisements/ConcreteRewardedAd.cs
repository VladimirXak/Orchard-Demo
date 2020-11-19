using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.GameSpace.Advertisements
{
    public class ConcreteRewardedAd
    {
#if UNITY_IOS
        private readonly string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_ANDROID
        private readonly string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_EDITOR
        private readonly string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#endif

        public TypeLoadingAds TypeLoadingAds { get; private set; }

        private RewardedAd _rewardedAd;

        private Action OnGiveReward;

        public ConcreteRewardedAd()
        {
            CreateAndLoadAd();
        }

        public bool IsReadyAds()
        {
            if (_rewardedAd.IsLoaded())
                return true;

            if (TypeLoadingAds == TypeLoadingAds.Error)
                CreateAndLoadAd();

            return false;
        }

        public void Show(Action giveReward = null)
        {
            OnGiveReward = giveReward;

            if (IsReadyAds())
                _rewardedAd.Show();
        }

        public void CreateAndLoadAd()
        {
            TypeLoadingAds = TypeLoadingAds.Loading;

            _rewardedAd = new RewardedAd(_adUnitId);

            _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
            _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;

            AdRequest request = new AdRequest.Builder().Build();
            _rewardedAd.LoadAd(request);
        }

        private void HandleRewardedAdLoaded(object sender, EventArgs e)
        {
            TypeLoadingAds = TypeLoadingAds.Ready;
        }

        private void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs e)
        {
            TypeLoadingAds = TypeLoadingAds.Error;
        }

        private void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            CreateAndLoadAd();
        }

        private void HandleUserEarnedReward(object sender, Reward args)
        {
            OnGiveReward?.Invoke();
        }
    }
}
