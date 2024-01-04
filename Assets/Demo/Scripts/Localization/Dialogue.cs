//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | A class for defining a dialogue, including an ID for the Localization      |
//   | table, a trigger for animations and an audio clip for the track to be      |
//   | played.                                                                    |
//   ==============================================================================

using System;
using UnityEngine;

namespace Lynx.Onboarding
{
    [Serializable]
    public class Dialogue
    {
        [SerializeField, Tooltip("Select the dialogue ID of the Localization table.")]
        public string id;
        [SerializeField, Tooltip("Select the name of the trigger parameter you want to use for body animation.")]
        public string bodyAnimation;
        [SerializeField, Tooltip("Select the name of the trigger parameter you want to use for visage animation.")]
        public string visageAnimation;
        [SerializeField, Tooltip("Select the audio you want to play during the dialogue.")]
        public AudioClip audio;

        /// <summary>
        /// Dialogue constructor.
        /// </summary>
        /// <param name="id">Select the dialogue ID of the Localization table.</param>
        /// <param name="triggerBod">Select the name of the trigger parameter you want to use for body animation."</param>
        /// <param name="triggerVis">Select the name of the trigger parameter you want to use for visage animation."</param>
        /// <param name="audio">Select the audio you want to play during the dialogue."</param>
        public Dialogue(string id, string triggerBod, string triggerVis, AudioClip audio)
        {
            this.id = id;
            this.bodyAnimation = triggerBod;
            this.visageAnimation = triggerVis;
            this.audio = audio;
        }

        /// <summary>
        /// Dialogue constructor.
        /// </summary>
        /// <param name="id">Select the dialogue ID of the Localization table.</param>
        /// <param name="trigger">Select the name of the trigger parameter you want to use for animation."</param>
        /// <param name="audio">Select the audio you want to play during the dialogue."</param>
        public Dialogue(string id, string trigger, AudioClip audio)
        {
            this.id = id;
            this.bodyAnimation = trigger;
            this.audio = audio;
        }

        /// <summary>
        /// Dialogue constructor.
        /// </summary>
        /// <param name="id">Select the dialogue ID of the Localization table.</param>
        /// <param name="trigger">Select the name of the trigger parameter you want to use for animation."</param>
        public Dialogue(string id, string trigger)
        {
            this.id = id;
            this.bodyAnimation = trigger;
            this.audio = null;
        }

        /// <summary>
        /// Dialogue constructor.
        /// </summary>
        /// <param name="id">Select the dialogue ID of the Localization table.</param>
        public Dialogue(string id)
        {
            this.id = id;
            this.bodyAnimation = null;
            this.audio = null;
        }

        /// <summary>
        /// Dialogue constructor.
        /// </summary>
        public Dialogue()
        {
            this.id = null;
            this.bodyAnimation = null;
            this.audio = null;
        }
    }
}