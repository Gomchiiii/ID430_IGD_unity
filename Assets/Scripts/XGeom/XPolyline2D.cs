using System.Collections.Generic;
using UnityEngine;

namespace XGeom {
    public class XPolyline2D : XGeom2D {
        // fields
        private readonly List<Vector2> mPts = null;
        public List<Vector2> getPts() {
            return this.mPts;
        }

        // constructor 
        public XPolyline2D(List<Vector2> pts) {
            this.mPts = pts;
        }

        // utility methods
        public float calcLength() {
            float length = 0f;
            for (int i = 1; i < this.mPts.Count; i++) {
                length += Vector2.Distance(this.mPts[i - 1], this.mPts[i]);
            }
            return length;
        }
        
        public Vector2 calcCentroid() {
            Vector2 centroid = Vector3.zero;
            int num = this.mPts.Count;
            foreach (Vector2 pt in this.mPts) {
                centroid += pt;
            }
            centroid /= (float)num;
            return centroid;
        }

        public float calcMaxDevFrom(Vector2 fromPt) {
            float maxDev = float.NegativeInfinity;
            foreach (Vector2 pt in this.mPts) {
                float dev = Vector2.Distance(pt, fromPt);
                if (dev > maxDev) {
                    maxDev = dev;
                }
            }
            return maxDev;
        }
    }
}