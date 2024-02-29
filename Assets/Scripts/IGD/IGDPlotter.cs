using XAppObject;
using UnityEngine;
using System.Collections.Generic;
using XPlot;

namespace IGD {
    public partial class IGDPlotter {
        // constants
        public static readonly float DEFAULT_LINE_WIDTH_3D = 0.01f;
        public static readonly float DEFAULT_ARROW_WIDTH_3D = 0.03f;
        public static readonly float DEFAULT_ARROW_LENGTH_3D = 0.08f;
        public static readonly float DEFAULT_DASH_LENGTH_3D = 0.03f;
        public static readonly float DEFAULT_DOT_SIZE_3D = 0.02f;
        
        public readonly float DEFAULT_LINE_WIDTH_2D;
        public readonly float DEFAULT_ARROW_WIDTH_2D;
        public readonly float DEFAULT_ARROW_LENGTH_2D;
        public readonly float DEFAULT_DASH_LENGTH_2D;
        public readonly float DEFAULT_DOT_SIZE_2D;
        
        public static readonly Color DEFAULT_LINE_COLOR = Color.black;
        
        // fields
        private IGD3DCameraPerson m3DCameraPerson = null;
        private IGD2DCameraPerson m2DCameraPerson = null;
        private List<XPlotImage3D> mImage3Ds = new List<XPlotImage3D>();
        
        // constructor
        public IGDPlotter(IGDApp app) {
            this.m3DCameraPerson = app.get3DCameraPerson();
            this.m2DCameraPerson = app.get2DCameraPerson();

            // Set default values for 2D drawing so that the apparent size of
            // 2D and 3D lines are the same.
            float orthoSize3D =
                this.m3DCameraPerson.getCamera().orthographicSize;
            float orthoSize2D =
                this.m2DCameraPerson.getCamera().orthographicSize;
            float scale = orthoSize2D / orthoSize3D;
            DEFAULT_LINE_WIDTH_2D = DEFAULT_LINE_WIDTH_3D * scale;
            DEFAULT_ARROW_WIDTH_2D = DEFAULT_ARROW_WIDTH_3D * scale;
            DEFAULT_ARROW_LENGTH_2D = DEFAULT_ARROW_LENGTH_3D * scale;
            DEFAULT_DASH_LENGTH_2D = DEFAULT_DASH_LENGTH_3D * scale;
            DEFAULT_DOT_SIZE_2D = DEFAULT_DOT_SIZE_3D * scale;
        }
        
        // helper methods for draw XPlot components
        // Line
        public void drawLine3D(Vector3 start, Vector3 end, float scale = 1f) {
            XAppLine3D line = new XAppLine3D("Line3D", start, end,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawLine3D(Vector3 start, Vector3 end, float scale,
            Color color) {
            XAppLine3D line = new XAppLine3D("Line3D", start, end,
                DEFAULT_LINE_WIDTH_3D * scale, color);
        }
        public void drawLine2D(Vector2 start, Vector2 end, float scale = 1f) {
            XAppLine2D line = new XAppLine2D("Line2D", start, end,
                DEFAULT_LINE_WIDTH_2D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawLine2D(Vector2 start, Vector2 end, float scale,
            Color color) {
            XAppLine2D line = new XAppLine2D("Line2D", start, end,
                DEFAULT_LINE_WIDTH_2D * scale, color);
        }
        
        // Polyline
        public void drawPolyline3D(List<Vector3> pts, float scale = 1f) {
            XAppPolyline3D polyline = new XAppPolyline3D("Polyline3D", pts,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawPolyline3D(List<Vector3> pts, float scale, Color color) {
            XAppPolyline3D polyline = new XAppPolyline3D("Polyline3D", pts,
                DEFAULT_LINE_WIDTH_3D * scale, color);
        }
        public void drawPolyline2D(List<Vector2> pts, float scale = 1f) {
            XAppPolyline2D polyline = new XAppPolyline2D("Polyline2D", pts,
                DEFAULT_LINE_WIDTH_2D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawPolyline2D(List<Vector2> pts, float scale, Color color) {
            XAppPolyline2D polyline = new XAppPolyline2D("Polyline2D", pts,
                DEFAULT_LINE_WIDTH_2D * scale, color);
        }
        
        // Arrow
        public void drawArrow3D(Vector3 start, Vector3 end, float scale = 1f) {
            XPlotArrow3D arrow = new XPlotArrow3D("Arrow3D", start, end,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_ARROW_LENGTH_3D * scale,
                DEFAULT_ARROW_WIDTH_3D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawArrow3D(Vector3 start, Vector3 end, float scale,
            Color color) {
            XPlotArrow3D arrow = new XPlotArrow3D("Arrow3D", start, end,
                DEFAULT_LINE_WIDTH_3D * scale, DEFAULT_ARROW_LENGTH_3D * scale,
                DEFAULT_ARROW_WIDTH_3D * scale, color);
        }
        public void drawArrow2D(Vector2 start, Vector2 end, float scale = 1f) {
            XPlotArrow2D arrow = new XPlotArrow2D("Arrow2D", start, end,
                DEFAULT_LINE_WIDTH_2D * scale, DEFAULT_ARROW_LENGTH_2D * scale,
                DEFAULT_ARROW_WIDTH_2D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawArrow2D(Vector2 start, Vector2 end, float scale,
            Color color) {
            XPlotArrow2D arrow = new XPlotArrow2D("Arrow2D", start, end,
                DEFAULT_LINE_WIDTH_2D * scale, DEFAULT_ARROW_LENGTH_2D * scale,
                DEFAULT_ARROW_WIDTH_2D * scale, color);
        }
        
        // Dashed line
        public void drawDashedLine3D(Vector3 start, Vector3 end, float scale = 1f) {
            XPlotDashedLine3D dashedLine = new XPlotDashedLine3D("DashedLine3D",
                start, end, DEFAULT_LINE_WIDTH_3D * scale,
                DEFAULT_DASH_LENGTH_3D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawDashedLine3D(Vector3 start, Vector3 end, float scale,
            Color color) {
            XPlotDashedLine3D dashedLine = new XPlotDashedLine3D("DashedLine3D",
                start, end, DEFAULT_LINE_WIDTH_3D * scale,
                DEFAULT_DASH_LENGTH_3D * scale, color);
        }
        public void drawDashedLine2D(Vector2 start, Vector2 end, float scale = 1f) {
            XPlotDashedLine2D dashedLine = new XPlotDashedLine2D("DashedLine2D",
                start, end, DEFAULT_LINE_WIDTH_2D * scale,
                DEFAULT_DASH_LENGTH_2D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawDashedLine2D(Vector2 start, Vector2 end, float scale,
            Color color) {
            XPlotDashedLine2D dashedLine = new XPlotDashedLine2D("DashedLine2D",
                start, end, DEFAULT_LINE_WIDTH_2D * scale,
                DEFAULT_DASH_LENGTH_2D * scale, color);
        }
        
        // Dashed polyline
        public void drawDashedPolyline3D(List<Vector3> pts, float scale = 1f) {
            XPlotDashedPolyline3D dashedPolyline = new XPlotDashedPolyline3D(
                "DashedPolyline3D", pts, DEFAULT_LINE_WIDTH_3D * scale,
                DEFAULT_DASH_LENGTH_3D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawDashedPolyline3D(List<Vector3> pts, float scale,
            Color color) {
            XPlotDashedPolyline3D dashedPolyline = new XPlotDashedPolyline3D(
                "DashedPolyline3D", pts, DEFAULT_LINE_WIDTH_3D * scale,
                DEFAULT_DASH_LENGTH_3D * scale, color);
        }
        public void drawDashedPolyline2D(List<Vector2> pts, float scale = 1f) {
            XPlotDashedPolyline2D dashedPolyline = new XPlotDashedPolyline2D(
                "DashedPolyline2D", pts, DEFAULT_LINE_WIDTH_2D * scale,
                DEFAULT_DASH_LENGTH_2D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawDashedPolyline2D(List<Vector2> pts, float scale,
            Color color) {
            XPlotDashedPolyline2D dashedPolyline = new XPlotDashedPolyline2D(
                "DashedPolyline2D", pts, DEFAULT_LINE_WIDTH_2D * scale,
                DEFAULT_DASH_LENGTH_2D * scale, color);
        }
        
        // Dot
        public void drawDot3D(Vector3 pos, float scale = 1f) {
            XPlotDot3D dot = new XPlotDot3D("Dot3D", pos,
                DEFAULT_DOT_SIZE_3D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawDot3D(Vector3 pos, float scale, Color color) {
            XPlotDot3D dot = new XPlotDot3D("Dot3D", pos,
                DEFAULT_DOT_SIZE_3D * scale, color);
        }
        public void drawDot2D(Vector2 pos, float scale = 1f) {
            XPlotDot2D dot = new XPlotDot2D("Dot2D", pos,
                DEFAULT_DOT_SIZE_2D * scale, DEFAULT_LINE_COLOR);
        }
        public void drawDot2D(Vector2 pos, float scale, Color color) {
            XPlotDot2D dot = new XPlotDot2D("Dot2D", pos,
                DEFAULT_DOT_SIZE_2D * scale, color);
        }
        
        // Image
        public XPlotImage3D addImage3D(string path, Vector3 pos, float scale = 1f) {
            XPlotImage3D image3D = new XPlotImage3D(path, path, pos);
            image3D.setScale(scale);
            this.mImage3Ds.Add(image3D);
            return image3D;
        }
        public XPlotImage2D addImage2D(string path, Vector2 pos, float scale = 1f) {
            XPlotImage2D image2D = new XPlotImage2D(path, path, pos);
            image2D.setScale(scale);
            return image2D;
        }
        public void updateImageOrientation() {
            foreach (XPlotImage3D image3D in this.mImage3Ds) {
                image3D.setLocalRotation(
                    Quaternion.LookRotation(this.m3DCameraPerson.getView()));
            }
        }
    }
}