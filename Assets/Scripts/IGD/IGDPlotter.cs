using XAppObject;
using UnityEngine;
using System.Collections.Generic;
using XPlot;

namespace IGD {
    public static partial class IGDPlotter {
        // constants
        public static readonly float DEFAULT_LINE_WIDTH_3D = 0.01f;
        public static readonly float DEFAULT_ARROW_WIDTH_3D = 0.03f;
        public static readonly float DEFAULT_ARROW_LENGTH_3D = 0.08f;
        public static readonly float DEFAULT_DASH_LENGTH_3D = 0.03f;
        public static readonly float DEFAULT_DOT_SIZE_3D = 0.02f;
        public static readonly Color DEFAULT_LINE_COLOR = Color.black;
        
        // fields
        public static float mDefaultLineWidth2D;
        public static float mDefaultArrowWidth2D;
        public static float mDefaultArrowLength2D;
        public static float mDefaultDashLength2D;
        public static float mDefaultDotSize2D;
        
        private static IGD3DCameraPerson m3DCameraPerson = null;
        private static IGD2DCameraPerson m2DCameraPerson = null;
        private static List<XAppObject2D> mObject2Ds = new List<XAppObject2D>();
        private static List<XAppObject3D> mObject3Ds = new List<XAppObject3D>();
        private static List<XPlotImage2D> mImage2Ds = new List<XPlotImage2D>();
        private static List<XPlotImage3D> mImage3Ds = new List<XPlotImage3D>();
        
        // config methods
        public static void set3DCameraPerson(IGD3DCameraPerson cameraPerson) {
            IGDPlotter.m3DCameraPerson = cameraPerson;
        }
        public static void set2DCameraPerson(IGD2DCameraPerson cameraPerson) {
            IGDPlotter.m2DCameraPerson = cameraPerson;
        }
        
        // Set default values for 2D drawing so that the apparent size of
        // 2D and 3D lines are the same.
        public static void set2DConstants() {
            if (IGDPlotter.m3DCameraPerson == null ||
                IGDPlotter.m2DCameraPerson == null) {
                Debug.LogWarning(
                    "3D and 2D cameras must bet set before setting 2D constants.");
                return;
            }
            
            float orthoSize3D =
                IGDPlotter.m3DCameraPerson.getCamera().orthographicSize;
            float orthoSize2D =
                IGDPlotter.m2DCameraPerson.getCamera().orthographicSize;
            float scale = orthoSize2D / orthoSize3D;
            mDefaultLineWidth2D = DEFAULT_LINE_WIDTH_3D * scale;
            mDefaultArrowWidth2D = DEFAULT_ARROW_WIDTH_3D * scale;
            mDefaultArrowLength2D = DEFAULT_ARROW_LENGTH_3D * scale;
            mDefaultDashLength2D = DEFAULT_DASH_LENGTH_3D * scale;
            mDefaultDotSize2D = DEFAULT_DOT_SIZE_3D * scale;
        }

        // helper methods for draw XPlot components
        // Line
        public static void drawLine3D(Vector3 start, Vector3 end,
            float scale = 1f) {
            XAppLine3D line = new("Line3D", start, end,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_LINE_COLOR);
            IGDPlotter.mObject3Ds.Add(line);
        }
        public static void drawLine3D(Vector3 start, Vector3 end, float scale,
            Color color) {
            XAppLine3D line = new("Line3D", start, end,
                DEFAULT_LINE_WIDTH_3D * scale, color);
            IGDPlotter.mObject3Ds.Add(line);
        }
        public static void drawLine2D(Vector2 start, Vector2 end,
            float scale = 1f) {
            XAppLine2D line = new("Line2D", start, end,
                mDefaultLineWidth2D * scale, DEFAULT_LINE_COLOR);
            IGDPlotter.mObject2Ds.Add(line);
        }
        public static void drawLine2D(Vector2 start, Vector2 end, float scale,
            Color color) {
            XAppLine2D line = new("Line2D", start, end,
                mDefaultLineWidth2D * scale, color);
            IGDPlotter.mObject2Ds.Add(line);
        }

        // Polyline
        public static void drawPolyline3D(List<Vector3> pts, float scale = 1f) {
            XAppPolyline3D polyline = new("Polyline3D", pts,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_LINE_COLOR);
            IGDPlotter.mObject3Ds.Add(polyline);
        }
        public static void drawPolyline3D(List<Vector3> pts, float scale,
            Color color) {
            XAppPolyline3D polyline = new("Polyline3D", pts,
                DEFAULT_LINE_WIDTH_3D * scale, color);
            IGDPlotter.mObject3Ds.Add(polyline);
        }
        public static void drawPolyline2D(List<Vector2> pts, float scale = 1f) {
            XAppPolyline2D polyline = new("Polyline2D", pts,
                mDefaultLineWidth2D * scale, DEFAULT_LINE_COLOR);
            IGDPlotter.mObject2Ds.Add(polyline);
        }
        public static void drawPolyline2D(List<Vector2> pts, float scale,
            Color color) {
            XAppPolyline2D polyline = new("Polyline2D", pts,
                mDefaultLineWidth2D * scale, color);
            IGDPlotter.mObject2Ds.Add(polyline);
        }

        // Arrow
        public static void drawArrow3D(Vector3 start, Vector3 end,
            float scale = 1f) {
            XPlotArrow3D arrow = new("Arrow3D", start, end,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_ARROW_LENGTH_3D * scale,
                DEFAULT_ARROW_WIDTH_3D * scale, DEFAULT_LINE_COLOR);
            IGDPlotter.mObject3Ds.Add(arrow);
        }
        public static void drawArrow3D(Vector3 start, Vector3 end, float scale,
            Color color) {
            XPlotArrow3D arrow = new("Arrow3D", start, end,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_ARROW_LENGTH_3D * scale,
                DEFAULT_ARROW_WIDTH_3D * scale, color);
            IGDPlotter.mObject3Ds.Add(arrow);
        }
        public static void drawArrow2D(Vector2 start, Vector2 end,
            float scale = 1f) {
            XPlotArrow2D arrow = new("Arrow2D", start, end,
                mDefaultLineWidth2D * scale, mDefaultArrowLength2D * scale,
                mDefaultArrowWidth2D * scale, DEFAULT_LINE_COLOR);
            IGDPlotter.mObject2Ds.Add(arrow);
        }
        public static void drawArrow2D(Vector2 start, Vector2 end, float scale,
            Color color) {
            XPlotArrow2D arrow = new("Arrow2D", start, end,
                mDefaultLineWidth2D * scale, mDefaultArrowLength2D * scale,
                mDefaultArrowWidth2D * scale, color);
            IGDPlotter.mObject2Ds.Add(arrow);
        }

        // Dashed line
        public static void drawDashedLine3D(Vector3 start, Vector3 end,
            float scale = 1f) {
            XPlotDashedLine3D dashedLine = new("DashedLine3D", start, end,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_DASH_LENGTH_3D * scale,
                DEFAULT_LINE_COLOR);
            IGDPlotter.mObject3Ds.Add(dashedLine);
        }
        public static void drawDashedLine3D(Vector3 start, Vector3 end,
            float scale, Color color) {
            XPlotDashedLine3D dashedLine = new("DashedLine3D", start, end,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_DASH_LENGTH_3D * scale,
                color);
            IGDPlotter.mObject3Ds.Add(dashedLine);
        }
        public static void drawDashedLine2D(Vector2 start, Vector2 end,
            float scale = 1f) {
            XPlotDashedLine2D dashedLine = new("DashedLine2D", start, end,
                mDefaultLineWidth2D * scale, mDefaultDashLength2D * scale,
                DEFAULT_LINE_COLOR);
            IGDPlotter.mObject2Ds.Add(dashedLine);
        }
        public static void drawDashedLine2D(Vector2 start, Vector2 end,
            float scale, Color color) {
            XPlotDashedLine2D dashedLine = new("DashedLine2D", start, end,
                mDefaultLineWidth2D * scale, mDefaultDashLength2D * scale,
                color);
            IGDPlotter.mObject2Ds.Add(dashedLine);
        }

        // Dashed polyline
        public static void drawDashedPolyline3D(List<Vector3> pts,
            float scale = 1f) {
            XPlotDashedPolyline3D dashedPolyline = new("DashedPolyline3D", pts,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_DASH_LENGTH_3D * scale,
                DEFAULT_LINE_COLOR);
            IGDPlotter.mObject3Ds.Add(dashedPolyline);
        }
        public static void drawDashedPolyline3D(List<Vector3> pts, float scale,
            Color color) {
            XPlotDashedPolyline3D dashedPolyline = new("DashedPolyline3D", pts,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_DASH_LENGTH_3D * scale,
                color);
            IGDPlotter.mObject3Ds.Add(dashedPolyline);
        }
        public static void drawDashedPolyline2D(List<Vector2> pts,
            float scale = 1f) {
            XPlotDashedPolyline2D dashedPolyline = new("DashedPolyline2D", pts,
                mDefaultLineWidth2D * scale, mDefaultDashLength2D * scale,
                DEFAULT_LINE_COLOR);
            IGDPlotter.mObject2Ds.Add(dashedPolyline);
        }
        public static void drawDashedPolyline2D(List<Vector2> pts, float scale,
            Color color) {
            XPlotDashedPolyline2D dashedPolyline = new("DashedPolyline2D", pts,
                mDefaultLineWidth2D * scale, mDefaultDashLength2D * scale,
                color);
            IGDPlotter.mObject2Ds.Add(dashedPolyline);
        }

        // Dot
        public static void drawDot3D(Vector3 pos, float scale = 1f) {
            XPlotDot3D dot = new("Dot3D", pos, DEFAULT_DOT_SIZE_3D * scale,
                DEFAULT_LINE_COLOR);
            IGDPlotter.mObject3Ds.Add(dot);
        }
        public static void drawDot3D(Vector3 pos, float scale, Color color) {
            XPlotDot3D dot = new("Dot3D", pos, DEFAULT_DOT_SIZE_3D * scale,
                color);
            IGDPlotter.mObject3Ds.Add(dot);
        }
        public static void drawDot2D(Vector2 pos, float scale = 1f) {
            XPlotDot2D dot = new("Dot2D", pos, mDefaultDotSize2D * scale,
                DEFAULT_LINE_COLOR);
            IGDPlotter.mObject2Ds.Add(dot);
        }
        public static void drawDot2D(Vector2 pos, float scale, Color color) {
            XPlotDot2D dot = new("Dot2D", pos, mDefaultDotSize2D * scale,
                color);
            IGDPlotter.mObject2Ds.Add(dot);
        }

        // Image
        public static void addImage3D(string path, Vector3 pos,
            float scale = 1f) {
            XPlotImage3D image3D = new(path, path, pos);
            image3D.setScale(scale);
            IGDPlotter.mImage3Ds.Add(image3D);
        }
        public static void addImage3D(string path, Vector3 pos,
            float scale, Color color) {
            XPlotImage3D image3D = new(path, path, pos);
            image3D.setTintColor(color);
            image3D.setScale(scale);
            IGDPlotter.mImage3Ds.Add(image3D);
        }
        public static void addImage2D(string path, Vector2 pos,
            float scale = 1f) {
            XPlotImage2D image2D = new(path, path, pos);
            image2D.setScale(scale);
            IGDPlotter.mImage2Ds.Add(image2D);
        }
        public static void addImage2D(string path, Vector2 pos,
            float scale, Color color) {
            XPlotImage2D image2D = new(path, path, pos);
            image2D.setTintColor(color);
            image2D.setScale(scale);
            IGDPlotter.mImage2Ds.Add(image2D);
        }
        public static void updateImageOrientation() {
            foreach (XPlotImage3D image3D in mImage3Ds) {
                image3D.setLocalRotation(
                    Quaternion.LookRotation(m3DCameraPerson.getView()));
            }
        }
    }
}
