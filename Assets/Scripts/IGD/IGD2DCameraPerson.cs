using UnityEngine;

namespace IGD {
    public class IGD2DCameraPerson : IGDCameraPerson {
        // constants
        public static readonly float NEAR = 0.1f; // in meter (10 cm)
        public static readonly float FAR = 2.0f; // in meter (2 m)
        public static readonly float SCREEN_CAMERA_DIST = 1.0f;

        // fields
        private float mScreenWidth = float.NaN;
        private float mScreenHeight = float.NaN;

        // constructor
        public IGD2DCameraPerson() : base("2DCameraPerson") {
        }

        protected override void defineInternalCameraParameters() {
            this.mCamera.orthographic = true;
            this.mCamera.depth = 1.0f; // rendering order 
            this.mCamera.clearFlags = CameraClearFlags.Depth; // depth buffer
            this.mCamera.cullingMask = 32; // 100000. UI layer only.

            this.mCamera.nearClipPlane = IGD2DCameraPerson.NEAR;
            this.mCamera.farClipPlane = IGD2DCameraPerson.FAR;
        }

        protected override void defineExternalCameraParameters() {
            this.update();
        }

        public void update() {
            if (Screen.width != this.mScreenWidth ||
                Screen.height != this.mScreenHeight) {

                // update the screen size.
                this.mScreenWidth = Screen.width;
                this.mScreenHeight = Screen.height;

                // updates the screen camera.
                this.mCameraRig.getGameObject().transform.position =
                    new Vector3(0f, 0f, -IGD2DCameraPerson.SCREEN_CAMERA_DIST);
                this.mCamera.orthographicSize = this.mScreenHeight / 2.0f;
            }
        }
    }
}