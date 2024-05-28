using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using XAppObject;
using XGeom.NURBS;
using XGeom.NURBS.CurveFit;


// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public static partial class IGDPlotter {
        public static void W01_draw2DVectors() {
            // Draw a dot (0, 0)
            IGDPlotter.drawDot2D(Vector2.zero);

            // Draw x and y axes
            float yMax = Screen.height * 0.6f;
            float yMin = -Screen.height * 0.2f;
            Vector2 yAxisStart = new Vector2(0f, yMin);
            Vector2 yAxisEnd = new Vector2(0f, yMax);
            IGDPlotter.drawArrow2D(yAxisStart, yAxisEnd);

            float xMax = yMax;
            float xMin = yMin;
            Vector2 xAxisStart = new Vector2(xMin, 0f);
            Vector2 xAxisEnd = new Vector2(xMax, 0f);
            IGDPlotter.drawArrow2D(xAxisStart, xAxisEnd);

            // Draw dots A and B 
            Vector2 A = new Vector2(xMin * 0.9f, yMax * 0.4f);
            Vector2 B = new Vector2(xMax * 0.2f, yMax * 0.5f);
            IGDPlotter.drawDot2D(A);
            IGDPlotter.drawDot2D(B);

            // Draw vector AB
            IGDPlotter.drawArrow2D(A, B);

            //Label vector AB
            Vector2 labelPosAB = (A + B) / 2f;
            float offset = 50f;
            labelPosAB += Vector2.up * offset;
            labelPosAB += Vector2.left * (offset * 0.5f);
            IGDPlotter.addImage2D("Formulas/W01_vecAB", labelPosAB, 0.7f);

            //Label A and B
            IGDPlotter.addImage2D("Letters/A", A + Vector2.up * offset, 0.7f);
            IGDPlotter.addImage2D("Letters/B", B + Vector2.up * offset, 0.7f);

            //Label x and y axes
            IGDPlotter.addImage2D("Letters/x", xAxisEnd + Vector2.right * offset, 0.7f);
            IGDPlotter.addImage2D("Letters/y", yAxisEnd + Vector2.up * offset, 0.7f);

            //Draw vetctor OP
            Vector2 vecAB = B - A;
            Vector2 P = vecAB;
            IGDPlotter.drawArrow2D(Vector2.zero, P);

            //Lavel 0 and P
            IGDPlotter.addImage2D("Letters/O", Vector2.left * offset + Vector2.up * offset, 0.7f);
            IGDPlotter.addImage2D("Letters/P", P + Vector2.left * offset + Vector2.up * offset, 0.7f);

            // Draw dots C and D 
            Vector2 C = new Vector2(xMax * 0.5f, yMax * 0.4f);
            Vector2 D = C + vecAB;
            IGDPlotter.drawDot2D(C);
            IGDPlotter.drawDot2D(D);

            //Label C and D
            IGDPlotter.addImage2D("Letters/C", C + Vector2.up * offset, 0.7f);
            IGDPlotter.addImage2D("Letters/D", D + Vector2.up * offset, 0.7f);

            // Draw vector CD
            IGDPlotter.drawArrow2D(C, D);

            // Draw dots E and F 
            Vector2 E = new Vector2(xMin * 0.9f, yMin * 0.4f);
            Vector2 F = E + vecAB;
            IGDPlotter.drawDot2D(E);
            IGDPlotter.drawDot2D(F);

            //Label E and F
            IGDPlotter.addImage2D("Letters/E", E + Vector2.up * offset, 0.7f);
            IGDPlotter.addImage2D("Letters/F", F + Vector2.up * offset, 0.7f);

            // Draw vector EF
            IGDPlotter.drawArrow2D(E, F);

            // Draw dashed lines AC, BD, AE, BF
            IGDPlotter.drawDashedLine2D(A, C, 1f, Color.red);
            IGDPlotter.drawDashedLine2D(B, D, 1f, Color.red);
            IGDPlotter.drawDashedLine2D(A, E, 1f, Color.red);
            IGDPlotter.drawDashedLine2D(B, F, 1f, Color.red);


        }

        public static void W02_drawBezierCure() {
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            Vector4[] cps = new Vector4[5];
            cps[0] = new Vector4(1f, 0f, 0f, 0f);
            cps[1] = new Vector4(1f, 1f, 0f, 1f);
            cps[2] = new Vector4(1f, 1f, 1f, 1f);
            cps[3] = new Vector4(0f, 1f, 1f, 1f);
            cps[4] = new Vector4(0f, 0f, 1f, 1f);

            XBezierCurve3D bcv = new XBezierCurve3D(cps.Length - 1, cps);
            List<Vector3> bcvPtS = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                bcvPtS.Add(bcv.calcPos((double)i / 100.0));
            }
            IGDPlotter.drawPolyline3D(bcvPtS, 0.7f);
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(cps).ToList(), 0.7f);
        }

        public static void W03_drawBernsteinDerivatives() {
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

            //Plot B_2,5(u)
            List<Vector2> B25Pts = new List<Vector2>();
            int sampleNum = 100;
            for (int i = 0; i < sampleNum; i++) {
                double u = (double)i / sampleNum;
                double B25 = XBezier.calcBernsteinPolynomial(2, 5, u);
                Vector2 pt = new Vector2((float)u * xMax, (float)B25 * yMax);
                B25Pts.Add(pt);
            }

            IGDPlotter.drawPolyline2D(B25Pts, 0.7f);


            //Plot B_1,4(u)
            List<Vector2> B14Pts = new List<Vector2>();
            for (int i = 0; i < sampleNum; i++) {
                double u = (double)i / sampleNum;
                double B14 = XBezier.calcBernsteinPolynomial(1, 4, u);
                Vector2 pt = new Vector2((float)u * xMax, (float)B14 * yMax);
                B14Pts.Add(pt);
            }


            IGDPlotter.drawPolyline2D(B14Pts, 0.7f);



            //Plot B_2,4(u)
            List<Vector2> B24Pts = new List<Vector2>();
            for (int i = 0; i < sampleNum; i++) {
                double u = (double)i / sampleNum;
                double B24 = XBezier.calcBernsteinPolynomial(2, 4, u);
                Vector2 pt = new Vector2((float)u * xMax, (float)B24 * yMax);
                B24Pts.Add(pt);
            }


            IGDPlotter.drawPolyline2D(B24Pts, 0.7f);

            // Plot B'_2,5(u)
            List<Vector2> B25DerPts = new List<Vector2>();
            for (int i = 0; i <= sampleNum; i++) {
                double u = (double)i / sampleNum;
                double B25der = XBezier.calcDerivBernsteinPolynomial(1, 2, 5, u);
                Vector2 pt = new Vector2((float)u * xMax, (float)B25der * yMax);
                B25DerPts.Add(pt);
            }

            IGDPlotter.drawPolyline2D(B25DerPts, 0.7f);
        }

        public static void W04_drawBSPlineBasisFns() {
            float xMax = Screen.width / 1.5f;
            float yMax = Screen.height / 1.5f;

            IGDPlotter.drawArrow2D(Vector2.zero, new Vector2(xMax, 0));
            IGDPlotter.drawArrow2D(Vector2.zero, new Vector2(0, yMax));
            float unitLength = xMax / 5f;

            //Knot Vector in Fig 2.6
            double[] knotVector =
                new double[] { 0, 0, 0, 1, 2, 3, 4, 4, 5, 5, 5 };
            double us = knotVector[0];
            double ue = knotVector[knotVector.Length - 1];

            //Sampling for render 
            int usSample = (int)(us * (double)unitLength);
            int ueSample = (int)(ue * (double)unitLength);

            // m = 11, p = 2
            int m = knotVector.Length;
            int p = 2;

            //point to render the basis function 
            List<List<Vector2>> basisFnsPts = new List<List<Vector2>>();
            for (int i = 0; i < m - p - 1; i++) {
                basisFnsPts.Add(new List<Vector2>());
            }

            for (int i = usSample; i <= ueSample; i++) {
                double[] BasisFnsValues = XBSpline.calcBasisFns((double)i / (double)unitLength, p, knotVector);
                for (int j = 0; j < BasisFnsValues.Length; j++) {
                    double basisFnsValue = BasisFnsValues[j];
                    basisFnsPts[j].Add(new Vector2(i, (float)basisFnsValue * unitLength));
                }
            }
            //plot each basis funs in hsb colors 
            for (int i = 0; i < basisFnsPts.Count; i++) {
                drawPolyline2D(basisFnsPts[i], 0.7f, Color.HSVToRGB((float)i / (float)basisFnsPts.Count, 1, 1));
            }
        }

        public static void W05_drawDerivBsisFns() {
            float unitLen = Screen.width / 5f;

            //draw u and N axes
            float uAxisLength = 4.5f * unitLen;
            float NAxisLength = 2f * unitLen;

            Vector2 uAxisEnd = new Vector2(uAxisLength, 0f);
            Vector2 NAxisEnd = new Vector2(0f, NAxisLength);
            IGDPlotter.drawArrow2D(Vector2.zero, uAxisEnd);
            IGDPlotter.drawArrow2D(Vector2.zero, NAxisEnd);

            //Plot 0-th to 2nd derivatives of .....
            int order = 2;
            int p = 2;
            double[] U = { 0, 0, 0, 2, 3, 3, 4, 4, 4 };
            double us = 0.0;
            double ue = 4.0;
            int m = U.Length - 1;
            int n = m - p - 1;
            int i = 2;

            int samplenum = 1000;
            for (int j = 0; j <= samplenum; j++) {
                double u = us + (double)j / samplenum * (ue - us);
                double[,] dN = XBSpline.calcAllDerivBasisFns(order, u, p, U);

                //Plot
                double dN0 = dN[0, i];
                Vector2 pt0 = new Vector2((float)u * unitLen, (float)dN0 * unitLen);
                IGDPlotter.drawDot2D(pt0, 0.3f, Color.black);


                double dN1 = dN[1, i];
                Vector2 pt1 = new Vector2((float)u * unitLen, (float)dN1 * unitLen);
                IGDPlotter.drawDot2D(pt1, 0.3f, Color.blue);

                double dN2 = dN[2, i];
                Vector2 pt2 = new Vector2((float)u * unitLen, (float)dN2 * unitLen);
                IGDPlotter.drawDot2D(pt2, 0.3f, Color.red);
            }


        }

        public static void W06_drawRationalBezierCurve() {
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
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(cps).ToList(), 0.7f);
            foreach (Vector4 v in cps) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(v));
            }

            //plot the rational bezier cuve
            IGDPlotter.drawPolyline3D(rbcvPts, 1f);

            //Draw an arc using cos, sin 
            List<Vector3> circlePts = new List<Vector3>();
            for (int i = 0; i <= 90; i++) {
                circlePts.Add(new Vector3(Mathf.Cos(i * Mathf.Deg2Rad),
                    Mathf.Sin(i * Mathf.Deg2Rad), 0));
            }
            IGDPlotter.drawPolyline3D(circlePts, 1.2f, Color.red);

        }

        public static void W06_homogeneousCoordinate() {
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            //Draw an arc using rational Bezier curve

            Vector4[] cps = new Vector4[4];
            cps[0] = new Vector4(1f, 0f, 1f, 1f);
            cps[1] = new Vector4(1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                1f * (float)Math.Cos(45 * Mathf.Deg2Rad), (float)Math.Cos(45 * Mathf.Deg2Rad));
            cps[2] = new Vector4(0f, 1f, 1f, 1f);
            cps[3] = new Vector4(-1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                -1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                1f * (float)Math.Cos(45 * Mathf.Deg2Rad), (float)Math.Cos(45 * Mathf.Deg2Rad));

            XBezierCurve3D rbcv = new XBezierCurve3D(cps.Length - 1, cps);
            List<Vector3> rbcvPts = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                rbcvPts.Add(rbcv.calcPos((double)i / 100.0));
            }

            //plot
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(cps).ToList(), 0.7f);
            foreach (Vector4 v in cps) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(v));
            }

            //plot the rational bezier cuve
            IGDPlotter.drawPolyline3D(rbcvPts, 1f);

            //draw non- rational Bezier curve
            //each control point hs the same coordinate except weight is 1
            cps = new Vector4[4];
            cps[0] = new Vector4(1f, 0f, 1f, 1f);
            cps[1] = new Vector4(1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
                1f * (float)Math.Cos(45 * Mathf.Deg2Rad), 1f);
            cps[2] = new Vector4(0f, 1f, 1f, 1f);
            cps[3] = new Vector4(-1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
    -1f * (float)Math.Cos(45 * Mathf.Deg2Rad),
    1f * (float)Math.Cos(45 * Mathf.Deg2Rad), 1f);

            XBezierCurve3D bcv = new XBezierCurve3D(cps.Length - 1, cps);
            List<Vector3> bcvPts = new List<Vector3>();

            for (int i = 0; i <= 100; i++) {
                bcvPts.Add(bcv.calcPos((double)i / 100.0));

            }

            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(cps).ToList(), 0.5f, Color.red);
            foreach (Vector4 v in cps) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(v), 0.5f, Color.red);
            }

            //PLOT THE BEZIER CURVE
            IGDPlotter.drawPolyline3D(bcvPts, 0.5f, Color.red);
        }


        public static void W06_drawDerivatives() {
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
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(cps).ToList(), 0.7f);
            foreach (Vector4 v in cps) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(v));
            }

            //plot the rational bezier cuve
            IGDPlotter.drawPolyline3D(rbcvPts, 1f);

            //draw first derivatevs
            Vector3 firstDerAtStart = rbcv.calcDer(1, 0.0);
            IGDPlotter.drawArrow3D(rbcv.calcPos(0.0), rbcv.calcPos(0.0) + firstDerAtStart, 1f, Color.blue);

            Vector3 firstDerAtHalf = rbcv.calcDer(1, 0.5);
            IGDPlotter.drawArrow3D(rbcv.calcPos(0.5), rbcv.calcPos(0.5) + firstDerAtHalf, 1f, Color.blue);

            Vector3 firstDerAtEnd = rbcv.calcDer(1, 1.0);
            IGDPlotter.drawArrow3D(rbcv.calcPos(1.0), rbcv.calcPos(1.0) + firstDerAtEnd, 1f, Color.blue);


            //draw second derivatevs
            Vector3 secondDerAtStart = rbcv.calcDer(2, 0.0);
            IGDPlotter.drawArrow3D(rbcv.calcPos(0.0), rbcv.calcPos(0.0) + secondDerAtStart, 1f, Color.red);

            Vector3 secondDerAtHalf = rbcv.calcDer(2, 0.5);
            IGDPlotter.drawArrow3D(rbcv.calcPos(0.5), rbcv.calcPos(0.5) + secondDerAtHalf, 1f, Color.red);

            Vector3 secondDerAtEnd = rbcv.calcDer(2, 1.0);
            IGDPlotter.drawArrow3D(rbcv.calcPos(1.0), rbcv.calcPos(1.0) + secondDerAtEnd, 1f, Color.red);


        }

        //public static void W08_drawKnotInsertedCurve() {
        //    ///Draw axes
        //    IGDPlotter.draw3DAxes(5f, 5f, 5f, 5f, Color.black, "x", "y", "", 4f);

        //    //Define the Bspline curve
        //    int p = 3;
        //    double[] U = new double[] {
        //        0, 0, 0, 0, 1, 2, 3, 4, 5, 5, 5, 5
        //    };

        //    Vector4[] Pw = new Vector4[] {
        //        new Vector4(-6f, -1f, 0f, 1f),
        //        new Vector4(-5f, 2f, 0f, 1f),
        //        new Vector4(-3f, 3f, 0f, 1f),
        //        new Vector4(-1f, 2f, 0f, 1f),
        //        new Vector4(0f, 0f, 0f, 1f),
        //        new Vector4(3f, 1f, 0f, 1f),
        //        new Vector4(3f, 3f, 0f, 1f),
        //        new Vector4(1f, 5f, 0f, 1f),
        //    };

        //    XBSplineCurve3D cvBef = new XBSplineCurve3D(p, U, Pw);

        //    // draw
        //    IGDPlotter.drawParametricCurve3D(cvBef, 300, 5f, Color.black);
        //    IGDPlotter.drawControlPoints(Pw, 8F, Color.black);
        //    IGDPlotter.drawControlPolygon(Pw, 5f, Color.black);
        //    IGDPlotter.lableControlPoints(Pw, 3f, Color.black, "\\mathbf{P}", Vector3.down * 0.3f);

        //    //insert knots
        //    double u = 2.3;
        //    int r = 3;
        //   // XBSplineCurve3D cvAft = XKnotInsertion.createKnotInsertedCurve(u, r, cvBef);
        //    Vector4[] Qw = cvAft.getCPs();

        //    IGDPlotter.drawParametricCurve3D(cvAft, 300, 5f, Color.red);
        //    IGDPlotter.drawControlPoints(Qw, 8F, Color.red);
        //    IGDPlotter.drawControlPolygon(Qw, 5f, Color.red);
        //    IGDPlotter.lableControlPoints(Qw, 3f, Color.red, "\\mathbf{Q}", Vector3.down * 0.3f);
        //}

        public static void W09_drawInvertedPoint() {
            ///Draw axes
            IGDPlotter.draw3DAxes(5f, 5f, 5f, 5f, Color.black, "x", "y", "", 4f);

            //Define the Bspline curve
            int p = 3;
            double[] U = new double[] {
                0, 0, 0, 0, 1, 2, 3, 4, 5, 5, 5, 5
            };

            Vector4[] Pw = new Vector4[] {
                new Vector4(-6f, -1f, 0f, 1f),
                new Vector4(-5f, 2f, 0f, 1f),
                new Vector4(-3f, 3f, 0f, 1f),
                new Vector4(-1f, 2f, 0f, 1f),
                new Vector4(0f, 0f, 0f, 1f),
                new Vector4(3f, 1f, 0f, 1f),
                new Vector4(3f, 3f, 0f, 1f),
                new Vector4(1f, 5f, 0f, 1f),
            };

            XBSplineCurve3D cv = new XBSplineCurve3D(p, U, Pw);

            // draw
            IGDPlotter.drawParametricCurve3D(cv, 300, 5f, Color.black);
            IGDPlotter.drawControlPoints(Pw, 8F, Color.black);
            IGDPlotter.drawControlPolygon(Pw, 5f, Color.black);
            //IGDPlotter.lableControlPoints(Pw, 3f, Color.black, "\\mathbf{P}", Vector3.down * 0.3f);

            //insert knots
            Vector3 nearPoint = new Vector3(2f, 1f, 0f);
            IGDPlotter.drawDot3D(nearPoint, 10f, Color.red);

            double u = cv.calcParamAtPt(nearPoint);
            Debug.Log(u);
            IGDPlotter.drawDot3D(cv.calcPos(u), 10f, Color.blue);

        }

        public static void W09_drawInterPolatedCurve() {
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

        public static void W10_drawBezierSurfac() {
            //draw axes
            IGDPlotter.draw3DAxes(5f, "x", "y", "z");

            //define surface
            int n = 3;
            int m = 3;
            Vector4[,] cps = new Vector4[n + 1, m + 1];
            for (int i = 0; i <= n; i++) {
                for (int j = 0; j <= m; j++) { 
                    float x = i * 1f;
                    float y = UnityEngine.Random.Range(0f, 2f);
                    float z = j * 1f;
                    Vector4 cp = new Vector4(x, y, z, 1f);
                    cps[i, j] = cp;
                }
            }
            XBezierSurface3D surface = new XBezierSurface3D(n, m, cps);

            IGDPlotter.drawBezierSurface3D(surface);
            IGDPlotter.drawControlPoints(cps, 1f, Color.blue);
            IGDPlotter.drawControlNet(cps, 1f, Color.blue);
            IGDPlotter.labelControlPoints(cps, 1f, Color.blue, "P", Vector3.up * 0.1f);

            //Draw boundary curves
            XBezierCurve3D cvUStart = surface.calcIsoCurve(0.0, XGeom.XParametricSurface3D.Dir.U);
            XBezierCurve3D cvUEnd = surface.calcIsoCurve(1.0, XGeom.XParametricSurface3D.Dir.U);
            XBezierCurve3D cvVStart = surface.calcIsoCurve(0.0, XGeom.XParametricSurface3D.Dir.V);
            XBezierCurve3D cvVEnd = surface.calcIsoCurve(1.0, XGeom.XParametricSurface3D.Dir.V);

            IGDPlotter.drawParametricCurve3D(cvUStart, cvUStart.getStartParam(), cvUStart.getEndParam(), 
                100, 1f, Color.black);



            //for (int i = 0; i <= 20; i++) {
            //    for (int j = 0; j <= 20; j++) {
            //        double u = i / 20.0;
            //        double v = j / 20.0;
            //        Vector3 pt = surface.calcPos(u, v);
            //        IGDPlotter.drawDot3D(pt, 0.2f, Color.blue);
            //    }
            //}
        }
    }

}