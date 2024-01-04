//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to define the part 3 of the Onboarding.                |
//   ==============================================================================

using System.Collections;
using UnityEngine;

namespace Lynx.Onboarding
{
    public class OBPart3 : OBPart
    {
        #region INSPECTOR VARIABLES

        [Header("Part 3 References")]
        [Space(10)]

        [SerializeField] private GameObject nextButton = null;
        [SerializeField] private GameObject cube = null;

        [Space(10)]
        [SerializeField] private AudioSource sound = null;

        #endregion

        #region PRIVATE VARIABLES

        private bool isRunning = false;

        private const int min = 0;
        private const int max = 5;

        #endregion

        #region UNITY API

#if UNITY_EDITOR

        // Start is called before the first frame update
        private void Start()
        {
            cube.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            // KeyCode to pass handtracking gestures.
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
        /// Call this function to enable and disable rights gesture detectors.
        /// </summary>
        public void SubTaskVerif()
        {
            if (this.currentDialogueIndex > min && this.currentDialogueIndex < max)
            {
                nextButton.SetActive(false);

                if(currentDialogueIndex == 1)
                {
                    LynxGestureManager.Instance.PinchDetector.SetActive(true);
                    LynxGestureManager.Instance.PalmDetector.SetActive(false);
                    LynxGestureManager.Instance.ThumbDetector.SetActive(false);
                    cube.SetActive(false);

                    LynxGestureManager.Instance.PinchEvent.AddListener(ActionDone);
                }

                if(currentDialogueIndex == 2)
                {
                    LynxGestureManager.Instance.PinchDetector.SetActive(false);
                    LynxGestureManager.Instance.PalmDetector.SetActive(true);
                    LynxGestureManager.Instance.ThumbDetector.SetActive(false);
                    cube.SetActive(false);

                    LynxGestureManager.Instance.PalmEvent.AddListener(ActionDone);
                }

                if(currentDialogueIndex == 3)
                {
                    LynxGestureManager.Instance.PinchDetector.SetActive(false);
                    LynxGestureManager.Instance.PalmDetector.SetActive(false);
                    LynxGestureManager.Instance.ThumbDetector.SetActive(true);
                    cube.SetActive(false);

                    LynxGestureManager.Instance.ThumbEvent.AddListener(ActionDone);
                }

                if(currentDialogueIndex == 4)
                {
                    LynxGestureManager.Instance.PinchDetector.SetActive(false);
                    LynxGestureManager.Instance.PalmDetector.SetActive(false);
                    LynxGestureManager.Instance.ThumbDetector.SetActive(false);
                    cube.SetActive(true);
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
