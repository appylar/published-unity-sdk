namespace UnityEngine.AppylarSdkWrapper
{
    public interface AppylarInitializationListener
    {
        void onInitialized();

        void onError(string error);
    }
}
