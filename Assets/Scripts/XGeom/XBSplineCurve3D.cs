using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using XGeom.NURBS;
using XMath;

namespace XGeom.NURBS {
    public class XBSplineCurve3D : XParametricCurve3D {
        // fields
        private int mDeg = int.MinValue;
        private static readonly int MAX_ITERATION_NUM = 100;
        private static readonly double POINT_INVERSION_END_DISTANCE = 1.0e-2;
        private static readonly double POINT_INVERSION_END_ANGLE = 1.0e-5;
        private static readonly double POINT_INVERSION_END_CONV_SPEED = 1.0e-5;
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
        // constructor
        public XBSplineCurve3D(XBezierCurve3D bcv) {
            int p = bcv.getDeg();
            double[] U = XKnotVectorUtil.createUniformKnotVector(p, 0.0, 1.0,
                1, 0); // interiorKnotMul=0 means nothing. no interior knots.
            Vector4[] cps = XCPsUtil.copyCPs(bcv.getCPs());
            this.setCurve(p, U, cps);
        }
        public XBSplineCurve3D(XBezierCurve3D[] bcs, double[] U) {
            int p = bcs[0].getDeg();

            Debug.Assert((bcs.Length + 1) * p + 2 == U.Length);

            int m = U.Length - 1; // {FU[0],...,FU[m]}.
            int n = m - p - 1; // {CP[0],...,CP[n]}.
            Vector4[] cps = new Vector4[n + 1];
            int k = 0;
            for (int i = 0; i < bcs.Length; i++) {
                for (int j = 0; j < p; j++) {
                    cps[k] = XCPsUtil.copyCP(bcs[i].getCP(j)); k++;
                }
            }
            cps[k] = XCPsUtil.copyCP(bcs[bcs.Length - 1].getCP(p));
            this.setCurve(p, U, cps);
        }

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

        public override Vector3 calcPos(double u) {
            return calcPosByDefinition(u);
            //return calcPosByKnotInsertion(u);
        }

        // Use this method for CA5-2.
        public Vector3 calcPosByDefinition(double u) {
            int p = this.mDeg;
            int m = this.getNumKnots() - 1;
            int n = m - p - 1;
            double[] U = this.getU();

            // 1. Find the knot span index j such that u is in [u_j, u_{j+1}).
            int j = XKnotVectorUtil.findKnotSpanIndex(u, U, p);

            // 2. Calculate the sum of N_{i,p}(u) * P_i for i = j-p to j.
            Vector4[] cps = this.getCPs();
            Vector3[] cpPos = XCPsUtil.perspectiveMap(cps);
            Vector3 pos = Vector3.zero;
            double[] N = XBSpline.calcBasisFns(u, p, U);
            for (int i = j - p; i <= j; i++) {
                pos += (float)N[i] * cpPos[i];
            }

            return pos;
        }

        public Vector3 calcPosByKnotInsertion(double u) {
            int p = this.mDeg;
            int m = this.getNumKnots() - 1;
            int n = m - p - 1;
            double[] U = this.getU();

            /* 
             * Implement here for CA7-2.
             */

            throw new System.NotImplementedException();
        }

        // {D[0],...,D[order]} = {C(u), C'(u), C"(u),...,C^{order}(u)}
        public override Vector3[] calcDers(int order, double u) {
            int p = this.getDeg();
            double[] U = this.getU();
            Debug.Assert(order <= p);

            // Derivative of basis function
            double[,] dB = XBSpline.calcAllDerivBasisFns(order, u, p, U);

            // Derivative of NURBS curve
            Vector3[] ders = new Vector3[order + 1];
            int span = XKnotVectorUtil.findKnotSpanIndex(u, this.mU, p);

            // calculate derivative of X, Y, Z, W
            double[] dX = new double[order + 1];
            double[] dY = new double[order + 1];
            double[] dZ = new double[order + 1];
            double[] dW = new double[order + 1];

            for (int k = 0; k <= order; k++) {
                for (int i = span - p; i <= span; i++) {
                    dX[k] += dB[k, i] * this.mCPs[i].x;
                    dY[k] += dB[k, i] * this.mCPs[i].y;
                    dZ[k] += dB[k, i] * this.mCPs[i].z;
                    dW[k] += dB[k, i] * this.mCPs[i].w;
                }
            }

            // calculate derivative of curve from 0th to kth order
            for (int k = 0; k <= order; k++) {
                Vector3 dC = Vector3.zero;
                dC += new Vector3((float)dX[k], (float)dY[k], (float)dZ[k]);

                for (int i = 1; i <= k; i++) {
                    float binomial = (float)XMathUtil.calcBinomialCoeff(k, i);
                    dC -= binomial * (float)dW[i] * ders[k - i];
                }
                ders[k] = dC / (float)dW[0];
            }
            return ders;
        }

        // Use this method for CA5-6.
        // CAUTION: This doesn't work for rational curves.
        public XBSplineCurve3D calcDerivCurve(int order) {
            Debug.Assert(order <= this.mDeg);

            // 1. Calculate the new control points using calcDerivCPs.
            Vector4[] derivCPs = this.calcDerivCPs(order);

            // 2. Calculate the new knot vector using calcDerivKnotVector.
            double[] derivU = this.calcDerivKnotVector(order);

            // 3. Create a new B-spline curve.
            int newDeg = this.mDeg - order;
            XBSplineCurve3D derivCurve = new XBSplineCurve3D(newDeg, derivU,
                derivCPs);

            return derivCurve;
        }

        // Calculate the control points of C^(order)(u)
        public Vector4[] calcDerivCPs(int order) {
            Debug.Assert(order >= 0 && order <= this.mDeg);

            if (order == 0) {
                return this.getCPs();
            }

            int p = this.mDeg;
            double[] U = this.getU();
            Vector4[] prevCPs = this.calcDerivCPs(order - 1);
            Vector4[] dCPs = new Vector4[prevCPs.Length - 1];
            for (int i = 0; i < dCPs.Length; i++) {
                if (XKnotVectorUtil.checkParamsAreSameKnotVals(U[i + p + 1],
                    U[i + order], U)) {
                    dCPs[i] = new Vector4(0f, 0f, 0f, 1f);
                    continue;
                }
                float a = (p - order + 1) /
                    (float)(U[i + p + 1] - U[i + order]);
                dCPs[i] = a * (prevCPs[i + 1] - prevCPs[i]);
                dCPs[i].w = 1f;
            }
            return dCPs;
        }

        // Calculate the know vector of C^(order)(u)
        public double[] calcDerivKnotVector(int order) {
            double[] U = this.getU();
            double[] dU = new double[U.Length - 2 * order];
            for (int i = 0; i < dU.Length; i++) {
                dU[i] = U[i + order];
            }
            return dU;
        }

        // Split the curve into two B-spline curves at C(u).
        // Returns the two B-spline curves in an array.
        public XBSplineCurve3D[] splitAtParam(double u) {
            int p = this.mDeg;
            int m = this.getNumKnots() - 1;
            int n = m - p - 1;
            double[] U = this.getU();

            /*
             * Implement here for CA7-3.
             */

            throw new System.NotImplementedException();
        }

        // Split the B-spline curve into Bezier curves by knot insertion.
        public XBezierCurve3D[] splitIntoBezierCurves() {
            int p = this.mDeg;
            int m = this.getNumKnots() - 1;
            int n = m - p - 1;
            double[] U = this.getU();

            /*
             * Implement here for CA7-4.
             */

            throw new System.NotImplementedException();
        }

        //CALCULATE PARMETER U THAT IS NEAREDS FROM THE SAMPLED POINT

        public double calcParamAtPt(Vector3 pt) {
            double nearestU = 0.0;
            float nearestDistance = float.MaxValue;

            //uniform spaced sample
            for (int i = 0; i <= 10; i++) {
                double prevU = this.getStartParam() + (this.getEndParam() - this.getStartParam()) * i / 10.0;

                Vector3[] ders = this.calcDers(2, prevU);
                double u = prevU;
                int iterationNum = 0;

                while (iterationNum < XBSplineCurve3D.MAX_ITERATION_NUM) {
                    double num = Vector3.Dot(ders[1], ders[0] - pt);
                    double denom;
                    if (ders.Length - 1 >= i) {
                        denom = Vector3.Dot(ders[2], ders[0] - pt) + Vector3.SqrMagnitude(ders[i]);
                    } else {
                        denom = Vector3.Dot(ders[2], ders[0] - pt);
                    }
                    u = prevU - (num / denom);

                    if (u < this.getStartParam()) {
                        u = this.getStartParam();
                    }
                    if (u > this.getEndParam()) {
                        u = this.getEndParam();
                    }

                    //check end condition
                    ders = this.calcDers(2, u);
                    double distance = Vector3.Magnitude(pt - ders[0]);
                    double angle =
                        Math.Abs(
                            Vector3.Dot(ders[1], ders[0] - pt) /
                            (Vector3.Magnitude(ders[1]) *
                            Vector3.Magnitude(ders[0] - pt)));
                    double convSpeed =
                        Math.Abs((u - prevU) * Vector3.Magnitude(ders[1]));

                    bool endCondition =
                        distance < POINT_INVERSION_END_DISTANCE ||
                        angle < POINT_INVERSION_END_ANGLE ||
                        convSpeed < POINT_INVERSION_END_CONV_SPEED;

                    if (endCondition) {
                        break;
                    }

                    prevU = u;
                    iterationNum++;
                }

                if (Vector3.Distance(this.calcPos(u), pt) < nearestDistance) {
                    nearestU = u;
                    nearestDistance = Vector3.Distance(this.calcPos(u), pt);
                }
            }

            return nearestU;

        }
    }
}