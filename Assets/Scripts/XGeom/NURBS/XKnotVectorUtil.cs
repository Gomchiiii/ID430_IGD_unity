using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XGeom.NURBS {
    public static class XKnotVectorUtil {
        //constants

        //for a knot vector range [0,1].
        //need to discuss the numerical value.

        private static readonly double SAME_KNOT_VAL_TOL_0_1 = 1.0e-6;

        //for a knot vector not range [0,1]
        public static double calcSameKnotValTol(double us, double ue) {
            Debug.Assert(us < ue);
            double tol = (ue - us) * XKnotVectorUtil.SAME_KNOT_VAL_TOL_0_1;
            return tol;
        }

        //check that u0 and u1 are the same knot values on 
        // a knot vector ranges [us, ue]
        public static bool checkParamsAreSameKnotVals(double u0, double u1, double us, double ue) {
            double tol = XKnotVectorUtil.calcSameKnotValTol(us, ue);
            double du = Math.Abs(u0 - u1);
            if (du > tol) {
                return false;
            } else {
                return true;
            }
        }

        //check that u0 and u1 are the same knot values on a knot vaector U
        public static bool checkParamsAreSameKnotVals(double u0, double u1, double[] U) {
            int m = U.Length - 1; //
            double us = U[0];
            double ue = U[m];

            return XKnotVectorUtil.checkParamsAreSameKnotVals(u0, u1, us, ue);
        }

        public static double[] copyKnotVector(double[] U) {
            double[] newU = new double[U.Length];
            for (int i = 0; i < U.Length; i++) {
                newU[i] = U[i];
            }
            return newU;
        }

        //find thd knot span index i (p, ...m-p-1) such that
        // u is in [U[i], U[i+1])for a Bspline curve of degree p 
        //using binary search. If multipul knots (U[i-k].,,,,,U[i])
        //have the ame u, this function returns the last one.
        //그니까 관심있는 (=0이아님) 구간 u를 뽑겟다는거아니냐?
        //Refer to the NURBS Book. pp 67-68 
        public static int findKnotSpanIndex(double u, double[] U, int p) {
            int m = U.Length - 1;

            //if u is the exterior knot value for U[m-p],.....U[m]
            if (XKnotVectorUtil.checkParamsAreSameKnotVals(u, U[m - p], U)) {
                return (m - p - 1);
            }

            //binary search.
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
            return mid;

        }

    }
}
