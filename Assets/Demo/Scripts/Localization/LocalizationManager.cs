//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | A script layer for the Unity Localization plugin.                          |
//   ==============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;
using TMPro;

namespace Lynx.Onboarding
{
    public class LocalizationManager : MonoBehaviour
    {
        #region PUBLIC VARIABLES

        public LocalizeStringEvent localizeStringEvent = null;
        public AudioSource audioSource = null;

        public TMP_FontAsset OccidentalFont = null;
        public TMP_FontAsset JapaneseFont = null;
        public TMP_FontAsset ChineseFont = null;

        #endregion

        #region PRIVATE VARIABLES

        private TMP_FontAsset currentFont = null;
        private bool isActive = false;

        #endregion

        #region UNITY API

        // Start is called before the first frame update
        private void Start()
        {
            SetCurrentLanguage(0);
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to update the font of the TextMeshPro.
        /// </summary>
        /// <param name="textMeshPro">TextMeshPro to update.</param>
        public void UpdateFont(TextMeshProUGUI textMeshPro)
        {
            textMeshPro.font = currentFont;
        }

        /// <summary>
        /// Call this function to set the current Localize String Event for dialogues.
        /// </summary>
        /// <param name="localize">Localize String Event which you want to set as current Localize String Event for dialogues.</param>
        public void SetLocalizeStringEvent(LocalizeStringEvent localize)
        {
            localizeStringEvent = localize;
        }

        /// <summary>
        /// Call this function to select a new language for dialogues.
        /// </summary>
        /// <param name="index">Localization language index.</param>
        public void SetCurrentLanguage(int index)
        {
            SetCurrentFont(index);
            ChangeLanguage(index);
        }

        /// <summary>
        /// Call this funtion to play/set the current dialogue.
        /// </summary>
        /// <param name="dialogue">Dialogue to set.</param>
        public void PlayDialogue(Dialogue dialogue)
        {
            try
            {
                if (!string.IsNullOrEmpty(dialogue.id)) localizeStringEvent.StringReference.SetReference("UITest", dialogue.id);

                if (!string.IsNullOrEmpty(dialogue.bodyAnimation)) AnimationManager.Instance.PlayBodyAnimation(dialogue.bodyAnimation);

                if (!string.IsNullOrEmpty(dialogue.visageAnimation)) AnimationManager.Instance.PlayVisageAnimation(dialogue.visageAnimation);

                if (dialogue.audio != null)
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(dialogue.audio);
                }
            }
            catch
            {
                Debug.Log("Souci");
            }
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Call this function to set the current language.
        /// </summary>
        /// <param name="languageID">Language ID in Edit > Project Settings > Localization.</param>
        /// <returns></returns>
        private IEnumerator SetLanguage(int languageID)
        {
            isActive = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageID];
            isActive = false;
        }

        /// <summary>
        /// Call this function to change the current language.
        /// </summary>
        /// <param name="languageID">Language ID in Edit > Project Settings > Localization.</param>
        private void ChangeLanguage(int languageID)
        {
            if (isActive) return;
            StartCoroutine(SetLanguage(languageID));
        }

        /// <summary>
        /// Call this function to change the font, depends of the language index.
        /// </summary>
        /// <param name="index">Localization language index.</param>
        private void SetCurrentFont(int index)
        {
            if (index == 2) currentFont = JapaneseFont;
            else if (index == 7 || index == 8) currentFont = ChineseFont;
            else currentFont = OccidentalFont;
        }

        #endregion

        #region SINGLETON

        public static LocalizationManager Instance { get; private set; } = null;

        private void Awake()
        {
            Instance = this;
        }

        protected LocalizationManager() { }

        #endregion
    }
}