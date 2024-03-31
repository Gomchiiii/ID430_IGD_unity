using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using XAppObject;
using Xgeom.NURBS;
using XGeom.NURBS;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public static partial class IGDPlotter {
        // Use this CPs in CA5-2 to CA5-6 and compare the results with the
        // reference in the assignment document.
        public static Vector4[] getReferenceCPs() {
            Vector4[] cps = {
                new Vector4(0.7f, 0.2f, -0.5f, 1f),
                new Vector4(0.4f, 0.7f, -0.2f, 1f),
                new Vector4(0.1f, 0.9f, 0.2f, 1f),
                new Vector4(0.2f, 0.6f, 0.5f, 1f),
                new Vector4(0.1f, 0.2f, 0.4f, 1f),
                new Vector4(-0.2f, 0.2f, 1f, 1f)
            };
            return cps;
        }
        
        // CA5-1: Write code to calculate the k-th derivative value of the p-th order
        // B-spline basis function given a parameter u, and draw the graphs of the first
        // second derivatives of the quadratic B-spline basis function defined on
        // U = {0, 0, 0, 2, 3, 3, 4, 4, 4} along with the knot vector.
        public static void CA5_1() {
            float unitLen = Screen.width / 5f;

            //draw u and N axes
            float uAxisLength = 4.5f * unitLen;
            float NAxisLength = 2f * unitLen;

            Vector2 uAxisEnd = new Vector2(uAxisLength, 0f);
            Vector2 NAxisEnd = new Vector2(0f, NAxisLength);
            IGDPlotter.drawArrow2D(Vector2.zero, uAxisEnd);
            IGDPlotter.drawArrow2D(Vector2.zero, NAxisEnd);

            //Plot 0-th to 2nd derivatives of...
            int order = 2;
            int p = 2;
            double[] U = { 0, 0, 0, 2, 3, 3, 4, 4, 4 };
            double us = 0.0;
            double ue = 4.0;
            int m = U.Length - 1;
            int n = m - p - 1;
            int i = 2;

            List<Vector2> basisFns = new List<Vector2>();
            List<Vector2> basisFnsder = new List<Vector2>();
            List<Vector2> basisFns2ndder = new List<Vector2>();

            int samplenum = 1000;
            for (int j = 0; j <= samplenum; j++) {
                double u = us + (double)j / samplenum * (ue - us);
                double[,] dN = XBspline.calcAllDerivBasisFns(order, u, p, U);

                //B-spline basis function. 
                double dN0 = dN[0, i];
                Vector2 pt0 = new Vector2((float)u * unitLen, (float)dN0 * unitLen);
                basisFns.Add(pt0);

                //1th derivative 
                double dN1 = dN[1, i];
                Vector2 pt1 = new Vector2((float)u * unitLen, (float)dN1 * unitLen);
                basisFnsder.Add(pt1);

                //2nd derivative 
                double dN2 = dN[2, i];
                Vector2 pt2 = new Vector2((float)u * unitLen, (float)dN2 * unitLen);
                basisFns2ndder.Add(pt2);
            }

            //Plot 
            IGDPlotter.drawDashedPolyline2D(basisFns);
            IGDPlotter.drawPolyline2D(basisFnsder);
            IGDPlotter.drawPolyline2D(basisFns2ndder, 1.0f, Color.red);

        }
    
        
        // CA5-2: Write code to calculate a point on the p-th order B-spline curve given a
        // parameter u, and draw the quadratic B-spline curve (with arbitrarily chosen
        // control points) defined on U = {0, 0, 0, 2, 3, 3, 4, 4, 4} along with the knot
        // vector, control points, and control polygon.
        public static void CA5_2() {
            //Draw x, y, and z 
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            double[] U = { 0, 0, 0, 2, 3, 3, 4, 4, 4 };
            double us = 0.0;
            double ue = 4.0;

            int deg = 2; //U.Length - IGDPlotter.getReferenceCPs().Length - 1;
            //Debug.Log("deg = " + deg);

            XBSplineCurve3D bcv = new XBSplineCurve3D(deg, U, IGDPlotter.getReferenceCPs());
            //Debug.Log("bcv creation");
            List<Vector3> bcvPtS = new List<Vector3>();

            for (int i = 0; i <= 100; i++) {
                double u = us + (double)i / 100.0 * (ue - us);
                //Debug.Log(u);
                bcvPtS.Add(bcv.calcPos(u));
            }
            IGDPlotter.drawPolyline3D(bcvPtS, 0.7f, Color.blue);
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(IGDPlotter.getReferenceCPs()).ToList(), 0.7f);

            for (int i = 0; i <= IGDPlotter.getReferenceCPs().Length - 1; i++) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(IGDPlotter.getReferenceCPs())[i]);
                //Label points 
                string PointPath = "Formulas/P" + i.ToString();
                IGDPlotter.addImage3D(PointPath, XCPsUtil.perspectiveMap(IGDPlotter.getReferenceCPs())[i] + 0.05f * Vector3.up, 0.5f);
            }
        }

        // CA5-3: Write code to calculate the k-th derivative value of a p-th order
        // B-spline curve given a parameter u, and draw the first and second derivative
        // vectors at u = 0, 1, 2, 3, 4 on the quadratic B-spline curve (with arbitrarily
        // chosen control points) defined on U = {0, 0, 0, 2, 3, 3, 4, 4, 4}.
        public static void CA5_3() {
            //1st ~ 4th order derivative colors. 
            List<Color> Colors = new List<Color>();
            Colors.Add(Color.red);
            Colors.Add(Color.cyan);
            Colors.Add(Color.gray);
            Colors.Add(Color.green);

            //Draw x, y, and z 
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.right, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.forward, 0.7f);
            IGDPlotter.drawArrow3D(Vector3.zero, Vector3.up, 0.7f);

            double[] U = { 0, 0, 0, 2, 3, 3, 4, 4, 4 };
            double us = 0.0;
            double ue = 4.0;

            int deg = 2;
            int Derorder = 2;

            List<Vector3> bcvDerPts = new List<Vector3>();
            List<Vector3> bcv2ndDerPts = new List<Vector3>();
            List<Vector3> bcvPtsforDerLabeling = new List<Vector3>();

            //Bspline curve
            XBSplineCurve3D bcv = new XBSplineCurve3D(deg, U, IGDPlotter.getReferenceCPs());
            List<Vector3> bcvPts = new List<Vector3>();

            for (int i = 0; i <= 100; i++) {
                double u = us + (double)i / 100.0 * (ue - us);
                //Debug.Log(u);
                bcvPts.Add(bcv.calcPos(u));

                if (u == 0 || u == 1 || u == 2 || u == 3 || u == 4) {
                    Debug.Log(bcv.calcDer(1, u));
                    bcvDerPts.Add(bcv.calcDer(1, u));
                    bcv2ndDerPts.Add(bcv.calcDer(2, u));
                    bcvPtsforDerLabeling.Add(bcv.calcPos(u));
                }
            }

            IGDPlotter.drawPolyline3D(bcvPts, 0.7f, Color.blue);
            IGDPlotter.drawDashedPolyline3D(XCPsUtil.perspectiveMap(IGDPlotter.getReferenceCPs()).ToList(), 0.7f);

            for (int i = 0; i <= IGDPlotter.getReferenceCPs().Length - 1; i++) {
                IGDPlotter.drawDot3D(XCPsUtil.perspectiveMap(IGDPlotter.getReferenceCPs())[i]);
                //Label points 
                string PointPath = "Formulas/P" + i.ToString();
                IGDPlotter.addImage3D(PointPath, XCPsUtil.perspectiveMap(IGDPlotter.getReferenceCPs())[i] + 0.05f * Vector3.up, 0.5f);
            }

            for (int i = 0; i < 4; i++) {
                IGDPlotter.drawArrow3D(bcvPtsforDerLabeling[i],
                    bcvPtsforDerLabeling[i] + bcvDerPts[i], 0.5f, Colors[1]);
                IGDPlotter.drawArrow3D(bcvPtsforDerLabeling[i],
                    bcvPtsforDerLabeling[i] + bcv2ndDerPts[i], 0.5f, Colors[2]);

            }

            //Mark each Der 
            IGDPlotter.addImage3D("Formulas/MarkCu1stDer", Vector3.forward, 0.5f, Colors[0]);
            IGDPlotter.addImage3D("Formulas/MarkCu2ndDer", Vector3.forward + 0.1f * Vector3.up, 0.5f, Colors[1]);
            IGDPlotter.addImage3D("Formulas/MarkCu3rdDer", Vector3.forward + 0.2f * Vector3.up, 0.5f, Colors[2]);


        }

        // CA5-4: Draw the sub-curves and their corresponding convex hulls for each knot
        // interval, demonstrating that the quadratic B-spline curve defined on U = {0, 0,
        // 0, 2, 3, 3, 4, 4, 4} satisfies the strong convex hull property.
        public static void CA5_4() {
        }
        
        // CA5-5: Draw the sub-curves that are locally influenced by each control point,
        // demonstrating that the quadratic B-spline curve (with arbitrarily chosen
        // control points) defined on U = {0, 0, 0, 2, 3, 3, 4, 4, 4} satisfies the local
        // support property.
        public static void CA5_5() {
        }
        
        // CA5-6: Write code to calculate the k-th derivative curve of a p-th order
        // B-spline curve, and draw the first and second derivative curves of the
        // quadratic B-spline curve (with arbitrarily chosen control points) defined on
        // U = {0, 0, 0, 2, 3, 3, 4, 4, 4} along with the knot vector, control points, and
        // control polygon.
        public static void CA5_6() {
        }
    }
}