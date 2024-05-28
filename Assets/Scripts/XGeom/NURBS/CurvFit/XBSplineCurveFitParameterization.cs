namespace XGeom.NURBS.CurveFit {
    public abstract class XBSplineCurveFitParameterization {
        // fields
        protected XBSplineCurveFit mFit = null;
        public void setFit(XBSplineCurveFit fit) {
            this.mFit = fit;
        }

        // constructor
        public XBSplineCurveFitParameterization() {
        }

        // abstract methods
        public abstract double[] calcParams();        
    }
}