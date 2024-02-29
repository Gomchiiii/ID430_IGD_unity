using System.Collections.Generic;
using UnityEngine;
using XAppObject;
using XGeom;

namespace XPlot {
    public class XPlotDashedPolyline3D : XAppPolyline3D {
        // constants
        private static readonly float DEFAULT_DASH_LENGTH = 0.06f;
        
        // fields
        private float mDashLength = DEFAULT_DASH_LENGTH; // 0 means solid line
        public void setDashLength(float dashLength) {
            this.mDashLength = dashLength;
            this.refreshRenderer();
        }
        private Texture2D mTexture;
        
        // constructor
        public XPlotDashedPolyline3D(string name, List<Vector3> points,
            float width, float dashLength, Color color) : base(
            $"{name}/DashedPolyline3D", points, width, color) {
            this.mDashLength = dashLength;
            this.mTexture = this.createTexture();
            
            this.refreshAtGeomChange();
        }
        
        // methods
        private Texture2D createTexture() {
            Texture2D tex = new Texture2D(2, 1);
            tex.SetPixel(0, 0, this.getColor());
            tex.SetPixel(1, 0, Color.clear);
            tex.Apply();
            tex.wrapMode = TextureWrapMode.Mirror;
            tex.filterMode = FilterMode.Point;
            return tex;
        }
        
        protected override void refreshRenderer() {
            XPolyline3D polyline = (XPolyline3D)this.mGeom;
            LineRenderer lr = this.mGameObject.GetComponent<LineRenderer>();
            lr.useWorldSpace = false;
            lr.alignment = LineAlignment.View; // lines face the camera.
            lr.positionCount = polyline.getPts().Count;
            lr.SetPositions(polyline.getPts().ToArray());
            lr.startWidth = this.getWidth();
            lr.endWidth = this.getWidth();
            lr.material = new Material(Shader.Find("Unlit/Transparent"));
            lr.material.mainTexture = this.mTexture;
            float lineLength = polyline.calcLength();
            float texRepeat = (this.mDashLength == 0f) ? 0f: 
                Mathf.Ceil(lineLength / this.mDashLength);
            lr.material.mainTextureScale = new Vector2(texRepeat, 1f);
        }
    }
}