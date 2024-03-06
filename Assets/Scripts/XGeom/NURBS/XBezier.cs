using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


namespace XGeom.NURBS {
    public static class XBezier {
        //{B[0, ....B[n]}
        //refer to The Nurbs book p.9 eq.(1.7)

        public static double[] calcBasicFns(int n, double u) {
            double[] B = new double[n + 1];
            for (int i = 0; i <= n; i++) {
                B[i] = calcBernsteinPolynomial(i, n, u);
            }
            return B;
        }

        public static double calcBernsteinPolynomial(int i, int n, double u) {
            double b = XBezier.calcBernsteinPolynomialByDefiniton(i, n, u);
            return b;
        }

        //B_{i,n} (u) = ... def, p.10 eq 1.8
        public static double calcBernsteinPolynomialByDefiniton(int i, int n, double u)
        {
            Debug.Assert(i >= 0);
            Debug.Assert(n >= 0);
            Debug.Assert(i <= n);

            //i!
            double d0 = 1.0;
            for (int j = i; j > 1; j--)
            {
                d0 *= (double)j;
            }

            //n!
            double n0 = 1.0;
            for (int j = n; j > 1; j--)
            {
                n0 *= (double)j;
            }

            //n-i!
            double d1 = 1.0;
            for (int j = (n - i); j > 1; j--)
            {
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
            throw new NotImplementedException();
        }

        public static double[] calcAllBernsteinPolynomialByDynamicProg(int n, double u) {
            throw new NotImplementedException(); 
        }

        //refer to The NURBS Book, p.17 Alg 
        public static double[,] calcAllDeriveBasisFns(int order, int n, double u) {  
            throw new NotImplementedException();
        }
    }
}
