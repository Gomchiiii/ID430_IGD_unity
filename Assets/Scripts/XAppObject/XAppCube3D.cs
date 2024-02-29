using System.Collections.Generic;
using UnityEngine;
using XGeom;

namespace XAppObject {
    public class XAppCube3D : XAppGeom3D {
        // fields
        public void setWidth(float width) {
            XCube3D cube = (XCube3D) this.mGeom;
            this.mGeom = new XCube3D(width, cube.getPos(), cube.getRot());
            this.refreshAtGeomChange();
        }
        private Color mColor = Color.red; // easily noticeable color
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        
        // constructor
        public XAppCube3D(string name, float width, Color color) : 
            base($"{name}/Cube3D") {
            
            this.mGeom = new XCube3D(width, Vector3.zero, Quaternion.identity);
            this.mColor = color;
            
            this.refreshAtGeomChange();
        }
        public XAppCube3D(string name, XCube3D cube, Color color) : 
            base($"{name}/Cube3D") {

            this.mGeom = cube;
            this.mColor = color;

            this.refreshAtGeomChange();
        }
        
        // methods
        protected override void addComponents() {
            this.mGameObject.AddComponent<MeshFilter>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<BoxCollider>();
        }
        
        protected override void refreshRenderer() {
            XCube3D cube = (XCube3D)this.mGeom;
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = this.calcMesh();
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            mr.material.color = this.mColor;
        }
        
        protected override void refreshCollider() {
            BoxCollider bc = this.mGameObject.GetComponent<BoxCollider>();
            float width = ((XCube3D)this.mGeom).getWidth();
            bc.size = new Vector3(width, width, width);
        }

        //    5-----6
        //   /|    /|
        //  1-----2 |
        //  |.4---|.7
        //  0-----3
        private Mesh calcMesh() {
            XCube3D cube = (XCube3D)this.mGeom;
            float width = cube.getWidth();
            float halfWidth = 0.5f * width;
            Vector3 center = cube.getPos();
            Vector3 xDir = cube.calcXDir();
            Vector3 yDir = cube.calcYDir();
            Vector3 zDir = cube.calcZDir();
            
            Vector3 left = center - halfWidth * xDir;
            Vector3 right = center + halfWidth * xDir;
            Vector3 bottom = center - halfWidth * yDir;
            Vector3 top = center + halfWidth * yDir;
            Vector3 front = center - halfWidth * zDir;
            Vector3 back = center + halfWidth * zDir;
            
            Vector3[] vertices = new Vector3[8];
            vertices[0] = front + left + bottom;
            vertices[1] = front + left + top;
            vertices[2] = front + right + top;
            vertices[3] = front + right + bottom;
            vertices[4] = back + left + bottom;
            vertices[5] = back + left + top;
            vertices[6] = back + right + top;
            vertices[7] = back + right + bottom;
            
            List<int> triangles = new List<int>();
            // helper method to add a quad.
            void addQuad(int a, int b, int c, int d) {
                triangles.Add(a);
                triangles.Add(b);
                triangles.Add(c);
                triangles.Add(a);
                triangles.Add(c);
                triangles.Add(d);
            }
            // top
            addQuad(1, 5, 6, 2);
            // front
            addQuad(0, 1, 2, 3);
            // left
            addQuad(4, 5, 1, 0);
            // back
            addQuad(7, 6, 5, 4);
            // right
            addQuad(3, 2, 6, 7);
            // bottom
            addQuad(4, 0, 3, 7);
            
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles.ToArray();
            
            return mesh;
        }
    }
}