using System;
using System.Collections.Generic;
using UnityEngine;
using XMath;

namespace XGeom.NURBS.CurveFit {
    

    public class XBSplineInterpolationWithDer : XBSplineCurveFit {
        // fields
        Vector3 derivateAt0 = Vector3.zero;
        Vector3 derivateAt1 = Vector3.zero;

        // constructor
        public XBSplineInterpolationWithDer(int deg, Vector3[] qs,
            XBSplineCurveFitParameterization parameterization,
            XBSplineCurveFitKnotVectorSelection uSelection,
            Vector3 Der0, Vector3 Der1) : base(deg, qs,
                parameterization, uSelection){
            derivateAt0 = Der0;
            derivateAt1 = Der1;
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
            double[,] n = new double[this.mQs.Length + 2, numOfCPs + 2];
            for (int i = 1; i < this.mQs.Length + 1; i++) {
                double[] nRow = XBSpline.calcBasisFns(this.mParams[i - 1], this.mDeg, this.mU);
                for (int j = 0; j < numOfCPs; j++) {
                    n[i, j] = nRow[j];
                }
            }
            n[0, 0] = 1;
            n[this.mQs.Length + 1, numOfCPs - 1] = 1;

            XMatrix N = new XMatrix(n);
            //seperate q to separted (x,y, z) value 
            double[] qx = new double[this.mQs.Length + 2];
            double[] qy = new double[this.mQs.Length + 2];
            double[] qz = new double[this.mQs.Length + 2];

            for (int i = 1; i < this.mQs.Length + 1; i++) {
                qx[i] = this.mQs[i - 1].x;
                qy[i] = this.mQs[i - 1].y;
                qz[i] = this.mQs[i - 1].z;
            }

            qx[0] = derivateAt0.x;
            qy[0] = derivateAt0.y;
            qz[0] = derivateAt0.z;

            qx[this.mQs.Length + 1] = derivateAt1.x;
            qy[this.mQs.Length + 1] = derivateAt1.y;
            qz[this.mQs.Length + 1] = derivateAt1.z;

            XColVector QX = new XColVector(qx);
            XColVector QY = new XColVector(qy);
            XColVector QZ = new XColVector(qz);

            XColVector PX = N.solveLinearEqnWith(QX);
            XColVector PY = N.solveLinearEqnWith(QY);
            XColVector PZ = N.solveLinearEqnWith(QZ);

            this.buildCurve(PX, PY, PZ);

            if ((this.mDeg * (-PX[0] + PX[1]) == this.mU[this.mDeg + 1] * derivateAt0.x) &&
                    (this.mDeg * (-PY[0] + PY[1]) == this.mU[this.mDeg + 1] * derivateAt0.x) &&
                    (this.mDeg * (-PZ[0] + PZ[1]) == this.mU[this.mDeg + 1] * derivateAt0.x)) {
                return true;
            }

            return false;

        }
    }
}