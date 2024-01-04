//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to define the part 4 of the Onboarding.                |
//   ==============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.Onboarding
{
    public class OBPart4 : OBPart
    {
        #region INSPECTOR VARIABLES

        [Header("Part 4 References")]
        [Space(10)]

        [SerializeField] private List<GameObject> subSteps = new List<GameObject>();
        [SerializeField] private GameObject nextButton = null;

        [Space(10)]

        [SerializeField] private AudioSource sound = null;

        #endregion

        #region PRIVATE VARIABLES

        private bool isRunning = false;
        private const int min = 0;
        private const int max = 6;

        #endregion

        #region UNITY API

#if UNITY_EDITOR

        // Update is called once per frame
        private void Update()
        {
            // KeyCode to pass handtracking interfaces.
            if (Input.GetKeyDown(KeyCode.P)) ActionDone();
        }

#endif
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to validate current step.
        /// </summary>
        public void ActionDone()
        {
            if (isRunning == false)
            {
                StartCoroutine(Delay(2.5f));
                MaterialEffectsManager.Instance.NextButtonEffect();
                MaterialEffectsManager.Instance.HandsEffect();
                nextButton.SetActive(true);
                sound.Play();
            }
        }

        /// <summary>
        /// Call this function to enable and disable rights interface elements.
        /// </summary>
        public void SubTaskVerif()
        {
            if (this.currentDialogueIndex > min && this.currentDialogueIndex < max)
            {
                nextButton.SetActive(false);

                foreach (GameObject subStep in subSteps)
                {
                    subStep.SetActive(false);
                }

                subSteps[currentDialogueIndex].SetActive(true);
            }
            else if (this.currentDialogueIndex == max)
            {
                foreach (GameObject subStep in subSteps)
                {
                    subStep.SetActive(false);
                }
            }
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Call this coroutine to wait a delay between two ActionDone.
        /// </summary>
        /// <param name="time">Delay to wait.</param>
        private IEnumerator Delay(float time)
        {
            isRunning = true;
            yield return new WaitForSeconds(time);
            isRunning = false;
        }

        #endregion
    }
}
