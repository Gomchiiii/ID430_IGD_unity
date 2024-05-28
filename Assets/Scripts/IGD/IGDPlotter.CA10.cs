using System.Collections.Generic;
using UnityEngine;
using XAppObject;
using XGeom.NURBS;

// Write the code for the assignments in this file.
// Make sure that each task becomes a separate method. Test the methods by
// calling them from the Start method of the IGDApp class.
namespace IGD {
    public static partial class IGDPlotter {
        // Create a class for non-rational Bézier surfaces. Implement an
        // algorithm to calculate a point S(u0,v0) on a surface given parameters
        // (u0,v0), using the algorithm for calculating a point on non-rational
        // Bézier curves. Draw surfaces of various degrees with their control
        // points and control nets. Refer to [N039] A1.7.
        public static void CA10_1() {
            /*
             * Implement here.
             */
        }
        
        // Create a class for non-rational B-spline surfaces. Implement an
        // algorithm to calculate a point S(u0,v0) on a surface given parameters
        // (u0,v0), using the algorithm for calculating a point on non-rational
        // B-spline curves. Draw B-spline surfaces of various degrees with their
        // control points and control nets. Refer to [N103] A3.5.
        public static void CA10_2() {
            /*
             * Implement here.
             */
        }
        
        // Implement an algorithm to calculate the derivative at a point S(u0,v0)
        // on a non-rational B-spline surface given parameters (u0,v0). Draw the
        // derivative vectors on B-spline surfaces of various degrees along with
        // their control points and control nets. Refer to [N111] A3.6 and
        // [N112] figures.
        public static void CA10_3() {
            /*
             * Implement here.
             */
        }
        
        // Implement an algorithm to calculate the (k,l)-th order derivative
        // surface of a non-rational B-spline surface (0<=k+l<=d). Draw (k,l)-th
        // order derivative surfaces for B-spline surfaces of various degrees
        // along with their control points and control nets. Refer to [N114]
        // A3.7.
        public static void CA10_4() {
            /*
             * Implement here.
             */
        }
        
        // Represent a NURBS surface as a non-rational B-spline surface and
        // implement an algorithm to calculate a point on a NURBS surface given
        // parameters (u,v). Draw NURBS surfaces of various degrees along with
        // their control points and control nets. Refer to [N134] A4.3.
        public static void CA10_5() {
            /*
             * Implement here.
             */
        }
        
        // Implement code to calculate the derivatives of a NURBS surface from
        // a non-rational B-spline surface with 4D control points. Draw the
        // first and second derivative vectors at various points on an arbitrary
        // NURBS surface along with the surface itself (including control points
        // and the control net). Refer to [N137] A4.4.
        public static void CA10_6() {
            /*
             * Implement here.
             */
        }
    }
}