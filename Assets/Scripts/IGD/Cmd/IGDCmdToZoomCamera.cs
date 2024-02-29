using System.Text;
using X;
using UnityEngine;

namespace IGD.Cmd {
    public class IGDCmdToZoomCamera : XLoggableCmd {
        // constants
        private static readonly float MIN_ORTHO_SIZE = 0.1f; // max zoom
        
        // fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        // private contructor
        private IGDCmdToZoomCamera(XApp app) : base(app) {
            IGDApp xpApp = (IGDApp) this.mApp;
            IGDPenMark pm = xpApp.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = pm.getRecentPt(1);
            this.mCurPt = pm.getRecentPt(0);
        }
        
        // static method to construct and execute this command
        public static bool execute(XApp app) {
            IGDCmdToZoomCamera cmd = new IGDCmdToZoomCamera(app);
            return cmd.execute();
        }
        
        protected override bool defineCmd() {
            IGDApp app = (IGDApp) this.mApp;
            IGD3DCameraPerson cp = app.get3DCameraPerson();

            float zoomFactor = 1.0f / Screen.height;
            float dy = this.mCurPt.y - this.mPrevPt.y;
            
            float prevOrthoSize = cp.getOrthographicSize();
            float newOrthoSize = prevOrthoSize - zoomFactor * dy;
            if (newOrthoSize < IGDCmdToZoomCamera.MIN_ORTHO_SIZE) {
                newOrthoSize = IGDCmdToZoomCamera.MIN_ORTHO_SIZE;
            }
            cp.setOrthographicSize(newOrthoSize);

            return true;
        }

        protected override string createLog() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            sb.Append(this.mPrevPt).Append("\t");
            sb.Append(this.mCurPt);
            return sb.ToString();
        }    
    }
}