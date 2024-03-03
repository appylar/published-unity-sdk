using System;
using System.Collections.Generic;
using UnityEngine.AppylarSdkWrapper.Platform;

namespace UnityEngine.AppylarSdkWrapper
{
    /// <summary>
    /// The wrapper class used to interact with the Appylar SDK.
    /// </summary>
    public static class Appylar
    {
        private static IPlatform s_Platform;

        public static bool isSupported => IsSupported();

        static Appylar()
        {
            if (s_Platform == null)
            {
                s_Platform = CreatePlatform();
            }
        }

        /// <summary>
        /// Initializes the SDK with configuration.
        /// </summary>
        /// <param name="appkey">App key for your app. This can be found in your app in appylar.com</param>
        /// <param name="arrayOf(AdType.BANNER, AdType.INTERSTITIAL)">What type of ads you want to use</param>
        /// <param name="testMode">Test Mode, true for development, false for production</param>
        /// <param name="initializationListener">Listener for AppylarInitializationListener callbacks</param>
        public static void Initialize(
            string appKey,
            AdType[] adTypes,
            bool testMode,
            AppylarInitializationListener initializationListener
        )
        {
            s_Platform.Initialize(appKey, adTypes, testMode, initializationListener);
        }

        /// <summary>
        /// Show banner
        /// <param name="position">An enum representing the on-screen anchor position of the banner ad.</param>
        /// <param name="placementId">Placement identifier</param>
        /// <param name="bannerListener">Listener for AppylarBannerListener callbacks</param>
        /// </summary>
        public static void ShowBanner(
            BannerPosition position,
            string placementId,
            AppylarBannerListener bannerListener
        )
        {
            s_Platform.ShowBanner(position, placementId, bannerListener);
        }

        /// <summary>
        /// Show banner
        /// <param name="position">An enum representing the on-screen anchor position of the banner ad.</param>
        /// <param name="bannerListener">Listener for AppylarBannerListener callbacks</param>
        /// </summary>
        public static void ShowBanner(BannerPosition position, AppylarBannerListener bannerListener)
        {
            ShowBanner(position, "", bannerListener);
        }

        /// <summary>
        /// Hide the banner, if visible.
        /// </summary>
        public static void HideBanner()
        {
            s_Platform.HideBanner();
        }

        /// <summary>
        /// Check if there are available ads to show for the specific type.
        /// <param name="adType">The ad type</param>
        /// </summary>
        public static bool CanShowAd(AdType adType)
        {
            return s_Platform.CanShowAd(adType);
        }

        /// <summary>
        /// Show interstitial
        /// <param name="placementId">Placement identifier</param>
        /// <param name="interstitialListener">Listener for AppylarInterstitialListener callbacks</param>
        /// </summary>
        public static void ShowInterstitial(
            string placementId,
            AppylarInterstitialListener interstitialListener
        )
        {
            s_Platform.ShowInterstitial(placementId, interstitialListener);
        }

        /// <summary>
        /// Show interstitial
        /// <param name="interstitialListener">Listener for AppylarInterstitialListener callbacks</param>
        /// </summary>
        public static void ShowInterstitial(AppylarInterstitialListener interstitialListener)
        {
            ShowInterstitial("", interstitialListener);
        }

        /// <summary>
        /// Set parameters
        /// <param name="parameters">The dictionary of parameters to set</param>
        /// </summary>
        public static void SetParameter(Dictionary<string, string[]> parameters)
        {
            s_Platform.SetParameter(parameters);
        }

        private static IPlatform CreatePlatform()
        {
            try
            {
                INativePlatform nativePlatform;
#if UNITY_ANDROID
                nativePlatform = new Platform.Android.AndroidPlatform();
                ;
#elif UNITY_IOS
                nativePlatform = new Platform.iOS.IosPlatform();
#else
                nativePlatform = new Platform.Unsupported.UnsupportedPlatform();
#endif
                return new Platform.Platform(nativePlatform);
            }
            catch (Exception exception)
            {
                try
                {
                    Debug.LogError("Initializing Appylar Ads.");
                    Debug.LogError(exception.Message);
                }
                catch (MissingMethodException) { }
                return new Platform.Platform(new Platform.Unsupported.UnsupportedPlatform());
            }
        }

        /// <summary>
        /// Check for any supported platform
        /// </summary>
        public static bool IsSupported()
        {
            return Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }
}
