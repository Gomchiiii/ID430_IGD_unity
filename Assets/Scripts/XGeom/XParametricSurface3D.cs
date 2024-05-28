using UnityEngine;

namespace XGeom {
    public abstract class XParametricSurface3D : XSurface3D {
        
        public enum Dir { U, V }
        public static Dir getOppositeDir(Dir dir) {
            switch (dir) {
                case Dir.U:
                    return Dir.V;
                case Dir.V:
                    return Dir.U;
                default: // means nothing. 
                    return Dir.V;
            }
        }

        public abstract double getStartParam(Dir dir);
        public abstract double getEndParam(Dir dir);

        // calculate the position at parameter (u, v).
        public abstract Vector3 calcPos(double u, double v);

        // calculate the derivative of (orderU, orderV) at parameters (u, v).
        public Vector3 calcDer(int orderU, int orderV, double u, double v) {
            Vector3[,] ders = this.calcDers(orderU, orderV, u, v);
            return ders[orderU, orderV];
        }
        // calculate all the derivatives of orders <= (orderU, orderV)
        // at parameters (u, v).
        public abstract Vector3[,] calcDers(int orderU, int orderV, double u, 
            double v);

        public Vector3 calcSurfaceNormal(double u, double v) {
            Vector3[,] ders = this.calcDers(1, 1, u, v);
            Vector3 Su = ders[1, 0];
            Vector3 Sv = ders[0, 1];
            Vector3 N = Vector3.Cross(Su, Sv); // left-hand rule. 
            return N.normalized;
        }
    }
}