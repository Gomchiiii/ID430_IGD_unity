using UnityEngine;

namespace IGD {
    public partial class IGDPlotter {
        // L01_Fig1
        public void draw_L01_fig1() {
            float w = Screen.width;
            float h = Screen.height;
            
            float xa = w * 0.1f;
            float ya = h * 0.5f;
            float xb = w * 0.8f;
            float yb = h * 0.9f;
            
            Vector2 A = new Vector2(xa, ya);
            Vector2 B = new Vector2(xb, yb);

            float scale = 1.5f;
            this.drawDot2D(A, scale);
            this.drawDot2D(B, scale);
            this.drawArrow2D(A, B, scale);

            float offset = 80f;
            this.addImage2D("Letters/A_bold", A + Vector2.down * offset, scale);
            this.addImage2D("Letters/B_bold", B + Vector2.down * offset, scale);

            float offset2 = 100f;
            Vector2 midAB = (A + B) / 2f;
            this.addImage2D("Formulas/L01_fig1_vecAB",
                midAB + Vector2.left * offset2 + Vector2.up * offset2, scale);
        }
        
        // L01_Fig2
        public void draw_L01_fig2() {
            float w = Screen.width;
            float h = Screen.height;

            float xMin = w * 0.1f;
            float x0 = w * 0.25f;
            float xMax = w * 0.9f;
            float yMin = h * 0.1f;
            float y0 = h * 0.3f;
            float yMax = h * 0.9f;
            
            Vector2 xAxisStart = new Vector2(xMin, y0);
            Vector2 xAxisEnd = new Vector2(xMax, y0);
            Vector2 yAxisStart = new Vector2(x0, yMin);
            Vector2 yAxisEnd = new Vector2(x0, yMax);
            
            float scale = 1.3f;
            this.drawArrow2D(xAxisStart, xAxisEnd, scale);
            this.drawArrow2D(yAxisStart, yAxisEnd, scale);
            
            float offset = 50f;
            Vector2 O = new Vector2(x0, y0);
            this.addImage2D("Letters/O_bold", O + Vector2.left * offset +
                Vector2.down * offset, scale);

            this.addImage2D("Letters/x", xAxisEnd + Vector2.down * offset,
                scale);
            this.addImage2D("Letters/y", yAxisEnd + Vector2.left * offset,
                scale);

            float xLen = xMax - xMin;
            float yLen = yMax - yMin;
            Vector2 P = new Vector2(O.x + xLen * 0.35f, O.y + yLen * 0.2f);
            this.drawArrow2D(O, P, scale);
            this.addImage2D("Letters/P_bold", P + Vector2.right * offset, 
                scale);

            Vector2 A = new Vector2(x0 - xLen * 0.2f, y0 + yLen * 0.3f);
            Vector2 OP = P - O;
            Vector2 B = A + OP;
            this.drawArrow2D(A, B, scale);
            this.addImage2D("Letters/A_bold", A + Vector2.left * offset, scale);
            this.addImage2D("Letters/B_bold", B + Vector2.right * offset, scale);

            Vector2 C = new Vector2(B.x, A.y);
            Vector2 D = C + OP;
            this.drawArrow2D(C, D, scale);
            this.addImage2D("Letters/C_bold", C + Vector2.left * offset, scale);
            this.addImage2D("Letters/D_bold", D + Vector2.right * offset, scale);

            Vector2 E = new Vector2(A.x, y0 - yLen * 0.25f);
            Vector2 F = E + OP;
            this.drawArrow2D(E, F, scale);
            this.addImage2D("Letters/E_bold", E + Vector2.left * offset, scale);
            this.addImage2D("Letters/F_bold", F + Vector2.right * offset, scale);
        }
        
        // L01_Fig3
        public void draw_L01_fig3() {
            Vector3 xDir = Vector3.right;
            Vector3 yDir = Vector3.forward;
            Vector3 zDir = Vector3.up;
            
            Vector3 O = Vector3.zero;
            float axisLen = 1.2f;
            Vector3 xEnd = xDir * axisLen;
            Vector3 yEnd = yDir * axisLen;
            Vector3 zEnd = zDir * axisLen;
            
            float scale = 1f;
            float scaleSmall = 0.8f;
            this.drawArrow3D(O, xEnd);
            this.drawArrow3D(O, yEnd);
            this.drawArrow3D(O, zEnd);

            float offset = 0.08f;
            this.addImage3D("Letters/O_bold", O - yDir * offset + zDir * 
                (offset * 0.5f), scaleSmall);
            this.addImage3D("Letters/x", xEnd + xDir * offset, scaleSmall);
            this.addImage3D("Letters/y", yEnd + yDir * offset, scaleSmall);
            this.addImage3D("Letters/z", zEnd + zDir * offset, scaleSmall);

            float v1 = 0.4f;
            float v2 = 1.1f;
            float v3 = 0.6f;
            Vector3 V = v1 * xDir + v2 * yDir + v3 * zDir;
            this.drawDot3D(O, scale);
            this.drawDot3D(V, scale);
            this.drawArrow3D(O, V, scale);
            this.addImage3D("Formulas/L01_fig3_pointV", V + zDir * offset, 
                scaleSmall);
            Vector3 midOV = (O + V) / 2f;
            this.addImage3D("Formulas/L01_fig3_vecV", midOV + zDir * offset, 
                scaleSmall);

            Vector3 Vx = v1 * xDir;
            Vector3 Vy = v2 * yDir;
            Vector3 Vxy = Vx + Vy;
            this.drawDashedLine3D(Vx, Vxy);
            this.drawDashedLine3D(Vy, Vxy);
            this.drawDashedLine3D(Vxy, V);
            Vector3 v1Pos = (Vy + Vxy) / 2f + yDir * offset;
            Vector3 v2Pos = (Vx + Vxy) / 2f + xDir * offset;
            Vector3 v3Pos = (Vxy + V) / 2f + yDir * offset;
            this.addImage3D("Formulas/L01_fig3_v1", v1Pos, scaleSmall);
            this.addImage3D("Formulas/L01_fig3_v2", v2Pos, scaleSmall);
            this.addImage3D("Formulas/L01_fig3_v3", v3Pos, scaleSmall);

            Vector3 P = 0.6f * xDir + 0.6f * zDir;
            Vector3 OV = V - O;
            Vector3 Q = P + OV;
            this.drawDot3D(P, scale);
            this.drawDot3D(Q, scale);
            this.drawArrow3D(P, Q, scale);
            this.addImage3D("Formulas/L01_fig3_pointP", P + zDir * offset, 
                scaleSmall);
            this.addImage3D("Formulas/L01_fig3_pointQ", Q + zDir * offset, 
                scaleSmall);
        }
    }
}