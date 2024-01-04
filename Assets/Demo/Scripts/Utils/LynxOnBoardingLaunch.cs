//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | Author : Cédric Morel Francoz                                              |
//   | Manage the specific launch of the Lynx Onboarding application.             |
//   ==============================================================================

using UnityEngine;

namespace Lynx.Onboarding
{
    public class LynxOnBoardingLaunch : MonoBehaviour
    {
        private static string mLYNX_OB_FILE_DIR = "/sdcard/DCIM/Lynx/OB";
        private static string mLYNX_OB_FILE_NAME = "LynxOB";
        private static string mLYNX_OB_PACKAGE_NAME = "com.lynx.onboarding";

        /// <summary>
        /// Call this function to create and fill the Onboarding file.
        /// </summary>
        static void createAndFillOBFile(string _OBTestFileDir, byte value)
        {
            string OBTestFilePath = _OBTestFileDir + "/" + mLYNX_OB_FILE_NAME;
            byte[] data = new byte[1];
            data[0] = value;
            System.IO.File.WriteAllBytes(OBTestFilePath, data);
        }

        /// <summary>
        /// Call this function to end the Onboarding and save that it's finished.
        /// </summary>
        static public void EndOfOnBoarding()
        {
            string OBFilePath;

#if !UNITY_EDITOR && UNITY_ANDROID
        OBFilePath = mLYNX_OB_FILE_DIR + "/"+ mLYNX_OB_FILE_NAME;
        Debug.Log("On Boarding File Path on device : " + OBFilePath);
#else
            OBFilePath = Application.dataPath + "/DCIM/lynx/OB/" + mLYNX_OB_FILE_NAME;
#endif

            if (System.IO.File.Exists(OBFilePath))
            {
                Debug.Log("EndOfOnBoarding(): LynxOB file exist, so write info inside");
                byte[] data = new byte[1];
                data[0] = 0x1;
                System.IO.File.WriteAllBytes(OBFilePath, data);
            }
            else
            {
                Debug.LogWarning("EndOfOnBoarding(): LynxOB file doesn't exist, recreate folder and file and write info inside");

#if !UNITY_EDITOR && UNITY_ANDROID
                string  OBFileDir = mLYNX_OB_FILE_DIR;
                Debug.Log("On Boarding File Dir on device : " + OBFileDir);

                if (!System.IO.Directory.Exists(OBFileDir))
                {
                    Debug.Log("LynxOnBoardingLaunch(): Create On boarding OB folder because it doesn't exist");
                    System.IO.Directory.CreateDirectory(OBFileDir);
                    createAndFillOBFile(OBFileDir, 0x1);
                }
                else
                {
                    Debug.Log("LynxOnBoardingLaunch(): Create OB file and write info inside");
                    createAndFillOBFile(OBFileDir, 0x1);
                }
#endif
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// An useless function to avoid Unity Warnings.
        /// </summary>
        private void AvoidWarning()
        {
            Debug.Log(mLYNX_OB_FILE_DIR);
            Debug.Log(mLYNX_OB_FILE_NAME);
            Debug.Log(mLYNX_OB_PACKAGE_NAME);
        }
#endif
    }
}