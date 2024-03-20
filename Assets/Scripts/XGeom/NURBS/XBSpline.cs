using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XGeom.NURBS {
    public class XBspline {

        public static double[] calcNonZeroBasisFns(double u, int i, int p, double[] U) {
            double[] M = new double[p + 1];
            double[] L = new double[p + 1];
            double[] R = new double[p + 1];
            double saved, temp;

            M[0] = 1.0;
            for (int j = 1;  j <= p; j++) {
                //set the L and R array
                L[j] = u - U[i + 1 - j];
                R[j] = U[i + j] - u;

                saved = 0.0;
                ////calculate the rth degreeBasisFns
                for (int r = 0; r < j; r++) {
                    temp = M[r] / (R[r + 1] + L[j - r]);
                    M[r] = saved + R[r + 1] * temp;
                    saved = L[j - r] * temp;
                }
                M[j] = saved;
            }
            
            return M;
        }

        public static double[] calcBasisFns(double u, int p, double[] U) {
            int m = U.Length - 1;
            int n = m - p - 1;
            double[] N = new double[n + 1];
            int i = XKnotVectorUtil.findKnotSpanIndex(u, U, p);
            double[] M = XBspline.calcNonZeroBasisFns(u, i, p, U);


            for (int j = i - p, k = 0; j <= i; j++, k++) {
                N[j] = M[k];
            }

            return N;
        }
    }
}
