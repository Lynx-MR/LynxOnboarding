//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to manage all materials effects on the project.        |
//   ==============================================================================

using System.Collections;
using UnityEngine;

namespace Lynx.Onboarding
{
    public class MaterialEffectsManager : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] Material introductionMaterial = null;
        [SerializeField] Material handsMaterial = null;
        [SerializeField] Material glowMaterial = null;

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to start the golden glow effect animation around the next button.
        /// </summary>
        public void NextButtonEffect()
        {
            StartCoroutine(MaterialEffect(glowMaterial, "_Spawn"));
        }

        /// <summary>
        /// Call this function to start the green glowing effect on the hands.
        /// </summary>
        public void HandsEffect()
        {
            StartCoroutine(MaterialEffect(handsMaterial, "_ValidationValue"));
        }

        /// <summary>
        /// Call this function to start the black introduction effect.
        /// </summary>
        public void IntroductionEffect(GameObject effectObject)
        {
            StartCoroutine(MaterialEffect(introductionMaterial, "_Spawn", effectObject));
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Call this coroutine to trigger the material's effect.
        /// </summary>
        /// <param name="material">Material to vary.</param>
        /// <param name="name">Name of parameter to be varied.</param>
        private IEnumerator MaterialEffect(Material material, string name)
        {
            float elapsedTime = 0.0f;

            while (elapsedTime < 1)
            {
                elapsedTime += Time.deltaTime / 2;
                material.SetFloat(name, elapsedTime / 1);
                yield return new WaitForEndOfFrame();
            }

            material.SetFloat(name, 0);
        }

        /// <summary>
        /// Call this coroutine to trigger the material's effect, and ddisable the GameObject to which it is applied at the end.
        /// </summary>
        /// <param name="material">Material to vary.</param>
        /// <param name="name">Name of parameter to be varied.</param>
        /// <param name="effectObject">GameObject to disable.</param>
        private IEnumerator MaterialEffect(Material material, string name, GameObject effectObject)
        {
            float elapsedTime = 0.0f;

            while (elapsedTime < 1)
            {
                elapsedTime += Time.deltaTime / 2;
                material.SetFloat(name, elapsedTime / 1);
                yield return new WaitForEndOfFrame();
            }

            effectObject.SetActive(false);
        }

        #endregion

        #region SINGLETON

        public static MaterialEffectsManager Instance { get; private set; } = null;

        private void Awake()
        {
            Instance = this;
        }

        protected MaterialEffectsManager() { }

        #endregion
    }
}