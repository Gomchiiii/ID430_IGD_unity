using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.UIElements;

namespace XGeom.NURBS {
    public static class XBezier {
        //{B[0, ....B[n]}
        //refer to The Nurbs book p.9 eq.(1.7)

        public static double[] calcBasicFns(int n, double u) {
            double[] B = new double[n + 1];
            for (int i = 0; i <= n; i++) {
                B[i] = calcBernsteinPolynomialByDynamicProg(i, n, u);
            }
            return B;
        }

        public static double calcBernsteinPolynomial(int i, int n, double u) {
            double b = XBezier.calcBernsteinPolynomialByDefiniton(i, n, u);
            return b;
        }

        //B_{i,n} (u) = ... def, p.10 eq 1.8
        public static double calcBernsteinPolynomialByDefiniton(int i, int n, double u) {
            Debug.Assert(i >= 0);
            Debug.Assert(n >= 0);
            Debug.Assert(i <= n);

            //i!
            double d0 = 1.0;
            for (int j = i; j > 1; j--) {
                d0 *= (double)j;
            }

            //n!
            double n0 = 1.0;
            for (int j = n; j > 1; j--) {
                n0 *= (double)j;
            }

            //n-i!
            double d1 = 1.0;
            for (int j = (n - i); j > 1; j--) {
                d1 *= (double)j;
            }

            //u^i
            double n1 = Math.Pow(u, (double)i);
            //
            double n2 = Math.Pow((1.0 - u), (double)(n - i));
            double b = n0 * n1 * n2 / d0 / d1;
            return b;
        }

        //refer to the NURBS Book, p.20 Alg 
        public static double calcBernsteinPolynomialByDynamicProg(int i, int n, double u) {
            //compute the value of a Bernstein polynominal
            double[] temp = new double[n + 1];

            for (int j = 0; j <= n; j++) { 
                temp[j] = 0.0;
            }
            temp[n - i] = 1.0; // b 0 0 À§Ä¡ 
            double u1 = 1.0 - u;

            for (int k = 1; k <= n; k++) {
                for (int j = n; j >= k; j--) {
                    temp[j] = u1 * temp[j] + u * temp[j - 1];
                }
            }

            double b = temp[n];
            return b;
        }

        public static double[] calcAllBernsteinPolynomialsByDynamicProg(int n, double u) {
            double[] BernsteinPolynomials = new double[n + 1];

            BernsteinPolynomials[0] = 1.0;
            double u1 = 1.0 - u;

            for(int j = 1; j <= n; j++) {
                double saved = 0.0;
                for (int k = 0; k < j; k++) { 
                    double temp = BernsteinPolynomials[k];
                    BernsteinPolynomials[k] = saved + u1 * temp;
                    saved = u * temp;
                }
                BernsteinPolynomials[j] = saved;
            }

            return BernsteinPolynomials;
        
        }

        //refer to The NURBS Book, p.17 Alg (p1.7)
        //Output : dB[k, i] = B^(K)_[i, n](u) for k = 0 to order and i = 0 to n 
        public static double[,] calcAllDeriveBasisFns(int order, int n, double u) {  
            Debug.Assert(order >= 0);
            Debug.Assert(n >= 0);
            Debug.Assert(order <= n);
            Debug.Assert(u >= 0.0);
            Debug.Assert(u <= 1.0);

            double[,] dB = new double[order + 1, n + 1];

            //First, calculate all the Bersntrein polynomails for order = 0;
            double[,] B = new double[n + 1, n + 1];
            B[0, 0] = 1.0;
            for (int j = 1; j <= n; j++) {
                int i = 0;
                B[j, i] = (1.0 - u) * B[j - 1, i];
                for (i = 1; i <= j; i++) {
                    B[j, i]  = (1.0 - u) * B[j - 1, i] + u * B[j - 1, i - 1];
                }
            }

            for (int i = 0; i <= n; i++) {
                dB[0, i] = B[n, i];
            }

            //Then, update last row of B to dB

            //Then update B matrix for order 1 to order, from the last row to the first row at each other.
            // Copy the last row of B to dB for each round.

            for(int k = 1; k <= order; k++) {
                for (int j = n; j >= k; j--) {
                    int i = 0;
                    B[j, i] = j * (-1) * B[j - 1, i];
                    for (i = 1; i <= j; i++) {
                        B[j, i] = j * (B[j - 1, i - 1] - B[j - 1, i]);
                    }
                }

                for (int i = 0; i <= n; i++) {
                    dB[k, i] = B[n, i]; 
                }
            }
            return dB;
        }

        public static double calcDerivBernsteinPolynomial(int order, int i , int n , double u) {
            double[,] dB = calcAllDeriveBasisFns(order, n, u);
            return dB[order, i];
        }

        public static Vector3 PointOnBezierCurve(Vector3[] P, int n, int u) {
            double[] B = calcAllBernsteinPolynomialsByDynamicProg(n, u);
            Vector3 C = Vector3.zero;
            for (int k = 0; k <= n; k++) {
                C += ((float)B[k]) * P[k];
            }
            return C;
        }
    }
}
