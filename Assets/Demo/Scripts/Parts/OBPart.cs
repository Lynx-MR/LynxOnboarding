//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to define a part of the Onboarding.                    |
//   ==============================================================================

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Lynx.Onboarding
{
    public class OBPart : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private List<Dialogue> dialogues = new List<Dialogue>();

        [Header("Part References")]
        [Space(10)]

        [SerializeField] private LocalizeStringEvent localizeStringEvent = null;

        [Space(10)]

        [SerializeField] private Slider slider = null;
        [SerializeField] private string animationTrigger;

        [Space(10)]

        [SerializeField] private GameObject screen = null;

        #endregion

        #region PUBLIC VARIABLES

        [HideInInspector] public int currentDialogueIndex = 0;

        #endregion

        #region UNITY API

        // This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            LocalizationManager.Instance.localizeStringEvent = localizeStringEvent;

            SetupProgressionBar();
            AnimationManager.Instance.PlayBodyAnimation(animationTrigger);

            // Update the current Dialogue with the last Dialogue display in this part 
            LocalizationManager.Instance.PlayDialogue(dialogues[currentDialogueIndex]);

            ScreenEnabling(true);
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Call this function to enable or disable the screen if he exists
        /// </summary>
        /// <param name="state">True to enable, false to disable</param>
        private void ScreenEnabling(bool state)
        {
            if (screen != null) screen.SetActive(state);
        }

        /// <summary>
        /// Call this function to setup the values of the progression slider.
        /// </summary>
        private void SetupProgressionBar()
        {
            slider.minValue = 0;
            slider.maxValue = dialogues.Count - 1;
            slider.value = 0;
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to move the part forward one step.
        /// </summary>
        public void NextStep()
        {
            currentDialogueIndex++;
            if (currentDialogueIndex > dialogues.Count - 1)
            {
                currentDialogueIndex = dialogues.Count - 1;
                OBPartManager.Instance.SetState(true);
                OBPartManager.Instance.InstantiateMenu();
            }
            else
            {
                LocalizationManager.Instance.PlayDialogue(dialogues[currentDialogueIndex]);
                slider.value = currentDialogueIndex;
            }
        }

        /// <summary>
        /// Call this function to move the part backward one step.
        /// </summary>
        public void PreviousStep()
        {
            currentDialogueIndex--;
            if (currentDialogueIndex < 0)
            {
                currentDialogueIndex = 0;
                OBPartManager.Instance.InstantiateMenu();
            }
            else
            {
                LocalizationManager.Instance.PlayDialogue(dialogues[currentDialogueIndex]);
                slider.value = currentDialogueIndex;
            }
        }

        /// <summary>
        /// Call this function to go back to the menu.
        /// </summary>
        public void ReturnToMenu()
        {
            OBPartManager.Instance.InstantiateMenu();
        }

        #endregion
    }
}
