//   ==============================================================================
//   | Lynx Mixed Reality                                                         |
//   |======================================                                      |
//   | Lynx Gesture Manager                                                       |
//   | First version of a script to interface with XR Hands gesture detection.    |
//   ==============================================================================

using UnityEngine;
using UnityEngine.Events;

namespace Lynx
{
    public class LynxGestureManager : MonoBehaviour
    {
        #region INSPECTOR VARIABLES

        [Header("GameObject References")]
        [Space(10)]

        public GameObject PinchDetector = null;
        public GameObject PalmDetector = null;
        public GameObject ThumbDetector = null;

        [Space(20)]

        [Header("Unity Events")]
        [Space(10)]

        public UnityEvent PinchEvent;
        public UnityEvent PalmEvent;
        public UnityEvent ThumbEvent;

        #endregion

        #region PUBLIC METHODS

        public void PinchDetected()
        {
            Debug.Log("PINCH DETECTED");
            PinchEvent.Invoke();
        }

        public void PalmDetected()
        {
            Debug.Log("PALM DETECTED");
            PalmEvent.Invoke();
        }

        public void ThumbDetected()
        {
            Debug.Log("THUMB DETECTED");
            ThumbEvent.Invoke();
        }

        #endregion

        #region SINGLETON

        public static LynxGestureManager Instance { get; private set; } = null;

        private void Awake()
        {
            Instance = this;
        }

        protected LynxGestureManager() { }

        #endregion
    }
}