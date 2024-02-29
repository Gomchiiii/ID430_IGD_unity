using XGeom;
using System.Collections.Generic;
using UnityEngine;

namespace XAppObject {
    public class XAppPolyline3D : XAppGeom3D {
        // fields
        private float mWidth = float.NaN;
        public float getWidth() {
            return this.mWidth;
        }
        public void setWidth(float width) {
            this.mWidth = width;
            this.refreshRenderer();
        }
        private Color mColor = Color.red; // easily noticeable color
        public Color getColor() {
            return this.mColor;
        }
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        public void setPts(List<Vector3> pts) {
            this.mGeom = new XPolyline3D(pts);
            this.refreshAtGeomChange();
        }

        // constructor
        public XAppPolyline3D(string name, List<Vector3> pts, 
            float width, Color color) : base($"{ name }/Polyline3D") {

            this.mGeom = new XPolyline3D(pts);
            this.mWidth = width;
            this.mColor = color;

            this.refreshAtGeomChange();
        }

        protected override void addComponents() {
            this.mGameObject.AddComponent<LineRenderer>();
            this.mGameObject.AddComponent<SphereCollider>();
        }

        protected override void refreshRenderer() {
            XPolyline3D polyline = (XPolyline3D) this.mGeom;
            LineRenderer lr = this.mGameObject.GetComponent<LineRenderer>();
            lr.useWorldSpace = false;
            lr.alignment = LineAlignment.View; // lines face the camera
            lr.positionCount = polyline.getPts().Count;
            lr.SetPositions(polyline.getPts().ToArray());
            lr.startWidth = this.mWidth;
            lr.endWidth = this.mWidth;
            lr.material = new Material(Shader.Find("Unlit/Color"));
            lr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
            XPolyline3D polyline = (XPolyline3D)this.mGeom;
            Vector3 ctr = polyline.calcCentroid();
            float r = polyline.calcMaxDevFrom(ctr);

            SphereCollider sc = 
                this.mGameObject.GetComponent<SphereCollider>();
            sc.center = ctr;
            sc.radius = r;
        }
    }
}