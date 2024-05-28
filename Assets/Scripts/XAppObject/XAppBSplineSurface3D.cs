using UnityEngine;
using XGeom;
using XGeom.NURBS;

namespace XAppObject {
    public class XAppBSplineSurface3D : XAppSurface3D {
        // constants 
        public static readonly int DEFAULT_NUM_CURVE_SEGS_U = 20;
        public static readonly int DEFAULT_NUM_CURVE_SEGS_V = 20;
        public static readonly Color DEFAULT_COLOR = 
            new Color(0.5f, 0.5f, 1f, 0.5f);
        
        // fields
        private Color mColor = XAppBSplineSurface3D.DEFAULT_COLOR;
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        public void setCPs(Vector4[,] cps) {
            XBSplineSurface3D bs = (XBSplineSurface3D)this.mGeom;
            int degU = bs.getDegU();
            int degV = bs.getDegV();
            double[] U = bs.getU();
            double[] V = bs.getV();
            this.mGeom = new XBSplineSurface3D(degU, degV, U, V, cps);
            this.refreshAtGeomChange();
        }
        
        // constructor 
        public XAppBSplineSurface3D(string name, int degU, int degV, 
            double[] U, double[] V, Vector4[,] cps, Color color) : 
            base($"{ name }/BSplineSurface3D") {
            
            this.mGeom = new XBSplineSurface3D(degU, degV, U, V, cps);
            this.mColor = color;

            this.refreshAtGeomChange();
        }
        public XAppBSplineSurface3D(string name, XBSplineSurface3D surface, 
            Color color) : base($"{ name }/BSplineSurface3D") {

            this.mGeom = surface;
            this.mColor = color;

            this.refreshAtGeomChange();
        }

        // methods 
        protected override void addComponents() {
            this.mGameObject.AddComponent<MeshFilter>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<MeshCollider>();
        }

        protected override void refreshRenderer() {
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = this.calcMesh(
                XAppBSplineSurface3D.DEFAULT_NUM_CURVE_SEGS_U,
                XAppBSplineSurface3D.DEFAULT_NUM_CURVE_SEGS_V);
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            mr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
            MeshCollider mc = this.mGameObject.GetComponent<MeshCollider>();
            mc.sharedMesh = this.mGameObject.GetComponent<MeshFilter>().mesh;
        }

        public override Vector3[,] samplePts(int numPtsU, int numPtsV) {
            XBSplineSurface3D bs = (XBSplineSurface3D)this.getGeom();

            double us = bs.getStartParam(XParametricSurface3D.Dir.U);
            double ue = bs.getEndParam(XParametricSurface3D.Dir.U);
            double vs = bs.getStartParam(XParametricSurface3D.Dir.V);
            double ve = bs.getEndParam(XParametricSurface3D.Dir.V);
            double du = (ue - us) / (double)(numPtsU - 1);
            double dv = (ve - vs) / (double)(numPtsV - 1);

            Vector3[,] pts = new Vector3[numPtsU, numPtsV];
            for (int i = 0; i < numPtsU; i++) {
                double u = us + (double)i * du;
                for (int j = 0; j < numPtsV; j++) {
                    double v = vs + (double)j * dv;
                    pts[i, j] = bs.calcPos(u, v);
                }
            }
            return pts;
        }

        public override Mesh calcMesh(int numSegsU, int numSegsV) {
            XBSplineSurface3D bs = (XBSplineSurface3D)this.getGeom();

            Vector3[,] pts = this.samplePts(numSegsU + 1, numSegsV + 1);
            Vector3[] vertices = new Vector3[(numSegsU + 1) * (numSegsV + 1)];

            int k;
            for (int i = 0; i <= numSegsU; i++) {
                for (int j = 0; j <= numSegsV; j++) {
                    k = i * (numSegsV + 1) + j;
                    vertices[k] = pts[i, j];
                }
            }

            int[] triangles = new int[2 * 3 * numSegsU * numSegsV];
            k = 0;
            for (int i = 0; i < numSegsU; i++) {
                for (int j = 0; j < numSegsV; j++) {
                    triangles[k] = i * (numSegsV + 1) + j; k++;
                    triangles[k] = i * (numSegsV + 1) + (j + 1); k++;
                    triangles[k] = (i + 1) * (numSegsV + 1) + j; k++;
                    triangles[k] = (i + 1) * (numSegsV + 1) + j; k++;
                    triangles[k] = i * (numSegsV + 1) + (j + 1); k++;
                    triangles[k] = (i + 1) * (numSegsV + 1) + (j + 1); k++;
                }
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}