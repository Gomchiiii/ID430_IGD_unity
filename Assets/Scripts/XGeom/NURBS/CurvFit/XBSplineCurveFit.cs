using System;
using System.Collections.Generic;
using UnityEngine;
using XMath;

namespace XGeom.NURBS.CurveFit {

    public abstract class XBSplineCurveFit {
        // fields
        protected int mDeg = 3; // default is 3. 
        public int getDeg() {
            return this.mDeg;
        }
        protected Vector3[] mQs = null;
        public Vector3[] getQs() {
            return this.mQs;
        }
        protected double[] mParams = null;
        public double[] getParams() {
            return this.mParams;
        }
        protected double[] mU = null;
        public double[] getU() {
            return this.mU;
        }
        protected XBSplineCurve3D mFittedCurve = null;
        public XBSplineCurve3D getFittedCv() {
            return this.mFittedCurve;
        }
        protected XBSplineCurveFitParameterization mParameterization = null;
        public XBSplineCurveFitParameterization getParameterization() {
            return this.mParameterization;
        }
        protected XBSplineCurveFitKnotVectorSelection mUSelection = null;
        public XBSplineCurveFitKnotVectorSelection getUSelection() {
            return this.mUSelection;
        }

        // constructor
        public XBSplineCurveFit(int deg, Vector3[] qs,
            XBSplineCurveFitParameterization parameterization,
            XBSplineCurveFitKnotVectorSelection uSelection) {

            this.mDeg = deg;
            this.mQs = qs;
            this.mParameterization = parameterization;
            this.mParameterization.setFit(this);
            this.mUSelection = uSelection;
            this.mUSelection.setFit(this);
        }
        public abstract bool fit();

        protected void buildCurve(XColVector PX, XColVector PY,
            XColVector PZ) {

            double[] U = XKnotVectorUtil.copyKnotVector(this.mU);
            int numOfCPs = PX.getLength();
            Vector4[] cps = new Vector4[numOfCPs];
            for (int i = 0; i < numOfCPs; i++) {
                cps[i] = new Vector4((float)PX[i], (float)PY[i],
                    (float)PZ[i], 1f);
            }
            XBSplineCurve3D cv = new XBSplineCurve3D(this.mDeg, U, cps);
            this.mFittedCurve = cv;
        }
    }
}