using XGeom;
using UnityEngine;

namespace XAppObject {
    public abstract class XAppGeom2D : XAppObject2D {
        // fields
        protected XGeom2D mGeom = null;
        public XGeom2D getGeom() {
            return this.mGeom;
        }
        public void setGeom(XGeom2D geom) {
            this.mGeom = geom;
            this.refreshAtGeomChange();
        }
        public Collider2D getCollider() {
            Collider2D collider = this.mGameObject.GetComponent<Collider2D>();
            return collider;
        }

        // constructor
        public XAppGeom2D(string name) : base(name) {
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