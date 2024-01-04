//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to manage the R1 animations.                           |
//   ==============================================================================

using UnityEngine;

namespace Lynx.Onboarding
{
    public class AnimationManager : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private Animator bodyAnimator = null;
        [SerializeField] private Animator visageAnimator = null;
        [SerializeField] private Animator transformAnimator = null;

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to play a body animation by trigger.
        /// </summary>
        /// <param name="animation">The trigger.</param>
        public void PlayBodyAnimation(string animation)
        {
            if (!string.IsNullOrEmpty(animation)) bodyAnimator.SetTrigger(animation);
        }

        /// <summary>
        /// Call this function to play a visage animation by trigger.
        /// </summary>
        /// <param name="animation">The trigger.</param>
        public void PlayVisageAnimation(string animation)
        {
            if (!string.IsNullOrEmpty(animation)) visageAnimator.SetTrigger(animation);
        }

        #endregion

        #region SINGLETON

        public static AnimationManager Instance { get; private set; } = null;

        private void Awake()
        {
            Instance = this;
        }

        protected AnimationManager() { }

        #endregion
    }
}