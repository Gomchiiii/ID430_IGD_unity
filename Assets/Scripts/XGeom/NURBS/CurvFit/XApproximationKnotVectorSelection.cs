using System;
using System.Text;
using UnityEditor.Performance.ProfileAnalyzer;
using UnityEngine;

namespace XGeom.NURBS.CurveFit {
    public class XApproximationKnotVectorSelection : 
        XBSplineCurveFitKnotVectorSelection{
        
        // constructor
        public XApproximationKnotVectorSelection() : base() {
        }
        public override void setFit(XBSplineCurveFit fit) {
            this.mFit = fit;
            XBSplineApproximation mApprox = (XBSplineApproximation)this.mFit;
            this.mNumOfDistinctKnotSpans = mApprox.getN() - mFit.getDeg();
        }

        public override double[] calcU() {
            int p = this.mFit.getDeg();
            XBSplineApproximation mApprox = (XBSplineApproximation)this.mFit;
            int n = mApprox.getN();
            int numOfDistinctKnots = this.mNumOfDistinctKnotSpans + 1;
            double[] U = new double[n + p + 2];

            double d = (this.mFit.getQs().Length) / (double)(n - p + 1);

            for (int j = 1; j <= n - p; j++) {
                double jd = (double)j * d;
                int Ij = (int)jd;
                double alpha = jd - Math.Floor(jd);
                Debug.Log(Ij);
                U[j + p] = (1 - alpha) * this.mFit.getParams()[Ij - 1] +
                    alpha * this.mFit.getParams()[Ij];
            }
            for (int j = U.Length - p - 1; j < U.Length; j++) {
                U[j] = 1.0;
            }

            return U;
        }
    }
}