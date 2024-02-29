using UnityEngine;
using XGeom;

namespace XAppObject {
    public class XAppLine2D : XAppGeom2D {
        // constants
        public static readonly float DEFAULT_WIDTH = 2f;
        public static readonly Color DEFAULT_COLOR = Color.red;

        // fields
        private float mWidth = XAppLine2D.DEFAULT_WIDTH;
        public float getWidth() {
            return this.mWidth;
        }
        public void setWidth(float width) {
            this.mWidth = width;
            this.refreshRenderer();
        }
        private Color mColor = XAppLine2D.DEFAULT_COLOR;
        public Color getColor() {
            return this.mColor;
        }
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        public void setPts(Vector2 pt0, Vector2 pt1) {
            this.mGeom = new XLine2D(pt0, pt1);
            this.refreshAtGeomChange();
        }

        // constructor 
        public XAppLine2D(string name, Vector2 pt0, Vector2 pt1, 
            float width, Color color) : base($"{ name }/Line2D") {
            
            this.mGeom = new XLine2D(pt0, pt1);
            this.mWidth = width;
            this.mColor = color;

            this.refreshAtGeomChange();
        }
        public XAppLine2D(string name, XLine2D line, float width, 
            Color color) : base($"{ name }/Line2D") {

            this.mGeom = line;
            this.mWidth = width;
            this.mColor = color;

            this.refreshAtGeomChange();
        }

        // methods 
        protected override void addComponents() {
            this.mGameObject.AddComponent<LineRenderer>();
            this.mGameObject.AddComponent<EdgeCollider2D>();
        }

        protected override void refreshRenderer() {
            XLine2D line = (XLine2D) this.mGeom;
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
            XLine2D line = (XLine2D)this.mGeom;
            EdgeCollider2D ec =
                this.mGameObject.GetComponent<EdgeCollider2D>();
            Vector2[] pts = new Vector2[2];
            pts[0] = line.getPt0();
            pts[1] = line.getPt1();
            ec.points = pts;
        }
    }
}