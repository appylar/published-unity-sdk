namespace UnityEngine.AppylarSdkWrapper
{
    public interface AppylarBannerListener
    {
        void onNoBanner();

        void onBannerShown(int height);
    }
}
