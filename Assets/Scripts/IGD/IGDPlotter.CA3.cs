using System.Collections.Generic;
using UnityEngine;
using XAppObject;
using XGeom.NURBS;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public static partial class IGDPlotter {
        // CA3-1: (1) Implement XBezier.calcAllDerivBasisFns.
        // (2) Draw the derivative graphs for n = 2, 3-th degree Bernstein
        // polynomials.
        public static void CA3_1(int n) {
            // Draw x and y axes
            float yMax = Screen.height * 0.6f;
            float yMin = -Screen.height * 0.2f;
            Vector2 yAxisStart = new Vector2(0f, yMin);
            Vector2 yAxisEnd = new Vector2(0f, yMax);
            IGDPlotter.drawArrow2D(yAxisStart, yAxisEnd);

            float xMax = yMax * 4f / 3f;
            float xMin = yMin;
            Vector2 xAxisStart = new Vector2(xMin, 0f);
            Vector2 xAxisEnd = new Vector2(xMax, 0f);
            IGDPlotter.drawArrow2D(xAxisStart, xAxisEnd);

            //Plot B_i,n(u)
            for (int i = 0; i <= n; i++) {
                List<Vector2> BinDerPts = new List<Vector2>();
                int sampleNum = 100;
                for (int j = 0; j < sampleNum; j++)
                {
                    double u = (double)j / sampleNum;
                    double BinDer = XBezier.calcDerivBernsteinPolynomial(1, i, n, u);
                    Vector2 pt = new Vector2((float)u * xMax, (float)BinDer * yMax / 3f);
                    BinDerPts.Add(pt);
                }
                IGDPlotter.drawPolyline2D(BinDerPts, 0.7f);
                //Label B'_i,n(u)
                string BDerinpath = "Formulas/BDer" + i.ToString() + n.ToString();
                Debug.Log(BDerinpath);
                Vector2 BDerLabelpos = new Vector2( (1f - (float)i / (float)n) * xMax - 100f, (float) XBezier.calcDerivBernsteinPolynomial(1, i, n, (1.0 - (double)i / (double)n)) * yMax / 3f + 50f);
                IGDPlotter.addImage2D(BDerinpath, BDerLabelpos, 0.5f);

            }
        }

        // CA3-2: (1) Implement XBezierCurve3D.calcDers.
        // (2) Draw the derivative vectors at various points on the curve.
        public static void CA3_2(int n, Vector4[] cps) {
            XBezierCurve3D mXBezierCurve3D = new XBezierCurve3D(n, cps);
            Vector3[] test = new Vector3[4];
            test = mXBezierCurve3D.calcDers(3, 2.0);
            Debug.Log(test[0]);
            Debug.Log(test[1]);
            Debug.Log(test[2]);
        }

        // CA3-3: Calculate the k-th derivative curve of an n-th degree Bezier
        // curve and draw it along with the control points and control polygon.
            public static void CA3_3(int n, int k, Vector4[] cps) {
        }
        
        // CA3-4: Draw the first and second derivative vectors at the end points
        // of the n-th degree Bezier curve.
        public static void CA3_4(int n, Vector4[] cps) {
        }
        
        // CA3-5: (1) Implement XBezierCurve3D.calcPosByDeCasteljauAlg.
        // (2) Draw Bezier curves of various degrees along with their control
        // points and control polygon.
        public static void CA3_5(int n, Vector4[] cps) {
        }
        
        // CA3-6: Using the de Casteljau algorithm, write code to split an n-th
        // degree Bezier curve at a given parameter u, and draw examples of
        // Bezier curves of various degrees being divided. Include the control
        // points and control polygon of the intermediate steps.
        public static void CA3_6(int n, Vector4[] cps, double u) {
        }
    }
}