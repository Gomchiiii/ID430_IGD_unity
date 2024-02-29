using UnityEngine;

namespace XGeom {
    public class XCube3D : XGeom3D {
        // fields
        private readonly float mWidth = float.NaN;
        public float getWidth() {
            return this.mWidth;
        }
        private readonly Vector3 mPos = Vector3.zero;
        public Vector3 getPos() {
            return this.mPos;
        }
        private readonly Quaternion mRot = Quaternion.identity;
        public Quaternion getRot() {
            return this.mRot;
        }
        
        // constructor
        public XCube3D(float width, Vector3 pos, Quaternion rot) {
            this.mWidth = width;
            this.mPos = pos;
            this.mRot = rot;
        }
        
        // methods
        public Vector3 calcXDir() {
            return this.mRot * Vector3.right;
        }
        public Vector3 calcYDir() {
            return this.mRot * Vector3.up;
        }
        public Vector3 calcZDir() {
            return this.mRot * Vector3.forward;
        }
    }
}