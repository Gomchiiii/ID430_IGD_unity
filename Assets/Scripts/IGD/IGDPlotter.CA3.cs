using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using XAppObject;
using Xgeom.NURBS;
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

            //1st ~ 4th order derivative colors. 
            List<Color> Colors= new List<Color>();
            Colors.Add(Color.blue);
            Colors.Add(Color.cyan);
            Colors.Add(Color.gray);
            Colors.Add(Color.green);

            //Draw x, y, and z 
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            List<Vector3> bcvDerPts = new List<Vector3>();

            int Derorder = 3;

            //Draw Bezier curve
            XBezierCurve3D bcv = new XBezierCurve3D(cps.Length - 1, cps);
            List<Vector3> bcvPts = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                bcvPts.Add(bcv.calcPos((double)i / 100.0));

                //Calc DerPts
                if (i % 25 == 0) {
                    for (int j = 1; j <= Derorder; j++) {
                        //normalize and put vector on each point  
                        bcvDerPts.Add(Vector3.Normalize(bcv.calcDer(j, (double)i / 100.0)) * 0.15f);
                    }
                }
            }

            //Draw and Label
            IGDPlotter.drawPolyline3D(bcvPts, 0.7f);
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(cps).ToList(), 0.7f);

            for (int i = 0; i <= n; i++) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(cps)[i]);
                string PointPath = "Formulas/P" + i.ToString();
                IGDPlotter.addImage3D(PointPath, XCPsUtil.perspectiveMap(cps)[i] + 0.05f * Vector3.up, 0.5f);

            }

            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < Derorder; j++) {
                    IGDPlotter.drawArrow3D(bcvPts[i * 25], bcvPts[i * 25] + bcvDerPts[i * Derorder + j], 0.5f, Colors[j]);
                }
            }

            //Mark each Der 
            IGDPlotter.addImage3D("Formulas/MarkCu1stDer", Vector3.forward, 0.5f, Colors[0]);
            IGDPlotter.addImage3D("Formulas/MarkCu2ndDer", Vector3.forward + 0.1f * Vector3.up, 0.5f, Colors[1]);
            IGDPlotter.addImage3D("Formulas/MarkCu3rdDer", Vector3.forward + 0.2f * Vector3.up, 0.5f, Colors[2]);


        }

        // CA3-3: Calculate the k-th derivative curve of an n-th degree Bezier
        // curve and draw it along with the control points and control polygon.
        public static void CA3_3(int n, int k, Vector4[] cps) {

            //1st ~ 4th order derivative colors. 
            List<Color> Colors = new List<Color>();
            Colors.Add(Color.blue);
            Colors.Add(Color.cyan);
            Colors.Add(Color.gray);
            Colors.Add(Color.green);


            //Draw x, y, and z 
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            List<Vector3> bcvDerPts = new List<Vector3>();

            //Calc Bezier curve and its derivative 
            XBezierCurve3D bcv = new XBezierCurve3D(cps.Length - 1, cps);
            List<Vector3> bcvPts = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                bcvPts.Add(bcv.calcPos((double)i / 100.0));
                bcvDerPts.Add(bcv.calcDer(k, (double)i / 100.0));
            }

            IGDPlotter.drawPolyline3D(bcvPts, 0.7f);
            IGDPlotter.drawPolyline3D(bcvDerPts, 1, Colors[k - 1]);
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(cps).ToList(), 0.7f);

            for (int i = 0; i <= n; i++) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(cps)[i]);
                //Label points 
                string PointPath = "Formulas/P" + i.ToString();
                IGDPlotter.addImage3D(PointPath, XCPsUtil.perspectiveMap(cps)[i] + 0.05f * Vector3.up, 0.5f);
            }

            IGDPlotter.addImage3D("Formulas/MarkCu1stDer", Vector3.forward, 0.5f, Color.blue);
            IGDPlotter.addImage3D("Formulas/MarkCu2ndDer", Vector3.forward + 0.1f * Vector3.up, 0.5f, Color.cyan);
            IGDPlotter.addImage3D("Formulas/MarkCu3rdDer", Vector3.forward + 0.2f * Vector3.up, 0.5f, Color.gray);
        }
        
        // CA3-4: Draw the first and second derivative vectors at the end points
        // of the n-th degree Bezier curve.
        public static void CA3_4(int n, Vector4[] cps) {
            //1st ~ 4th order derivative colors. 
            List<Color> Colors = new List<Color>();
            Colors.Add(Color.blue);
            Colors.Add(Color.cyan);
            Colors.Add(Color.gray);
            Colors.Add(Color.green);

            //Draw x, y, and z 
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            List<Vector3> bcvDerPts = new List<Vector3>();

            int Derorder = 2;

            //Draw Bezier curve
            XBezierCurve3D bcv = new XBezierCurve3D(cps.Length - 1, cps);
            List<Vector3> bcvPts = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                bcvPts.Add(bcv.calcPos((double)i / 100.0));

                //Calc DerPts
                if (i % 100 == 0) {
                    for (int j = 1; j <= Derorder; j++) {
                        //크기가 다 달라서 normalize 
                        bcvDerPts.Add(Vector3.Normalize(bcv.calcDer(j, (double)i / 100.0)) * 0.15f);
                    }
                }
            }

            IGDPlotter.drawPolyline3D(bcvPts, 0.7f);
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(cps).ToList(), 0.7f);

            for (int i = 0; i <= n; i++) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(cps)[i]);
                string PointPath = "Formulas/P" + i.ToString();
                IGDPlotter.addImage3D(PointPath, XCPsUtil.perspectiveMap(cps)[i] + 0.05f * Vector3.up, 0.5f);
            }

            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < Derorder; j++) {
                    IGDPlotter.drawArrow3D(bcvPts[i * 100], bcvPts[i * 100] + bcvDerPts[i * Derorder + j], 0.5f, Colors[j]);
                }
            }


            IGDPlotter.addImage3D("Formulas/MarkCu1stDer", Vector3.forward, 0.5f, Color.blue);
            IGDPlotter.addImage3D("Formulas/MarkCu2ndDer", Vector3.forward + 0.1f * Vector3.up, 0.5f, Color.cyan);

        }
        
        // CA3-5: (1) Implement XBezierCurve3D.calcPosByDeCasteljauAlg.
        // (2) Draw Bezier curves of various degrees along with their control
        // points and control polygon.
        public static void CA3_5(int n, Vector4[] cps) {
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

            for (int i = 0; i <= n; i++) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(cps)[i]);
                string PointPath = "Formulas/P" + i.ToString();
                IGDPlotter.addImage3D(PointPath, XCPsUtil.perspectiveMap(cps)[i] + 0.05f * Vector3.up, 0.5f);
            }

        }
        
        // CA3-6: Using the de Casteljau algorithm, write code to split an n-th
        // degree Bezier curve at a given parameter u, and draw examples of
        // Bezier curves of various degrees being divided. Include the control
        // points and control polygon of the intermediate steps.
        public static void CA3_6(int n, Vector4[] cps, double u) {

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
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(cps).ToList(), 1f);

            Vector4[] Q = new Vector4[n + 1];

            //Control points of new Bezier curve. 
            Vector4[] Q1 = new Vector4[n + 1];
            Vector4[] Q2 = new Vector4[n + 1];

            for (int i = 0; i <= n; i++) {
                Q[i] = cps[i];
            }

            for (int k = 1; k <= n; k++) {
                for (int i = 0; i <= n - k; i++) {
                    Q[i] = (float)(1.0f - u) * Q[i] + (float)u * Q[i + 1];
                    IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(Q)[i], 0.5f, Color.gray);

                }
                IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(Q).ToList(), 0.5f, Color.gray);
                Q1[k] = Q[0];
                Q2[n - k] = Q[n - k];
            }


            //Draw devided dot 
            IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(Q)[0], 1.5f);
            Q1[0] = cps[0];
            Q2[n] = cps[n];

            XBezierCurve3D bcv1 = new XBezierCurve3D(n, Q1);
            XBezierCurve3D bcv2 = new XBezierCurve3D(n, Q2);


            for (int i = 0; i <= n; i++) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(cps)[i]);
                string PointPath = "Formulas/P" + i.ToString();
                IGDPlotter.addImage3D(PointPath, XCPsUtil.perspectiveMap(cps)[i] + 0.05f * Vector3.up, 0.5f);
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(Q1)[i], 0.7f, Color.blue);
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(Q2)[i], 0.7f, Color.red);

            }

            List<Vector3> bcv1PtS = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                bcv1PtS.Add(bcv1.calcPos((double)i / 100.0));
            }
            IGDPlotter.drawPolyline3D(bcv1PtS, 1f, Color.blue);

            List<Vector3> bcv2PtS = new List<Vector3>();
            for (int i = 0; i <= 100; i++) {
                bcv2PtS.Add(bcv2.calcPos((double)i / 100.0));
            }
            IGDPlotter.drawPolyline3D(bcv2PtS, 1f, Color.red);

            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(Q1).ToList(), 0.7f, Color.blue);
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(Q2).ToList(), 0.7f, Color.red);
        }
    }
}