using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HomeOnboarding : MonoBehaviour
{
    private static string OB_CONFIG_PATH = "/sdcard/DCIM/Lynx/OB/config.txt";
    private const string OB_KEY = "ONBOARDING_ENDED";

    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
    OB_CONFIG_PATH = "/sdcard/DCIM/Lynx/OB/config.txt";
#else
        OB_CONFIG_PATH = Path.Combine(Application.persistentDataPath, "config.txt");
#endif
    }

    /// <summary>
    /// Creates the configuration file if it does not already exist, and setting the ONBOARDING_ENDED value to FALSE.
    /// </summary>
    private void InitializeConfigFile()
    {
        if (!File.Exists(OB_CONFIG_PATH))
        {
            // Creation of a folder if it does not exist
            Directory.CreateDirectory(Path.GetDirectoryName(OB_CONFIG_PATH));

            // Writing the file with the default value
            File.WriteAllText(OB_CONFIG_PATH, $"{OB_KEY}=false");
        }
    }

    /// <summary>
    /// Set configuation file.
    /// </summary>
    public void SetOnboardingCompleted()
    {
        OnboardingProperty.SetOnboardingAsFinished();

        /*
        InitializeConfigFile();
        File.WriteAllText(OB_CONFIG_PATH, $"{OB_KEY}=true");
        */
    }
}
