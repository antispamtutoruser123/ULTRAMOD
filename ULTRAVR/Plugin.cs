using BepInEx;
using HarmonyLib;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Valve.VR;

namespace ULTRAVR
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "com.NoSmoke.VRMods.UEBSVR";
        public const string PLUGIN_NAME = "UEBSVR";
        public const string PLUGIN_VERSION = "0.0.1";

        public static string gameExePath = Process.GetCurrentProcess().MainModule.FileName;
        public static string gamePath = Path.GetDirectoryName(gameExePath);

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            new AssetLoader();

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Logs.WriteInfo($"LLLL: Calling InitSteamVR");
            InitSteamVR();
        }
        private static void InitSteamVR()
        {
            Logs.WriteInfo($"LLLL: Calling SteamVR.Initialize");
            SteamVR.Initialize(true);

            SteamVR_Settings.instance.pauseGameWhenDashboardVisible = true;

            // INPUT TEST
            SteamVR_Actions._default.RTrigger.AddOnStateDownListener(TriggerRightDown, SteamVR_Input_Sources.Any);
            SteamVR_Actions._default.LTrigger.AddOnStateDownListener(TriggerLeftDown, SteamVR_Input_Sources.Any);
            SteamVR_Actions._default.GrabRight.AddOnStateDownListener(GrabRightDown, SteamVR_Input_Sources.Any);
            SteamVR_Actions._default.GrabRight.AddOnStateUpListener(GrabRightUp, SteamVR_Input_Sources.Any);
            SteamVR_Actions._default.GrabLeft.AddOnStateDownListener(GrabLeftDown, SteamVR_Input_Sources.Any);
            SteamVR_Actions._default.GrabLeft.AddOnStateUpListener(GrabLeftUp, SteamVR_Input_Sources.Any);

            SteamVR_Actions._default.Right_pose.AddOnUpdateListener(SteamVR_Input_Sources.Any, UpdateRightHand);
            SteamVR_Actions._default.Left_pose.AddOnUpdateListener(SteamVR_Input_Sources.Any, UpdateLeftHand);


            SteamVR_Actions._default.Menu.AddOnStateDownListener(OnMenuDown, SteamVR_Input_Sources.Any);
            SteamVR_Actions._default.Menu.AddOnStateUpListener(OnMenuUp, SteamVR_Input_Sources.Any);

            SteamVR_Actions._default.InteractUI.AddOnStateDownListener(OnUIDown, SteamVR_Input_Sources.Any);
            SteamVR_Actions._default.InteractUI.AddOnStateUpListener(OnUIUp, SteamVR_Input_Sources.Any);
        }

        // INPUT TEST

        public static void OnMenuDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            //  CameraManager.SwitchPOV();
            Logs.WriteInfo($"LLLL: Calling CameraManager.SpawnHands");
            CameraManager.SpawnHands();
        }
        public static void OnMenuUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            Logs.WriteInfo("SwitchPOVButton is UP");
        }

        public static void OnUIDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            //  CameraManager.SwitchPOV();
            Logs.WriteInfo($"LLLL: Calling CameraManager.SpawnHands");
            CameraManager.SpawnHands();
        }
        public static void OnUIUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            Logs.WriteInfo("SwitchPOVButton is UP");
        }


        public static void TriggerLeftDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            float speed = 4f * Time.deltaTime;
            Logs.WriteInfo("TriggerLeft is Down");
            CameraPatches.DummyCamera.transform.parent.RotateAround(Camera.main.transform.position, Vector3.up, -30f*Time.deltaTime);

        }
        public static void TriggerRightDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            float speed = 4f * Time.deltaTime;
            Logs.WriteInfo("TriggerLeft is Down");
            CameraPatches.DummyCamera.transform.parent.RotateAround(Camera.main.transform.position, Vector3.up, 30f * Time.deltaTime);

        }
        public static void GrabRightDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            CameraManager.RightHandGrab = true;
            Logs.WriteInfo($"LLLL: Calling CameraManager.SpawnHands");
            CameraManager.SpawnHands();
        }

        public static void GrabRightUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            CameraManager.RightHandGrab = false;
        }

        public static void GrabLeftDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            CameraManager.LeftHandGrab = true;

        }

        public static void GrabLeftUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            CameraManager.LeftHandGrab = false;
        }

        public static void UpdateRightHand(SteamVR_Action_Pose fromAction, SteamVR_Input_Sources fromSource)
        {
            if (CameraManager.RightHand)
            {
                CameraManager.RightHand.transform.localPosition = SteamVR_Actions._default.Right_pose.localPosition;
            }

        }

        public static void UpdateLeftHand(SteamVR_Action_Pose fromAction, SteamVR_Input_Sources fromSource)
        {
            if (CameraManager.LeftHand)
            {
                CameraManager.LeftHand.transform.localPosition = SteamVR_Actions._default.Left_pose.localPosition;
            }
        }
    }
}

