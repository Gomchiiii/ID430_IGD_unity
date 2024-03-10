using UnityEngine;
using X;

namespace IGD {
    public class IGDApp : XApp {
        // fields
        private IGD3DCameraPerson m3DCameraPerson = null;
        public IGD3DCameraPerson get3DCameraPerson() {
            return this.m3DCameraPerson;
        }
        private IGD2DCameraPerson m2DCameraPerson = null;
        public IGD2DCameraPerson get2DCameraPerson() {
            return this.m2DCameraPerson;
        }
        private XScreen mScreen = null;
        
        private IGDPenMarkMgr mPenMarkMgr = null;
        public IGDPenMarkMgr getPenMarkMgr() {
            return this.mPenMarkMgr;
        }

        private XScenarioMgr mScenarioMgr = null;
        public override XScenarioMgr getScenarioMgr() {
            return this.mScenarioMgr;
        }
        private XLogMgr mLogMgr = null;
        public override XLogMgr getLogMgr() {
            return this.mLogMgr;
        }
        private IGDKeyEventSource mKeyEventSource = null;
        private IGDMouseEventSource mMouseEventSource = null;
        private IGDEventListener mEventListener = null;
        private IGDCursor2D mCursor = null;
        public IGDCursor2D getCursor() {
            return this.mCursor;
        }

        void Start() {
            // Unity physics options
            Physics.queriesHitBackfaces = true;

            this.m3DCameraPerson = new IGD3DCameraPerson();
            this.m2DCameraPerson = new IGD2DCameraPerson();
            IGDPlotter.set2DCameraPerson(this.m2DCameraPerson);
            IGDPlotter.set3DCameraPerson(this.m3DCameraPerson);
            IGDPlotter.set2DConstants();
            this.mScreen = new XScreen();

            this.mPenMarkMgr = new IGDPenMarkMgr();
            
            this.mScenarioMgr = new IGDScenarioMgr(this);
            this.mLogMgr = new XLogMgr();
            this.mLogMgr.setPrintOn(true);

            this.mKeyEventSource = new IGDKeyEventSource();
            this.mMouseEventSource = new IGDMouseEventSource();
            this.mEventListener = new IGDEventListener(this);
            this.mCursor = new IGDCursor2D(this);

            this.mKeyEventSource.setEventListener(this.mEventListener);
            this.mMouseEventSource.setEventListener(this.mEventListener);

            // Call the methods you write in IGDPlotter.HW.cs here.

            Vector4[] cps = new Vector4[8];
            cps[0] = new Vector4(1f, 0f, 0f, 0f);
            cps[1] = new Vector4(1f, 1f, 0f, 1f);
            cps[2] = new Vector4(1f, 1f, 0.3f, 1f);
            cps[3] = new Vector4(1f, 1f, 0.6f, 1f);
            cps[4] = new Vector4(0f, 1f, 1f, 1f);
            cps[5] = new Vector4(0.3f, 1f, 1f, 1f);
            cps[6] = new Vector4(0.7f, 0.7f, 1f, 1f);
            cps[7] = new Vector4(1f, 0f, 0f, 0f);

            IGDPlotter.CA2_3(7, cps);
        }
            
        void Update() {
            this.m2DCameraPerson.update();
            this.mKeyEventSource.update();
            this.mMouseEventSource.update();
            IGDPlotter.updateImageOrientation();
        }
    }
}