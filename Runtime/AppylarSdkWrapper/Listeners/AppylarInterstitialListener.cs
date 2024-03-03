namespace UnityEngine.AppylarSdkWrapper
{
    public interface AppylarInterstitialListener
    {
        void onNoInterstitial();

        void onInterstitialShown();

        void onInterstitialClosed();
    }
}
