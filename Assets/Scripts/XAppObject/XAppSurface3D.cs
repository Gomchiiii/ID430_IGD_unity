using UnityEngine;

namespace XAppObject {
    public abstract class XAppSurface3D : XAppGeom3D {
        // constructor
        public XAppSurface3D(string name) : base(name) {
        }

        // methods
        public abstract Vector3[,] samplePts(int numPtsU, int numPtsV);
        public abstract Mesh calcMesh(int numSegsU, int numSegsV);
    }
}