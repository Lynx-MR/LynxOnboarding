//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | Author : Cédric Morel Francoz                                              |
//   | Manage the way to quit the applictaion and go back to Lynx Home.           |
//   ==============================================================================

using System.Collections;
using UnityEngine;

namespace Lynx.Onboarding
{
    public class BackToLauncher : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public void GoToLauncher()
        {
            if (LynxAPI.IsAR())
            {
                LynxAPI.SetVR();
            }

            StartCoroutine(BackToLauncherInCoroutine());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator BackToLauncherInCoroutine()
        {
            yield return new WaitForSecondsRealtime(0.2f);

            bool fail = false;

            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

            AndroidJavaObject launchIntent = null;

            string lynxLauncherPackageName = "com.lynx.scenehome";

            try
            {
                launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", lynxLauncherPackageName);
            }
            catch (System.Exception)
            {
                fail = true;
            }

            if (fail)
            {
                // Open app in store.
                Application.OpenURL("https://lynx-r.com/");
            }
            else
            {
                // Open the app.
                ca.Call("startActivity", launchIntent);
            }

            ca.Call("finishAndRemoveTask");

            up.Dispose();
            ca.Dispose();
            packageManager.Dispose();
            launchIntent.Dispose();
        }
    }
}