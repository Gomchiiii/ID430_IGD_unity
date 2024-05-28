using System;
using UnityEngine;
using XMath.ExternalLib;

namespace XMath {
    public class XLinearAlgebra {
        // Calculates x minimizing [(b-A*x)T]*(b-A*x) with constraints C*x=d.
        // f(x)=[(b-A*x)T]*(b-A*x)+gT*(C*x-d) where
        // g is the Lagrange multiplier vector.
        // Refer to The NURBS Book, 2nd Edition, pp.413-421.
        //
        // [AT*A CT][x]=[AT*b] 
        // [C     0][g] [d   ]
        //
        // A (mxn), x (nx1), b (mx1)
        // C (rxn), x (nx1), d (rx1)
        // g (rx1)
        // 
        // Step 1:  calculate Inv(AT*A).
        // Step 2:  calculate g by solving 
        //          {C*Inv(AT*A)*CT}*g=C*Inv(AT*A)*AT*b-d
        // Step 3:  calculate x=Inv(AT*A)*(AT*b-CT*g).
        //          
        public static XColVector solveLSQ(XMatrix A, XColVector b, 
            XMatrix C, XColVector d) {

            Debug.Assert(A.getNumOfCols() == C.getNumOfCols());
            Debug.Assert(A.getNumOfRows() == b.getLength());
            Debug.Assert(C.getNumOfRows() == d.getLength());
            
            // ATA=AT*A: (nxm)x(mxn)=(nxn)
            XMatrix AT = A.calcTranspose();
            XMatrix ATA = AT * A;

            // IATA=inverse of ATA: (nxn)
            XMatrix IATA = ATA.calcInverse();

            // tM1=C*Inv(AT*A): (rxn)(nxn)=(rxn)
            XMatrix tM1 = C * IATA;

            // Mg=C*Inv(AT*A)*CT=tM1*CT: (rxn)(nxn)(nxr)=(rxr)
            XMatrix CT = C.calcTranspose();
            XMatrix Mg = tM1 * CT;

            // tM2=C*Inv(AT*A)*AT=tM1*AT: (rxn)(nxn)(nxm)=(rxm)
            XMatrix tM2 = tM1 * AT;

            // vg=C*Inv(AT*A)*AT=tM1*AT*b-d=tM2*b-d: (rxm)(mx1)=(rx1)
            XColVector vg = tM2 * b - d;

            // solve Mg*g=vg for g; {C*Inv(AT*A)*CT}*g=C*Inv(AT*A)*AT*b-d
            XColVector g = Mg.solveLinearEqnWith(vg);

            // x=Inv(AT*A)*(AT*b-CT*g)=IATA*tV1.
            XColVector tV1 = AT * b - CT * g;
            XColVector x = IATA * tV1;

            return x;
        }
    }
}