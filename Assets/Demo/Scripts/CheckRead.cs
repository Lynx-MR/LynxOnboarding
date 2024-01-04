//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to detect if the user reached the bottom of a scroll   |
//   | view (not yet implemented).                                                |
//   ==============================================================================

using UnityEngine;
using UnityEngine.UI;

namespace Lynx.Onboarding
{
    public class CheckRead : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GameObject button;

        #endregion

        #region UNITY API

        // Start is called before the first frame update
        void Start()
        {
            scrollRect.onValueChanged.AddListener(OnBottomReached);
        }

        #endregion

        #region PRIVATE METHODS

        private void OnBottomReached(Vector2 normalizedPosition)
        {
            if (scrollRect.verticalNormalizedPosition <= 0f)
            {
                button.SetActive(true);
            }
        }

        #endregion
    }
}