using UnityEngine;

namespace IGD {
    public class IGD3DCameraPerson : IGDCameraPerson {
        // constants
        public static readonly Color BG_COLOR = 
            new Color(1f, 1f, 1f);
        public static readonly float NEAR = 0.01f; // in meter (1 cm)
        public static readonly float FAR = 100.0f; // in meter (100 m)
        public static readonly Vector3 HOME_EYE = 
            new Vector3(1.8f, 1.15f, 1.5f);
        public static readonly Vector3 HOME_VIEW = 
            new Vector3(-0.73f, -0.32f, -0.6f);
        public static readonly Vector3 HOME_PIVOT = 
            new Vector3(0.0f, 0.0f, 0.0f);

        // fields
        private Vector3 mPivot = Vector3.zero;
        public Vector3 getPivot() {
            return this.mPivot;
        }
        public void setPivot(Vector3 pivot) {
            this.mPivot = pivot;
        }
        
        // constructor
        public IGD3DCameraPerson() : base("3DCameraPerson") {
        }

        protected override void defineInternalCameraParameters() {
            this.mCamera.orthographic = true;
            this.mCamera.orthographicSize = 1.0f;
            this.mCamera.clearFlags = CameraClearFlags.Color; 
            this.mCamera.backgroundColor = IGD3DCameraPerson.BG_COLOR;
            this.mCamera.cullingMask = 1; // default layer only 

            this.mCamera.nearClipPlane = IGD3DCameraPerson.NEAR;
            this.mCamera.farClipPlane = IGD3DCameraPerson.FAR;
        }

        protected override void defineExternalCameraParameters() {
            this.setEye(IGD3DCameraPerson.HOME_EYE);
            this.setView(IGD3DCameraPerson.HOME_VIEW);
            this.setPivot(IGD3DCameraPerson.HOME_PIVOT);
        }
        
        public float getOrthographicSize() {
            return this.mCamera.orthographicSize;
        }
        
        public void setOrthographicSize(float size) {
            this.mCamera.orthographicSize = size;
        }
    }
}