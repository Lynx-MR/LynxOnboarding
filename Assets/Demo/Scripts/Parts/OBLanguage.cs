//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used as overlay, to bridge the gap between instantiated     |
//   | scirpts and external references to the prefab.                             |
//   ==============================================================================

using UnityEngine;
using UnityEngine.Localization.Components;

namespace Lynx.Onboarding
{
    public class OBLanguage : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("Localization References")]
        [Space(10)]

        [SerializeField] private LocalizeStringEvent localizeStringEvent = null;

        #endregion

        #region UNITY API

        // This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            LocalizationManager.Instance.localizeStringEvent = localizeStringEvent;
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to select english as current language.
        /// </summary>
        public void English()
        {
            LocalizationManager.Instance.SetCurrentLanguage(0);
            OBPartManager.Instance.InstantiateMenu();
        }

        /// <summary>
        /// Call this function to select french as current language.
        /// </summary>
        public void French()
        {
            LocalizationManager.Instance.SetCurrentLanguage(1);
            OBPartManager.Instance.InstantiateMenu();
        }
        #endregion
    }
}