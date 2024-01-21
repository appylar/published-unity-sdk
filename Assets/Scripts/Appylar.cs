using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AppylarSdkWrapper;


public class AppylarSample : MonoBehaviour, AppylarInitializationListener, AppylarInterstitialListener, AppylarBannerListener
{
    private string appKey;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Appylar.Initialize(
            GetAppKey()
        , new AdType[]{
                AdType.INTERSTITIAL, AdType.BANNER
            }
            , true
            , this
            );
    }

    private string GetAppKey()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return appKey = "<appKeyAndroid>";
        }
        else
        {
            return appKey = "<appKeyIos>";
        }
    }

    public void Interstitial()
    {
        Appylar.ShowInterstitial(" ", this);
    }

    public void ShowBanner()
    {
        Appylar.ShowBanner(BannerPosition.BOTTOM, " ", this);
    }

    public void HideBanner()
    {
        Appylar.HideBanner();
    }

    #region Interface Implementations
    public void onNoBanner()
    {
        print("onNoBanner");
    }

    public void onBannerShown(int height)
    {
        print("onBannerShown " + height);
    }

    public void onInitialized()
    {
        print("onInitialized");
    }

    public void onError(string message)
    {
        print($"onError : {message}");
    }

    public void onNoInterstitial()
    {
        print("onNoInterstitial ");
    }

    public void onInterstitialShown()
    {
        print("onInterstitialShown");
    }

    public void onInterstitialClosed()
    {
        print("onInterstitialClosed ");
    }
    #endregion


}