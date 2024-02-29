using UnityEngine;
using XGeom;
using XAppObject;

namespace XPlot {
    public class XPlotDashedLine2D : XAppLine2D {
        // constants
        private static readonly float DEFAULT_DASH_LENGTH = 6f;

        // fields
        private float mDashLength = DEFAULT_DASH_LENGTH; // 0 means solid line
        public void setDashLength(float dashLength) {
            this.mDashLength = dashLength;
            this.refreshRenderer();
        }
        private Texture2D mTexture;

        // constructor
        public XPlotDashedLine2D(string name, Vector2 start, Vector2 end,
            float width, float dashLength, Color color) : base(
            $"{name}/DashedLine3D", start, end, width, color) {
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
            XLine2D line = (XLine2D)this.mGeom;
            LineRenderer lr = this.mGameObject.GetComponent<LineRenderer>();
            lr.useWorldSpace = false;
            lr.alignment = LineAlignment.View; // lines face the camera.
            lr.positionCount = 2;
            lr.SetPosition(0, line.getPt0());
            lr.SetPosition(1, line.getPt1());
            lr.startWidth = this.getWidth();
            lr.endWidth = this.getWidth();
            lr.material = new Material(Shader.Find("Unlit/Transparent"));
            lr.material.mainTexture = this.mTexture;
            float lineLength = Vector2.Distance(line.getPt0(), line.getPt1());
            float texRepeat = (this.mDashLength == 0f) ? 0f: 
                Mathf.Ceil(lineLength / this.mDashLength);
            lr.material.mainTextureScale = new Vector2(texRepeat, 1f);
        }
    }
}