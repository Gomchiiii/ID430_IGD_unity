using UnityEngine;

namespace XGeom.NURBS {
    public class XBSplineSurface3D : XParametricSurface3D {
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

        // {U[0], ... U[r]} 
        private double[] mU = null;
        public double[] getU() {
            return this.mU;
        }
        public void setU(double[] U) {
            Debug.Assert(U.Length == this.mU.Length);
            this.mU = U;
        }
        public int getNumKnotsU() {
            return this.mU.Length;
        }

        // {V[0], ... V[s]} 
        private double[] mV = null;
        public double[] getV() {
            return this.mV;
        }
        public void setV(double[] V) {
            Debug.Assert(V.Length == this.mV.Length);
            this.mV = V;
        }
        public int getNumKnotsV() {
            return this.mV.Length;
        }

        //
        public double[] getKnotVector(XParametricSurface3D.Dir dir) {
            switch (dir) {
                case Dir.U:
                    return this.mU;
                case Dir.V:
                    return this.mV;
                default:
                    return this.mU; // means nothing. 
            }
        }
        public int getNumKnots(XParametricSurface3D.Dir dir) {
            switch (dir) {
                case Dir.U:
                    return this.mU.Length;
                case Dir.V:
                    return this.mV.Length;
                default:
                    return this.mU.Length; // means nothing. 
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
        public XBSplineSurface3D(int degU, int degV, double[] U, 
            double[] V, Vector4[,] cps) {
            this.setSurface(degU, degV, U, V, cps);
        }

        // copy constructor
        public XBSplineSurface3D(XBSplineSurface3D sf) {
            Vector4[,] cps = XCPsUtil.copyCPs(sf.getCPs());
            double[] U = XKnotVectorUtil.copyKnotVector(sf.getU());
            double[] V = XKnotVectorUtil.copyKnotVector(sf.getV());
            this.setSurface(sf.getDegU(), sf.getDegV(), U, V, cps);
        }

        // checks the condition: r=n+p+1, s=m+q+1
        // Refer to The NURBS Book, 2nd Edition, p.101. Eq.(3.12)
        public void setSurface(int degU, int degV, double[] U, 
            double[] V, Vector4[,] cps) {

            Debug.Assert(U.Length == cps.GetLength(0) + degU + 1); // r=n+p+1
            Debug.Assert(V.Length == cps.GetLength(1) + degV + 1); // r=n+p+1
            this.mDegU = degU;
            this.mDegV = degV;
            this.mU = U;
            this.mV = V;
            this.mCPs = cps;
        }

        // methods
        public override double getStartParam(Dir dir) {
            switch (dir) {
                case Dir.U:
                    return this.mU[0];
                case Dir.V:
                    return this.mV[0];
                default:
                    return this.mU[0]; // means nothing. 
            }
        }
        public override double getEndParam(Dir dir) {
            switch (dir) {
                case Dir.U:
                    return this.mU[this.mU.Length - 1];
                case Dir.V:
                    return this.mV[this.mV.Length - 1];
                default:
                    return this.mU[this.mU.Length - 1]; // means nothing. 
            }
        }
        public double getParam(int i, Dir dir) {
            switch (dir) {
                case Dir.U:
                    return this.mU[i];
                case Dir.V:
                    return this.mV[i];
                default:
                    return this.mU[i]; // means nothing. 
            }
        }

        public override Vector3 calcPos(double u, double v) {
            Vector3 S = this.calcPosByBSplineDefinition(u, v);
            return S;
        }

        // Refer to The NURBS Book, 2nd Edition, pp.132-134. Alg.(A4.3)
        private Vector3 calcPosByBSplineDefinition(double u, double v) {
            /*
             * CA10-2: Implement here.
             */
            throw new System.NotImplementedException();
        }

        // Refer to The NURBS Book, 2nd Edition, pp.110-115. Alg.(A3.6)
        public Vector3 calcDer(int orderU, int orderV, 
            double u, double v) {
            /*
             * CA10-3: Implement here.
             */
            throw new System.NotImplementedException();
        }
        
        // Refer to The NURBS Book, 2nd Edition, pp.110-115. Alg.(A3.6)
        public override Vector3[,] calcDers(int orderU, int orderV, 
            double u, double v) {
            /*
             * CA10-4: Implement here.
             */
            throw new System.NotImplementedException();
        }

        // Refer to The NURBS Book, 2nd Edition, pp.109-110. Eq.(3.14), (3.15)
        // if dir is Dir.U: the resulting curve is v-directional and the given
        // parameter is u0. C_{u0}(v)
        // if dir is Dir.V: the resulting curve is u-directional and the given
        // parameter is v0. C_{v0}(u)
        public XBSplineCurve3D calcIsoCurve(double u, 
            XParametricSurface3D.Dir dir) {

            XParametricSurface3D.Dir oDir = 
                XParametricSurface3D.getOppositeDir(dir);
            XBSplineCurve3D[] cvs = this.decompose(dir);
            Vector4[] cps = new Vector4[this.getNumCPs(oDir)];
            for (int i = 0; i < cps.Length; i++) {
                Vector3 p = cvs[i].calcPos(u);
                cps[i] = new Vector4(p.x, p.y, p.z, 1f);
            }
            double[] U = XKnotVectorUtil.copyKnotVector(
                this.getKnotVector(oDir));
            XBSplineCurve3D curve = new XBSplineCurve3D(this.getDeg(oDir), U,
                cps);
            return curve;
        }

        // Refer to The NURBS Book, 2nd Edition, pp.109-110. Eq.(3.14), (3.15)
        // if dir is Dir.U: the resulting curves are u-directional. 
        // { C_{j}(u) = sum_{i=0}^{n} N_{i,p}(u) P_{i,j} }_{j=0}^{m} 
        // if dir is Dir.V: the resulting curves are v-directional.
        // { C_{i}(v) = sum_{j=0}^{m} N_{j,q}(v) P_{i,j} }_{i=0}^{n} 
        public XBSplineCurve3D[] decompose(XParametricSurface3D.Dir dir) {
            XParametricSurface3D.Dir oDir = 
                XParametricSurface3D.getOppositeDir(dir);
            XBSplineCurve3D[] cvs = new XBSplineCurve3D[this.getNumCPs(oDir)];
            for (int i = 0; i < cvs.Length; i++) {
                Vector4[] cps = XCPsUtil.copyCPs(this.mCPs, i, dir);
                double[] U = XKnotVectorUtil.copyKnotVector(
                    this.getKnotVector(dir));
                cvs[i] = new XBSplineCurve3D(this.getDeg(dir), U, cps);
            }
            return cvs;
        }
    }
}