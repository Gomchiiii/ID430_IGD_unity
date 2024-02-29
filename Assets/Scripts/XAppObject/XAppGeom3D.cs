using XGeom;
using UnityEngine;

namespace XAppObject {
    public abstract class XAppGeom3D : XAppObject3D {
        // fields
        protected XGeom3D mGeom = null;
        public XGeom3D getGeom() {
            return this.mGeom;
        }
        public void setGeom(XGeom3D geom) {
            this.mGeom = geom;
            this.refreshAtGeomChange();
        }
        public Collider getCollider() {
            Collider collider = this.mGameObject.GetComponent<Collider>();
            return collider;
        }

        // constructor
        public XAppGeom3D(string name) : base(name) {
        }

        // methods
        public void refreshAtGeomChange() {
            this.refreshRenderer();
            this.refreshCollider();
        }
        protected abstract void refreshRenderer();
        protected abstract void refreshCollider();
    }
}