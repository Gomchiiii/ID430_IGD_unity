using System.Collections.Generic;
using UnityEngine;
using XAppObject;
using XGeom;
using XGeom.NURBS;
using XPlot;

namespace IGD {
    public static partial class IGDPlotter {
        private static IGDApp mApp = null;
        public static void setApp(IGDApp app) {
            IGDPlotter.mApp = app;
        }

        // CAUTION: Write label in LaTex format.
        public static void draw2DAxes(float xStart, float xEnd, float yStart,
            float yEnd, float lineScale, Color color, string xLabel,
            string yLabel, float labelScale) {
            Vector2 xAxisStart = new Vector2(xStart, 0f);
            Vector2 xAxisEnd = new Vector2(xEnd, 0f);
            IGDPlotter.drawArrow2D(xAxisStart, xAxisEnd, lineScale, color);
            
            Vector2 yAxisStart = new Vector2(0f, yStart);
            Vector2 yAxisEnd = new Vector2(0f, yEnd);
            IGDPlotter.drawArrow2D(yAxisStart, yAxisEnd, lineScale, color);

            float labelOffset = 50f * labelScale;
            if (xLabel != null) {
                Vector2 xLabelPos = new Vector2(xEnd, -labelOffset);
                IGDPlotter.writeFormula2D(xLabel, xLabelPos, labelScale, color);
            }
            if (yLabel != null) {
                Vector2 yLabelPos =
                    new Vector2(-labelOffset, yEnd + labelOffset);
                IGDPlotter.writeFormula2D(yLabel, yLabelPos, labelScale, color);
            }
        }
        public static void draw2DAxes(float xLength, float yLength,
            float lineScale, Color color, string xLabel = null,
            string yLabel = null, float labelScale = 0.8f) {
            IGDPlotter.draw2DAxes(0f, xLength, 0f, yLength, lineScale, color,
                xLabel, yLabel, labelScale);
        }
        public static void draw2DAxes(float xLength, float yLength,
            string xLabel = null, string yLabel = null,
            float labelScale = 0.8f) {
            IGDPlotter.draw2DAxes(0f, xLength, 0f, yLength, 0.8f, Color.black,
                xLabel, yLabel, labelScale);
        }
        public static void draw2DAxes(float length, string xLabel = null,
            string yLabel = null, float labelScale = 0.8f) {
            IGDPlotter.draw2DAxes(0f, length, 0f, length, 0.8f, Color.black,
                xLabel, yLabel, labelScale);
        }
        
        // CAUTION: Write label in LaTex format.
        public static void draw3DAxes(float xStart, float xEnd, float yStart,
            float yEnd, float zStart, float zEnd, float lineScale, Color color,
            string xLabel, string yLabel, string zLabel, float labelScale) {
            Vector3 xAxisStart = new Vector3(xStart, 0f, 0f);
            Vector3 xAxisEnd = new Vector3(xEnd, 0f, 0f);
            IGDPlotter.drawArrow3D(xAxisStart, xAxisEnd, lineScale, color);
            
            Vector3 yAxisStart = new Vector3(0f, yStart, 0f);
            Vector3 yAxisEnd = new Vector3(0f, yEnd, 0f);
            IGDPlotter.drawArrow3D(yAxisStart, yAxisEnd, lineScale, color);
            
            Vector3 zAxisStart = new Vector3(0f, 0f, zStart);
            Vector3 zAxisEnd = new Vector3(0f, 0f, zEnd);
            IGDPlotter.drawArrow3D(zAxisStart, zAxisEnd, lineScale, color);
            
            float labelOffset = 0.08f * labelScale;
            if (xLabel != null) {
                Vector3 xLabelPos = new Vector3(xEnd + labelOffset, 0f, 0f);
                IGDPlotter.writeFormula3D(xLabel, xLabelPos, labelScale, color);
            }
            if (yLabel != null) {
                Vector3 yLabelPos = new Vector3(0f, yEnd + labelOffset, 0f);
                IGDPlotter.writeFormula3D(yLabel, yLabelPos, labelScale, color);
            }
            if (zLabel != null) {
                Vector3 zLabelPos = new Vector3(0f, 0f, zEnd + labelOffset);
                IGDPlotter.writeFormula3D(zLabel, zLabelPos, labelScale, color);
            }
        }
        public static void draw3DAxes(float xLength, float yLength,
            float zLength, float lineScale, Color color, string xLabel = null,
            string yLabel = null, string zLabel = null, float labelScale = 0.8f) {
            IGDPlotter.draw3DAxes(0f, xLength, 0f, yLength, 0f, zLength,
                lineScale, color, xLabel, yLabel, zLabel, labelScale);
        }
        public static void draw3DAxes(float xLength, float yLength,
            float zLength, string xLabel = null, string yLabel = null,
            string zLabel = null, float labelScale = 0.8f) {
            IGDPlotter.draw3DAxes(0f, xLength, 0f, yLength, 0f, zLength, 0.8f,
                Color.black, xLabel, yLabel, zLabel, labelScale);
        }
        public static void draw3DAxes(float length, string xLabel = null,
            string yLabel = null, string zLabel = null, float labelScale = 0.8f) {
            IGDPlotter.draw3DAxes(0f, length, 0f, length, 0f, length, 0.8f,
                Color.black, xLabel, yLabel, zLabel, labelScale);
        }

        public static void drawParametricCurve3D(XParametricCurve3D curve,
            double startParam, double endParam, int numSegments, float lineScale,
            Color color) {
            List<Vector3> pts = new List<Vector3>();
            double delta = (endParam - startParam) / numSegments;
            for (int i = 0; i <= numSegments; i++) {
                double u = startParam + i * delta;
                pts.Add(curve.calcPos(u));
            }
            IGDPlotter.drawPolyline3D(pts, lineScale, color);
        }
        public static void drawParametricCurve3D(XParametricCurve3D curve,
            int numSegments, float lineScale, Color color) {
            double startParam = curve.getStartParam();
            double endParam = curve.getEndParam();
            IGDPlotter.drawParametricCurve3D(curve, startParam, endParam,
                numSegments, lineScale, color);
        }
        
        public static void drawControlPoints(Vector4[] cps, float dotScale,
            Color color) {
            foreach (Vector4 cp in cps) {
                Vector3 pt = XCPsUtil.perspectiveMap(cp);
                IGDPlotter.drawDot3D(pt, dotScale, color);
            }
        }

        public static void labelControlPoints(Vector4[] cps, float scale,
            Color color, string prefix, Vector3 offset) {
            for (int i = 0; i < cps.Length; i++) {
                Vector3 pos = XCPsUtil.perspectiveMap(cps[i]) + offset;
                IGDPlotter.writeFormula3D($"{prefix}_{{{i}}}", pos, scale, color);
			}
		}
        
        public static void drawControlPolygon(Vector4[] cps, float lineScale,
            Color color) {
            List<Vector3> pts = new List<Vector3>();
            foreach (Vector4 cp in cps) {
                pts.Add(XCPsUtil.perspectiveMap(cp));
            }
            IGDPlotter.drawDashedPolyline3D(pts, lineScale, color);
        }

        public static void drawKnotVector(double[] U, float lineLength,
            float tickLength, float tickInterval, float lineScale,
            float labelScale, Color color, bool labelKnots) {
            Vector2 lineStart = Vector2.zero;
            Vector2 lineEnd = new Vector2(lineLength, 0f);
            IGDPlotter.drawLine2D(lineStart, lineEnd, lineScale, color);
            
            List<double> uniqueKnots = new List<double>();
            List<int> multiplicities = new List<int>();
            
            for (int i = 0; i < U.Length; i++) {
                if (i == 0 || U[i] != U[i - 1]) {
                    uniqueKnots.Add(U[i]);
                    multiplicities.Add(1);
                } else {
                    multiplicities[^1]++;
                }
            }

            double us = U[0];
            double ue = U[U.Length - 1];
            float tickHalfLen = tickLength / 2f;
            for (int i = 0; i < uniqueKnots.Count; i++) {
                double u = uniqueKnots[i];
                float centerX = Mathf.Lerp(0f, lineLength,
                    (float)(u - us) / (float)(ue - us));
                Vector2 center = new Vector2(centerX, 0f);
                int count = multiplicities[i];
                float totInterval = tickInterval * (count - 1);
                float startX = center.x - totInterval / 2;
                for (int j = 0; j < count; j++) {
                    float x = startX + j * tickInterval;
                    Vector2 top = new Vector2(x, tickHalfLen);
                    Vector2 bot = new Vector2(x, -tickHalfLen);
                    IGDPlotter.drawLine2D(top, bot, 0.8f, color);
                }
            }

            if (!labelKnots) {
                return;
            }

            // Label the knots
            for (int i = 0; i < uniqueKnots.Count; i++) {
                double u = uniqueKnots[i];
                float centerX = Mathf.Lerp(0f, lineLength,
                    (float)(u - us) / (float)(ue - us));
                Vector2 labelPos = new Vector2(centerX,
                    -tickHalfLen - 50f * labelScale);
                string label = u.ToString();
                IGDPlotter.writeFormula2D(label, labelPos, labelScale, color);
            }
        }
        public static void drawKnotVector(double[] U, float lineLength,
            float lineScale, float labelScale, Color color) {
            float tickLength = lineLength / 20f;
            float tickInterval = lineLength / 100f;
            IGDPlotter.drawKnotVector(U, lineLength, tickLength, tickInterval,
                lineScale, labelScale, color, true);
        }
        public static void drawKnotVector(double[] U, float lineLength) {
            IGDPlotter.drawKnotVector(U, lineLength, 0.8f, 0.8f, Color.black);
        }
        
        // Surface
        public static XAppBezierSurface3D drawBezierSurface3D(
            XBezierSurface3D bsf, Color color) {
            return new XAppBezierSurface3D("Bezier Surface", bsf, color);
        }
        public static XAppBezierSurface3D drawBezierSurface3D(
            XBezierSurface3D bsf) {
            Color color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            return new XAppBezierSurface3D("Bezier Surface", bsf, color);
        }
        public static XAppBSplineSurface3D drawBSplineSurface3D(
            XBSplineSurface3D bssf, Color color) {
            return new XAppBSplineSurface3D("BSpline Surface", bssf, color);
        }
        public static XAppBSplineSurface3D drawBSplineSurface3D(
            XBSplineSurface3D bssf) {
            Color color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            return new XAppBSplineSurface3D("BSpline Surface", bssf, color);
        }
        
        public static void drawControlPoints(Vector4[,] cps,
            float dotScale, Color color) {
            foreach (Vector4 cp in cps) {
                Vector3 pt = XCPsUtil.perspectiveMap(cp);
                IGDPlotter.drawDot3D(pt, dotScale, color);
            }
        }
        
        public static void labelControlPoints(Vector4[,] cps, float scale, 
            Color color, string prefix, Vector3 offset) {
            for (int i = 0; i < cps.GetLength(0); i++) {
                for (int j = 0; j < cps.GetLength(1); j++) {
                    Vector3 pos = XCPsUtil.perspectiveMap(cps[i, j]) + offset;
                    IGDPlotter.writeFormula3D($"{prefix}_{{{i},{j}}}", pos, scale,
                        color);
                }
            }
        }

        public static void drawControlNet(Vector4[,] cps,
            float lineScale, Color color) {
            for (int i = 0; i < cps.GetLength(0); i++) {
                List<Vector3> pts = new List<Vector3>();
                for (int j = 0; j < cps.GetLength(1); j++) {
                    pts.Add(XCPsUtil.perspectiveMap(cps[i, j]));
                }
                IGDPlotter.drawDashedPolyline3D(pts, lineScale, color);
            }
            
            for (int j = 0; j < cps.GetLength(1); j++) {
                List<Vector3> pts = new List<Vector3>();
                for (int i = 0; i < cps.GetLength(0); i++) {
                    pts.Add(XCPsUtil.perspectiveMap(cps[i, j]));
                }
                IGDPlotter.drawDashedPolyline3D(pts, lineScale, color);
            }
        }
    }
}