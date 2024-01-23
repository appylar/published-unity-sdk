# Appylar Unity Wrapper 

License: GPL

Appylar Wrapper for use on both platforms ios and Android in Unity. It is a lightweight and easy-to-use Ad integration Wrapper provided by Appylar. Wrapper enables developers to integrate Appylar Ads in any type of Unity application.

Appylar provides several types of Ads, enabling you to set Ads wherever you want in the Unity application. 
The Ads provided by Appylar are like this: 
- Banners 
- Interstitials

# Usage
The installation guide will lead you through the following steps.

- Add Appylar to Packages
- Setup Wrapper configuration
- Add ShowBanner
- Add ShowInterstitial
- Add canShowAd and Set parameters

# Step 1: Add Appylar to your Packages
- First, we go to Appylar GitHub and copy to the clone link.
- Open Unity Project, Click the Window option on top.
- Click on Package Manager.
- Click on (+), then open to dropdown.
- Select “Add Package from git URL” past copied link and click Add.
- Import the package in the Assets folder successfully.

# Step 2: Setup the configuration for your App and Listeners

Create one script in Assets Folder, make one public class with Monobehaviour, and implement all listeners(interface) with methods.
		 	 	 								
### C# : [Appylar. cs] 
```
using UnityEngine;
using System;
using UnityEngine.AppylarSdkWrapper;
public class AppylarSample: MonoBehaviour, AppylarInitializationListener, AppylarInterstitialListener, AppylarBannerListener {

    #region Interface Implementations

    public void onNoBanner() {
      //Callback if there are no banners available to show
    }

    public void onBannerShown(int height) {
      //Callback for Banner is shown success
    }

    public void onInitialized() {
      //Callback for successful initialization
    }

    public void onError(string message) {
      //Callback for error thrown by SDK
    }

    public void onNoInterstitial() {
      //Callback if there are no interstitials available to show
    }

    public void onInterstitialShown() {
      //Callback for interstitial shown success
    }

    public void onInterstitialClosed() {
      //Callback for the close event of interstitial
    }
    #endregion

Private void Start() {
  Appylar.Initialize(
    "<YOUR_APP_KEY>" //APP KEY provide by the console for Development use for Android ["jrctNFE1b-7IqHPShB-gKw"] and iOS ["OwDmESooYtY2kNPotIuhiQ"]
    , new AdType[] {
      AdType.INTERSTITIAL, AdType.BANNER
    } //What type of Ads do you want to integrate
    , true //Test Mode, [TRUE] for development & [FALSE] for production
    , this //Attach callback listeners for SDK before initialization
  );
}
```
		 	 	 							
# Step 3: Add Show Banner to the application 					
### 1. To integrate the Show Banner Function
```
public void ShowBanner() {
  //ShowBanner to show Ads on the UI with optional customized placement
  //Leave blank if not required
  Appylar.ShowBanner(
    BannerPosition //In which position do you want to see the banner
    , "<PLACEMENT_STRING>" //Placement string is an optional parameter
    , this //Attach	Event Listeners
  );
}
```
### 2. For hiding the banner at the run time.
```
Appylar.HideBanner();
```
# Step 4: Add Interstitial to the application

### To integrate the Interstitial Function
```
public void Interstitial() {
  //Interstitial to show Ads on the UI with optional customized placement
  //Leave blank if not required
  Appylar.ShowInterstitial(
    "<PLACEMENT_STRING>" //Placement string is an optional parameter
    , this //Attach	Event Listeners
  );
}
```

# Step 5: Add canShowAd and Set parameters to the application
### To integrate the CanShowAd() Function
```
 public void CanShowAd()
    {
//Pass enum value of Adtype parameter
        bool result = AppylarAd.CanShowAd(AdType.BANNER);
        if (result == true)
        {
        //for banner available to be shown 
        }
        else
        {
       //for the not available banner to be shown 
        }
    }
```

### To integrate the SetParameters() Function
```
public void SetParameter()
    {
	// First, hide the banner when already shown a banner then call the set parameter function
	HideBanner();
	// Pass Dictionary type of value parameter  
        Appylar.SetParameter(parameter);
    }
```

# Sample Codes	

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AppylarSdkWrapper;

public class AppylarSample: MonoBehaviour, AppylarInitializationListener, AppylarInterstitialListener, AppylarBannerListener {
  private string appKey;
  // Start is called before the first frame update
  void Start() {
    Initialize();
  }
```
### For initialization
```
  public void Initialize() {
    Appylar.Initialize(GetAppKey(), new AdType[] {
      AdType.INTERSTITIAL, AdType.BANNER
    }, true, this);
  }

  private string GetAppKey() {
    if (Application.platform == RuntimePlatform.Android) {
      return appKey = "<androidKey>";
    } else {
      return appKey = "<iosKey>";
    }
  }
```
### Implementation for both types of ads
```
  public void Interstitial() {
    Appylar.ShowInterstitial(" ", this);
  }

  public void ShowBanner() {
    Appylar.ShowBanner(BannerPosition.BOTTOM, " ", this);
  }

  public void HideBanner() {
    Appylar.HideBanner();
  }

  #region Interface Implementations
  public void onNoBanner() {
    print("onNoBanner");
  }

  public void onBannerShown(int height) {
    print("onBannerShown " + height);
  }


  public void onInitialized() {
    print("onInitialized");
  }


  public void onError(string message) {
    print($"onError : {message}");
  }


  public void onNoInterstitial() {
    print("onNoInterstitial ");
  }


  public void onInterstitialShown() {
    print("onInterstitialShown");
  }


  public void onInterstitialClosed() {
    print("onInterstitialClosed ");
  }
  #endregion

}
```
### Implementation for both types of canShowAd
```
 public void CanShowAdBanner()
    {
        bool result = AppylarAd.CanShowAd(AdType.BANNER);
        if (result == true)
        {
         print("canShowAdBanner - Available ");
        }
        else
        {
         print( "canShowAdBanner - Not available");
        }
    }

    public void CanShowInterstitial()
    {
        bool result = AppylarAd.CanShowAd(AdType.INTERSTITIAL);
        if (result == true)
        {
           print( "canShowAdInterstitial - Available");
        }
        else
        {
         print("canShowAdInterstitial - Not available");
        }
    }
```
   ### Implementation for setParameter function
   ```
    public void SetParameter()
    {
        HideBanner();
        Appylar.SetParameter(Dictionary);
    }
```
