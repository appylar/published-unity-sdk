using System.Collections.Generic;
using UnityEngine.AppylarSdkWrapper.Platform;

namespace UnityEngine.AppylarSdkWrapper
{
    internal interface INativePlatform
    {
        void SetupPlatform(IPlatform platform);
        void Initialize(
            string appKey,
            AdType[] adTypes,
            bool testMode,
            AppylarInitializationListener initializationListener
        );
        void ShowBanner(
            BannerPosition position,
            string placementId,
            AppylarBannerListener bannerListener
        );
        void ShowInterstitial(string placementId, AppylarInterstitialListener interstitialListener);
        void SetParameter(Dictionary<string, string[]> parameters);
        void HideBanner();
        bool CanShowAd(AdType adType);
    }
}
