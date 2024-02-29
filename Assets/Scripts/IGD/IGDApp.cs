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
        private IGDPlotter mPlotter = null;
        
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
            this.mPlotter = new IGDPlotter(this);
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
            this.mPlotter.CA1_1();
        }
            
        void Update() {
            this.m2DCameraPerson.update();
            this.mKeyEventSource.update();
            this.mMouseEventSource.update();
            this.mPlotter.updateImageOrientation();
        }
    }
}