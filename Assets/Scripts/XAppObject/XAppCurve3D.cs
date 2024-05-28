using UnityEngine;

namespace XAppObject {
    public abstract class XAppCurve3D : XAppGeom3D {
        // constructor
        public XAppCurve3D(string name) : base(name) {
        }

        // methods
        public abstract Vector3[] samplePts(int numPts);
    }
}