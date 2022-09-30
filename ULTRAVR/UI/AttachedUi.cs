using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ULTRAVR
{
    class AttachedUi : MonoBehaviour
    {
        private Transform targetTransform;

        public static void Create<TAttachedUi>(Canvas canvas, float scale = 0)
            where TAttachedUi : AttachedUi
        {
            var instance = canvas.gameObject.AddComponent<TAttachedUi>();
              if (scale > 0) canvas.transform.localScale = Vector3.one * scale;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
        protected virtual void Start()
        {
            transform.Rotate(0, 180f, 0);
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
              transform.position = Camera.main.transform.position + Camera.main.transform.forward;
              transform.rotation = Camera.main.transform.rotation;
        }
    }
}
