using UnityEngine;

namespace XGeom.NURBS {
    public class XBezierSurface3D : XParametricSurface3D {
        // fields
        private int mDegU = int.MinValue;
        public int getDegU() {
            return this.mDegU;
        }
        private int mDegV = int.MinValue;
        public int getDegV() {
            return this.mDegV;
        }
        public int getDeg(XParametricSurface3D.Dir dir) {
            switch (dir) {
                case Dir.U:
                    return this.mDegU;
                case Dir.V:
                    return this.mDegV;
                default:
                    return this.mDegU; // means nothing. 
            }
        }

        // 4D control points {(X,Y,Z,W)} = {(wx,wy,wz,w)} 
        private Vector4[,] mCPs = null;
        public Vector4[,] getCPs() {
            return this.mCPs;
        }
        public void setCPs(Vector4[,] cps) {
            Debug.Assert(
                cps.GetLength(0) == this.mCPs.GetLength(0) &&
                cps.GetLength(1) == this.mCPs.GetLength(1));
            this.mCPs = cps;
        }
        public Vector4 getCP(int i, int j) {
            return this.mCPs[i, j];
        }
        public void setCP(Vector4 cp, int i, int j) {
            this.mCPs[i, j] = cp;
        }
        public int getNumCPsU() {
            //return this.mDegU + 1;
            return this.mCPs.GetLength(0);
        }
        public int getNumCPsV() {
            // return this.mDegV + 1;
            return this.mCPs.GetLength(1);
        }
        public int getNumCPs(XParametricSurface3D.Dir dir) {
            switch (dir) {
                case Dir.U:
                    //return this.mDegU + 1;
                    return this.mCPs.GetLength(0);
                case Dir.V:
                    // return this.mDegV + 1;
                    return this.mCPs.GetLength(1);
                default:
                    //return this.mDegU + 1;
                    return this.mCPs.GetLength(0); // means nothing. 
            }
        }

        // constructor
        public XBezierSurface3D(int degU, int degV, Vector4[,] cps) {
            this.setSurface(degU, degV, cps);
        }

        // copy constructor
        public XBezierSurface3D(XBezierSurface3D bsf) {
            Vector4[,] cps = XCPsUtil.copyCPs(bsf.getCPs());
            this.setSurface(bsf.getDegU(), bsf.getDegV(), cps);
        }

        // checks the condition: n=p+1, m=q+1
        // Refer to The NURBS Book, 2nd Edition, p.37. Eq.(1.23)
        public void setSurface(int degU, int degV, Vector4[,] cps) {
            Debug.Assert(cps.GetLength(0) == degU + 1); // n=p+1 
            Debug.Assert(cps.GetLength(1) == degV + 1); // m=q+1
            this.mDegU = degU;
            this.mDegV = degV;
            this.mCPs = cps;
        }

        // methods
        public override double getStartParam(Dir dir) {
            return 0.0;
        }
        public override double getEndParam(Dir dir) {
            return 1.0;
        }

        public override Vector3 calcPos(double u, double v) {
            Vector3 S = this.calcPosByBezierDefinition(u, v); // should be replaced.
            //Vector3 S = this.calcPosByDeCasteljauAlg(u, v);
            return S;
        }
        
        // Refer to The NURBS Book, 2nd Edition, p.37. Eq.(1.23)
        private Vector3 calcPosByBezierDefinition(double u, double v) {
            int n = this.getDegU();
            int m = this.getDegV();
            double[] Bu = XBezier.calcBasicFns(n, u);
            double[] Bv = XBezier.calcBasicFns(m, v);
            Vector4 Sw = Vector4.zero;
            for(int i = 0; i <= n; i++) {
                for (int j = 0; j <= m; j++) {
                    Sw += (float)(Bu[i] * Bv[j]) * this.getCP(i, j);
                }
            }
            Vector3 S = XCPsUtil.perspectiveMap(Sw);
            return S;

        }
        
        // Refer to The NURBS Book, 2nd Edition, pp.38-40. Alg.(1.7)
        private Vector3 calcPosByDeCasteljauAlg(double u, double v) {
            /*
             * CA10-1: Implement here.
             */
            int n = this.getDegU();
            int m = this.getDegV();

            Vector4[,] Q = this.getCPs();
            Vector3[,] P = new Vector3[n + 1, m + 1];

            if ( n <= m ) {
                for ( int j = 0; j <= m; j++) {
                    XBezierCurve3D bcv = new XBezierCurve3D(this.mDegU, Q[j])
                    for (int i = 0; i <= n; i ++)
                        P[j][i] = XBezierCurve3D.calcPosByDeCasteljouAlgo(double u) {
                    }
            }

            throw new System.NotImplementedException();
        }

        public override Vector3[,] calcDers(int orderU, int orderV, double u, 
            double v) {

            throw new System.NotImplementedException();
        }

        // Refer to The NURBS Book, 2nd Edition, pp.34-40. Eq.(1.24)
        // if dir is Dir.U: the resulting curve is v-directional and the given
        // parameter is u0. C_{u0}(v)
        // if dir is Dir.V: the resulting curve is u-directional and the given
        // parameter is v0. C_{v0}(u)
        public XBezierCurve3D calcIsoCurve(double u, XParametricSurface3D.Dir dir) {
            XParametricSurface3D.Dir oDir = XParametricSurface3D.getOppositeDir(dir);
            XBezierCurve3D[] cvs = this.decompose(dir);
            Vector4[] cps = new Vector4[this.getNumCPs(oDir)];
            for (int i = 0; i < cps.Length; i++) {
                Vector3 p = cvs[i].calcPos(u);
                cps[i] = new Vector4(p.x, p.y, p.z, 1f);
            }
            XBezierCurve3D bc = new XBezierCurve3D(this.getDeg(oDir), cps);
            return bc;
        }

        // Refer to The NURBS Book, 2nd Edition, pp.34-40. Eq.(1.24)
        // if dir is Dir.U: the resulting curves are u-directional. 
        // { C_{j}(u) = sum_{i=0}^{n} B_{i,n}(u) P_{i,j} }_{j=0}^{m} 
        // if dir is Dir.V: the resulting curves are v-directional.
        // { C_{i}(v) = sum_{j=0}^{m} B_{j,m}(v) P_{i,j} }_{i=0}^{n} 
        public XBezierCurve3D[] decompose(XParametricSurface3D.Dir dir) {
            XParametricSurface3D.Dir oDir = XParametricSurface3D.getOppositeDir(dir);
            XBezierCurve3D[] cvs = new XBezierCurve3D[this.getNumCPs(oDir)];
            for (int i = 0; i < cvs.Length; i++) {
                Vector4[] cps = XCPsUtil.copyCPs(this.mCPs, i, dir);
                cvs[i] = new XBezierCurve3D(this.getDeg(dir), cps);
            }
            return cvs;
        }
    }
}