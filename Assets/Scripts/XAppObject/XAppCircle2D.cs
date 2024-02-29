using XGeom;
using UnityEngine;

namespace XAppObject {
    public class XAppCircle2D : XAppGeom2D {
        // constants
        public static readonly int NUM_SIDE = 64;

        // fields
        public void setRadius(float r) {
            XCircle2D circle = (XCircle2D)this.mGeom;
            this.mGeom = new XCircle2D(r, circle.getPos(), circle.getRot());
            this.refreshAtGeomChange();
        }
        private Color mColor = Color.red; // easily noticeable color
        public void setColor(Color c) {
            this.mColor = c;
            this.refreshRenderer();
        }

        // constructor 
        public XAppCircle2D(string name, float radius, Color color) : 
            base($"{ name }/Circle2D") {

            this.mGeom = new XCircle2D(radius, Vector2.zero,
                Quaternion.identity);
            this.mColor = color;
            this.refreshAtGeomChange();
        }

        protected override void addComponents() {
            this.mGameObject.AddComponent<MeshFilter>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<CircleCollider2D>();
        }

        protected override void refreshRenderer() {
            XCircle2D circle = (XCircle2D)this.mGeom;
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = circle.calcMesh(XAppCircle2D.NUM_SIDE);
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            mr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
            XCircle2D circle = (XCircle2D)this.mGeom;
            CircleCollider2D cc =
                this.mGameObject.GetComponent<CircleCollider2D>();
            cc.radius = circle.getRadius();
        }
    }
}