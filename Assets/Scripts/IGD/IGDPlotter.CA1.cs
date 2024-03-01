using UnityEngine;
using XAppObject;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public partial class IGDPlotter {
        float offset3 = 0.05f;

        // CA1-1: Draw Fig.1 in the assignment document
        public void CA1_1() {
   
            //Draw x, y, z axes.
            float axisLength = 1f;

            //opposite with unity coordinate system
            Vector3 i = Vector3.right;
            Vector3 j = Vector3.forward;
            Vector3 k = Vector3.up;

            Vector3 xAxisEnd = i * axisLength;
            Vector3 xAxisStart = - i * axisLength * 0.15f;
            Vector3 yAxisEnd = j * axisLength;
            Vector3 yAxisStart = - j * axisLength * 0.15f;
            Vector3 zAxisEnd = k * axisLength;
            Vector3 zAxisStart = - k * axisLength * 0.15f;

            this.drawArrow3D(xAxisStart, xAxisEnd);
            this.drawArrow3D(yAxisStart, yAxisEnd);
            this.drawArrow3D(zAxisStart, zAxisEnd);

            // Label x, y, z;
            this.addImage3D("Letters/x", xAxisEnd + Vector3.right * offset3); 
            this.addImage3D("Letters/y", yAxisEnd + Vector3.forward * offset3);
            this.addImage3D("Letters/z", zAxisEnd + Vector3.up * offset3);

            // Label Orientation
            this.addImage3D("Letters/O", -Vector3.forward * offset3 + Vector3.up * offset3, 0.7f);

            // Draw dots P and Q 
            Vector3 P = new Vector3(axisLength * 0.5f, axisLength * 0.3f, 0f);
            Vector3 Q = new Vector3(axisLength * 0.1f, axisLength * 0.4f, axisLength * 0.3f);
            this.drawDot3D(P);
            this.drawDot3D(Q);

            // Draw vector PQ
            this.drawArrow3D(P, Q);

            /*
            //Label vector P
            Vector2 labelPosAB = (A + B) / 2f;
            float offset = 50f;
            labelPosAB += Vector2.up * offset;
            labelPosAB += Vector2.left * (offset * 0.5f);
            this.addImage2D("Formulas/W01_vecAB", labelPosAB, 0.7f);
            */

            //Label P and Q
            this.addImage3D("Letters/P", P + Vector3.up * offset3, 0.5f);
            this.addImage3D("Formulas/C01_dotP", P - Vector3.up * offset3, 0.3f);
            this.addImage3D("Letters/Q", Q + Vector3.up * offset3, 0.5f);
            this.addImage3D("Formulas/C01_dotQ", Q - Vector3.up * offset3, 0.3f);

            // Draw vector vec(v)
            Vector3 v = new Vector3(axisLength * 0.2f, axisLength * 0.4f, axisLength * 0.9f);
            this.drawArrow3D(Vector3.zero, v);

            //Label vec(v)
            this.addImage3D("Formulas/C01_dotv1v2v3", v - Vector3.up * offset3, 0.3f);
            this.addImage3D("Formulas/C01_vecv", v + Vector3.up * offset3, 0.3f);

            //Draw Dashed Lines v1, v2, v3
            Vector3 v1 = new Vector3(axisLength * 0.2f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 0f, axisLength * 0.9f);
            Vector3 v0 = new Vector3(axisLength * 0.2f, 0f, axisLength * 0.9f);
            this.drawDashedLine3D(v1, v0, 0.5f, Color.blue);
            this.drawDashedLine3D(v2, v0, 0.5f, Color.blue);
            this.drawDashedLine3D(v0, v, 0.5f, Color.blue);

            //Label Dashed Lines v1, v2, v3
            this.addImage3D("Formulas/C01_dashv1", v2 + v1 * 0.5f, 0.3f);
            this.addImage3D("Formulas/C01_dashv2", v1 + v2 * 0.5f, 0.3f);
            this.addImage3D("Formulas/C01_dashv3", (v + v0) * 0.5f, 0.3f);

        }

        // CA1-2: Draw Fig.2 in the assignment document
        public void CA1_2() {
            //Draw x, y, z axes.
            float axisLength = 1f;

            //opposite with unity coordinate system
            Vector3 i = Vector3.right;
            Vector3 j = Vector3.forward;
            Vector3 k = Vector3.up;

            Vector3 xAxisEnd = i * axisLength;
            Vector3 xAxisStart = -i * axisLength * 0.15f;
            Vector3 yAxisEnd = j * axisLength;
            Vector3 yAxisStart = -j * axisLength * 0.15f;
            Vector3 zAxisEnd = k * axisLength;
            Vector3 zAxisStart = -k * axisLength * 0.15f;

            this.drawArrow3D(xAxisStart, xAxisEnd);
            this.drawArrow3D(yAxisStart, yAxisEnd);
            this.drawArrow3D(zAxisStart, zAxisEnd);

            // Label x, y, z;
            this.addImage3D("Letters/x", xAxisEnd + Vector3.right * offset3);
            this.addImage3D("Letters/y", yAxisEnd + Vector3.forward * offset3);
            this.addImage3D("Letters/z", zAxisEnd + Vector3.up * offset3);

            // Label Orientation
            this.addImage3D("Letters/O", -Vector3.forward * offset3 + Vector3.up * offset3, 0.7f);

            //Draw vector i, j, k
            Vector3 iEnd = i * 0.3f;
            Vector3 jEnd = j * 0.3f;
            Vector3 kEnd = k * 0.3f;

            this.drawArrow3D(Vector3.zero, iEnd, 1.3f, Color.blue);
            this.drawArrow3D(Vector3.zero, jEnd, 1.3f, Color.blue);
            this.drawArrow3D(Vector3.zero, kEnd, 1.3f, Color.blue);

            //Lavel vector i, j, k
            this.addImage3D("Formulas/C01_veci", iEnd * 0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);
            this.addImage3D("Formulas/C01_vecj", jEnd * 0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);
            this.addImage3D("Formulas/C01_veck", kEnd *0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);

            // Draw vector vec(v)
            Vector3 v = new Vector3(axisLength * 0.4f, axisLength * 0.9f, axisLength * 0.9f);
            this.drawArrow3D(Vector3.zero, v);

            //Label vec(v)
            this.addImage3D("Formulas/C01_vecv", v + Vector3.up * offset3, 0.3f);
            this.addImage3D("Formulas/C01_v1iv2jv3k", v - Vector3.up * offset3, 0.3f);

            //Draw Dashed Lines v1, v2, v3
            //Hadamard Product between unit vectors and v
            Vector3 v1 = Vector3.Scale(Vector3.right, v);
            Vector3 v2 = Vector3.Scale(Vector3.forward, v);
            Vector3 v0 = v1 + v2; // v - v3
            this.drawDashedLine3D(v1, v0, 0.5f, Color.blue);
            this.drawDashedLine3D(v2, v0, 0.5f, Color.blue);
            this.drawDashedLine3D(v0, v, 0.5f, Color.blue);

            //Label Dashed Lines v1, v2, v3
            this.addImage3D("Formulas/C01_dashv1", v2 + v1 * 0.5f, 0.3f);
            this.addImage3D("Formulas/C01_dashv2", v1 + v2 * 0.5f, 0.3f);
            this.addImage3D("Formulas/C01_dashv3", (v + v0) * 0.5f, 0.3f);

            //Draw vector v1i, v2j, v3k
            this.drawArrow3D(Vector3.zero, v1, 1.3f, Color.red);
            this.drawArrow3D(Vector3.zero, v - v0, 1.3f, Color.red);
            this.drawArrow3D(Vector3.zero, v2, 1.3f, Color.red);

            //Label vector v1i, v2j, v3k
            this.addImage3D("Formulas/C01_v1i", v1 * 0.5f, 0.5f);
            this.addImage3D("Formulas/C01_v2j", (v - v0) * 0.5f, 0.5f);
            this.addImage3D("Formulas/C01_v3k", v2 * 0.5f, 0.5f);

        }

        // CA1-3: Draw Fig.3 in the assignment document
        public void CA1_3() {
            //Draw x, y, z axes.
            float axisLength = 1f;

            //opposite with unity coordinate system
            Vector3 i = Vector3.right;
            Vector3 j = Vector3.forward;
            Vector3 k = Vector3.up;

            Vector3 xAxisEnd = i * axisLength;
            Vector3 xAxisStart = -i * axisLength * 0.15f;
            Vector3 yAxisEnd = j * axisLength;
            Vector3 yAxisStart = -j * axisLength * 0.15f;
            Vector3 zAxisEnd = k * axisLength;
            Vector3 zAxisStart = -k * axisLength * 0.15f;

            this.drawArrow3D(xAxisStart, xAxisEnd);
            this.drawArrow3D(yAxisStart, yAxisEnd);
            this.drawArrow3D(zAxisStart, zAxisEnd);

            // Label x, y, z;
            this.addImage3D("Letters/x", xAxisEnd + Vector3.right * offset3);
            this.addImage3D("Letters/y", yAxisEnd + Vector3.forward * offset3);
            this.addImage3D("Letters/z", zAxisEnd + Vector3.up * offset3);

            // Label Orientation
            this.addImage3D("Letters/O", -Vector3.forward * offset3 + Vector3.up * offset3, 0.7f);

            //Draw vector i, j, k
            Vector3 iEnd = i * 0.3f;
            Vector3 jEnd = j * 0.3f;
            Vector3 kEnd = k * 0.3f;

            this.drawArrow3D(Vector3.zero, iEnd, 1.3f);
            this.drawArrow3D(Vector3.zero, jEnd, 1.3f);
            this.drawArrow3D(Vector3.zero, kEnd, 1.3f);

            //Lavel vector i, j, k
            this.addImage3D("Formulas/C01_veci", iEnd * 0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);
            this.addImage3D("Formulas/C01_vecj", jEnd * 0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);
            this.addImage3D("Formulas/C01_veck", kEnd * 0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);

            //Draw Dot P and vector OP
            Vector3 P = new Vector3(axisLength * 0.6f, axisLength * 0.7f, axisLength * 0.8f);
            this.drawDot3D(P);
            this.drawArrow3D(Vector3.zero, P);

            //Lavel Dot P and vector Op
            this.addImage3D("Letters/P", P + Vector3.up * offset3, 0.5f);
            this.addImage3D("Formulas/C01_vecv", P * 0.5f + Vector3.up * offset3, 0.5f);

            //Draw vector alpha1i, alpha2j, alpha3k
            Vector3 alpha1 = Vector3.Scale(Vector3.right, P);
            Vector3 alpha2 = alpha1 + Vector3.Scale(Vector3.forward, P);
            
            this.drawArrow3D(Vector3.zero, alpha1, 1f, Color.red);
            this.drawArrow3D(alpha1, alpha2, 1f, Color.red);
            this.drawArrow3D(alpha2, P, 1f, Color.red);

            //Draw Dashed Line 
            this.drawDashedLine3D(Vector3.Scale(Vector3.forward, P), alpha2, 0.5f, Color.red);

            ////Label 
            //this.addImage3D("Formulas/C01_v1i", v1 * 0.5f, 0.5f);
            //this.addImage3D("Formulas/C01_v2j", (v - v0) * 0.5f, 0.5f);
            //this.addImage3D("Formulas/C01_v3k", v2 * 0.5f, 0.5f);
        }
        // CA1-4: Draw Fig.4 in the assignment document
        public void CA1_4() {
            /*
            Impelment here
            */
        }
        // CA1-5: Draw Fig.5 in the assignment document
        public void CA1_5() {
            /*
            Impelment here
            */
        }
    }
}