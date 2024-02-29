using UnityEngine;
using XAppObject;

namespace XPlot {
    public class XPlotDot2D : XAppNoGeom2D {
        // constants
        private static readonly float DEFAULT_SIZE = 10f;
        
        // fields
        private float mSize = DEFAULT_SIZE; // diameter of the dot
        public float getSize() {
            return this.mSize;
        }
        public void setSize(float size) {
            this.mDisc.setRadius(size / 2f);
        }
        
        private XAppCircle2D mDisc = null;
        
        // constructor
        public XPlotDot2D(string name, Vector2 pos, float size, Color color)
            : base($"{name}/Dot2D") {
            this.mSize = size;
            this.mDisc = new XAppCircle2D(name, size / 2f, color);
            this.addChild(this.mDisc);
            this.setPosition(pos);
        }
    }
}