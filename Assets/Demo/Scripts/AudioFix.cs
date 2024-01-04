//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to correct sound deterioration in Android.             |
//   ==============================================================================

using UnityEngine;

namespace Lynx.Onboarding
{
    public class AudioFix : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // Sound deteriorated on PC.
            AudioConfiguration config = AudioSettings.GetConfiguration();
            config.dspBufferSize = 64;
            AudioSettings.Reset(config);
        }
    }
}