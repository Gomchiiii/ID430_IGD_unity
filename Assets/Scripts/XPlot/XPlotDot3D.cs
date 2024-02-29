using UnityEngine;
using XAppObject;

namespace XPlot {
    public class XPlotDot3D : XAppNoGeom3D {
        // constants
        private static readonly float DEFAULT_SIZE = 0.04f;
        
        // fields
        private float mSize = DEFAULT_SIZE; // diameter of the dot
        public float getSize() {
            return this.mSize;
        }
        public void setSize(float size) {
            this.mCube.setWidth(size);
        }
        
        private XAppCube3D mCube = null;
        
        // constructor
        public XPlotDot3D(string name, Vector3 pos, float size, Color color)
            : base($"{name}/Dot3D") {
            this.mSize = size;
            this.mCube = new XAppCube3D(name, size, color);
            this.addChild(this.mCube);
            this.setPosition(pos);
        }
    }
}