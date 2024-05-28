using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;
using XMath;

namespace XGeom.NURBS.CurveFit {
    

    public class XBSplineApproximation : XBSplineCurveFit {
        // field
        int n;
        public int getN() {
            return this.n;
        }

        // constructor
        public XBSplineApproximation(int deg, int cpNum, Vector3[] qs,
            XBSplineCurveFitParameterization parameterization,
            XBSplineCurveFitKnotVectorSelection uSelection) : base(deg, qs,
                parameterization, uSelection){
            this.n = cpNum;
            uSelection.setFit(this);
        }

        // The number of given points to fit should be equal to or
        // greater than the number of control points.
        public override bool fit() {
            // 1. Parameterization.
            this.mParams = this.mParameterization.calcParams();
            
            // 2. Knot vector Selection.
            this.mU = this.mUSelection.calcU();
            int numOfCPs = this.n + 1;
            if (this.mQs.Length <= numOfCPs) {
                return false;
            }
            // 3. Approximate Curve.
            double[,] n = new double[this.mQs.Length - 2, numOfCPs - 2];
            for (int i = 1; i < this.mQs.Length - 1; i++) {
                double[] nRow = XBSpline.calcBasisFns(this.mParams[i], this.mDeg, this.mU);
                for (int j = 1; j < numOfCPs - 1; j++) {
                    n[j - 1, j - 1] = nRow[j];
                }
            }
            XMatrix N  = new XMatrix(n);
            XMatrix NTN = N.calcTranspose() * N;

            //calc matrix R
            Vector3[] rk = new Vector3[this.mQs.Length];
            for (int i = 0; i < this.mQs.Length; i++) {
                double[] Np = XBSpline.calcBasisFns(this.mParams[i], this.mDeg, this.mU);
                rk[i] = this.mQs[i] - ((float)Np[0] * this.mQs[0]) - ((float)Np[^1] * this.mQs[^1]);
            }

            double[] rx = new double[numOfCPs - 2];
            double[] ry = new double[numOfCPs - 2];
            double[] rz = new double[numOfCPs - 2];

            for (int i = 1; i < this.mQs.Length - 1; i++) {
                double[] Np = XBSpline.calcBasisFns(this.mParams[i], this.mDeg, this.mU);
                for (int j = 1; j < numOfCPs - 1; j++) {
                    rx[j - 1] += Np[j] * rk[i].x;
                    ry[j - 1] += Np[j] * rk[i].y;
                    rz[j - 1] += Np[j] * rk[i].z;
                }
            }

            XColVector RX = new XColVector(rx);
            XColVector RY = new XColVector(ry);
            XColVector RZ = new XColVector(rz);

            //calc P for (N^TN)P = Q
            XColVector PXSolved = NTN.solveLinearEqnWith(RX);
            XColVector PYSolved = NTN.solveLinearEqnWith(RY);
            XColVector PZSolved = NTN.solveLinearEqnWith(RZ);

            double[] px = new double[PXSolved.getLength() + 2];
            px[0] = this.mQs[0].x;
            for (int i = 1; i < px.Length - 1; i++ ) {
                px[i] = PXSolved[i - 1];
            }
            px[^1] = this.mQs[^1].x;

            double[] py = new double[PYSolved.getLength() + 2];
            py[0] = this.mQs[0].y;
            for (int i = 1; i < py.Length - 1; i++) {
                py[i] = PYSolved[i - 1];
            }
            py[^1] = this.mQs[^1].y;

            double[] pz = new double[PZSolved.getLength() + 2];
            pz[0] = this.mQs[0].z;
            for (int i = 1; i < pz.Length - 1; i++) {
                pz[i] = PZSolved[i - 1];
            }
            pz[^1] = this.mQs[^1].z;

            XColVector PX = new XColVector(px);
            XColVector PZ = new XColVector(py);
            XColVector PY = new XColVector(pz);

            return true;
        }
    }
}