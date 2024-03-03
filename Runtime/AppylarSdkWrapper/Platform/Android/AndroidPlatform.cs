#if UNITY_ANDROID
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.AppylarSdkWrapper.Utilities.JSON;

namespace UnityEngine.AppylarSdkWrapper.Platform.Android
{
    internal class AndroidPlatform : INativePlatform, AppylarBannerListener
    {
        private const string ADS_BASE_CLASS = "com.appylar.android.sdk.unity.UnityManager";
        private const string ADS_POSITION_CLASS = "com.appylar.android.sdk.enums.Position";
        private const string ADS_BANNER_MANAGER_CLASS =
            "com.appylar.android.sdk.bannerview.SingleBannerManager";
        private const string ADS_INTERSTITIAL_CLASS =
            "com.appylar.android.sdk.interstitial.Interstitial";
        private const string UNITY_PLAYER_CLASS = "com.unity3d.player.UnityPlayer";
        private AppylarBannerListener internalBannerListener;
        private IPlatform m_Platform;
        private AndroidJavaObject m_ApplicationContext; //Global application context
        private AndroidJavaObject m_CurrentActivity;
        private AndroidJavaObject appylarNative; //Appylar Native Object
        private AndroidJavaObject singleBannerManagerNative; //Appylar Native Object

        //Native Class References
        private AndroidJavaClass positionEnumNative; //Postion Enum
        private AndroidJavaClass appylarInterstitial; //Native Interstitial

        public void SetupPlatform(IPlatform platform)
        {
            m_Platform = platform;
            m_CurrentActivity = GetCurrentAndroidActivity();
            m_ApplicationContext = m_CurrentActivity.Call<AndroidJavaObject>(
                "getApplicationContext"
            );
            //Get native appylar class instance
            appylarNative = new AndroidJavaObject(ADS_BASE_CLASS);
            //Get native appylar Orientation class reference
            positionEnumNative = new AndroidJavaClass(ADS_POSITION_CLASS);
            //Get native appylar Interstitial class reference
            appylarInterstitial = new AndroidJavaClass(ADS_INTERSTITIAL_CLASS);
            singleBannerManagerNative = new AndroidJavaObject(
                ADS_BANNER_MANAGER_CLASS,
                m_CurrentActivity,
                new AndroidAppylarBannerListener(this)
            );
        }

        public void Initialize(
            string appKey,
            AdType[] adTypes,
            bool testMode,
            AppylarInitializationListener initializationListener
        )
        {
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
            appylarNative.CallStatic(
                "setEventListener",
                new AndroidAppylarInitializationListener(initializationListener)
            );
            //Native initialize calling
            appylarNative.CallStatic("init", m_ApplicationContext, appKey, adTypesString, testMode);
        }

        public void SetParameter(Dictionary<string, string[]> parameters)
        {
            appylarNative.CallStatic("setParameters", Json.ConvertDictionaryToJson(parameters));
        }

        public void HideBanner()
        {
            singleBannerManagerNative.Call("hideBanner");
        }

        public bool CanShowAd(AdType adType)
        {
            if (adType == AdType.BANNER)
            {
                return singleBannerManagerNative.Call<bool>("canShowAd");
            }
            else
            {
                return appylarInterstitial.CallStatic<bool>("canShowAd");
            }
        }

        public void ShowBanner(
            BannerPosition position,
            string placementId,
            AppylarBannerListener bannerListener
        )
        {
            internalBannerListener = bannerListener;
            //Get native appylar Single Banner Manage class instance
            if (position == BannerPosition.TOP)
            {
                AndroidJavaObject topEnumValue = positionEnumNative.CallStatic<AndroidJavaObject>(
                    "valueOf",
                    "TOP"
                );
                singleBannerManagerNative.Call("showBanner", topEnumValue, placementId);
            }

            if (position == BannerPosition.BOTTOM)
            {
                AndroidJavaObject bottomEnumValue =
                    positionEnumNative.CallStatic<AndroidJavaObject>("valueOf", "BOTTOM");
                singleBannerManagerNative.Call("showBanner", bottomEnumValue, placementId);
            }
        }

        public void ShowInterstitial(
            string placementId,
            AppylarInterstitialListener interstitialListener
        )
        {
            appylarInterstitial.CallStatic(
                "setEventListener",
                new AndroidAppylarInterstitialListener(interstitialListener)
            );
            appylarInterstitial.CallStatic("showAd", m_CurrentActivity, placementId);
        }

        public static AndroidJavaObject GetCurrentAndroidActivity()
        {
            var unityPlayerClass = new AndroidJavaClass(UNITY_PLAYER_CLASS);
            return unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }

        #region Interface Implementations
        public void onNoBanner()
        {
            internalBannerListener.onNoBanner();
        }

        public void onBannerShown(int height)
        {
            internalBannerListener.onBannerShown(height);
        }
        #endregion
    }
}
#endif
