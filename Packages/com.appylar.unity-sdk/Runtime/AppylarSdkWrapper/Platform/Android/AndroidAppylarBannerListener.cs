namespace UnityEngine.AppylarSdkWrapper
{
    internal class AndroidAppylarBannerListener : AndroidJavaProxy
    {
        private const string CLASS_REFERENCE =
            "com.appylar.android.sdk.bannerview.BannerViewListener";
        private AppylarBannerListener m_ManagedListener;

        public AndroidAppylarBannerListener(AppylarBannerListener mListener)
            : base(CLASS_REFERENCE)
        {
            m_ManagedListener = mListener;
        }

        void onNoBanner()
        {
            m_ManagedListener?.onNoBanner();
        }

        void onBannerShown(int height)
        {
            m_ManagedListener?.onBannerShown(height);
        }
    }
}
