using UnityEngine;

namespace XGeom.NURBS {
    public class XDegreeElevation {
        // fields
        private XBSplineCurve3D mCvBef = null;
        private XBSplineCurve3D mCvAft = null; 
        private XBezierCurve3D mBCvBef = null;
        private XBezierCurve3D mBCvAft = null;
        
        // constructor
        private XDegreeElevation(XBSplineCurve3D cvBef) {
            this.mCvBef = cvBef;
        }
        private XDegreeElevation(XBezierCurve3D bcvBef) {
            this.mBCvBef = bcvBef;
        }

        // Create a new B-spline curve by elevating degree of a B-spline curve
        // by t.
        public static XBSplineCurve3D createDegreeElevatedCurve(int t,
            XBSplineCurve3D cvBef) {
            
            XDegreeElevation de = new XDegreeElevation(cvBef);
            de.elevateDegreeOfBSplineCurve(t);
            return de.mCvAft;
        }
        // Create a new Bezier curve by elevating degree of a Bezier curve
        // by t.
        public static XBezierCurve3D createDegreeElevatedCurve(int t,
            XBezierCurve3D bcvBef) {
            
            XDegreeElevation de = new XDegreeElevation(bcvBef);
            de.elevateDegreeOfBezierCurve(t);
            return de.mBCvAft;
        }

        // Elevate degree of a B-spline curve from p to (p + t).
        private void elevateDegreeOfBSplineCurve(int t) {
            int pP = this.mCvBef.getDeg();
            int mP = this.mCvBef.getNumKnots() - 1;
            int nP = this.mCvBef.getNumCPs() - 1;
            
            // Implement below.
            
            // Step 1. Decompose the B-spline curve into Bezier curves.
            
            // Step 2. Elevate degree of each Bezier curve by t.
            
            // Step 3. Recompose the Bezier curves into a B-spline curve.
            
            // Step 4. Remove unnecessary knots.
        }

        // Elevate degree mBCvBef by t, and save the result in mBCvAft.
        private void elevateDegreeOfBezierCurve(int t) {
            /*
             * Implement here.
             */
        }
    }
}