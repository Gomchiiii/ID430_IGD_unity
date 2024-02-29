using Unity.VisualScripting;
using UnityEngine;
using XAppObject;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public partial class IGDPlotter {
        public void W01_draw2DVectors()
        {
            // Draw a dot (0, 0)
            this.drawDot2D(Vector2.zero);

            // Draw x and y axes
            float yMax = Screen.height * 0.6f;
            float yMin = -Screen.height * 0.2f;
            Vector2 yAxisStart = new Vector2(0f, yMin);
            Vector2 yAxisEnd = new Vector2(0f, yMax);
            this.drawArrow2D(yAxisStart, yAxisEnd);

            float xMax = yMax;
            float xMin = yMin;
            Vector2 xAxisStart = new Vector2(xMin, 0f);
            Vector2 xAxisEnd = new Vector2(xMax, 0f);
            this.drawArrow2D(xAxisStart, xAxisEnd);

            // Draw dots A and B 
            Vector2 A = new Vector2(xMin * 0.9f, yMax * 0.4f);
            Vector2 B = new Vector2(xMax * 0.2f, yMax * 0.5f);
            this.drawDot2D(A);
            this.drawDot2D(B);

            // Draw vector AB
            this.drawArrow2D(A, B);

            //Label vector AB
            Vector2 labelPosAB = (A + B) / 2f;
            float offset = 50f;
            labelPosAB += Vector2.up * offset;
            labelPosAB += Vector2.left * (offset * 0.5f);
            this.addImage2D("Formulas/W01_vecAB", labelPosAB, 0.7f);

            //Label A and B
            this.addImage2D("Letters/A", A + Vector2.up * offset, 0.7f);
            this.addImage2D("Letters/B", B + Vector2.up * offset, 0.7f);

            //Label x and y axes
            this.addImage2D("Letters/x", xAxisEnd + Vector2.right * offset, 0.7f);
            this.addImage2D("Letters/y", yAxisEnd + Vector2.up * offset, 0.7f);

            //Draw vetctor OP
            Vector2 vecAB = B - A;
            Vector2 P = vecAB;
            this.drawArrow2D(Vector2.zero, P);

            //Lavel 0 and P
            this.addImage2D("Letters/O", Vector2.left * offset + Vector2.up * offset, 0.7f);
            this.addImage2D("Letters/P", P + Vector2.left * offset + Vector2.up * offset, 0.7f);

            // Draw dots C and D 
            Vector2 C = new Vector2(xMax * 0.5f, yMax * 0.4f);
            Vector2 D = C + vecAB;
            this.drawDot2D(C);
            this.drawDot2D(D);

            //Label C and D
            this.addImage2D("Letters/C", C + Vector2.up * offset, 0.7f);
            this.addImage2D("Letters/D", D + Vector2.up * offset, 0.7f);

            // Draw vector CD
            this.drawArrow2D(C, D);

            // Draw dots E and F 
            Vector2 E = new Vector2(xMin * 0.9f, yMin * 0.4f);
            Vector2 F = E + vecAB;
            this.drawDot2D(E);
            this.drawDot2D(F);

            //Label E and F
            this.addImage2D("Letters/E", E + Vector2.up * offset, 0.7f);
            this.addImage2D("Letters/F", F + Vector2.up * offset, 0.7f);

            // Draw vector EF
            this.drawArrow2D(E, F);

            // Draw dashed lines AC, BD, AE, BF
            this.drawDashedLine2D(A, C, 1f, Color.red);
            this.drawDashedLine2D(B, D, 1f, Color.red);
            this.drawDashedLine2D(A, E, 1f, Color.red);
            this.drawDashedLine2D(B, F, 1f, Color.red);


        }
    }
}