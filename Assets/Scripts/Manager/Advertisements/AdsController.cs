using GoogleMobileAds.Api;
using UnityEngine;

namespace Orchard.GameSpace.Advertisements
{
    public class AdsController : MonoBehaviour
    {
        [SerializeField] private InterstitialAdMob _interstitialAdMob;
        [SerializeField] private RewardAdMob _rewardAdMob;

        public void Init()
        {
            MobileAds.Initialize(initStatus => { });
        }

        public bool TryShowInterstitial()
        {
            if (_interstitialAdMob.TryShowVideo())
                return true;

            return false;
        }

        public bool IsReadyRewardAds()
        {
            if (_rewardAdMob.IsReadyAds())
                return true;

            return false;
        }

        public void ShowRewardAds(System.Action giveReward = null)
        {
            _rewardAdMob.Show(giveReward);
        }

        public TypeLoadingAds GetTypeLoadingAds()
        {
            return _rewardAdMob.GetTypeLoadingAds();
        }
    }

    public enum TypeLoadingAds
    {
        Error,
        Ready,
        Loading,
    }
}
