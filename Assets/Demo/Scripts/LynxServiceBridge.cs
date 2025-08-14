//   ==============================================================================
//   | LYNX HOME (2024)                                                           |
//   |======================================                                      |
//   | LYNX SERVICE BRIDGE SCRIPT                                                 |
//   | A script to manage the bridge between Unity and the LynxUI service.        |
//   ==============================================================================

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Lynx
{
    public class LynxServiceBridge : MonoBehaviour
    {
        private const String LibraryName = "lynxclient-proxy";

        #region DLL IMPORT

        [DllImport(LibraryName)]
        private static extern void DisplayShutdownPopUp();

        [DllImport(LibraryName)]
        private static extern void DisplayRebootPopUp();

        [DllImport(LibraryName)]
        public static extern void DisplayAdvancedSettings();

        [DllImport(LibraryName)]
        public static extern bool Initialize();

        [DllImport(LibraryName)]
        public static extern void DisplayRemovePackagePopUp(string applicationName, string packageName);

        [DllImport(LibraryName)]
        public static extern void RegisterPackageDeleteCb(PackageDeleteCallback callback);

        [DllImport(LibraryName)]
        public static extern void ConfigureAppMenu(Int32 config);

        [DllImport(LibraryName)]
        public static extern void LaunchVirtualDisplayApp(string packageName);

        [Flags]
        public enum AppMenuFlags
        {
            APPMENU_HIDDEN = 0,
            FLAG_SETTINGS_BUTTON = 1,
            FLAG_SCREENSHOT_BUTTON = 2,
            FLAG_RECORD_BUTTON = 4,
            FLAG_FLOOR_BUTTON = 8,
            FLAG_HOME_BUTTON = 16,
            FLAG_ALL = unchecked((int)0xFFFFFFFF)
        }

        public delegate void PackageDeleteCallback(string packageName);

        #endregion

        #region UNITY API

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
           DisableLynxMenu();
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Call this function to configure the app menu.
        /// </summary>
        /// <param name="config">AppMenuFlags from bridge.</param>
        public static void CallConfigureAppMenu(Int32 config)
        {
            ConfigureAppMenu((Int32)config);
        }

        /// <summary>
        /// Call this function to disable the Lynx Menu.
        /// </summary>
        public static void ActiveReturnLynxMenu()
        {
            AppMenuFlags config = AppMenuFlags.FLAG_HOME_BUTTON;
            ConfigureAppMenu((Int32)config);
        }

        /// <summary>
        /// Call this function to disable the Lynx Menu.
        /// </summary>
        public static void DisableLynxMenu()
        {
            AppMenuFlags config = AppMenuFlags.APPMENU_HIDDEN;
            ConfigureAppMenu((Int32)config);
        }

        /// <summary>
        /// Call this function to shutdown your device.
        /// </summary>
        public static void CallDisplayShutdownPopUp()
        {
            DisplayShutdownPopUp();
        }

        /// <summary>
        /// Call this function to reboot your device.
        /// </summary>
        public static void CallRebootPopUp()
        {
            DisplayRebootPopUp();
        }

        /// <summary>
        /// Call this function to open advanced settings.
        /// </summary>
        public static void CallDisplayAdvancedSettings()
        {
            DisplayAdvancedSettings();
        }

        /// <summary>
        /// Call this function to open remove package popup.
        /// </summary>
        /// <param name="applicationName">Application name.</param>
        /// <param name="packageName">Package name.</param>
        public static void CallDisplayRemovePackagePopUp(string applicationName, string packageName)
        {
            DisplayRemovePackagePopUp(applicationName, packageName);
        }

        #endregion
    }
}