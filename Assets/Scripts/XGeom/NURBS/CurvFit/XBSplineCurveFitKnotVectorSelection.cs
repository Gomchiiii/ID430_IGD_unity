namespace XGeom.NURBS.CurveFit {
    public abstract class XBSplineCurveFitKnotVectorSelection {
        // fields
        protected XBSplineCurveFit mFit = null;
        public abstract void setFit(XBSplineCurveFit fit);
        protected int mNumOfDistinctKnotSpans = 1; // default: 1
        public void setNumOfDistinctKnotSpans(int n) {
            this.mNumOfDistinctKnotSpans = n;
        }
        protected int mInteriorKnotMultiplicity = 1; // default: 1
        public void setInteriorKnotMultiplicity(int s) {
            this.mInteriorKnotMultiplicity = s;
        }

        // constructor
        public XBSplineCurveFitKnotVectorSelection() {
        }
        
        // abstract methods
        public abstract double[] calcU();
    }
}