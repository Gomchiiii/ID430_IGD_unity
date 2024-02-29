using XAppObject;
using UnityEngine;

namespace IGD {
    public abstract class IGDCameraPerson {
        // fields
        protected XAppNoGeom3D mCameraRig = null;
        public XAppNoGeom3D getCameraRig() {
            return this.mCameraRig;
        }
        protected Camera mCamera = null;
        public Camera getCamera() {
            return this.mCamera;
        }

        // contructor
        public IGDCameraPerson(string name) {
            this.mCameraRig = new XAppNoGeom3D($"{name}/CameraRig");
            this.getCameraRig().getGameObject().AddComponent<Camera>();
            this.mCamera = 
                this.getCameraRig().getGameObject().GetComponent<Camera>();
            this.defineInternalCameraParameters(); // camera attributes 
            this.defineExternalCameraParameters(); // camera rig attributes
        }

        protected abstract void defineInternalCameraParameters();
        protected abstract void defineExternalCameraParameters();

        // utility methods
        public Vector3 getEye() {
            return this.mCameraRig.getGameObject().transform.position;
        }
        public void setEye(Vector3 eye) {
            this.mCameraRig.getGameObject().transform.position = eye;
        }
        public Vector3 getView() {
            return this.mCameraRig.getGameObject().transform.forward;
        }
        public void setView(Vector3 view) {
            this.mCameraRig.getGameObject().transform.rotation =
                Quaternion.LookRotation(view, Vector3.up);
        }
        public Vector3 getUp() {
            return this.mCameraRig.getGameObject().transform.up;
        }
        public Vector3 getRight() {
            return this.mCameraRig.getGameObject().transform.right;
        }
    }
}