using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xgeom.NURBS;

namespace XGeom.NURBS {
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
        public Vector4[] getCPs() {
            return mCPs;
        }

        public void setCPs(Vector4[] cps) {
            Debug.Assert(cps.Length == this.mCPs.Length);
            this.mCPs = cps;
        }

        public void setCP(Vector4 cp, int i) {
            this.mCPs[i] = cp;
        }
        public Vector4 getCP(int i) {
            return this.mCPs[i];
        }

        public int getNumCps() {
            return this.mCPs.Length;
        }

        //constructor 
        public XBezierCurve3D(int deg, Vector4[] cps) {
            this.setCurve(deg, cps);
        }

        public XBezierCurve3D(XBezierCurve3D bcv) {
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
            Vector3 C = this.calcPosByDeCasteljouAlgo(u);
            return C;
        }

        private Vector3 calcPosByBezierDefinition(double u)
        {
            int n = this.getDeg();
            double[] B = XBezier.calcAllBernsteinPolynomialsByDynamicProg(n, u);
            Vector4 Cw = Vector4.zero;
            for (int i = 0; i <=n; i++)
            {
                Cw += (float)B[i] * this.getCP(i);
            }
            Vector3 C = XCPsUtil.perspectiveMap(Cw);
            return C;
        }

        private Vector3 calcPosByDeCasteljouAlgo(double u) {
            int n = this.getDeg();

            Vector4[] Q = new Vector4[n + 1];
            
            for (int i = 0; i<=n; i++) {
                Q[i] = this.getCP(i);
            }

            for (int k = 1; k <= n; k++) {
                for (int i = 0; i <= n - k; i++) {
                    Q[i] = (float)(1.0f - u) * Q[i] + (float)u * Q[i + 1]; 
                }

            }

            Vector3 C = XCPsUtil.perspectiveMap(Q[0]);
            return C; 
        }

        //Output : 0 -th to dorder-th derivates at u
        public override Vector3[] calcDers(int order, double u)
        {
            List<Vector3> Ders = new List<Vector3>();
            int n = this.getCPs().Length - 1;
            //New calc formula D_deg/num = D_deg-1/num+1 - De_deg-1/num
            
            for (int j = n - 1; j > n - order; j--) {
                Vector4[] Dpts = new Vector4[j];
                for (int i = 0; i < j - 3; i++) {
                    Vector4 temp0 = this.getCP(i);
                    Vector4 temp1 = this.getCP(i + 1);
                    Dpts[i] = temp1 - temp0;
                }
                this.setCurve(j - 1, Dpts);
                Ders.Add(this.calcPosByDeCasteljouAlgo(u));
                Debug.Log("index check");
            }

            return Ders.ToArray();
        }


        public XBezierCurve3D calcDerivCurve(int order) {
            throw new System.NotImplementedException();
        }
    }
}
