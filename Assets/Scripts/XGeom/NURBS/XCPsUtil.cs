using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xgeom.NURBS
{
    public static class XCPsUtil { 

        //constatns
        //use this tolerance value
        //to chaeck the whether a NURBS curve ofr surface is rational or not
        // if the weight of any control point is deviated from 1 by greater than this value, it is rational

        public static readonly double W1_TOL = 1.0e-6;

        public static Vector4[] copyCPs(Vector4[] cps)
        {
            Vector4[] newCPs = new Vector4[cps.Length];
            for (int i = 0; i < cps.Length; i++){
                newCPs[i] = new Vector4(cps[i].x, cps[i].y, cps[i].z, cps[i].w);
             }
            return newCPs;
        }

        // map a 4D homogeneous coordinates PW(X,Y,Z,W) to 3D P(x,y,z)
        // Refer to the NURBS Book, p.30
        public static Vector3 perspectiveMap(Vector4 pw) {
            Vector3 p;
            if (Mathf.Abs(pw.w - 1.0f) < XCPsUtil.W1_TOL)
            {    
                p = new Vector3(pw.x, pw.y, pw.z);

            } else if (Mathf.Abs(pw.w) < XCPsUtil.W1_TOL) {
                p = new Vector3(pw.x, pw.y, pw.z);
                p.Normalize();
            } else {
                p = new Vector3(pw.x / pw.w, pw.y / pw.w, pw.z / pw.w);

            }
            return p;
        }

        public static Vector3[] perspectiveMap(Vector4[] pws) {
            Vector3[] ps = new Vector3[pws.Length];
            for (int i = 0; i < pws.Length; i++)
            {
                ps[i] = XCPsUtil.perspectiveMap(pws[i]);
            }
            return ps;
        }

    }
}
