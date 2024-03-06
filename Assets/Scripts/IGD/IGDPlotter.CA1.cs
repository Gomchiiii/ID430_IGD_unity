using System.Collections.Generic;
using UnityEngine;
using XAppObject;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public static partial class IGDPlotter {
        static float offset3 = 0.05f;

        // CA1-1: Draw Fig.1 in the assignment document
        public static void CA1_1() {
   
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

            IGDPlotter.drawArrow3D(xAxisStart, xAxisEnd);
            IGDPlotter.drawArrow3D(yAxisStart, yAxisEnd);
            IGDPlotter.drawArrow3D(zAxisStart, zAxisEnd);

            // Label x, y, z;
            IGDPlotter.addImage3D("Letters/x", xAxisEnd + Vector3.right * offset3); 
            IGDPlotter.addImage3D("Letters/y", yAxisEnd + Vector3.forward * offset3);
            IGDPlotter.addImage3D("Letters/z", zAxisEnd + Vector3.up * offset3);

            // Label Orientation
            IGDPlotter.addImage3D("Letters/O", -Vector3.forward * offset3 + Vector3.up * offset3, 0.7f);

            // Draw dots P and Q 
            Vector3 P = new Vector3(axisLength * 0.5f, axisLength * 0.3f, 0f);
            Vector3 Q = new Vector3(axisLength * 0.1f, axisLength * 0.4f, axisLength * 0.3f);
            IGDPlotter.drawDot3D(P);
            IGDPlotter.drawDot3D(Q);

            // Draw vector PQ
            IGDPlotter.drawArrow3D(P, Q);

            //Label P and Q
            IGDPlotter.addImage3D("Letters/P", P + Vector3.up * offset3, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_dotP", P - Vector3.up * offset3, 0.3f);
            IGDPlotter.addImage3D("Letters/Q", Q + Vector3.up * offset3, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_dotQ", Q - Vector3.up * offset3, 0.3f);

            // Draw vector vec(v)
            Vector3 v = new Vector3(axisLength * 0.2f, axisLength * 0.4f, axisLength * 0.9f);
            IGDPlotter.drawArrow3D(Vector3.zero, v);

            //Label vec(v)
            IGDPlotter.addImage3D("Formulas/C01_dotv1v2v3", v - Vector3.up * offset3, 0.3f);
            IGDPlotter.addImage3D("Formulas/C01_vecv", v + Vector3.up * offset3, 0.3f);

            //Draw Dashed Lines v1, v2, v3
            Vector3 v1 = new Vector3(axisLength * 0.2f, 0f, 0f);
            Vector3 v2 = new Vector3(0f, 0f, axisLength * 0.9f);
            Vector3 v0 = new Vector3(axisLength * 0.2f, 0f, axisLength * 0.9f);
            IGDPlotter.drawDashedLine3D(v1, v0, 0.5f, Color.blue);
            IGDPlotter.drawDashedLine3D(v2, v0, 0.5f, Color.blue);
            IGDPlotter.drawDashedLine3D(v0, v, 0.5f, Color.blue);

            //Label Dashed Lines v1, v2, v3
            IGDPlotter.addImage3D("Formulas/C01_dashv1", v2 + v1 * 0.5f, 0.3f);
            IGDPlotter.addImage3D("Formulas/C01_dashv2", v1 + v2 * 0.5f, 0.3f);
            IGDPlotter.addImage3D("Formulas/C01_dashv3", (v + v0) * 0.5f, 0.3f);

        }

        // CA1-2: Draw Fig.2 in the assignment document
        public static void CA1_2() {
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

            IGDPlotter.drawArrow3D(xAxisStart, xAxisEnd);
            IGDPlotter.drawArrow3D(yAxisStart, yAxisEnd);
            IGDPlotter.drawArrow3D(zAxisStart, zAxisEnd);

            // Label x, y, z;
            IGDPlotter.addImage3D("Letters/x", xAxisEnd + Vector3.right * offset3);
            IGDPlotter.addImage3D("Letters/y", yAxisEnd + Vector3.forward * offset3);
            IGDPlotter.addImage3D("Letters/z", zAxisEnd + Vector3.up * offset3);

            // Label Orientation
            IGDPlotter.addImage3D("Letters/O", - Vector3.forward * offset3 + Vector3.up * offset3, 0.7f);

            //Draw vector i, j, k
            Vector3 iEnd = i * 0.3f;
            Vector3 jEnd = j * 0.3f;
            Vector3 kEnd = k * 0.3f;

            IGDPlotter.drawArrow3D(Vector3.zero, iEnd, 1.3f, Color.blue);
            IGDPlotter.drawArrow3D(Vector3.zero, jEnd, 1.3f, Color.blue);
            IGDPlotter.drawArrow3D(Vector3.zero, kEnd, 1.3f, Color.blue);

            //Lavel vector i, j, k
            IGDPlotter.addImage3D("Formulas/C01_veci", iEnd * 0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_vecj", jEnd * 0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_veck", kEnd *0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);

            // Draw vector vec(v)
            Vector3 v = new Vector3(axisLength * 0.4f, axisLength * 0.9f, axisLength * 0.9f);
            IGDPlotter.drawArrow3D(Vector3.zero, v);

            //Label vec(v)
            IGDPlotter.addImage3D("Formulas/C01_vecvcomp", v + Vector3.up * offset3, 0.3f);
            IGDPlotter.addImage3D("Formulas/C01_v1iv2jv3k", v - Vector3.up * offset3, 0.3f);

            //Draw Dashed Lines v1, v2, v3
            //Hadamard Product between unit vectors and v
            Vector3 v1 = Vector3.Scale(Vector3.right, v);
            Vector3 v2 = Vector3.Scale(Vector3.forward, v);
            Vector3 v0 = v1 + v2; // v - v3
            IGDPlotter.drawDashedLine3D(v1, v0, 0.5f, Color.blue);
            IGDPlotter.drawDashedLine3D(v2, v0, 0.5f, Color.blue);
            IGDPlotter.drawDashedLine3D(v0, v, 0.5f, Color.blue);

            //Label Dashed Lines v1, v2, v3
            IGDPlotter.addImage3D("Formulas/C01_dashv1", v2 + v1 * 0.5f, 0.3f);
            IGDPlotter.addImage3D("Formulas/C01_dashv2", v1 + v2 * 0.5f, 0.3f);
            IGDPlotter.addImage3D("Formulas/C01_dashv3", (v + v0) * 0.5f, 0.3f);

            //Draw vector v1i, v2j, v3k
            IGDPlotter.drawArrow3D(Vector3.zero, v1, 1.3f, Color.red);
            IGDPlotter.drawArrow3D(Vector3.zero, v - v0, 1.3f, Color.red);
            IGDPlotter.drawArrow3D(Vector3.zero, v2, 1.3f, Color.red);

            //Label vector v1i, v2j, v3k
            IGDPlotter.addImage3D("Formulas/C01_v1i", v1 * 0.5f, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_v2j", (v - v0) * 0.5f, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_v3k", v2 * 0.5f, 0.5f);

        }

        // CA1-3: Draw Fig.3 in the assignment document
        public static void CA1_3() {
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

            IGDPlotter.drawArrow3D(xAxisStart, xAxisEnd);
            IGDPlotter.drawArrow3D(yAxisStart, yAxisEnd);
            IGDPlotter.drawArrow3D(zAxisStart, zAxisEnd);

            // Label x, y, z;
            IGDPlotter.addImage3D("Letters/x", xAxisEnd + Vector3.right * offset3);
            IGDPlotter.addImage3D("Letters/y", yAxisEnd + Vector3.forward * offset3);
            IGDPlotter.addImage3D("Letters/z", zAxisEnd + Vector3.up * offset3);

            // Label Orientation
            IGDPlotter.addImage3D("Letters/O", - Vector3.forward * offset3 + Vector3.up * offset3, 0.7f);

            //Draw vector i, j, k
            Vector3 iEnd = i * 0.3f;
            Vector3 jEnd = j * 0.3f;
            Vector3 kEnd = k * 0.3f;

            IGDPlotter.drawArrow3D(Vector3.zero, iEnd, 1.3f);
            IGDPlotter.drawArrow3D(Vector3.zero, jEnd, 1.3f);
            IGDPlotter.drawArrow3D(Vector3.zero, kEnd, 1.3f);

            //Lavel vector i, j, k
            IGDPlotter.addImage3D("Formulas/C01_veci", iEnd * 0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_vecj", jEnd * 0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_veck", kEnd * 0.5f + Vector3.up * offset3 - Vector3.forward * offset3, 0.5f);

            //Draw Dot P and vector OP
            Vector3 P = new Vector3(axisLength * 0.6f, axisLength * 0.7f, axisLength * 0.8f);
            IGDPlotter.drawDot3D(P);
            IGDPlotter.drawArrow3D(Vector3.zero, P);

            //Lavel Dot P and vector Op
            IGDPlotter.addImage3D("Letters/P", P + Vector3.up * offset3, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_vecv", P * 0.5f + Vector3.up * offset3, 0.5f);

            //Draw vector alpha1i, alpha2j, alpha3k
            Vector3 alpha1 = Vector3.Scale(Vector3.right, P);
            Vector3 alpha2 = alpha1 + Vector3.Scale(Vector3.forward, P);

            IGDPlotter.drawArrow3D(Vector3.zero, alpha1, 1f, Color.red);
            IGDPlotter.drawArrow3D(alpha1, alpha2, 1f, Color.red);
            IGDPlotter.drawArrow3D(alpha2, P, 1f, Color.red);

            //Label vector alpha1i, alpha2j, alpha3k
            IGDPlotter.addImage3D("Formulas/C01_alpha1i", alpha1 * 0.5f + Vector3.up * offset3, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_alpha2j", (alpha1 + alpha2) * 0.5f + Vector3.up * offset3 , 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_alpha3k", (P + alpha2) * 0.5f + Vector3.forward * offset3, 0.5f);

            //Draw Dashed Line 
            IGDPlotter.drawDashedLine3D(Vector3.Scale(Vector3.forward, P), alpha2, 0.5f, Color.red);

        }
        // CA1-4: Draw Fig.4 in the assignment document
        public static void CA1_4() {

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

            IGDPlotter.drawArrow3D(xAxisStart, xAxisEnd);
            IGDPlotter.drawArrow3D(yAxisStart, yAxisEnd);
            IGDPlotter.drawArrow3D(zAxisStart, zAxisEnd);

            // Label x, y, z;
            IGDPlotter.addImage3D("Letters/x", xAxisEnd + Vector3.right * offset3);
            IGDPlotter.addImage3D("Letters/y", yAxisEnd + Vector3.forward * offset3);
            IGDPlotter.addImage3D("Letters/z", zAxisEnd + Vector3.up * offset3);

            // Label Orientation
            IGDPlotter.addImage3D("Letters/O", - Vector3.forward * offset3 + Vector3.up * offset3, 0.7f);

            // Draw dots A and B 
            Vector3 A = new Vector3(axisLength * 0.5f, axisLength * 0.5f, axisLength * 0.2f);
            Vector3 B = new Vector3( - axisLength * 0.1f, axisLength * 0.6f, axisLength * 0.3f);
            IGDPlotter.drawDot3D(A);
            IGDPlotter.drawDot3D(B);

            //Label dots A and B
            IGDPlotter.addImage3D("Letters/A", A + Vector3.up * offset3, 0.5f);
            IGDPlotter.addImage3D("Letters/B", B + Vector3.up * offset3, 0.5f);

            // Draw vector u1, u2
            IGDPlotter.drawArrow3D(Vector3.zero, A);
            IGDPlotter.drawArrow3D(Vector3.zero, B);

            //Label vector u1, u2
            IGDPlotter.addImage3D("Formulas/C01_vecu1", A - Vector3.up * offset3 * 2, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_vecu2", B - Vector3.up * offset3 * 2, 0.5f);

            //Linear interpolation of vector u1 and u2
            Vector3 ABStartPoint = ( - 1f) * A + 2f * B;
            Vector3 ABEndPoint = 2f * A + (-1f) * B;

            IGDPlotter.drawLine3D(ABStartPoint, ABEndPoint, 0.5f);
            IGDPlotter.drawLine3D(A, B, 1.2f, Color.red);

        }
        // CA1-5: Draw Fig.5 in the assignment document
        public static void CA1_5() {
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

            IGDPlotter.drawArrow3D(xAxisStart, xAxisEnd);
            IGDPlotter.drawArrow3D(yAxisStart, yAxisEnd);
            IGDPlotter.drawArrow3D(zAxisStart, zAxisEnd);

            // Label x, y, z;
            IGDPlotter.addImage3D("Letters/x", xAxisEnd + Vector3.right * offset3);
            IGDPlotter.addImage3D("Letters/y", yAxisEnd + Vector3.forward * offset3);
            IGDPlotter.addImage3D("Letters/z", zAxisEnd + Vector3.up * offset3);

            // Label Orientation
            IGDPlotter.addImage3D("Letters/O", - Vector3.forward * offset3 + Vector3.up * offset3, 0.7f);

            // Draw Vectors u1, u2, u3 
            Vector3 A = new Vector3(-axisLength * 0.1f, axisLength * 0.3f, axisLength * 0.3f);
            Vector3 B = new Vector3( - axisLength * 0.3f, axisLength * 0.6f, axisLength * 0.4f);
            Vector3 C = new Vector3(axisLength * 0.5f, axisLength * 0.8f, axisLength * 0.2f);

            IGDPlotter.drawArrow3D(Vector3.zero, A);
            IGDPlotter.drawArrow3D(Vector3.zero, B);
            IGDPlotter.drawArrow3D(Vector3.zero, C);

            //Label vector u1, u2
            IGDPlotter.addImage3D("Formulas/C01_vecu1", A - Vector3.up * offset3 * 2, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_vecu2", B - Vector3.up * offset3 * 2, 0.5f);
            IGDPlotter.addImage3D("Formulas/C01_vecu3", C - Vector3.up * offset3 * 2, 0.5f);

            //Draw Triangle
            List<Vector3> Triangle = new();
            Triangle.Add(A);
            Triangle.Add(B);
            Triangle.Add(C);
            Triangle.Add(A);
            IGDPlotter.drawPolyline3D(Triangle, 1f, Color.red);
        }
    }
}