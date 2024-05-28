using UnityEngine;
using XMath.ExternalLib;

namespace XMath {
    public class XRowVector : Matrix {
        // copies only matrix values. 
        public XRowVector(XRowVector m) : base(1, m.cols) {
            this.mat = (double[,])m.mat.Clone();
        }
        // copies only matrix values. 
        public XRowVector(Matrix m) : base(1, m.cols) {
            Debug.Assert(m.rows == 1);
            this.mat = (double[,])m.mat.Clone();
        }
        public XRowVector(double[] v) : base(1, v.Length) {
            for (int j = 0; j < cols; j++) {
                mat[0, j] = v[j];
            }
        }
        // defines its dimension only. 
        public XRowVector(int length) :
            base(1, length) {
        }

        // access this vector as a 1D array
        public double this[int j] {
            get { return mat[0, j]; }
            set { mat[0, j] = value; }
        }

        public void setRow(double[] v) {
            Debug.Assert(v.Length == cols);
            for (int j = 0; j < cols; j++) {
                mat[0, j] = v[j];
            }
        }

        public int getLength() {
            return this.cols;
        }

        public XColVector calcTranspose() {
            XColVector cv = new XColVector(this.cols);
            for (int j = 0; j < cols; j++) {
                cv[j] = this[j];
            }
            return cv;
        }
    }
}