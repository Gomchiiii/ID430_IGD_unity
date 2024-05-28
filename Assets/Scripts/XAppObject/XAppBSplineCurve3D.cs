using UnityEngine;
using XGeom;
using XGeom.NURBS;

namespace XAppObject {
    public class XAppBSplineCurve3D : XAppCurve3D {
        // constants 
        public static readonly int DEFAULT_NUM_CURVE_SEGS = 20;
        public static readonly float DEFAULT_WIDTH = 0.02f;
        public static readonly Color DEFAULT_COLOR = Color.blue;

        // fields
        private float mWidth = XAppBezierCurve3D.DEFAULT_WIDTH;
        public void setWidth(float width) {
            this.mWidth = width;
            this.refreshRenderer();
        }
        private Color mColor = XAppBezierCurve3D.DEFAULT_COLOR;
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        public void setCPs(Vector4[] cps) {
            XBSplineCurve3D curve = (XBSplineCurve3D)this.mGeom;
            curve.setCPs(cps);
            //this.mGeom = new XBSplineCurve3D(deg, cps);
            this.refreshAtGeomChange();
        }
        
        // constructor 
        public XAppBSplineCurve3D(string name, int deg, double[] U, 
            Vector4[] cps, float width, Color color) : 
            base($"{ name }/BSplineCurve3D") {
            
            this.mGeom = new XBSplineCurve3D(deg, U, cps);
            this.mWidth = width;
            this.mColor = color;

            this.refreshAtGeomChange();
        }
        public XAppBSplineCurve3D(string name, XBSplineCurve3D curve, 
            float width, Color color) : base($"{ name }/BSplineCurve3D") {

            this.mGeom = curve;
            this.mWidth = width;
            this.mColor = color;

            this.refreshAtGeomChange();
        }

        // methods 
        protected override void addComponents() {
            this.mGameObject.AddComponent<LineRenderer>();
        }

        protected override void refreshRenderer() {
            Vector3[] pts = this.samplePts(
                XAppBSplineCurve3D.DEFAULT_NUM_CURVE_SEGS + 1);
            LineRenderer lr = this.mGameObject.GetComponent<LineRenderer>();
            lr.useWorldSpace = false;
            lr.alignment = LineAlignment.View; // lines face the camera.
            lr.positionCount = pts.Length;
            lr.SetPositions(pts);
            lr.startWidth = this.mWidth;
            lr.endWidth = this.mWidth;
            lr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            lr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
        }

        public override Vector3[] samplePts(int numPts) {
            XBSplineCurve3D bc = (XBSplineCurve3D)this.mGeom;

            double us = bc.getStartParam();
            double ue = bc.getEndParam();
            double du = (ue - us) / (double)(numPts - 1);
            
            Vector3[] pts = new Vector3[numPts];
            for (int i = 0; i < numPts; i++) {
                double u = us + (double)i * du;
                Vector3 pt = bc.calcPos(u);
                pts[i] = pt;
            }
            return pts;
        }
    }
}