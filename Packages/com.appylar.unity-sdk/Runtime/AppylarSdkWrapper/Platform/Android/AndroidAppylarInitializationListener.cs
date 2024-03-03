namespace UnityEngine.AppylarSdkWrapper
{
    internal class AndroidAppylarInitializationListener : AndroidJavaProxy
    {
        private const string CLASS_REFERENCE = "com.appylar.android.sdk.interfaces.Events";
        private AppylarInitializationListener m_ManagedListener;

        public AndroidAppylarInitializationListener(AppylarInitializationListener mListener)
            : base(CLASS_REFERENCE)
        {
            m_ManagedListener = mListener;
        }

        void onInitialized()
        {
            m_ManagedListener?.onInitialized();
        }

        void onError(string error)
        {
            m_ManagedListener?.onError(error);
        }
    }
}
