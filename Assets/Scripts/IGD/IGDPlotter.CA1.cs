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


            // Draw vector vec(v)
            Vector3 v = new Vector3(axisLength * 0.4f, axisLength * 0.9f, axisLength * 0.9f);
            this.drawArrow3D(Vector3.zero, v);

            //Label vec(v)
            this.addImage3D("Formulas/C01_vecv", v + Vector3.up * offset3, 0.3f);

            //Draw Dashed Lines v1, v2, v3
            Vector3 v1 = new Vector3(axisLength * 0.4f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 0f, axisLength * 0.9f);
            Vector3 v0 = new Vector3(axisLength * 0.4f, 0f, axisLength * 0.9f);
            this.drawDashedLine3D(v1, v0, 0.5f, Color.blue);
            this.drawDashedLine3D(v2, v0, 0.5f, Color.blue);
            this.drawDashedLine3D(v0, v, 0.5f, Color.blue);

            //Label Dashed Lines v1, v2, v3
            this.addImage3D("Formulas/C01_dashv1", v2 + v1 * 0.5f, 0.3f);
            this.addImage3D("Formulas/C01_dashv2", v1 + v2 * 0.5f, 0.3f);
            this.addImage3D("Formulas/C01_dashv3", (v + v0) * 0.5f, 0.3f);

        }
        // CA1-3: Draw Fig.3 in the assignment document
        public void CA1_3() {
            /*
            Impelment here
            */
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