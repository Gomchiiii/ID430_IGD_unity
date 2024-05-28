using UnityEngine;
using XMath.ExternalLib;

namespace XMath {
    public class XMatrix : Matrix {

        public enum Dir { ROW, COL } // ROW: 0, COL: 1
        public static Dir getOppositeDir(Dir dir) {
            switch (dir) {
                case Dir.ROW:
                    return Dir.COL;
                case Dir.COL:
                    return Dir.ROW;
                default: // means nothing. 
                    return Dir.ROW;
            }
        }
        private static int calcNumOfRows(double[] v, Dir dir) {
            switch (dir) {
                case Dir.ROW:
                    return 1;
                case Dir.COL:
                    return v.Length;
                default: // means nothing.
                    return v.Length;
            }
        }

        private static int calcNumOfCols(double[] v, Dir dir) {
            switch (dir) {
                case Dir.ROW:
                    return v.Length;
                case Dir.COL:
                    return 1;
                default: // means nothing.
                    return 1;
            }
        }

        // copies only matrix values. 
        public XMatrix(XMatrix m) : base(m.rows, m.cols) {
            this.mat = (double[,])m.mat.Clone();
        }
        // copies only matrix values. 
        public XMatrix(Matrix m) : base(m.rows, m.cols) {
            this.mat = (double[,])m.mat.Clone();
        }
        public XMatrix(double[,] m) : base(m.GetLength(0), m.GetLength(1)) {
            this.mat = (double[,])m.Clone();
        }
        // creates a matrix whose form is a row vector or column vector. 
        public XMatrix(double[] v, Dir dir) :
            base(XMatrix.calcNumOfRows(v, dir), 
                XMatrix.calcNumOfCols(v, dir)) {

            switch (dir) {
                case Dir.ROW:
                    this.setRow(0, v);
                    break;
                case Dir.COL:
                    this.setCol(0, v);
                    break;
                default: // means nothing. 
                    break;
            }
        }
        
        // defines its dimension only. 
        public XMatrix(int numOfRows, int numOfCols) : 
            base(numOfRows, numOfCols) {
        }

        public int getNumOfRows() {
            return this.rows;
        }
        public int getNumOfCols() {
            return this.cols;
        }

        public bool isSquare() {
            return this.IsSquare();
        }

        public XMatrix getCol(int k) {
            Matrix mm = this.GetCol(k);
            XMatrix xm = new XMatrix(mm);
            return xm;
        }
        public void setCol(int k, XColVector v) {
            Debug.Assert(v.rows == this.rows);
            for (int i = 0; i < rows; i++) {
                mat[i, k] = v[i, 0];
            }
        }
        public void setCol(int k, double[] v) {
            Debug.Assert(v.Length == rows);
            for (int i = 0; i < rows; i++) {
                mat[i, k] = v[i];
            }
        }

        public XMatrix getRow(int k) {
            XMatrix m = new XMatrix(1, cols);
            for (int j = 0; j < cols; j++) {
                m[0, j] = mat[k, j];
            }
            return m;
        }
        public void setRow(int k, XRowVector v) {
            Debug.Assert(v.cols == this.cols);
            for (int j = 0; j < cols; j++) {
                mat[k, j] = v[0, j];
            }
        }
        public void setRow(int k, double[] v) {
            Debug.Assert(v.Length == cols);
            for (int j = 0; j < cols; j++) {
                mat[k, j] = v[j];
            }
        }

        // decompose this matrix into L and U matrices. 
        public void makeLU() {
            this.MakeLU();
        }

        // solves Ax = b for x. 
        public XColVector solveLinearEqnWith(XColVector b) {
            Matrix mm = this.SolveWith(b);
            XColVector xv = new XColVector(mm);
            return xv;
        }

        public XMatrix calcInverse() {
            Matrix mm = this.Invert();
            XMatrix xm = new XMatrix(mm);
            return xm;
        }

        public double calcDeterminant() {
            return this.Det();
        }

        public static XMatrix createZeroMatrix(
            int numOfRows, int numOfCols) {
            Matrix mm = Matrix.ZeroMatrix(numOfRows, numOfCols);
            XMatrix xm = new XMatrix(mm);
            return xm;
        }

        public static XMatrix createIdentityMatrix(
            int numOfRows, int numOfCols) {
            Matrix mm = Matrix.IdentityMatrix(numOfRows, numOfCols);
            XMatrix xm = new XMatrix(mm);
            return xm;
        }

        // needs to know what dispersion means. 
        public static XMatrix createRandomMatrix(
            int numOfRows, int numOfCols, int dispersion) {
            Matrix mm = Matrix.RandomMatrix(numOfRows, numOfCols, 
                dispersion);
            XMatrix xm = new XMatrix(mm);
            return xm;
        }

        public static XMatrix createMatrixByParsing(string s) {
            Matrix mm = Matrix.Parse(s);
            XMatrix xm = new XMatrix(mm);
            return xm;
        }
        
        public XMatrix calcTranspose() {
            Matrix mm = Matrix.Transpose(this);
            XMatrix xm = new XMatrix(mm);
            return xm;
        }

        public XMatrix calcPower(int pow) {
            Matrix mm = Matrix.Power(this, pow);
            XMatrix xm = new XMatrix(mm);
            return xm;
        }

        // operator overloading: negate
        public static XMatrix operator -(XMatrix m) {
            Matrix mm = -(Matrix)m;
            XMatrix xm = new XMatrix(mm);
            return xm;
        }
        // operator overloading: add
        public static XMatrix operator +(XMatrix m1, XMatrix m2) {
            Matrix mm = (Matrix)m1 + (Matrix)m2;
            XMatrix xm = new XMatrix(mm);
            return xm;
        }
        // operator overloading: subtract
        public static XMatrix operator -(XMatrix m1, XMatrix m2) {
            Matrix mm = (Matrix)m1 - (Matrix)m2;
            XMatrix xm = new XMatrix(mm);
            return xm;
        }
        // operator overloading: multiply
        public static XMatrix operator *(XMatrix m1, XMatrix m2) {
            Matrix mm = (Matrix)m1 * (Matrix)m2;
            XMatrix xm = new XMatrix(mm);
            return xm;
        }
        // operator overloading: multiply
        public static XColVector operator *(XMatrix m1, XColVector m2) {
            Matrix mm = (Matrix)m1 * (Matrix)m2;
            XColVector cv = new XColVector(mm);
            return cv;
        }
        // operator overloading: multiply
        public static XMatrix operator *(double s, XMatrix m) {
            Matrix mm = s * (Matrix)m;
            XMatrix xm = new XMatrix(mm);
            return xm;
        }
    }
}