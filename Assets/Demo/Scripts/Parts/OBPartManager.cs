//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to manage transitions between Onboarding parts.        |
//   ==============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lynx.Onboarding
{
    public class OBPartManager : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("Persistent GameObjects")]
        [Space(10)]

        [SerializeField] private GameObject environmentPrefab = null;
        [SerializeField] private GameObject robotPrefab = null;

        [Header("Non-Persistent GameObjects")]
        [Space(10)]

        [SerializeField] private GameObject languagePrefab = null;
        [SerializeField] private GameObject menuPrefab = null;

        [Space(10)]

        [SerializeField] private List<PartObject> partPrefabs = new List<PartObject>();

        [SerializeField] private GameObject quitPrefab = null;

        [Header("Reference GameObjects")]
        [Space(10)]

        [SerializeField] private GameObject currentPrefab = null;

        #endregion

        #region PRIVATE VARIABLES

        private int currentIndex = 0;

        #endregion

        #region PRIVATE CLASSES

        [Serializable]
        class PartObject
        {
            public GameObject Prefab;
            public bool IsDone;

            public PartObject(GameObject prefab, bool isDone)
            {
                this.Prefab = prefab;
                this.IsDone = isDone;
            }
        }

        private void Start()
        {
            InstantiateEnvironment();
            StartCoroutine(InstantiateNonPersistentGameObject(languagePrefab, 0));
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to Instantiate the environment.
        /// </summary>
        public void InstantiateEnvironment()
        {
            robotPrefab.SetActive(true);
            GameObject.Instantiate(environmentPrefab);
        }

        /// <summary>
        /// Call this function to Instantiate the language selection.
        /// </summary>
        public void InstantiateLanguageSelection()
        {
            // Move R1-Robot to the Language selection position.
            robotPrefab.GetComponent<Animator>().SetTrigger("LanguageSelection");

            StartCoroutine(InstantiateNonPersistentGameObject(languagePrefab, 0));
        }

        /// <summary>
        /// Call this function to Instantiate the part selection.
        /// </summary>
        public void InstantiateMenu()
        {
            // Move R1-Robot to the Menu position.
            robotPrefab.GetComponent<Animator>().SetTrigger("ToMenu");

            StartCoroutine(InstantiateNonPersistentGameObject(menuPrefab, 1));
        }

        /// <summary>
        /// Call this function to instantiate a part.
        /// </summary>
        /// <param name="index">Index of the part.</param>
        public void InstantiatePart(int index)
        {
            // Update the new current index.
            currentIndex = index;

            StartCoroutine(InstantiateNonPersistentGameObject(partPrefabs[index].Prefab, 0));
        }

        /// <summary>
        /// Call this function to Instantiate the final part.
        /// </summary>
        public void InstantiateQuitPart()
        {
            StartCoroutine(InstantiateNonPersistentGameObject(quitPrefab, 1));
        }

        /// <summary>
        /// Call this function to get the state of a part.
        /// </summary>
        /// <param name="index">Part index.</param>
        /// <returns></returns>
        public bool GetState(int index)
        {
            return partPrefabs[index].IsDone;
        }

        /// <summary>
        /// Call this function to checkmark the current part as done.
        /// </summary>
        public void SetState(bool value)
        {
            partPrefabs[currentIndex].IsDone = value;
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Call this coroutine to instantiate a Non-Persistent GameObject after a delay.
        /// </summary>
        /// <param name="prefab">Non-Persistent GameObject to instantiate.</param>
        /// <param name="delay">Delay to wait.</param>
        /// <returns></returns>
        private IEnumerator InstantiateNonPersistentGameObject(GameObject prefab, float delay)
        {
            if(currentPrefab != null)
            {
                GameObject.Destroy(currentPrefab);
                yield return new WaitForSeconds(delay);
            }
            currentPrefab = GameObject.Instantiate(prefab);
        }

        #endregion

        #region SINGLETON
        public static OBPartManager Instance { get; private set; } = null;

        private void Awake()
        {
            Instance = this;
        }

        protected OBPartManager() { }
        #endregion
    }
}