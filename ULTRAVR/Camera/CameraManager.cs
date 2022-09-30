using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Valve.VR;



namespace ULTRAVR
{
    public static class CameraManager
    {

        public static float NearClipPlaneDistance = 0.01f;
        public static float FarClipPlaneDistance = 59999f;
        public static bool DisableParticles = false;

        public static Transform OriginalCameraParent = null;
        public static GameObject VROrigin;
        public static GameObject LeftHand = null;
        public static GameObject RightHand = null;

        public static bool RightHandGrab = false;
        public static bool LeftHandGrab = false;

        public static float InitialHandDistance = 0f;
        public static Vector3 ZoomOrigin = Vector3.zero;
        public static float SpeedScalingFactor = 1f;
        public static Vector3 PreviousVelocityPosition = Vector3.zero;

        static CameraManager()
        {

            VROrigin = new GameObject();
        }


        public static void AddSkyBox()
        {/*
            // ADD THE LOADED SKYBOX !!!!
            var SceneSkybox = GameObject.Instantiate(AssetLoader.Skybox, Vector3.zeroVector, Quaternion.identityQuaternion);
            SceneSkybox.transform.localScale = new Vector3(999999, 999999, 999999);
            SceneSkybox.transform.eulerAngles = new Vector3(270, 0, 0);
               */
        }

        public static void SwitchPOV()
        {/*
            Logs.WriteInfo("Entered SwitchPOV function");

            // ADD A SKYBOX

            Camera OriginalCamera = Game.GetCamera();
            // If we are not in firstperson
            if (CameraManager.CurrentCameraMode != CameraManager.VRCameraMode.FirstPerson)
            {
                Logs.WriteInfo("Got past cameramod check");
                if (Game.Instance.Player.MainCharacter != null)
                {
                    Logs.WriteInfo("Got past maincharacter exist check");
                    // switch to first person
                    VROrigin.transform.parent = null;
                    VROrigin.transform.position = Game.Instance.Player.MainCharacter.Value.GetPosition();

                    if (!OriginalCameraParent)
                    {
                        OriginalCameraParent = OriginalCamera.transform.parent;
                    }

                    OriginalCamera.transform.parent = VROrigin.transform;
                    if (RightHand)
                        RightHand.transform.parent = VROrigin.transform;
                    if (LeftHand)
                        LeftHand.transform.parent = VROrigin.transform;
                    CameraManager.CurrentCameraMode = CameraManager.VRCameraMode.FirstPerson;
                }

            }
            else
            {
                VROrigin.transform.position = OriginalCameraParent.position;
                VROrigin.transform.rotation = OriginalCameraParent.rotation;
                VROrigin.transform.localScale = OriginalCameraParent.localScale;

                VROrigin.transform.parent = OriginalCameraParent;

                CameraManager.CurrentCameraMode = CameraManager.VRCameraMode.DemeoLike;
            }
            */
        }

        public static void SpawnHands()
        {
            Logs.WriteInfo($"LLLL: SpawnHands");

            if (!RightHand)
            {
                RightHand = GameObject.Instantiate(AssetLoader.RightHandBase, new Vector3(0,1f,0), Quaternion.identityQuaternion);
                RightHand.transform.parent = CameraPatches.DummyCamera.transform;
            }
            if (!LeftHand)
            {
                LeftHand = GameObject.Instantiate(AssetLoader.LeftHandBase, new Vector3(0, 1f, 0), Quaternion.identityQuaternion);
                LeftHand.transform.parent = CameraPatches.DummyCamera.transform;
            }
            Logs.WriteInfo($"LLLL: Exit SpawnHands");
        }

       


    }

}