using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Valve.VR;

namespace ULTRAVR
{


    [HarmonyPatch]
    public class CameraPatches
    {

        public static GameObject DummyCamera, VRCamera, VRPlayer, Dummy2;

        private static readonly string[] canvasesToIgnore =
{
        "com.sinai.unityexplorer_Root", // UnityExplorer.
        "com.sinai.unityexplorer.MouseInspector_Root", // UnityExplorer.
        "IntroCanvas"
    };

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CameraController), "Start")]
        private static void CameraParent(CameraController __instance)
        {
            Logs.WriteInfo($"LLLL: camera reparent");
            if (__instance.transform.parent.name == "Player")
            {
                  DummyCamera = new GameObject("DummyCamera");
                  DummyCamera.transform.parent = __instance.transform.parent;
                  DummyCamera.transform.localPosition = new Vector3(0, -1.4f, 0);
                  DummyCamera.transform.localRotation = Quaternion.identity;
                  __instance.transform.parent = DummyCamera.transform;
                
                VRCamera = __instance.gameObject;
                VRPlayer = __instance.transform.parent.gameObject;


            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(RevolverBeam), "Shoot")]
        private static void RotateGun(RevolverBeam __instance)
        {
            __instance.transform.forward = CameraManager.RightHand.transform.forward;
            __instance.transform.position = CameraManager.RightHand.transform.position;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CameraController), "Update")]
        private static void MoveCamera(CameraController __instance)
        {
          /*  float newx, newz;

            float speed = 4f * Time.deltaTime;
            if (Input.GetKey("1") || Input.GetKey("2"))
                speed = 40f * Time.deltaTime;

            if (Input.GetKey("a"))
            {
                newx = (VRCamera.transform.localRotation * Vector3.right * -speed).x;
                newz = (VRCamera.transform.localRotation * Vector3.right * -speed).z;
                DummyCamera.transform.parent.Translate(newx, 0, newz);
            }

            if (Input.GetKey("d"))
            {
                newx = (VRCamera.transform.localRotation * Vector3.right * speed).x;
                newz = (VRCamera.transform.localRotation * Vector3.right * speed).z;
                DummyCamera.transform.parent.Translate(newx, 0, newz);
            }
            if (Input.GetKey("w"))
            {
                newx = (VRCamera.transform.localRotation * Vector3.forward * speed).x;
                newz = (VRCamera.transform.localRotation * Vector3.forward * speed).z;
                DummyCamera.transform.parent.Translate(newx, 0, newz);
            }

            if (Input.GetKey("s"))
            {
                newx = (VRCamera.transform.localRotation * Vector3.forward * -speed).x;
                newz = (VRCamera.transform.localRotation * Vector3.forward * -speed).z;
                DummyCamera.transform.parent.Translate(newx, 0, newz);
            }
            */

            // snap turn
            if (Input.GetKeyDown("g"))
                    __instance.rotationY -= 45f;
                else if (Input.GetKeyDown("h"))
                    __instance.rotationY += 45f;

           /* if (Input.GetKey("u"))
                VRPlayer.transform.RotateAround(VRCamera.transform.position,VRPlayer.transform.right, -90f * Time.deltaTime);
            else if (Input.GetKey("j"))
                VRPlayer.transform.RotateAround(VRCamera.transform.position, VRPlayer.transform.right, 90f * Time.deltaTime);
          */

            VRCamera.transform.Find("Guns").rotation = CameraManager.RightHand.transform.rotation;
            VRCamera.transform.Find("Guns").localPosition = CameraManager.RightHand.transform.localPosition;
            Logs.WriteInfo($"LLLLLLL {__instance.tag} {VRCamera.transform.eulerAngles}");

        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Movement), "Awake")]
        private static void NewDummy(Movement __instance)
        {
            Dummy2 = new GameObject("DummyMovement");
            Dummy2.transform.parent = __instance.transform.parent;

           
        }
  

            [HarmonyPostfix]
        [HarmonyPatch(typeof(CanvasScaler), "OnEnable")]
        private static void MoveIntroCanvas(CanvasScaler __instance)
        {
            if (IsCanvasToIgnore(__instance.name)) return;


            Logs.WriteInfo($"Hiding Canvas:  {__instance.name}");
            var canvas = __instance.GetComponent<Canvas>();

            //canvas.transform.parent = VRCamera.transform;

            // Canvases with graphic raycasters are the ones that receive click events.
            // Those need to be handled differently, with colliders for the laser ray.
            // if (canvas.GetComponent<GraphicRaycaster>())
            //    AttachedUi.Create<InteractiveUi>(canvas, 0.002f);
            /* canvas.renderMode = RenderMode.WorldSpace;
             canvas.worldCamera.nearClipPlane = .01f;
             canvas.worldCamera.farClipPlane = 50000f;
             canvas.transform.position = new Vector3(44.5747f, 40f, 828f);
             canvas.transform.localPosition = new Vector3(44.5747f, 40f, 828f);
             */

            AttachedUi.Create<StaticUi>(canvas, 0f);
        }

        
     

        private static bool IsCanvasToIgnore(string canvasName)
        {
            foreach (var s in canvasesToIgnore)
                if (Equals(s, canvasName))
                    return true;
            return false;
        }

    }
}


