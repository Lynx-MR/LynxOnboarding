using UnityEngine;

namespace Lynx
{
    public class EnableLynxMenu : MonoBehaviour
    {
        private void Start()
        {
            LynxServiceBridge.ActiveReturnLynxMenu();
        }
    }
}

