using UnityEngine;

namespace XGeom.NURBS.CurveFit {
    public class XChordLengthParameterization : 
        XBSplineCurveFitParameterization {

        // constructor
        public XChordLengthParameterization() {
        }

        public override double[] calcParams() {
            Debug.Assert(this.mFit != null);
            Vector3[] qs = this.mFit.getQs();
            Debug.Assert(qs != null && qs.Length > 1);
            double[] us = new double[qs.Length];

            double totalLen = 0;
            for (int i = 1; i < qs.Length; i++) {
                totalLen += Vector3.Distance(qs[i], qs[i - 1]);
            }

            us[0] = 0.0;
            double len = 0.0;
            for (int i = 1; i < qs.Length; i++) {
                len += Vector3.Distance(qs[i], qs[i - 1]);
                us[i] = len / totalLen;
            }

            us[qs.Length - 1] = 1.0;

            return us;
        }
    }
}