using UnityEngine;
using XAppObject;
using XGeom;

namespace XPlot {
    public class XPlotArrow2D : XAppNoGeom2D {
        // nested classes for arrow head
        private class XPlotArrowHead2D : XAppLine2D {
            // constructor
            public XPlotArrowHead2D(string name, Vector2 start, Vector2 end, 
                float width, Color color) : base($"{name}/ArrowHead3D", 
                start, end, width, color) { }
            
            // methods
            protected override void addComponents() {
                this.mGameObject.AddComponent<LineRenderer>();
            }
            
            protected override void refreshRenderer() {
                XLine2D line = (XLine2D)this.mGeom;
                Vector3 start = line.getPt0();
                Vector3 end = line.getPt1();
                
                LineRenderer lr = this.mGameObject.GetComponent<LineRenderer>();
                lr.useWorldSpace = false;
                lr.alignment = LineAlignment.View; // lines face the camera.
                lr.positionCount = 2;
                lr.SetPosition(0, start);
                lr.SetPosition(1, end);
                lr.startWidth = this.getWidth();
                lr.endWidth = 0f;
                lr.material = new Material(Shader.Find("Unlit/Color"));
                lr.material.color = this.getColor();
            }
            
            protected override void refreshCollider() { }
        }
        
        // fields
        private XAppLine2D mBody;
        private XPlotArrowHead2D mHead;
        
        // constructor
        public XPlotArrow2D(string name, Vector2 start, Vector2 end, 
            float width, float headLength, float headWidth, Color color) :
            base($"{name}/Arrow2D") {
            Vector2 bodyStart = start;
            Vector2 bodyEnd = end - headLength * (end - start).normalized;
            Vector2 headStart = bodyEnd;
            Vector2 headEnd = end;
            
            this.mBody = new XAppLine2D($"{name}/ArrowBody", 
                bodyStart, bodyEnd, width, color);
            this.addChild(this.mBody);
            
            this.mHead = new XPlotArrowHead2D($"{name}/ArrowHead", 
                headStart, headEnd, headWidth, color);
            this.addChild(this.mHead);
        }
    }
}