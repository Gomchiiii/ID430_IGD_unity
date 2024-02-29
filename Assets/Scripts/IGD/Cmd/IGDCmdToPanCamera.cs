using System.Text;
using X;
using UnityEngine;

namespace IGD.Cmd {
    public class IGDCmdToPanCamera : XLoggableCmd {
        // fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        // private contructor
        private IGDCmdToPanCamera(XApp app) : base(app) {
            IGDApp xpApp = (IGDApp) this.mApp;
            IGDPenMark pm = xpApp.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = pm.getRecentPt(1);
            this.mCurPt = pm.getRecentPt(0);
        }
        
        // static method to construct and execute this command
        public static bool execute(XApp app) {
            IGDCmdToPanCamera cmd = new IGDCmdToPanCamera(app);
            return cmd.execute();
        }
        
        protected override bool defineCmd() {
            IGDApp app = (IGDApp) this.mApp;
            IGD2DCameraPerson cp = app.get2DCameraPerson();
            
            Vector3 offset = (Vector3)(this.mCurPt - this.mPrevPt);
            cp.setEye(cp.getEye() - offset);
            
            // update cursor position
            IGDCursor2D cursor = app.getCursor();
            cursor.setScreenPos(this.mCurPt);

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