//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Onboarding                                                            |
//   | This script is used to correct sound deterioration in Android.             |
//   ==============================================================================

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Lynx.Onboarding
{
    public class AutoSetupInteractionManager : MonoBehaviour
    {
        [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable xRGrabInteractable = null;

        // Start is called before the first frame update
        void Start()
        {
            xRGrabInteractable.interactionManager = FindObjectOfType<XRInteractionManager>();
        }
    }
}