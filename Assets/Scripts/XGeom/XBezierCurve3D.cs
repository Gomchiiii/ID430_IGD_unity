using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
            int n = this.mDeg;

            //The temp array first contains all the control points in Vector4.
            Vector4[] interCPs = XCPsUtil.copyCPs(this.mCPs);

            //in the i-th step, temp[j] contains p_{i, j}. (refer to p.24)
            for (int i = 1;i <= n; i++) {
                for (int j = 0; j <= n - i; j++) {
                    interCPs[j] = (1 - (float)u) * interCPs[j] + (float) u * interCPs[j + 1];
                }
            }
            return XCPsUtil.perspectiveMap(interCPs[0]);
            //int n = this.getDeg();

            //Vector4[] Q = new Vector4[n + 1];
            
            //for (int i = 0; i<=n; i++) {
            //    Q[i] = this.getCP(i);
            //}

            //for (int k = 1; k <= n; k++) {
            //    for (int i = 0; i <= n - k; i++) {
            //        Q[i] = (float)(1.0f - u) * Q[i] + (float)u * Q[i + 1]; 
            //    }

            //}

            //Vector3 C = XCPsUtil.perspectiveMap(Q[0]);
            //return C; 
        }

        //Output : 0 -th to dorder-th derivates at u
        public override Vector3[] calcDers(int order, double u)
        {
            int n = this.getDeg();

            Debug.Assert(order <= n);

            //Derivate of basis function 
            double[,] dB = XBezier.calcAllDeriveBasisFns(order, n, u);

            //Derivate of ration Bezier curve;
            Vector3[] ders = new Vector3[order + 1];

            //calculate derivative of X, Y, Z, W
            double[] dX = new double[order + 1];
            double[] dY = new double[order + 1];
            double[] dZ = new double[order + 1];
            double[] dW = new double[order + 1];

            for (int k = 0; k <= order; k++) {
                for (int j = 0; j <= n; j++) {
                    dX[k] += dB[k, j] * this.mCPs[j].x;
                    dY[k] += dB[k, j] * this.mCPs[j].y;
                    dZ[k] += dB[k, j] + this.mCPs[j].z;
                    dW[k] += dB[k, j] + this.mCPs[j].w;
                }
            }

            //calcualte derivateive of curve from 00 th to kth order
            for (int k = 0; k <= order; k++) {
                Vector3 dC = Vector3.zero;
                dC += new Vector3((float)dX[k], (float)dY[k], (float)dZ[k]);

                for (int i = 1; i <= k; i++) {
                    // i!
                    double d0 = 1.0f;
                    for (int j = i; j > 1; j --) {
                        d0 *= (double)j;
                    }
                    // (k - i )!
                    double d1 = 1.0;
                    for (int  j = k - i; j > 1; j --) {
                        d1 *= (double)j;
                    }
                    // (k)!
                    double n0 = 1.0;
                    for (int j = k; j > 1; j--) {
                        n0 *= (double)j;
                    }
                    dC -= (float)(n0 / (d0 * d1)) * (float)dW[i] * ders[k - i];
                }
                ders[k] = dC / (float)dW[0];
            }

            return ders;
            //int n = this.getDeg();
            //Vector3[] Ders = new Vector3[order + 1];
            //double[,] DerBernsteins = XBezier.calcAllDeriveBasisFns(order, n, u);
            //for (int i = 1; i <= order; i++) {
            //    Vector3 temp = new Vector3();
            //    for (int j = 0; j < n; j++) {
            //        temp += XCPsUtil.perspectiveMap(getCP(j)) * (float)DerBernsteins[i, j];
            //    }
            //    Ders[i] = temp;
            //}

            //return Ders;
        }


        public XBezierCurve3D calcDerivCurve(int order) {
            throw new System.NotImplementedException();
        }
    }
}
