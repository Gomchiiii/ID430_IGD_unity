using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XAppObject;
using XGeom.NURBS;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public static partial class IGDPlotter {
        // CA4-1: Write code to calculate the values of B-spline basis functions
        // of given parameters, and plot the graphs of orders 0, 1, and 2 basis
        // functions along with the knot vector.
        // Input: knot vector U, degree p(=0, 1, 2)
        public static void CA4_1(double[] U, int p) {

            float xMax = Screen.width / 1.5f;
            float yMax = Screen.height / 1.5f;

            IGDPlotter.drawArrow2D(Vector2.zero, new Vector2(xMax, 0));
            IGDPlotter.drawArrow2D(Vector2.zero, new Vector2(0, yMax));
            float unitLength = xMax / 5f;

            //Knot Vector in Fig 2.6
            double us = U[0];
            double ue = U[U.Length - 1];

            //Sampling for render 
            int usSample = (int)(us * (double)unitLength);
            int ueSample = (int)(ue * (double)unitLength);

            // m = 11, p = given
            int m = U.Length;
 

            //point to render the basis function 
            List<List<Vector2>> basisFnsPts = new List<List<Vector2>>();
            for (int i = 0; i < m - p - 1; i++) {
                basisFnsPts.Add(new List<Vector2>());
            }

            for (int i = usSample; i < ueSample; i++) {
                double[] BasisFnsValues = XBspline.calcBasisFns((double)i / (double)unitLength, p, U);
                for (int j = 0; j < BasisFnsValues.Length; j++) {
                    double basisFnsValue = BasisFnsValues[j];
                    basisFnsPts[j].Add(new Vector2(i, (float)basisFnsValue * unitLength));
                }
            }
            //plot each basis funs in hsb colors 
            for (int i = 0; i < basisFnsPts.Count; i++) {
                drawPolyline2D(basisFnsPts[i], 0.7f, Color.HSVToRGB((float)i / (float)basisFnsPts.Count, 1, 1));
                string imgpath = "Formulas/N" + i.ToString() + p.ToString();
                IGDPlotter.addImage2D(imgpath, new Vector2( - unitLength * 0.3f, i * unitLength * 0.3f), 0.7f, Color.HSVToRGB((float)i / (float)basisFnsPts.Count, 1, 1));
            }

            //label each knot
            for (int i = 0; i < m; i++) {
                IGDPlotter.drawDot2D(new Vector2(unitLength * (float)U[i], 0), 1.3f);
            }

        }
    }

}