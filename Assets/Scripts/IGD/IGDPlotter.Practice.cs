using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
            for (int i = 0; i <=  sampleNum; i++) {
                double u = (double)i / sampleNum;
                double B25der = XBezier.calcDerivBernsteinPolynomial(1, 2, 5, u);
                Vector2 pt = new Vector2((float)u * xMax, (float)B25der * yMax);
                B25DerPts.Add(pt);
            }

            IGDPlotter.drawPolyline2D(B25DerPts, 0.7f);
        }
    }
}