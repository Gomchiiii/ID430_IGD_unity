using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

namespace XGeom.NURBS {
    public class XBspline {
        public static double calcBasisFnByDefinition(double u, int i, int p, double[] U) {
            int m = U.Length - 1;
            int n = m - p - 1;

            if (i < 0 || i > n) {
                return 0;
            }

            if (p == 0) {
                if (u >= U[i]  && u < U[i + 1]) {
                    return 1;
                } else {
                    return 0;
                }
            } else {
                double left, right;
                if (XKnotVectorUtil.checkParamsAreSameKnotVals(U[i + p], U[i], U)){
                    left = 0;
                } else {
                    left = (u - U[i] / (U[i + p] - U[i])) * calcBasisFnByDefinition(u, i, p - 1, U);
                }

                if (XKnotVectorUtil.checkParamsAreSameKnotVals(U[i + p + 1], U[i + 1], U)) {
                    right = 0;
                } else {
                    right = (U[i + p + 1] - u / (U[i + p + 1] - U[i + 1])) * calcBasisFnByDefinition(u, i, p - 1, U);
                }

                return left + right;    
            }
        }


        public static double[] calcNonZeroBasisFnsByDynamicProg(double u, int i, int p, double[] U) {
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
            double[] M = XBspline.calcNonZeroBasisFnsByDynamicProg(u, i, p, U);


            for (int j = i - p, k = 0; j <= i; j++, k++) {
                N[j] = M[k]; // Xbspline.calcbasisfuns bydef
            }

            return N;
        }

        public static double[,] calcAllDerivBasisFns(int order, double u, int p, double[] U) {
            Debug.Assert(order <= p);

            int m = U.Length - 1;
            int n = m - p - 1;

            //dN[p,k,i] = N^(k)_{i,p}(u);
            double[,,] dN = new double[p + 1, order + 1, m];

            //if u is in [U[j], U[j+1]) only {N^(k)_{j - p, p} .......{j , p} non zero 
            int j = XKnotVectorUtil.findKnotSpanIndex(u, U, p);

            //Initialize the 0 th ortder derivatives of 0 th degree basis fns.
            dN[0, 0, j] = 1.0;

            //using Eq.1
            for (int q = 1; q <= p; q++) {
                for (int i = j - q; i <= j; i++) {
                    double left, right;
                    if (XKnotVectorUtil.checkParamsAreSameKnotVals(U[i + q], U[i], U)) {
                        left = 0.0;
                    } else {
                        left = (u - U[i]) / (U[i + q] - U[i]) * dN[q - 1, 0, i];
                    }
                    if (XKnotVectorUtil.checkParamsAreSameKnotVals(U[i + q + 1], U[i + 1], U)) {
                        right = 0.0;
                    } else {
                        right = (U[i + q + 1] - u) / (U[i + q + 1] - U[i + 1]) * dN[q - 1, 0, i + 1];
                    }
                    dN[q, 0, i] = left + right;
                }

                //second, calculate the 1st to ortderth order derivatives using eq.1
                for (int k = 1; k <= q - (p - order); k++) {
                    for (int i = j - q; i <= j; i++) {
                        double left, right;

                        if (XKnotVectorUtil.checkParamsAreSameKnotVals(U[i + q], U[i], U)) {
                            left = 0.0;
                        } else {
                            left = q / (U[i + q] - U[i]) * dN[q - 1, k - 1, i];
                        }

                        if (XKnotVectorUtil.checkParamsAreSameKnotVals(U[i + q + 1], U[i + 1], U)) {
                            right = 0.0;
                        } else {
                            right = q / (U[i + q + 1] - U[i + 1]) * dN[q - 1, k - 1, i + 1];
                        }

                        dN[q, k, i] = left - right;
                    }
                }
            }
            // Extract 2D array N from 3D array dN,
            double[,] N = new double[order + 1, m - p];

            for (int k = 0; k<= order; k++) {
                for (int i = 0; i <= m - p - 1; i++) {
                    N[k, i] = dN[p, k, i];
                }
            }

            return N;
        }


    }
}
