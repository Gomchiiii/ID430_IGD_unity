using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XGeom {
    public abstract class XParametricCurve3D : XCurve3D { 
        public abstract double getStartParam();
        public abstract double getEndParam();

        //calculate the position at parameter u 
        public abstract Vector3 calcPos(double u);
        public Vector3 calcStartPos()
        {
            double u = this.getStartParam();
            Vector3 p = calcPos(u);
            return p;
        }
        public Vector3 calcEndPos()
        {
            double u = this.getEndParam();
            Vector3 p = calcPos(u);
            return p;
        }

        //calculate the derivate of order at parameter u 
        //C(u) if order is 0. C'(u) if order is 1 c''(u) if order is 2.
        public Vector3 calcDer(int order, double u) {
            Vector3[] ds = this.calcDers(order, u);
            return ds[order];
        }

        //calculate all the derivates of order a parameter u 
        //c(u) if order si 0. {Cu, C'u} if order is 1.
        //{Cu, C'u, C''u{ if order is 2.
        public abstract Vector3[] calcDers(int order, double u);
        public Vector3 calcTan(double u) {
            Vector3 t = this.calcDer(1, u);
            if (t.magnitude == 0.0)
            {
                return XGeom.VECTOR3_NAN;
            }
            t.Normalize();
            return t;
        }

    }
}
