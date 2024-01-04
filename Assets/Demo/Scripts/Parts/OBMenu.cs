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
    public class OBMenu : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("Localization References")]
        [Space(10)]

        [SerializeField] private LocalizeStringEvent localizeStringEvent = null;

        [Header("State Indicators")]
        [Space(10)]

        [SerializeField] private GameObject HeadsetPositionState = null;
        [SerializeField] private GameObject HeadsetButtonsState = null;
        [SerializeField] private GameObject HandGestureState = null;
        [SerializeField] private GameObject HandInterfacesState = null;

        #endregion

        #region UNITY API

        // This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            HeadsetPositionState.SetActive(OBPartManager.Instance.GetState(0));
            HeadsetButtonsState.SetActive(OBPartManager.Instance.GetState(1));
            HandGestureState.SetActive(OBPartManager.Instance.GetState(2));
            HandInterfacesState.SetActive(OBPartManager.Instance.GetState(3));

            LocalizationManager.Instance.localizeStringEvent = localizeStringEvent;
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to destroy the menu and go to a specific part.
        /// </summary>
        /// <param name="index">Part to set as current and instantiate.</param>
        public void LaunchPart(int index)
        {
            OBPartManager.Instance.InstantiatePart(index);
        }

        /// <summary>
        /// Call this function to detroy the menu and go back to language selection.
        /// </summary>
        public void ReturnToLanguageSelection()
        {
            OBPartManager.Instance.InstantiateLanguageSelection();
        }

        /// <summary>
        /// Call this function to destroy the menu and go to the end of tutorials.
        /// </summary>
        public void ExitMenu()
        {
            OBPartManager.Instance.InstantiateQuitPart();
        }

        #endregion
    }
}
