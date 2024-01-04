//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to automatically recenter the scene.                   |
//   ==============================================================================

using System.Collections;
using UnityEngine;

namespace Lynx.Onboarding
{
    public class RecenterAtStart : MonoBehaviour
    {
        #region UNITY API

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(CoroutineStart());
        }

        #endregion

        #region PRIVATE METHODS

        private IEnumerator CoroutineStart()
        {
            yield return new WaitForSeconds(0.5f);
            this.gameObject.GetComponent<RecenterManager>().ResetToStart();
        }

        #endregion
    }
}