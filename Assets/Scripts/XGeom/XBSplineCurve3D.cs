using UnityEngine;
using Xgeom.NURBS;

namespace XGeom.NURBS {
    public class XBSplineCurve3D : XParametricCurve3D {
        // fields
        private int mDeg = int.MinValue;
        public int getDeg() {
            return this.mDeg;
        }

        // {U[0], ... U[m]} 
        private double[] mU = null;
        public double[] getU() {
            return this.mU;
        }
        public void setU(double[] U) {
            Debug.Assert(U.Length == this.mU.Length);
            this.mU = U;
        }
        public int getNumKnots() {
            return this.mU.Length;
        }

        // (n+1) 4D control points {(X,Y,Z,W)} = {(wx,wy,wz,w)} 
        // where n is the degree. 
        private Vector4[] mCPs = null;
        public Vector4[] getCPs() {
            return this.mCPs;
        }
        public void setCPs(Vector4[] cps) {
            Debug.Assert(cps.Length == this.mCPs.Length);
            this.mCPs = cps;
        }
        public Vector4 getCP(int i) {
            return this.mCPs[i];
        }
        public void setCP(Vector4 cp, int i) {
            this.mCPs[i] = cp;
        }
        public int getNumCPs() {
            return this.mCPs.Length;
        }

        // constructor
        public XBSplineCurve3D(int deg, double[] U, Vector4[] cps) {
            this.setCurve(deg, U, cps);
        }

        // checks the condition: m=n+p+1
        public void setCurve(int deg, double[] U, Vector4[] cps) {
            Debug.Assert(U.Length == cps.Length + deg + 1); // m=n+p+1
            this.mDeg = deg;
            this.mU = U;
            this.mCPs = cps;
        }

        // methods
        public override double getStartParam() {
            return this.mU[0];
        }
        public override double getEndParam() {
            return this.mU[this.getNumKnots() - 1];
        }
        public double getParam(int i) {
            return this.mU[i];
        }
        
        // Use this method for CA5-2.
        public override Vector3 calcPos(double u) {
            // 1. Find the knot span index j such that u is in [u_j, u_{j+1}).
            double[] U = this.getU();
            int m = U.Length - 1;
            int p = this.getDeg();
            int n = m - p - 1;
            //double[] N = new double[n + 1];
            int i = XKnotVectorUtil.findKnotSpanIndex(u, U, p);
            //Debug.Log("findknotspanindex");

            // 2. Calculate the sum of N_{i,p}(u) * P_i for i = j-p to j
            double[] M = XBspline.calcNonZeroBasisFnsByDynamicProg(u, i, p, U);
            //Debug.Log("calcBasisFns");

            Vector4 Cw = Vector4.zero;
            for (int j = 0; j <= p; j++) {
                Cw += (float)M[j] * this.getCP(i - p + j);
            }
            Vector3 C = XCPsUtil.perspectiveMap(Cw);
            return C;


            //throw new System.NotImplementedException();
        }

        // {D[0],...,D[order]} = {C(u), C'(u), C"(u),...,C^{order}(u)}
        // Use this method for CA5-3.
        public override Vector3[] calcDers(int order, double u) {
            // Use C^(k)(u) = sum_{i=0}^{n} N^(k)_{i,p}(u) * P_i
            // You only need to calculate the non-zero basis functions.
            double[] U = this.getU();
            int m = U.Length - 1;
            int p = this.getDeg();
            int n = m - p - 1;
            //double[] N = new double[n + 1];
            int i = XKnotVectorUtil.findKnotSpanIndex(u, U, p);
            //Debug.Log("findknotspanindex");

            // 2. Calculate the sum of N_{i,p}(u) * P_i for i = j-p to j
            Vector3[] DerC = new Vector3[order + 1];

            int du = System.Math.Min(order, p);

            for (int k = 0; k <= du; k++) {
                double[,] M = XBspline.calcAllDerivBasisFns(du, u, p, U);

                Vector4 Cw = Vector4.zero;
                for (int j = 0; j <= p; j++) {
                    Cw += (float)M[k, j] * this.getCP(i - p + j);
                }

                Vector3 C = XCPsUtil.perspectiveMap(Cw);
                DerC[k] = C;
            }

            return DerC;

            //throw new System.NotImplementedException();
        }
        
        // Use this method for CA5-6.
        public XBSplineCurve3D calcDerivCurve(int order) {
            // 1. Calculate the new control points using calcDerivCPs.
            
            // 2. Calculate the new knot vector using calcDerivKnotVector.
            
            // 3. Create a new B-spline curve.
            
            throw new System.NotImplementedException();
        }
        
        // Calculate the control points of C^(order)(u)
        public Vector4[] calcDerivCPs(int order) {
            throw new System.NotImplementedException();
        }
        
        // Calculate the know vector of C^(order)(u)
        public double[] calcDerivKnotVector(int order) {
            throw new System.NotImplementedException();
        }
    }
}