using System;
using System.Collections.Generic;

namespace UnityEngine.AppylarSdkWrapper.Platform
{
    internal class Platform : IPlatform
    {
        public INativePlatform NativePlatform { get; }

        public Platform(INativePlatform nativePlatform)
        {
            NativePlatform = nativePlatform;
            NativePlatform.SetupPlatform(this);
        }

        public void Initialize(
            string appKey,
            AdType[] adTypes,
            bool testMode,
            AppylarInitializationListener initializationListener
        )
        {
            NativePlatform.Initialize(appKey, adTypes, testMode, initializationListener);
        }

        public void ShowBanner(BannerPosition position, AppylarBannerListener bannerListener)
        {
            ShowBanner(position, "", bannerListener);
        }

        public void ShowBanner(
            BannerPosition position,
            string placementId,
            AppylarBannerListener bannerListener
        )
        {
            NativePlatform.ShowBanner(position, placementId, bannerListener);
        }

        public void ShowInterstitial(AppylarInterstitialListener interstitialListener)
        {
            ShowInterstitial("", interstitialListener);
        }

        public void ShowInterstitial(
            string placementId,
            AppylarInterstitialListener interstitialListener
        )
        {
            NativePlatform.ShowInterstitial(placementId, interstitialListener);
        }

        public void SetParameter(Dictionary<string, string[]> parameters)
        {
            NativePlatform.SetParameter(parameters);
        }

        public void HideBanner()
        {
            NativePlatform.HideBanner();
        }

        public bool CanShowAd(AdType adType)
        {
            return NativePlatform.CanShowAd(adType);
        }
    }
}
