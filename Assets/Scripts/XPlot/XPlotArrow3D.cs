using UnityEngine;
using XAppObject;
using XGeom;

namespace XPlot {
    public class XPlotArrow3D : XAppNoGeom3D {
        // nested classes for arrow head
        private class XPlotArrowHead3D : XAppLine3D {
            // constructor
            public XPlotArrowHead3D(string name, Vector3 start, Vector3 end, 
                float width, Color color) : base($"{name}/ArrowHead3D", 
                start, end, width, color) { }
            
            // methods
            protected override void addComponents() {
                this.mGameObject.AddComponent<LineRenderer>();
            }
            
            protected override void refreshRenderer() {
                XLine3D line = (XLine3D)this.mGeom;
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
        private XAppLine3D mBody;
        private XPlotArrowHead3D mHead;
        
        // constructor
        public XPlotArrow3D(string name, Vector3 start, Vector3 end, 
            float width, float headLength, float headWidth, Color color) :
            base($"{name}/Arrow3D") {
            Vector3 bodyStart = start;
            Vector3 bodyEnd = end - headLength * (end - start).normalized;
            Vector3 headStart = bodyEnd;
            Vector3 headEnd = end;
            
            this.mBody = new XAppLine3D($"{name}/ArrowBody", 
                bodyStart, bodyEnd, width, color);
            this.addChild(this.mBody);
            
            this.mHead = new XPlotArrowHead3D($"{name}/ArrowHead", 
                headStart, headEnd, headWidth, color);
            this.addChild(this.mHead);
        }
    }
}