using System;
using UnityEngine;

namespace XGeom.NURBS {
    public static class XCPsUtil {
        // constants 

        // use this tolerance value 
        // to check whether a NURBS curve or surface is rational or not. 
        // If the weight (the 4th coordinate) of any control point is 
        // deviated from 1 by greater than this value, it is rational. 
        public static readonly double W1_TOL = 1.0e-6;

        // methods  
        public static int getNumCPs(Vector4[,] cps, XParametricSurface3D.Dir dir) {
            switch (dir) {
                case XParametricSurface3D.Dir.U:
                    return cps.GetLength(0);
                case XParametricSurface3D.Dir.V:
                    return cps.GetLength(1);
                default:
                    return int.MaxValue; // means nothing. 
            }
        }

        // if dir is Dir.U: CP[i,j];
        // if dir is Dir.V: CP[j,i].
        public static Vector4 getCP(Vector4[,] cps, int i, int j, 
            XParametricSurface3D.Dir dir) { 

            switch (dir) {
                case XParametricSurface3D.Dir.U:
                    return cps[i, j];
                case XParametricSurface3D.Dir.V:
                    return cps[j, i];
                default:
                    return XGeom.VECTOR4_NAN; // means nothing. 
            }
        }

        //
        public static Vector4 copyCP(Vector4 cp) {
            return new Vector4(cp.x, cp.y, cp.z, cp.w);
        }
        // if dir is Dir.U: CP[i,j];
        // if dir is Dir.V: CP[j,i].
        public static Vector4 copyCP(Vector4[,] cps, int i, int j,
            XParametricSurface3D.Dir dir) {

            return XCPsUtil.copyCP(XCPsUtil.getCP(cps, i, j, dir));
        }
        public static Vector4[] copyCPs(Vector4[] cps) {
            Vector4[] newCPs = new Vector4[cps.Length];
            for (int i = 0; i < cps.Length; i++) {
                newCPs[i] = new Vector4(cps[i].x, cps[i].y, cps[i].z, 
                    cps[i].w);
            }
            return newCPs;
        }
        public static Vector4[,] copyCPs(Vector4[,] cps) {
            Vector4[,] newCPs = new Vector4[cps.GetLength(0), cps.GetLength(1)];
            for (int i = 0; i < cps.GetLength(0); i++) {
                for (int j = 0; j < cps.GetLength(1); j++) {
                    newCPs[i, j] = new Vector4(cps[i, j].x, cps[i, j].y,
                        cps[i, j].z, cps[i, j].w);
                }
            }
            return newCPs;
        }
        
        // copy the control points of the "dir" directional control polygon. 
        // if dir is Dir.U: {CP[0,k], CP[1,k], ... , CP[n,k]};
        // if dir is Dir.V: {CP[k,0], CP[k,1], ... , CP[k,m]}.
        public static Vector4[] copyCPs(Vector4[,] cps, int k,
            XParametricSurface3D.Dir dir) {

            Vector4[] ps = new Vector4[XCPsUtil.getNumCPs(cps, dir)];
            for (int i = 0; i < ps.Length; i++) {
                ps[i] = XCPsUtil.copyCP(cps, i, k, dir);
            }
            return ps;
        }

        // map a 4D homogeneous coordinates Pw(X,Y,Z,W) to 3D P(x,y,z).
        // Refer to The NURBS Book, 2nd Edition, p.30.
        public static Vector3 perspectiveMap(Vector4 pw) {
            Vector3 p;
            if (Math.Abs(pw.w - 1.0) < XCPsUtil.W1_TOL) {
                p = new Vector3(pw.x / pw.w, pw.y / pw.w, pw.z / pw.w);
            } else {
                p = new Vector3(pw.x, pw.y, pw.z);
                p.Normalize();
            }
            return p;
        }
        // map an array of 4D homogeneous coordinates {Pw(X,Y,Z,W)} to 3D {P(x,y,z)}.
        public static Vector3[] perspectiveMap(Vector4[] pws) {
            Vector3[] ps = new Vector3[pws.Length];
            for (int i = 0; i < ps.Length; i++) {
                ps[i] = XCPsUtil.perspectiveMap(pws[i]);
            }
            return ps;
        }
        public static Vector3[,] perspectiveMap(Vector4[,] pws) {
            Vector3[,] ps = new Vector3[pws.GetLength(0), pws.GetLength(1)];
            for (int i = 0; i < pws.GetLength(0); i++) {
                for (int j = 0; j < pws.GetLength(1); j++) {
                    ps[i, j] = XCPsUtil.perspectiveMap(pws[i, j]);
                }
            }
            return ps;
        }

        // create an array of weights {W} from 4D homogeneous coordinates {Pw(X,Y,Z,W)}.
        public static float[] getWs(Vector4[] pws) {
            float[] ws = new float[pws.Length];
            for (int i = 0; i < ws.Length; i++) {
                ws[i] = pws[i].w;
            }
            return ws;
        }
        public static float[,] getWs(Vector4[,] pws) {
            float[,] ws = new float[pws.GetLength(0), pws.GetLength(1)];
            for (int i = 0; i < pws.GetLength(0); i++) {
                for (int j = 0; j < pws.GetLength(1); j++) {
                    ws[i, j] = pws[i, j].w;
                }
            }
            return ws;
        }

        public static Vector4 calcHomogeneousCoordinates(Vector3 p, float w) {
            float X = p.x * w;
            float Y = p.y * w;
            float Z = p.z * w;
            float W = w;
            Vector4 pw = new Vector4(X, Y, Z, W);
            return pw;
        }
        public static Vector4[] calcHomogeneousCoordinates(Vector3[] ps, float[] ws) {
            Debug.Assert(ps.Length == ws.Length);

            Vector4[] pws = new Vector4[ps.Length];
            for (int i = 0; i < pws.Length; i++) {
                pws[i].x = ps[i].x * ws[i];
                pws[i].y = ps[i].y * ws[i];
                pws[i].z = ps[i].z * ws[i];
                pws[i].w = ws[i];
            }
            return pws;
        }
        public static Vector4[,] calcHomogeneousCoordinates(Vector3[,] ps, 
            float[,] ws) {

            Debug.Assert(ps.GetLength(0) == ws.GetLength(0) &&
                ps.GetLength(1) == ws.GetLength(1));

            Vector4[,] pws = new Vector4[ps.GetLength(0), ps.GetLength(1)];
            for (int i = 0; i < ps.GetLength(0); i++) {
                for (int j = 0; j < ps.GetLength(1); j++) {
                    pws[i, j].x = ps[i, j].x * ws[i, j];
                    pws[i, j].y = ps[i, j].y * ws[i, j];
                    pws[i, j].z = ps[i, j].z * ws[i, j];
                    pws[i, j].w = ws[i, j];
                }
            }
            return pws;
        }
    }
}