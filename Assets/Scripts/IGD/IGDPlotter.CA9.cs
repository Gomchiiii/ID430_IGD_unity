using System.Collections.Generic;
using UnityEngine;
using XAppObject;
using XGeom.NURBS;
using XGeom.NURBS.CurveFit;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public static partial class IGDPlotter {
        // Implement a point inversion algorithm for NURBS curves,
        // and output examples of point inversion on the curve closest
        // to the control points of each curve for various curves.
        public static void CA9_1() {
            ///Draw axes
            IGDPlotter.draw3DAxes(5f, 5f, 5f, 5f, Color.black, "x", "y", "", 4f);

            //Define the Bspline curve
            int p = 3;
            double[] U = new double[] {
                0, 0, 0, 0, 1, 2, 3, 4, 5, 5, 5, 5
            };

            Vector4[] Pw = new Vector4[] {
                new Vector4(-6f, -1f, 0f, 1f),
                new Vector4(-2f, 2f, 0f, 1f),
                new Vector4(4f, 6f, 0f, 1f),
                new Vector4(-1f, 3f, 0f, 1f),
                new Vector4(0f, 0f, 0f, 1f),
                new Vector4(3f, 1f, 0f, 1f),
                new Vector4(3f, 6f, 0f, 1f),
                new Vector4(7f, 5f, 0f, 1f),
            };

            XBSplineCurve3D cv = new XBSplineCurve3D(p, U, Pw);

            // draw
            IGDPlotter.drawParametricCurve3D(cv, 300, 5f, Color.black);
            IGDPlotter.drawControlPoints(Pw, 8F, Color.black);
            IGDPlotter.drawControlPolygon(Pw, 5f, Color.black);
            //IGDPlotter.lableControlPoints(Pw, 3f, Color.black, "\\mathbf{P}", Vector3.down * 0.3f);

            //insert knots
            Vector3 nearPoint = new Vector3(3f, 1f, 0f);
            IGDPlotter.drawDot3D(nearPoint, 10f, Color.red);

            double u = cv.calcParamAtPt(nearPoint);
            Debug.Log(u);
            IGDPlotter.drawDot3D(cv.calcPos(u), 10f, Color.blue);
        }
        // Implement an interpolation algorithm for curve fitting to
        // satisfy point data ([Refer to N360] Section A9.1),
        // and output the NURBS curve passing through
        // points Q:{(0,0),(3,4),(-1,4),(-4,0),(-4,-3)}
        public static void CA9_2() {
            //Draw Axes
            IGDPlotter.draw3DAxes(5f, 5f, 5f, 5f, Color.black, "x", "y", "", 4f);

            //consraint points 
            Vector3[] pts = new Vector3[] {
                new Vector3(0f, 0f, 0f),
                new Vector3(3f, 4f, 0f),
                new Vector3(-1f, 4f, 0f),
                new Vector3(-4f, 0f, 0f),
                new Vector3(-4f, -3f, 0f),
            };

            foreach (Vector3 pt in pts) {
                IGDPlotter.drawDot3D(pt, 4f, Color.red);
            }

            XBSplineInterpolation curveInterpolation =
                new XBSplineInterpolation(3, pts,
                new XChordLengthParameterization(),
                new XAveragingKnotVectorSelection());
            curveInterpolation.fit();

            foreach (double u in curveInterpolation.getParams()) {
                Debug.Log(u);
            }
            //IGDPlotter.drawKnotVector(U)
            IGDPlotter.drawParametricCurve3D(curveInterpolation.getFittedCv(), 100, 1f, Color.black);
        }

        // Implement an interpolation algorithm for curve fitting that
        // satisfies point data and first derivative conditions at both ends,
        // and output a 3rd degree B-spline curve passing through
        // points Q:{(0,0),(3,4),(-1,4),(-4,0),(-4,-3)},
        // and satisfying first derivative values D0=(3,0), D4=(0,-3)
        public static void CA9_3() {
            //Draw Axes
            IGDPlotter.draw3DAxes(5f, 5f, 5f, 5f, Color.black, "x", "y", "", 4f);

            //consraint points 
            Vector3[] pts = new Vector3[] {
                new Vector3(0f, 0f, 0f),
                new Vector3(3f, 4f, 0f),
                new Vector3(-1f, 4f, 0f),
                new Vector3(-4f, 0f, 0f),
                new Vector3(-4f, -3f, 0f),
            };

            foreach (Vector3 pt in pts) {
                IGDPlotter.drawDot3D(pt, 4f, Color.red);
            }

            XBSplineInterpolationWithDer curveInterpolation =
                new XBSplineInterpolationWithDer(3, pts,
                new XChordLengthParameterization(),
                new XAveragingKnotVectorSelection(),
                new Vector3(3, 0, 0), new Vector3(0, -3, 0));
            curveInterpolation.fit();

            foreach (double u in curveInterpolation.getParams()) {
                Debug.Log(u);
            }
            //IGDPlotter.drawKnotVector(U)
            IGDPlotter.drawParametricCurve3D(curveInterpolation.getFittedCv(), 100, 1f, Color.black);
        }
    }
}