using System.Text;
using X;
using UnityEngine;

namespace IGD.Cmd {
    public class IGDCmdToTumbleCamera : XLoggableCmd {
        // fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        // private contructor
        private IGDCmdToTumbleCamera(XApp app) : base(app) {
            IGDApp xpApp = (IGDApp) this.mApp;
            IGDPenMark pm = xpApp.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = pm.getRecentPt(1);
            this.mCurPt = pm.getRecentPt(0);
        }
        
        // static method to construct and execute this command
        public static bool execute(XApp app) {
            IGDCmdToTumbleCamera cmd = new IGDCmdToTumbleCamera(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            IGDApp app = (IGDApp) this.mApp;
            IGD3DCameraPerson cp = app.get3DCameraPerson();

            float dx = this.mCurPt.x - this.mPrevPt.x;
            float dy = this.mCurPt.y - this.mPrevPt.y;
            float dAzimuth = 180.0f * dx / Screen.width;
            float dZenith = 180.0f * dy / Screen.height;

            Quaternion qa = Quaternion.AngleAxis(dAzimuth, Vector3.up);
            Quaternion qz = Quaternion.AngleAxis(-dZenith, cp.getRight());

            Vector3 pivotToEye = cp.getEye() - cp.getPivot();
            Vector3 nextEye = cp.getPivot() + qa * qz * pivotToEye;
            Vector3 nextView = qa * qz * cp.getView();

            cp.setEye(nextEye);
            cp.setView(nextView);

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