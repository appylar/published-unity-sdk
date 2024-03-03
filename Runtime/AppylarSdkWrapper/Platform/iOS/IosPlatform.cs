#if UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;
using System.Linq;
using UnityEngine.AppylarSdkWrapper.Utilities.JSON;

namespace UnityEngine.AppylarSdkWrapper.Platform.iOS
{
    internal class IosPlatform : INativePlatform
    {
        private static IPlatform s_Platform;

        public void SetupPlatform(IPlatform iosPlatform)
        {
            s_Platform = iosPlatform;
            setOnInitialized(initialized);
            setOnError(error);
            setOnNoBanner(noBanner);
            setOnBannerShown(bannerShown);
            setOnNoInterstitial(noInterstitial);
            setOnInterstitialClosed(interstitialClosed);
            setOnInterstitialShown(interstitialShown);
        }

        private static AppylarInitializationListener appylarInitializationListener;
        private static AppylarInterstitialListener AppylarInterstitialListener;
        private static AppylarBannerListener appylarBannerListener;
        public delegate void onInitialized();

        [DllImport("__Internal")]
        private static extern void setOnInitialized(onInitialized callBack);

        public delegate void onError(string error);

        [DllImport("__Internal")]
        private static extern void setOnError(onError callBack);

        public delegate void onNoBanner();

        [DllImport("__Internal")]
        private static extern void setOnNoBanner(onNoBanner callBack);

        public delegate void onBannerShown(int height);

        [DllImport("__Internal")]
        private static extern void setOnBannerShown(onBannerShown callBack);

        public delegate void onNoInterstitial();

        [DllImport("__Internal")]
        private static extern void setOnNoInterstitial(onNoInterstitial callBack);

        public delegate void onInterstitialClosed();

        [DllImport("__Internal")]
        private static extern void setOnInterstitialClosed(onInterstitialClosed callBack);

        public delegate void onInterstitialShown();

        [DllImport("__Internal")]
        private static extern void setOnInterstitialShown(onInterstitialShown callBack);

        [DllImport("__Internal")]
        static extern void initialize(string appKey, string adTypes, bool testMode);

        [DllImport("__Internal")]
        private static extern void showTopBanner(string placementId);

        [DllImport("__Internal")]
        private static extern void showBottomBanner(string placementId);

        [DllImport("__Internal")]
        private static extern void showInterstitial(string placementId);

        [DllImport("__Internal")]
        private static extern void hideInterstitial();

        [DllImport("__Internal")]
        private static extern void hideBanner();

        [DllImport("__Internal")]
        private static extern void setParametersJSON(string parameters);

        [DllImport("__Internal")]
        private static extern bool canShowAdBanner();

        [DllImport("__Internal")]
        private static extern bool canShowAdInterstitial();

        public static ScreenOrientation orientation = ScreenOrientation.Portrait;

        public void SetParameter(Dictionary<string, string[]> parameters)
        {
            setParametersJSON(Json.ConvertDictionaryToJson(parameters));
        }

        public void Initialize(
            string appKey,
            AdType[] adTypes,
            bool testMode,
            AppylarInitializationListener initializationListener
        )
        {
            appylarInitializationListener = initializationListener;
            string[] adTypesString = new string[adTypes.Length];
            for (int i = 0; i < adTypes.Length; i++)
            {
                if (adTypes[i] == AdType.BANNER)
                {
                    adTypesString[i] = "banner";
                }
                if (adTypes[i] == AdType.INTERSTITIAL)
                {
                    adTypesString[i] = "interstitial";
                }
            }
            foreach (var value in adTypesString)
            {
                Debug.Log("Adtype value  " + value);
            }
            string adType = string.Join(" ", adTypesString);
            initialize(appKey, adType, true);
        }

        public void ShowBanner(
            BannerPosition position,
            string placementId,
            AppylarBannerListener bannerListener
        )
        {
            appylarBannerListener = bannerListener;
            if (position == BannerPosition.TOP)
            {
                showTopBanner(placementId);
            }
            else
            {
                showBottomBanner(placementId);
            }
        }

        public bool CanShowAd(AdType adType)
        {
            if (adType == AdType.BANNER)
            {
                return canShowAdBanner();
            }
            else
            {
                return canShowAdInterstitial();
            }
        }

        public void ShowInterstitial(
            string placementId,
            AppylarInterstitialListener InterstitialListener
        )
        {
            AppylarInterstitialListener = InterstitialListener;
            showInterstitial(placementId);
        }

        public void HideBanner()
        {
            hideBanner();
        }

        [MonoPInvokeCallback(typeof(onInitialized))]
        public static void initialized()
        {
            appylarInitializationListener.onInitialized();
            Debug.Log("onInitialized");
        }

        [MonoPInvokeCallback(typeof(onError))]
        public static void error(string error)
        {
            appylarInitializationListener.onError(error);
            Debug.Log(error);
        }

        [MonoPInvokeCallback(typeof(onNoBanner))]
        public static void noBanner()
        {
            appylarBannerListener.onNoBanner();
            Debug.Log("onNoBanner()");
        }

        [MonoPInvokeCallback(typeof(onBannerShown))]
        public static void bannerShown(int height)
        {
            appylarBannerListener.onBannerShown(height);
            Debug.Log("onBannerShown() " + height);
        }

        [MonoPInvokeCallback(typeof(onNoInterstitial))]
        public static void noInterstitial()
        {
            AppylarInterstitialListener.onNoInterstitial();
            Debug.Log("onNoInterstitial()");
        }

        [MonoPInvokeCallback(typeof(onInterstitialShown))]
        public static void interstitialShown()
        {
            AppylarInterstitialListener.onInterstitialShown();
            Debug.Log("onInterstitialShown()");
            orientation = Screen.orientation;
            if (Input.deviceOrientation == DeviceOrientation.Portrait)
            {
                Screen.orientation = ScreenOrientation.Portrait;
                Debug.Log("Portrait set");
            }
            else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                Debug.Log("LandscapeLeftSet set");
            }
            else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                Screen.orientation = ScreenOrientation.LandscapeRight;
                Debug.Log("LandscapeRightSet set");
            }
            else if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            {
                Screen.orientation = ScreenOrientation.PortraitUpsideDown;
                Debug.Log("PortraitUpsideDown set");
            }
            else
            {
                Screen.orientation = orientation;
                Debug.Log("Orientation set");
            }
        }

        [MonoPInvokeCallback(typeof(onInterstitialClosed))]
        public static void interstitialClosed()
        {
            AppylarInterstitialListener.onInterstitialClosed();
            Screen.orientation = ScreenOrientation.AutoRotation;
            Debug.Log("onInterstitialClosed()");
        }
    }
}
#endif
