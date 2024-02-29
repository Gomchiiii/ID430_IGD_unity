using UnityEngine;
using XGeom;

namespace XAppObject {
    public class XAppLine3D : XAppGeom3D {
        // constants
        public static readonly float DEFAULT_WIDTH = 0.02f;
        public static readonly Color DEFAULT_COLOR = Color.red;

        // fields
        private float mWidth = XAppLine3D.DEFAULT_WIDTH;
        public float getWidth() {
            return this.mWidth;
        }
        public void setWidth(float width) {
            this.mWidth = width;
            this.refreshRenderer();
        }
        private Color mColor = XAppLine3D.DEFAULT_COLOR;
        public Color getColor() {
            return this.mColor;
        }
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        public void setPts(Vector3 pt0, Vector3 pt1) {
            this.mGeom = new XLine3D(pt0, pt1);
            this.refreshAtGeomChange();
        }

        // constructor 
        public XAppLine3D(string name, Vector3 pt0, Vector3 pt1, 
            float width, Color color) : base($"{ name }/Line3D") {
            
            this.mGeom = new XLine3D(pt0, pt1);
            this.mWidth = width;
            this.mColor = color;

            this.refreshAtGeomChange();
        }
        public XAppLine3D(string name, XLine3D line, float width, 
            Color color) : base($"{ name }/Line3D") {

            this.mGeom = line;
            this.mWidth = width;
            this.mColor = color;

            this.refreshAtGeomChange();
        }

        // methods 
        protected override void addComponents() {
            this.mGameObject.AddComponent<LineRenderer>();
            this.mGameObject.AddComponent<SphereCollider>();
        }

        protected override void refreshRenderer() {
            XLine3D line = (XLine3D) this.mGeom;
            LineRenderer lr = this.mGameObject.GetComponent<LineRenderer>();
            lr.useWorldSpace = false;
            lr.alignment = LineAlignment.View; // lines face the camera.
            lr.positionCount = 2;
            lr.SetPosition(0, line.getPt0());
            lr.SetPosition(1, line.getPt1());
            lr.startWidth = this.mWidth;
            lr.endWidth = this.mWidth;
            lr.material = new Material(Shader.Find("Unlit/Color"));
            lr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
            XLine3D line = (XLine3D) this.mGeom;
            Vector3 ctr = 0.5f * (line.getPt0() + line.getPt1());
            float r = 0.5f * Vector3.Distance(line.getPt0(), line.getPt1());

            SphereCollider sc =
                this.mGameObject.GetComponent<SphereCollider>();
            sc.center = ctr;
            sc.radius = r;
        }
    }
}