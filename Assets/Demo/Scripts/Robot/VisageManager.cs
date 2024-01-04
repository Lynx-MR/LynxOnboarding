//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to list the R1 visage textures.                        |
//   ==============================================================================

using System.Collections.Generic;
using UnityEngine;

namespace Lynx.Onboarding
{
    public class VisageManager : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [SerializeField] private List<Texture2D> visages = new List<Texture2D>();
        [SerializeField] private GameObject visage = null;

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to set the visage texture frim index value.
        /// </summary>
        /// <param name="imageIndex">The index value.</param>
        public void SetVisage(int imageIndex)
        {
            if (imageIndex > 0 && imageIndex < visages.Count - 1)
            {
                visage.GetComponent<Renderer>().material.SetTexture("_EmissionMap", visages[imageIndex]);
                visage.GetComponent<Renderer>().material.SetTexture("_BaseMap", visages[imageIndex]);
            }
            else return;
        }

        #endregion
    }
}