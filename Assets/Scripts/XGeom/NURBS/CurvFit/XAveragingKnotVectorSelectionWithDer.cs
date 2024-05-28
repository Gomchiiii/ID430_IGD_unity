using UnityEngine;

namespace XGeom.NURBS.CurveFit {
    public class XAveragingKnotVectorSelectionWithDer : 
        XBSplineCurveFitKnotVectorSelection{
        
        // constructor
        public XAveragingKnotVectorSelectionWithDer() : base() {
        }
        public override void setFit(XBSplineCurveFit fit) {
            this.mFit = fit;
            this.mNumOfDistinctKnotSpans = 
                this.mFit.getQs().Length - this.mFit.getDeg() + 2;
        }

        public override double[] calcU() {
            return null;
        }
    }
}