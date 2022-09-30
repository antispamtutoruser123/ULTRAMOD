using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAVR
{
    class AttachedCamera : MonoBehaviour
    {
        private Transform targetTransform;
        float delta, prev;

        public static void Create<TAttachedCamera>(GameObject punch, float scale = 0)
            where TAttachedCamera : AttachedCamera
        {
            var instance = punch.AddComponent<TAttachedCamera>();

        }
        protected virtual void Start()
        {
           prev = Camera.main.transform.eulerAngles.y;
            delta = 0;
        }
        protected virtual void Update()
        {
            UpdateTransform();
        }

        public void SetTargetTransform(Transform target)
        {
            targetTransform = target;
        }

        private void UpdateTransform()
        {
            delta = Camera.main.transform.localRotation.eulerAngles.y-prev;

            CameraPatches.DummyCamera.transform.parent.eulerAngles = new Vector3(Camera.main.transform.localRotation.eulerAngles.x, Camera.main.transform.localRotation.eulerAngles.y, 0);
           
           // CameraPatches.DummyCamera.transform.eulerAngles = new Vector3(0, 0, 0);
            //CameraPatches.DummyCamera.transform.RotateAround(Camera.main.transform.position, Vector3.up,delta*.5f);
            prev = Camera.main.transform.localRotation.eulerAngles.y;

            CameraPatches.DummyCamera.transform.position = CameraPatches.VRPlayer.transform.position;
        }
    }
}

