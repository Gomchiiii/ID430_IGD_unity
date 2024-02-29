using UnityEngine;

namespace XGeom {
    public class XLine2D : XGeom2D {
        // fields
        private readonly Vector2 mPt0 = XGeom.VECTOR2_NAN;
        public Vector2 getPt0() {
            return this.mPt0;
        }
        private readonly Vector2 mPt1 = XGeom.VECTOR2_NAN;
        public Vector2 getPt1() {
            return this.mPt1;
        }

        // constructor
        public XLine2D(Vector2 pt0, Vector2 pt1) {
            this.mPt0 = pt0;
            this.mPt1 = pt1;
        }
    }
}