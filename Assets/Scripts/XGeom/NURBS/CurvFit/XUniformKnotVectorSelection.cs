namespace XGeom.NURBS.CurveFit {
    public class XUniformKnotVectorSelection : 
        XBSplineCurveFitKnotVectorSelection{

        // constructor
        public XUniformKnotVectorSelection() : base() {
        }
        public override void setFit(XBSplineCurveFit fit) {
            this.mFit = fit;
            this.mNumOfDistinctKnotSpans =
                this.mFit.getQs().Length - this.mFit.getDeg();
        }

        public override double[] calcU() {
            int p = this.mFit.getDeg();
            int numOfDistinctKnots = this.mNumOfDistinctKnotSpans + 1;
            double[] DU = new double[numOfDistinctKnots];

            DU[0] = 0.0;
            for (int i = 1; i < numOfDistinctKnots - 1; i++) {
                DU[i] = (double)i / (double)(numOfDistinctKnots - 1);
            }
            DU[numOfDistinctKnots - 1] = 1.0;

            double[] U = XKnotVectorUtil.createKnotVector(p, DU,
                this.mInteriorKnotMultiplicity);
            return U;
        }
    }
}