using System.Text;
using X;
using UnityEngine;

namespace IGD.Cmd {
    public class IGDCmdToDoSomething : XLoggableCmd {
        // fields
        // ...

        // private constructor
        private IGDCmdToDoSomething(IGDApp app) : base(app) {
        }
        
        // static method to construct and execute this command
        public static bool execute(IGDApp app) {
            IGDCmdToDoSomething cmd = new IGDCmdToDoSomething(app);
            return cmd.execute();
        }
        
        protected override bool defineCmd() {
            IGDApp app = (IGDApp) this.mApp;
            // ...
            
            return true;
        }

        protected override string createLog() {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name).Append("\t");
            // ...
            
            return sb.ToString();
        }    
    }
}