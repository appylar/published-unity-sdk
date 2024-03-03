namespace UnityEngine.AppylarSdkWrapper
{
    internal class AndroidAppylarInterstitialListener : AndroidJavaProxy
    {
        private const string CLASS_REFERENCE =
            "com.appylar.android.sdk.interstitial.InterstitialListener";
        private AppylarInterstitialListener m_ManagedListener;

        public AndroidAppylarInterstitialListener(AppylarInterstitialListener mListener)
            : base(CLASS_REFERENCE)
        {
            m_ManagedListener = mListener;
        }

        void onNoInterstitial()
        {
            m_ManagedListener?.onNoInterstitial();
        }

        void onInterstitialShown()
        {
            m_ManagedListener?.onInterstitialShown();
        }

        void onInterstitialClosed()
        {
            m_ManagedListener?.onInterstitialClosed();
        }
    }
}
