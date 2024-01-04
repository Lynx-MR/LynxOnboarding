//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to manage video players.                               |
//   ==============================================================================

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Lynx.Onboarding
{
    public class OBVideoPlayer : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("Video Player References")]
        [Space(10)]

        [SerializeField] private VideoPlayer videoPlayer = null;
        [SerializeField] private List<VideoClip> videos = new List<VideoClip>();

        #endregion

        #region PRIVATE VARIABLES

        private int currentStep = 0;

        #endregion

        #region UNITY API

        // This function is called when the object becomes enabled and active.
        private void OnEnable()
        {
            videoPlayer.clip = videos[currentStep];
            videoPlayer.Play();
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function on Previous button.
        /// </summary>
        public void PreviousButton()
        {
            currentStep--;

            if (currentStep < 0) currentStep = 0;
            else
            {
                videoPlayer.Stop();
                videoPlayer.clip = videos[currentStep];
                videoPlayer.Play();
            }
        }

        /// <summary>
        /// Call this function on Next button.
        /// </summary>
        public void NextButton()
        {
            currentStep++;

            if (currentStep > videos.Count - 1) currentStep = videos.Count - 1;
            else
            {
                videoPlayer.Stop();
                videoPlayer.clip = videos[currentStep];
                videoPlayer.Play();
            }
        }

        /// <summary>
        /// Call this function on Skip button.
        /// </summary>
        public void SkipButton()
        {
            currentStep = videos.Count - 1;

            videoPlayer.Stop();
            videoPlayer.clip = videos[currentStep];
        }

        #endregion
    }
}