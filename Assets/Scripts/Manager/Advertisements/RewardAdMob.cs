using System;
using UnityEngine;

namespace Orchard.GameSpace.Advertisements
{
    public class RewardAdMob : MonoBehaviour
    {
        private ConcreteRewardedAd _rewardedOne;
        private ConcreteRewardedAd _rewardedTwo;

        private void Start()
        {
            _rewardedOne = new ConcreteRewardedAd();
            _rewardedTwo = new ConcreteRewardedAd();
        }

        public TypeLoadingAds GetTypeLoadingAds()
        {
            if (_rewardedOne.TypeLoadingAds == TypeLoadingAds.Ready || _rewardedTwo.TypeLoadingAds == TypeLoadingAds.Ready)
                return TypeLoadingAds.Ready;
            else if (_rewardedOne.TypeLoadingAds == TypeLoadingAds.Loading || _rewardedTwo.TypeLoadingAds == TypeLoadingAds.Loading)
                return TypeLoadingAds.Loading;
            else
                return TypeLoadingAds.Error;
        }

        public bool IsReadyAds()
        {
            if ((_rewardedOne?.IsReadyAds() ?? false) || (_rewardedTwo?.IsReadyAds() ?? false))
                return true;

            return false;
        }

        public bool Show(Action giveReward = null)
        {
            if (_rewardedOne.IsReadyAds())
            {
                _rewardedOne.Show(giveReward);
                return true;
            }
            else if (_rewardedTwo.IsReadyAds())
            {
                _rewardedTwo.Show(giveReward);
                return true;
            }

            return false;
        }
    }
}
