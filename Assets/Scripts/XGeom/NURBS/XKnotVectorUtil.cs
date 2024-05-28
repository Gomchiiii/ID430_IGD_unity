using System;
using System.Collections.Generic;
using UnityEngine;

namespace XGeom.NURBS {
    public static class XKnotVectorUtil {
        // constants
        
        // for a knot vector ranged [0, 1]. 
        // need to discuss the numerical value. 
        private static readonly double SAME_KNOT_VAL_TOL_0_1 = 1.0e-6;

        // for a knot vector not ranged [0, 1].
        public static double calcSameKnotValTol(double us, double ue) {
            Debug.Assert(us < ue);
            double tol = (ue - us) * XKnotVectorUtil.SAME_KNOT_VAL_TOL_0_1;
            return tol;
        }

        // Check that u0 and u1 are the same knot values on
        // a knot vector ranged [us, ue].
        public static bool checkParamsAreSameKnotVals(double u0, 
            double u1, double us, double ue) {

            double tol = XKnotVectorUtil.calcSameKnotValTol(us, ue);
            double du = Math.Abs(u1 - u0);
            if (du > tol) {
                return false;
            } else {
                return true;
            }
        }

        // Check that u0 and u1 are the same knot values on
        // a knot vector U.
        public static bool checkParamsAreSameKnotVals(double u0, double u1, 
            double[] U) {

            int m = U.Length - 1; // {U[0],...,U[m]}.
            double us = U[0];
            double ue = U[m];
            return XKnotVectorUtil.checkParamsAreSameKnotVals(u0, u1, us, ue);
        }

        // Check that u is a knot(s) on a knot vector U for
        // a B-spline curve of degree p.
        public static bool checkParamIsKnot(double u, double[] U, int p) {
            int k = XKnotVectorUtil.findLastKnotIndex(u, U, p);
            if (k == -1) {
                return false;
            } else {
                return true;
            }
        }

        // Check that u is an exterior knot on a knot vector U for
        // a B-spline curve of degree p.   
        // U[0],...,U[p] or U[m-p],...,U[m]. 
        public static bool checkParamIsExteriorKnot(double u, double[] U, 
            int p) {

            int m = U.Length - 1; // {U[0],...,U[m]}.
            if (XKnotVectorUtil.checkParamsAreSameKnotVals(u, U[p], U) ||
                XKnotVectorUtil.checkParamsAreSameKnotVals(u, U[m - p], U)) {
                return true;
            } else {
                return false;
            }
        }

        // Check that u is an interior knot on a knot vector U for
        // a B-spline curve of degree p.   
        // U[p+1],...,U[m-p-1]. 
        public static bool checkParamIsInteriorKnot(double u, double[] U, 
            int p) {

            int m = U.Length - 1; // {U[0],...,U[m]}.
            int k = XKnotVectorUtil.findLastKnotIndex(u, U, p);
            if (k > p && k < m - p) {
                return true;
            } else {
                return false;
            }
        }

        // Check that u is in domain of a given knot vector U
        // for a B-spline curve of degree p. 
        // [U[0],U[m]] = [U[p],U[m-p]]. 
        public static bool checkParamIsInDomain(double u, double[] U, 
            int p) {
            
            int m = U.Length - 1; // {U[0],...,U[m]}.
            double us = U[p];
            double ue = U[m - p];
            
            if (u >= us && u <= ue) {
                return true;
            } else {
                return false;
            }
        }

        // Check that u is in interior domain of a given knot vector U
        // for a B-spline curve of degree p. 
        // (U[p],U[m-p]).
        public static bool checkParamIsInInteriorDomain(double u, double[] U, 
            int p) {
            if (XKnotVectorUtil.checkParamIsInDomain(u, U, p) &&
                !XKnotVectorUtil.checkParamIsExteriorKnot(u, U, p)) {
                return true;
            } else {
                return false;
            }
        }

        public static double[] copyKnotVector(double[] U) {
            double[] newU = new double[U.Length];
            for (int i = 0; i < U.Length; i++) {
                newU[i] = U[i];
            }
            return newU;
        }

        // Find the knot span index i (p,...,m-p-1) such that
        // u is in [U[i],U[i+1]) for a B-spline curve of degree p
        // using binary search. If multiple knots (U[i-k],...,U[i])
        // have the same u, this function returns the last one.
        // Refer to The NURBS Book, 2nd Edition, pp. 67-68. Alg.(A2.1)
        public static int findKnotSpanIndex(double u, double[] U, int p) {
            int m = U.Length - 1; // {U[0],...,U[m]}

            // if u is the end exterior knot value for U[m-p],...,U[m].
            if (XKnotVectorUtil.checkParamsAreSameKnotVals(u, U[m - p], U)) { 
                return (m - p - 1); // m-p-1=n
            }

            // binary search.
            int low = p;
            int high = m - p; // m-p=n+1
            int mid = (low + high) / 2;
            while (u < U[mid] || u >= U[mid + 1]) {
                if (u < U[mid]) {
                    high = mid;
                } else {
                    low = mid;
                }
                mid = (low + high) / 2;
            }
            return mid;
        }

        // Find the knot index i (p,...,m) for a specified parameter u on
        // a knot vector U for a B-spline curve of degree p.
        // If s multiple knots (U[i-s+1],...,U[i]) are at u,
        // this function returns the last one (i). If no knots are at u,
        // this function returns -1. This function uses binary search. 
        // Refer to The NURBS Book, 2nd Edition, pp. 67-68.
        public static int findLastKnotIndex(double u, double[] U, int p) {
            int m = U.Length - 1; // {U[0],...,U[m]}

            // if the end exterior knot(s) (U[m-p],...,U[m]) exists at u.
            if (XKnotVectorUtil.checkParamsAreSameKnotVals(u, U[m], U)) {
                return m;
            }

            // binary search.
            int low = p;
            int high = m - p;
            int mid = (low + high) / 2;
            while (u < U[mid] || u >= U[mid + 1]) {
                if (u < U[mid]) {
                    high = mid;
                } else {
                    low = mid;
                }
                mid = (low + high) / 2;
            }

            if (XKnotVectorUtil.checkParamsAreSameKnotVals(u, U[mid], U)) {
                return mid; // a knot(s) at u. 
            } else {
                return (-1); // no knots at u.
            }
        }

        // Find the distinct knot values of a knot vector U.
        public static double[] findDistinctKnotVals(double[] U, int p) {
            int m = U.Length - 1; // {U[0],...,U[m]}.
            int n = m - p - 1;
            
            List<double> DU = new List<double>();
            DU.Add(U[0]);
            for (int i = p + 1; i <= m - p; i++) {
                if (!XKnotVectorUtil.checkParamsAreSameKnotVals(U[i],
                    DU[DU.Count - 1], U)) {
                    DU.Add(U[i]);
                }
            }
            
            return DU.ToArray();
        }

        // Find the knot multiplicity at u on a knot vector U for 
        // a B-spline curve of degree p.
        public static int findKnotMultiplicity(double u, double[] U, int p) {
            int m = U.Length - 1; // {U[0],...,U[m]}.
            int k = XKnotVectorUtil.findLastKnotIndex(u, U, p);

            if (k == -1) { // no knots at u. 
                return 0; 
            } else if (k == p || k == m) { // exterior knots at u. 
                return (p + 1);
            }

            int s = 1;  // knot multiplicity.
            for (int i = 1; i <= p - 1; i++) {
                if (XKnotVectorUtil.checkParamsAreSameKnotVals(u, U[k - i], U)) {
                    s++;
                }
            }
            return s;
        }
        
        // Find the knot multiplicities of each distinct knot value.
        public static int[] findKnotMultiplicities(double[] U, int p) {
            int m = U.Length - 1; // {U[0],...,U[m]}.
            int n = m - p - 1;
            double[] DU = XKnotVectorUtil.findDistinctKnotVals(U, p);
            int[] DI = new int[DU.Length];
            for (int i = 0; i < DU.Length; i++) {
                DI[i] = XKnotVectorUtil.findKnotMultiplicity(DU[i], U, p);
            }
            return DI;
        }

        // Find the degree of B-spline curve with its knot vector U. 
        public static int findDeg(double[] U) {
            int p = 0;
            double us = U[0];
            double ue = U[U.Length - 1];
            for (int i = 1; i < U.Length; i++) {
                if (XKnotVectorUtil.checkParamsAreSameKnotVals(us, U[i], us, ue)) {
                    p++;
                }
            }
            return p;
        }

        // DU: a distinct knot vector
        // DI: a knot multiplicity vector whose length is the same to 
        // that of DU, where DI[0] and DI[m] should be (p+1); 
        // DI[1],...,DI[m-1] should be less than or equal to p
        public static double[] createKnotVector(int p, double[] DU, int[] DI) {
            Debug.Assert(DU.Length == DI.Length);
            Debug.Assert(DI[0] == p + 1); // DI[0]=p+1
            Debug.Assert(DI[DI.Length - 1] == p + 1); // DI[m]=p+1

            int numKnots = 0;
            foreach (int s in DI) {
                numKnots += s;
            }

            double[] U = new double[numKnots];
            int k = 0;
            for (int i = 0; i < DU.Length; i++) {
                int s = DI[i];
                if (i > 0 && i < DI.Length - 1) { // For interior knots.  
                    s = Math.Min(s, p);
                }
                for (int j = 0; j < s; j++) {
                    U[k] = DU[i]; k++;
                }
            }
            return U;
        }

        public static double[] createKnotVector(int p, double[] DU,
       int interiorKnotMultiplicity) {

            Debug.Assert(DU.Length >= 2);
            Debug.Assert(interiorKnotMultiplicity <= p);

            int m = DU.Length - 1; // the last index

            // The knot multiplicity vector. 
            int[] DI = new int[m + 1];
            DI[0] = p + 1;
            for (int i = 1; i < m; i++) {
                DI[i] = interiorKnotMultiplicity;
            }
            DI[m] = p + 1;

            double[] U = XKnotVectorUtil.createKnotVector(p, DU, DI);
            return U;
        }

        public static double[] createDistinctKnotVector(double us, double ue,
            int numKnotSpans) {

            Debug.Assert(us < ue);

            int m = numKnotSpans; // {DU[0],...,DU[m]}.

            double[] DU = new double[m + 1];
            double du = (ue - us) / (double)m;
            for (int i = 0; i <= m; i++) {
                DU[i] = us + (double)i * du;
            }

            return DU;
        }

        public static int[] createUniformKnotMulVector(int p,
            int numKnotSpans, int interiorKnotMul) {

            int m = numKnotSpans; // {DI[0],...,DI[m]}.

            int[] DI = new int[m + 1];
            DI[0] = p + 1;
            for (int i = 1; i <= m - 1; i++) {
                DI[i] = interiorKnotMul;
            }
            DI[m] = p + 1;

            return DI;
        }

        public static double[] createUniformKnotVector(int p, double us,
            double ue, int numKnotSpans, int interiorKnotMul) {

            Debug.Assert(us < ue);

            int m = numKnotSpans; // {DU[0],...,DU[m]}, {DI[0],...,DI[m]}
            double[] DU = XKnotVectorUtil.createDistinctKnotVector(us, ue,
                numKnotSpans);
            int[] DI = XKnotVectorUtil.createUniformKnotMulVector(p,
                numKnotSpans, interiorKnotMul);
            double[] U = XKnotVectorUtil.createKnotVector(p, DU, DI);

            return U;
        }
    }
}
