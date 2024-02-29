using XAppObject;
using UnityEngine;

namespace IGD {
    public class IGDCursor2D : XAppCircle2D {
        // constants
        public static readonly float RADIUS = 2.0f;
        //public static readonly Color COLOR = Color.clear;
        public static readonly Color COLOR = Color.red;

        // fields
        private IGDApp mApp = null;
        private IGD2DCameraPerson m2DCameraPerson = null;

        // constructor 
        public IGDCursor2D(IGDApp app) : base("Cursor2D", IGDCursor2D.RADIUS,
            IGDCursor2D.COLOR) {
            this.mApp = app;
            this.m2DCameraPerson = this.mApp.get2DCameraPerson();
        }

        // methods
        public void setScreenPos(Vector2 pt) {
            Vector3 cameraPos =
                this.m2DCameraPerson.getCamera().transform.position;
            Vector3 offset = new Vector3(-Screen.width / 2.0f + cameraPos.x,
                -Screen.height / 2.0f + cameraPos.y, 0f);
            this.mGameObject.transform.position = (Vector3)pt + offset;
        }
        
        public bool intersects(XAppGeom2D appGeom) {
            return this.getCollider().IsTouching(appGeom.getCollider());
        }

        public bool hits(XAppGeom3D appGeom3D) {
            Vector2 ctr = this.mGameObject.transform.position;

            IGD3DCameraPerson cp = this.mApp.get3DCameraPerson();
            Ray ray = cp.getCamera().ScreenPointToRay(ctr);
            RaycastHit hit;
            Collider collider = appGeom3D.getCollider();
            if (collider.Raycast(ray, out hit, Mathf.Infinity)) {
                //IGDUtil.createDebugSphere(hit.point);
                return true;
            } else {
                return false;
            }
        }

        public RaycastHit calcHit(XAppGeom3D appGeom3D) {
            Vector2 ctr = this.mGameObject.transform.position;

            IGD3DCameraPerson cp = this.mApp.get3DCameraPerson();
            Ray ray = cp.getCamera().ScreenPointToRay(ctr);
            RaycastHit hit;
            Collider collider = appGeom3D.getCollider();
            collider.Raycast(ray, out hit, Mathf.Infinity);
            return hit;
        }
    }
}