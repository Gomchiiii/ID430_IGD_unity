using UnityEngine;

namespace XGeom {
    public class XLine3D : XGeom3D {
        // fields
        private readonly Vector3 mPt0 = XGeom.VECTOR3_NAN;
        public Vector3 getPt0() {
            return this.mPt0;
        }
        private readonly Vector3 mPt1 = XGeom.VECTOR3_NAN;
        public Vector3 getPt1() {
            return this.mPt1;
        }

        // constructor
        public XLine3D(Vector3 pt0, Vector3 pt1) {
            this.mPt0 = pt0;
            this.mPt1 = pt1;
        }
    }
}