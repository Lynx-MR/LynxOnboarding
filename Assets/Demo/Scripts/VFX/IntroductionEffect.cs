//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used as overlay to trigger the introduction material effect.|
//   ==============================================================================

using UnityEngine;

namespace Lynx.Onboarding
{
    public class IntroductionEffect : MonoBehaviour
    {
        #region UNITY API

        // Start is called before the first frame update
        private void Start()
        {
            MaterialEffectsManager.Instance.IntroductionEffect(this.gameObject);
        }

        #endregion
    }
}