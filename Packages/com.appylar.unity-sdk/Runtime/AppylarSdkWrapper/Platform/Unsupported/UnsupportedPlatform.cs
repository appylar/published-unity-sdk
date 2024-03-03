using System;
using System.Collections.Generic;

namespace UnityEngine.AppylarSdkWrapper.Platform.Unsupported
{
    internal sealed class UnsupportedPlatform : INativePlatform
    {
        public void SetupPlatform(IPlatform platform) { }

        public void Initialize(
            string appKey,
            AdType[] adTypes,
            bool testMode,
            AppylarInitializationListener initializationListener
        ) { }

        public void ShowBanner(
            BannerPosition position,
            string placementId,
            AppylarBannerListener bannerListener
        ) { }

        public void ShowInterstitial(
            string placementId,
            AppylarInterstitialListener interstitialListener
        ) { }

        public void SetParameter(Dictionary<string, string[]> parameters) { }

        public void HideBanner() { }

        public bool CanShowAd(AdType adType)
        {
            return false;
        }
    }
}
