using X;
using UnityEngine;
using IGD.Cmd;

namespace IGD.Scenario {
    public partial class IGDDefaultScenario {
        public class ReadyScene : IGDScene {
            // singleton pattern 
            private static ReadyScene mSingleton = null;
            public static ReadyScene getSingleton() {
                Debug.Assert(ReadyScene.mSingleton != null);
                return ReadyScene.mSingleton;
            }
            public static ReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(ReadyScene.mSingleton == null);
                ReadyScene.mSingleton = new ReadyScene(scenario);
                return ReadyScene.mSingleton;
            }
            private ReadyScene(XScenario scenario) : base(scenario) {
            }

            public override void handleKeyDown(KeyCode kc) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(app, 
                            IGDNavigateScenario.RotateReadyScene.getSingleton(),
                            this);
                        break;
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(app,
                            IGDNavigateScenario.PanReadyScene.getSingleton(),
                            this);
                        break;
                }
            }

            public override void handleKeyUp(KeyCode kc) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                switch (kc) {
                }
            }

            public override void handleMouseDown(Vector2 pt) {
            }

            public override void handleMouseDrag(Vector2 pt) {
            }

            public override void handleMouseUp(Vector2 pt) { 
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }

    }
}