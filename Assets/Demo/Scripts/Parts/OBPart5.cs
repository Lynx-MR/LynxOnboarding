//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to define the part 5 of the Onboarding.                |
//   ==============================================================================

using System.Collections.Generic;
using UnityEngine;

namespace Lynx.Onboarding
{
    public class OBPart5 : OBPart
    {
        #region INSPECTOR VARIABLES

        [Header("Part 5 References")]
        [Space(10)]

        [SerializeField] private List<GameObject> subSteps = new List<GameObject>();
        [SerializeField] private GameObject nextButton = null;

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to validate current step.
        /// </summary>
        public void ActionDone()
        {
            this.NextStep();
            SubTaskVerif();
        }

        /// <summary>
        /// Call this function to enable and disable rights GameObjects.
        /// </summary>
        public void SubTaskVerif()
        {
            foreach (GameObject subStep in subSteps)
            {
                subStep.SetActive(false);
            }

            if (this.currentDialogueIndex == 0)
            {
                nextButton.SetActive(false);
                subSteps[0].SetActive(true);
            }

            if (this.currentDialogueIndex == 1)
            {
                nextButton.SetActive(false);
                subSteps[1].SetActive(true);
            }
        }

        #endregion
    }
}