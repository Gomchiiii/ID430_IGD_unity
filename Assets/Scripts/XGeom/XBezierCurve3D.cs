using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xgeom.NURBS;
using XGeom;
using XGeom.NURBS;

namespace XGeorm.NURBS {
    public class XBezierCurve3D : XParametricCurve3D {
     //FIELDS
        private int mDeg = int.MinValue;
        public int getDeg()
        {
            return mDeg;
        }

        // n+1 4d Ccontrol points 
        //wehere n is degree

        private Vector4[] mCPs = null;
        public Vector4[] getCPs()
        {
            return mCPs;
        }

        public void setCPs(Vector4[] cps)
        {
            Debug.Assert(cps.Length == this.mCPs.Length);
            this.mCPs = cps;
        }

        public void setCP(Vector4 cp, int i)
        {
            this.mCPs[i] = cp;
        }
        public Vector4 getCP(int i)
        {
            return this.mCPs[i];
        }

        public int getNumCps()
        {
            return this.mCPs.Length;
        }

        //constructor 
        public XBezierCurve3D(int deg, Vector4[] cps)
        {
            this.setCurve(deg, cps);
        }
        public XBezierCurve3D(XBezierCurve3D bcv)
        {
            Vector4[] cps = XCPsUtil.copyCPs(bcv.getCPs());
            this.setCurve(bcv.getDeg(), cps);
        }

        //check the condition : degree +1 = n 

        public void setCurve(int deg, Vector4[] cps)
        {
            Debug.Assert(cps.Length == deg + 1);
            this.mDeg = deg;
            this.mCPs = cps;
        }

        public override double getStartParam()
        {
            return 0.0;
        }

        public override double getEndParam()
        {
            return 1.0;
        }

        public override Vector3 calcPos(double u)
        {
            Vector3 C = this.calcPosByBezierDefinition(u);
            return C;
        }

        private Vector3 calcPosByBezierDefinition(double u)
        {
            int n = this.getDeg();
            double[] B = XBezier.calcBasicFns(n, u);
            Vector4 Cw = Vector4.zero;
            for (int i = 0; i <=n; i++)
            {
                Cw += (float)B[i] * this.getCP(i);
            }
            Vector3 C = XCPsUtil.perspectiveMap(Cw);
            return C;
        }

        public override Vector3[] calcDers(int order, double u)
        {
            throw new System.NotImplementedException();
        }
    }
}
