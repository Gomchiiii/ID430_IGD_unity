using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XGeom.NURBS;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public static partial class IGDPlotter {
        // CA6-1: Implement code to represent an n-th degree 3D rational 
        // B?ier curve by a non-rational B?ier curve with 4D control points, 
        // and calculate a point on the curve using perspective mapping. 
        // Draw the 3D curve along with its control points, weights, 
        // and control polygon.
        public static void CA6_1() {
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);


            //Draw an arc using rational Bezier curve

            Vector4[] cps = new Vector4[3];
            cps[0] = new Vector4(1f, 0f, 0f, 1f);
            cps[1] = new Vector4(1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                0f * (float)Math.Cos(45 * Mathf.Deg2Rad), (float)Math.Cos(45 * Mathf.Deg2Rad));
            cps[2] = new Vector4(0f, 1f, 0f, 1f);

            XBezierCurve3D rbcv = new XBezierCurve3D(cps.Length - 1, cps);
            List<Vector3> rbcvPts = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                rbcvPts.Add(rbcv.calcPos((double)i / 100.0));
            }

            //plot
            IGDPlotter.drawControlPoints(cps, 1f, Color.black);
            IGDPlotter.drawControlPolygon(cps, 1f, Color.black);

            //plot the rational bezier cuve
            IGDPlotter.drawPolyline3D(rbcvPts, 1f);


        }

        // CA6-2: Implement code to represent an -th degree NURBS curve by 
        // a non-rational B-spline curve with 4D control points, 
        // and calculate a point on the curve using perspective mapping. 
        // Draw the 3D curve along with its knot vector, control points, 
        // weights, and control polygon.
        public static void CA6_2() {
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            //Draw an arc using rational B-spline curve
            Vector4[] cps = new Vector4[9];
            cps[0] = new Vector4(1f, 0f, 0f, 1f);
            cps[1] = new Vector4(1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                0f * (float)Math.Cos(45 * Mathf.Deg2Rad), (float)Math.Cos(45 * Mathf.Deg2Rad));
            cps[2] = new Vector4(0f, 1f, 0f, 1f);
            cps[3] = new Vector4( - 1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                0f * (float)Math.Cos(45 * Mathf.Deg2Rad), (float)Math.Cos(45 * Mathf.Deg2Rad));
            cps[4] = new Vector4( - 1f, 0f, 0f, 1f);
            cps[5] = new Vector4( - 1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                - 1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                0f * (float)Math.Cos(45 * Mathf.Deg2Rad), (float)Math.Cos(45 * Mathf.Deg2Rad));
            cps[6] = new Vector4(0f, -1f, 0f, 1f);
            cps[7] = new Vector4(1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                -1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                0f * (float)Math.Cos(45 * Mathf.Deg2Rad), (float)Math.Cos(45 * Mathf.Deg2Rad));
            cps[8] = new Vector4(1f, 0f, 0f, 1f);


            double[] U = {0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 4};
            double us = 0.0;
            double ue = 4.0;
            int deg = 2;

            XBSplineCurve3D rbcv = new XBSplineCurve3D(deg, U, cps);
            List<Vector3> rbcvPts = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                double u = us + (double)i / 100.0 * (ue - us);
                rbcvPts.Add(rbcv.calcPos(u));

                if (u == 0 || u == 1 || u == 2 || u == 3) {
                    IGDPlotter.writeFormula3D("C(" + u.ToString() + ")", 
                        XCPsUtil.perspectiveMap(rbcv.calcPos(u)) + Vector3.down * 0.1f,
                        0.7f, Color.red);
                }
            }

            //plot
            IGDPlotter.drawControlPoints(cps, 1f, Color.black);
            IGDPlotter.drawControlPolygon(cps, 1f, Color.black);
            IGDPlotter.drawKnotVector(U, 1500f);
            
            //plot the rational bezier cuve
            IGDPlotter.drawPolyline3D(rbcvPts, 1f);


        }

        // CA6-3: Implement code to create an arc on the xy-plane as 
        // a NURBS curve, given a center, radius, start angle, and end angle. 
        // Draw various examples along with their knot vector, control points, 
        // weights, and control polygon. 
        public static void CA6_3(Vector3 center, float radius,
            float startDegree, float endDegree) {

            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            int deg = 2;

            if (endDegree > startDegree) endDegree += 360.0f;
            float theta = endDegree - startDegree;

            // get number of arcs
            int narcs = (int)(theta / 90.0f + 1);
            float dtheta = theta / (float)narcs; //divided theta

            int n = 2 * narcs; //  n + 1 contol points 
            Vector4[] Pw = new Vector4[n + 1];
            double[] U = new double[Pw.Length + deg + 1];
            float w1 = radius * Mathf.Cos(dtheta / 2.0f * Mathf.Deg2Rad); //base angle

   
            Vector4 P0 = new Vector4(radius * Mathf.Cos(startDegree * Mathf.Deg2Rad),
                radius * Mathf.Sin(startDegree * Mathf.Deg2Rad), 0f, Math.Abs(Mathf.Cos(startDegree * Mathf.Deg2Rad)));
            Vector4 T0 = new Vector4( - radius * Mathf.Sin(startDegree * Mathf.Deg2Rad),
                radius * Mathf.Cos(startDegree * Mathf.Deg2Rad), 0f, 1f);
            Pw[0] = P0;
         

            int index = 0;
            float angle = startDegree;

            for (int i = 1; i <= narcs; i++) {
                angle += dtheta;
                Vector4 P2 = new Vector4(radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                    radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Math.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                Pw[index + 2] = P2;
                Vector4 T2 = new Vector4(- radius * Mathf.Sin(angle * Mathf.Deg2Rad),
                    radius * Mathf.Cos(angle * Mathf.Deg2Rad), 0f, 1f);
                Vector4 P1 = IntersetctLines(P0, T0, P2, T2);
                Pw[index + 1] = new Vector4(P1.x * w1, P1.y * w1, 0f, 1f);
                Debug.Log(w1);

                index = index + 2;
                if (i < narcs) {
                    P0 = P2;
                    T0 = T2;
                }
            }

            int j = 2 * narcs + 1; 
            for ( int i = 0; i < 3; i++ ) {
                U[i] = 0.0;
                U[i + j] = 1.0;
            }

            switch (narcs) {
                case 1: break;
                case 2: U[3] = U[4] = 0.5;
                        break;
                case 3:
                    U[3] = U[4] = 1.0 / 3.0;
                    U[5] = U[6] = 2.0 / 3.0;
                    break;
                case 4:
                    U[3] = U[4] = 0.25;
                    U[5] = U[6] = 0.5;
                    U[7] = U[8] = 0.75;
                    break;
            }


            double us = 0.0;
            double ue = 1.0;

            XBSplineCurve3D rbcv = new XBSplineCurve3D(deg, U, Pw);
            List<Vector3> rbcvPts = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                double u = us + (double)i / 100.0 * (ue - us);
                rbcvPts.Add(rbcv.calcPos(u));
            }

            //plot
            IGDPlotter.drawControlPoints(Pw, 1f, Color.black);
            IGDPlotter.drawControlPolygon(Pw, 1f, Color.black);
            IGDPlotter.drawKnotVector(U, 1500f);


            //plot the rational bezier cuve
            IGDPlotter.drawPolyline3D(rbcvPts, 1f);
            //Debug.Log(U);
            //Debug.Log(Pw);
            //Debug.Log(narcs); 
        }

        private static Vector4 IntersetctLines(Vector4 p0, Vector4 t0, Vector4 p2, Vector4 t2) {
            float A1 = t0.y - p0.y;
            float B1 = p0.x - t0.x;
            float A2 = t2.y - p2.y;
            float B2 = p2.x - t2.x;

            float delta = A1 * B2 - A2 * B1;

            if (delta == 0) {
                return Vector4.zero;
            } else {
                return new Vector4(1f, 1f, 0f, 1f);
            }
            
        }


        // CA6-4: Implement code to calculate the derivatives of 
        // an n-th degree NURBS curve from a non-rational B-spline curve 
        // with 4D control points. Draw the first and second derivative vectors 
        // at several points on an arbitrary NURBS curve, along with the curve, 
        // knot vector, control points, weights, and control polygon.
        public static void CA6_4() {
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            //Draw an arc using rational Bezier curve

            Vector4[] cps = new Vector4[3];
            cps[0] = new Vector4(1f, 0f, 0f, 1f);
            cps[1] = new Vector4(1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                0f * (float)Math.Cos(45 * Mathf.Deg2Rad), (float)Math.Cos(45 * Mathf.Deg2Rad));
            cps[2] = new Vector4(0f, 1f, 0f, 1f);

            int deg = 2;
            double[] U = { 0, 0, 0, 1, 1, 1 };
            float us = 0f;
            float ue = 1f;

            XBSplineCurve3D rbcv = new XBSplineCurve3D(deg, U, cps);
            List<Vector3> rbcvPts = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                double u = us + (double)i / 100.0 * (ue - us);
                rbcvPts.Add(rbcv.calcPos(u));
            }

            //plot
            IGDPlotter.drawControlPoints(cps, 1f, Color.black);
            IGDPlotter.drawControlPolygon(cps, 1f, Color.black);

            //plot the rational bezier cuve
            IGDPlotter.drawPolyline3D(rbcvPts, 1f);

            //draw first derivatevs
            Vector3 firstDerAtStart = rbcv.calcDer(1, 0.0);
            IGDPlotter.drawArrow3D(rbcv.calcPos(0.0), rbcv.calcPos(0.0) + firstDerAtStart, 1f, Color.blue);
            IGDPlotter.writeFormula3D("C'(0)", rbcv.calcPos(0.0) + firstDerAtStart, 1f, Color.blue);

            Vector3 firstDerAtHalf = rbcv.calcDer(1, 0.5);
            IGDPlotter.drawArrow3D(rbcv.calcPos(0.5), rbcv.calcPos(0.5) + firstDerAtHalf, 1f, Color.blue);
            IGDPlotter.writeFormula3D("C'(0.5)", rbcv.calcPos(0.5) + firstDerAtHalf, 1f, Color.blue);

            Vector3 firstDerAtEnd = rbcv.calcDer(1, 1.0);
            IGDPlotter.drawArrow3D(rbcv.calcPos(1.0), rbcv.calcPos(1.0) + firstDerAtEnd, 1f, Color.blue);
            IGDPlotter.writeFormula3D("C'(1)", rbcv.calcPos(1.0) + firstDerAtEnd, 1f, Color.blue);


            //draw second derivatevs
            Vector3 secondDerAtStart = rbcv.calcDer(2, 0.0);
            IGDPlotter.drawArrow3D(rbcv.calcPos(0.0), rbcv.calcPos(0.0) + secondDerAtStart, 1f, Color.red);
            IGDPlotter.writeFormula3D("C''(0)", rbcv.calcPos(0.0) + secondDerAtStart, 1f, Color.red);

            Vector3 secondDerAtHalf = rbcv.calcDer(2, 0.5);
            IGDPlotter.drawArrow3D(rbcv.calcPos(0.5), rbcv.calcPos(0.5) + secondDerAtHalf, 1f, Color.red);
            IGDPlotter.writeFormula3D("C''(0.5)", rbcv.calcPos(0.5) + secondDerAtHalf, 1f, Color.red);

            Vector3 secondDerAtEnd = rbcv.calcDer(2, 1.0);
            IGDPlotter.drawArrow3D(rbcv.calcPos(1.0), rbcv.calcPos(1.0) + secondDerAtEnd, 1f, Color.red);
            IGDPlotter.writeFormula3D("C''(1)", rbcv.calcPos(1.0) + secondDerAtEnd, 1f, Color.red);

            IGDPlotter.drawKnotVector(U, 1500f);


        }
    }
    
}