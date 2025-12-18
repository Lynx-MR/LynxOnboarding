using UnityEngine;

public class OnboardingProperty : MonoBehaviour
{
    /// <summary>
    /// Lit une propriété système Android.
    /// </summary>
    public static string Get(string key, string defaultValue = "")
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        using (var sp = new AndroidJavaClass("android.os.SystemProperties"))
        {
            return sp.CallStatic<string>("get", key, defaultValue);
        }
        #else
        return defaultValue;
        #endif
    }

    /// <summary>
    /// Écrit une propriété système Android.
    /// </summary>
    public static void Set(string key, string value)
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        using (var sp = new AndroidJavaClass("android.os.SystemProperties"))
        {
            sp.CallStatic("set", key, value);
        }
        #endif
    }

    // Start is called before the first frame update (Example)
    void Start()
    {
        // Lire la valeur
        string val = OnboardingProperty.Get("persist.lynx.onboarding.finished", "1");

        // Modifier la valeur
        OnboardingProperty.Set("persist.lynx.onboarding.finished", "0");
    }
}
