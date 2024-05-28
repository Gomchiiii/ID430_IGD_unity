using UnityEngine;
using XMath.ExternalLib;

namespace XMath {
    public class XColVector : Matrix {
        // copies only matrix values. 
        public XColVector(XColVector m) : base(m.rows, 1) {
            this.mat = (double[,])m.mat.Clone();
        }
        // copies only matrix values. 
        public XColVector(Matrix m) : base(m.rows, 1) {
            Debug.Assert(m.cols == 1);
            this.mat = (double[,])m.mat.Clone();
        }
        public XColVector(double[] v) : base(v.Length, 1) {
            for (int i = 0; i < rows; i++) {
                mat[i, 0] = v[i];
            }
        }
        // defines its dimension only. 
        public XColVector(int length) :
            base(length, 1) {
        }
        
        // access this vector as a 1D array
        public double this[int i]      
        {
            get { return mat[i, 0]; }
            set { mat[i, 0] = value; }
        }

        public void setCol(double[] v) {
            Debug.Assert(v.Length == rows);
            for (int i = 0; i < rows; i++) {
                mat[i, 0] = v[i];
            }
        }

        public int getLength() {
            return this.rows;
        }

        public XRowVector calcTranspose() {
            XRowVector rv = new XRowVector(this.rows);
            for (int i = 0; i < rows; i++) {
                //rv.mat[0, i] = this.mat[i, 0];
                rv[i] = this[i];
            }
            return rv;
        }

        // operator overloading: negate
        public static XColVector operator -(XColVector m) {
            Matrix mm = -(Matrix)m;
            XColVector cv = new XColVector(mm);
            return cv;
        }
        // operator overloading: add
        public static XColVector operator +(XColVector m1, XColVector m2) {
            Matrix mm = (Matrix)m1 + (Matrix)m2;
            XColVector cv = new XColVector(mm);
            return cv;
        }
        // operator overloading: subtract
        public static XColVector operator -(XColVector m1, XColVector m2) {
            Matrix mm = (Matrix)m1 - (Matrix)m2;
            XColVector cv = new XColVector(mm);
            return cv;
        }
    }
}