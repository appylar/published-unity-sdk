using System.Collections.Generic;

namespace UnityEngine.AppylarSdkWrapper.Platform
{
    internal interface IPlatform
    {
        INativePlatform NativePlatform { get; }

        void Initialize(
            string appKey,
            AdType[] adTypes,
            bool testMode,
            AppylarInitializationListener initializationListener
        );
        void ShowBanner(BannerPosition position, AppylarBannerListener bannerListener);
        void ShowBanner(
            BannerPosition position,
            string placementId,
            AppylarBannerListener bannerListener
        );
        void ShowInterstitial(string placementId, AppylarInterstitialListener interstitialListener);
        void ShowInterstitial(AppylarInterstitialListener interstitialListener);
        void SetParameter(Dictionary<string, string[]> parameters);
        void HideBanner();
        bool CanShowAd(AdType adType);
    }
}
