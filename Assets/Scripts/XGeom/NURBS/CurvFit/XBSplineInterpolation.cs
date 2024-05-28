using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XMath;

namespace XGeom.NURBS.CurveFit {
    

    public class XBSplineInterpolation : XBSplineCurveFit {
        // constructor
        public XBSplineInterpolation(int deg, Vector3[] qs,
            XBSplineCurveFitParameterization parameterization,
            XBSplineCurveFitKnotVectorSelection uSelection) : base(deg, qs,
                parameterization, uSelection){
        }

        // The number of given points to fit should be equal to
        // number of control points.
        public override bool fit() {
            // 1. Parameterization.
            this.mParams = this.mParameterization.calcParams();
            
            // 2. Knot vector Selection.
            this.mU = this.mUSelection.calcU();
            int numOfCPs = this.mU.Length - this.mDeg - 1;
            if (this.mQs.Length < numOfCPs) {
                return false;
            }
            // 3. Interpolate the curve.
            // calc matrix N
            double[,] n = new double[this.mQs.Length, numOfCPs];
            for (int i = 0; i < this.mQs.Length; i++) {
                double[] nRow = XBSpline.calcBasisFns(this.mParams[i], this.mDeg, this.mU);
                for (int j = 0; j < numOfCPs; j++) {
                    n[i, j] = nRow[j];
                }
            }
            
            XMatrix N = new XMatrix(n);
            //seperate q to separted (x,y, z) value 
            double[] qx = new double[this.mQs.Length];
            double[] qy = new double[this.mQs.Length];
            double[] qz = new double[this.mQs.Length];

            for (int i = 0; i < this.mQs.Length; i++) {
                qx[i] = this.mQs[i].x;
                qy[i] = this.mQs[i].y;
                qz[i] = this.mQs[i].z;
            }
            XColVector QX = new XColVector(qx);
            XColVector QY = new XColVector(qy);
            XColVector QZ = new XColVector(qz);

            XColVector PX = N.solveLinearEqnWith(QX);
            XColVector PY = N.solveLinearEqnWith(QY);
            XColVector PZ = N.solveLinearEqnWith(QZ);

            this.buildCurve(PX, PY, PZ);

            return true;
        }
    }
}