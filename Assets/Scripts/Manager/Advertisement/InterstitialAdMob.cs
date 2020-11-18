using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orchard.GameSpace.Advertisements
{
    public class InterstitialAdMob : MonoBehaviour
    {

#if UNITY_IOS
        private readonly string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_ANDROID
        private readonly string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_EDITOR
        private readonly string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#endif

        private InterstitialAd _interstitial;

        private bool _isError;

        private void Start()
        {
            CreateAndLoadAd();
        }

        private void CreateAndLoadAd()
        {
            _isError = false;
            _interstitial = new InterstitialAd(_adUnitId);

            _interstitial.OnAdClosed += Interstitial_OnAdClosed;
            _interstitial.OnAdFailedToLoad += Interstitial_OnAdFailedToLoad;

            AdRequest request = new AdRequest.Builder().Build();
            _interstitial.LoadAd(request);
        }

        private void Interstitial_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            _isError = true;
        }

        private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
        {
            CreateAndLoadAd();
        }

        public bool TryShowVideo()
        {
            if (_interstitial.IsLoaded())
            {
                _interstitial.Show();
                return true;
            }

            if (_isError)
                CreateAndLoadAd();

            return false;
        }

        private void OnDestroy()
        {
            _interstitial?.Destroy();
        }
    }
}
