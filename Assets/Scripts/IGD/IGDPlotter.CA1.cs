using UnityEngine;
using XAppObject;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public partial class IGDPlotter {
        // CA1-1: Draw Fig.1 in the assignment document
        public void CA1_1() {
            
            //Draw x, y, z axes.

            float axisLength = 1f;
            //유니티 축방향이랑 반대인거 감안하기
            Vector3 i = Vector3.right;
            Vector3 j = Vector3.forward;
            Vector3 k = Vector3.up;

            Vector3 xAxisEnd = i * axisLength;
            Vector3 yAxisEnd = j * axisLength;
            Vector3 zAxisEnd = k * axisLength;

            this.drawArrow3D(Vector3.zero, xAxisEnd);
            this.drawArrow3D(Vector3.zero, yAxisEnd);
            this.drawArrow3D(Vector3.zero, zAxisEnd);

            // Label x, y, z;
            this.addImage3D("Letters/x", xAxisEnd);

        }
        // CA1-2: Draw Fig.2 in the assignment document
        public void CA1_2() {
            /*
            Impelment here
            */
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