using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XAppObject;
using Xgeom.NURBS;
using XGeom.NURBS;
using XGeorm.NURBS;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public static partial class IGDPlotter {
        // CA2-1: Using the recursive definition of the Bernstein polynomials,
        // calculate and print the values of all n-th degree Bernstein
        // polynomials for given parameter u.
        public static void CA2_1(int n, double u) {
            // u value is between 0 and 1
            Debug.Assert(u >= 0 && u <= 1);

            // 1. Calculate each Bernstein polynomial value
            Debug.Log("1. Calculate each Bernstein polynomial value:");
            for (int i = 0; i <= n; i++) {
                // Calc Bernstein value by dynamic programming
                // Implement the below method in XBezier.cs
                double B = XBezier.calcBernsteinPolynomialByDynamicProg(i, n, u);
                Debug.Log("B_" + i + "," + n + "(" + u + "): " + B);
            }

            // 2. Calculate all Bernstein polynomial values at once
            Debug.Log("2. Calculate all Bernstein polynomial values at once:");
            double[] Bs = XBezier.calcAllBernsteinPolynomialsByDynamicProg(n, u);
            for (int i = 0; i <= n; i++) {
                Debug.Log("B_" + i + "," + n + "(" + u + "): " + Bs[i]);
            }
        }

        // CA2-2: Plot the graph of the n = 1, 2, 3, 9-th degree Bernstein
        // polynomials.
        public static void CA2_2(int n) {

            // Draw x and y axes
            float yMax = Screen.height * 0.4f;
            float yMin = -Screen.height * 0.2f;
            //Debug.Log(yMax);
            Vector2 yAxisStart = new Vector2(0f, yMin);
            Vector2 yAxisEnd = new Vector2(0f, yMax);
            IGDPlotter.drawArrow2D(yAxisStart, yAxisEnd);

            float xMax = 2 * yMax;
            float xMin = yMin;
            Vector2 xAxisStart = new Vector2(xMin, 0f);
            Vector2 xAxisEnd = new Vector2(xMax, 0f);
            IGDPlotter.drawArrow2D(xAxisStart, xAxisEnd);

            // Draw BernstainPolynomial Graph 
            for (int i = 0; i <= n; i++) {
                IGDPlotter.DrawBernsteinPolynominalGraph(i, n);
                // Label Polynomial 
                //
            }

        }

        // CA2-3: Draw Bezier curves of various degrees along with their control
        // points and control polygons. Report how the shape of each Bezier
        // curve changes as changing its control points.
        public static void CA2_3(int deg, Vector4[] cps) {

            //Draw x, y, and z 
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            XBezierCurve3D bcv = new XBezierCurve3D(cps.Length - 1, cps);
            List<Vector3> bcvPtS = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                bcvPtS.Add(bcv.calcPos((double)i / 100.0));
            }
            IGDPlotter.drawPolyline3D(bcvPtS, 0.7f);
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(cps).ToList(), 0.7f);

            for (int i = 0; i <= deg; i++) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(cps)[i]);
            }
        }

        private static void DrawBernsteinPolynominalGraph(int i, int n) {
            List<Vector2> BernsteinDots = new List<Vector2>();
            for (double j = 0.0; j <= 50; j++) {
                double u = j / 50.0;
                double B = XBezier.calcBernsteinPolynomialByDynamicProg(i, n, u);
                Vector2 Bdot = new Vector2((float)(u * 1000.0), (float)(B * 600.0));
                BernsteinDots.Add(Bdot);
                //Debug.Log(B);
            }
            IGDPlotter.drawPolyline2D(BernsteinDots, 0.3f);
        }

    }
}