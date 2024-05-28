using UnityEngine;
using XGeom;
using XGeom.NURBS;

namespace XAppObject {
    public class XAppBezierSurface3D : XAppSurface3D {
        // constants 
        public static readonly int DEFAULT_NUM_CURVE_SEGS_U = 20;
        public static readonly int DEFAULT_NUM_CURVE_SEGS_V = 20;
        public static readonly Color DEFAULT_COLOR = 
            new Color(0f, 0f, 1f, 0.5f);
        
        // fields
        private Color mColor = XAppBezierCurve3D.DEFAULT_COLOR;
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        public void setCPs(Vector4[,] cps) {
            XBezierSurface3D bs = (XBezierSurface3D)this.mGeom;
            int degU = bs.getDegU();
            int degV = bs.getDegV();
            this.mGeom = new XBezierSurface3D(degU, degV, cps);
            this.refreshAtGeomChange();
        }
        
        // constructor 
        public XAppBezierSurface3D(string name, int degU, int degV, 
            Vector4[,] cps, Color color) : base($"{ name }/BezierSurface3D") {
            
            this.mGeom = new XBezierSurface3D(degU, degV, cps);
            this.mColor = color;

            this.refreshAtGeomChange();
        }
        public XAppBezierSurface3D(string name, XBezierSurface3D surface, 
            Color color) : base($"{ name }/BezierSurface3D") {

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
                XAppBezierSurface3D.DEFAULT_NUM_CURVE_SEGS_U,
                XAppBezierSurface3D.DEFAULT_NUM_CURVE_SEGS_V);
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            //mr.material = new Material(Shader.Find("Standard"));
            mr.material.color = this.mColor;
        }

        protected override void refreshCollider() {
            MeshCollider mc = this.mGameObject.GetComponent<MeshCollider>();
            mc.sharedMesh = this.mGameObject.GetComponent<MeshFilter>().mesh;
        }

        public override Vector3[,] samplePts(int numPtsU, int numPtsV) {
            XBezierSurface3D bs = (XBezierSurface3D)this.getGeom();

            double du = 1.0 / (double)(numPtsU - 1);
            double dv = 1.0 / (double)(numPtsV - 1);

            Vector3[,] pts = new Vector3[numPtsU, numPtsV];
            for (int i = 0; i < numPtsU; i++) {
                double u = (double)i * du;
                for (int j = 0; j < numPtsV; j++) {
                    double v = (double)j * dv;
                    pts[i, j] = bs.calcPos(u, v);
                }
            }
            return pts;
        }

        public override Mesh calcMesh(int numSegsU, int numSegsV) {
            XBezierSurface3D bs = (XBezierSurface3D)this.getGeom();

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