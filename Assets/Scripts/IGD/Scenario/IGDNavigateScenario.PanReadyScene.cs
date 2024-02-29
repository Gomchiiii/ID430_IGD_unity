using X;
using UnityEngine;
using IGD.Cmd;

namespace IGD.Scenario {
    public partial class IGDNavigateScenario {
        public class PanReadyScene : IGDScene {
            // singleton pattern 
            private static PanReadyScene mSingleton = null;
            public static PanReadyScene getSingleton() {
                Debug.Assert(PanReadyScene.mSingleton != null);
                return PanReadyScene.mSingleton;
            }
            public static PanReadyScene createSingleton(
                XScenario scenario) {
                Debug.Assert(PanReadyScene.mSingleton == null);
                PanReadyScene.mSingleton = 
                    new PanReadyScene(scenario);
                return PanReadyScene.mSingleton;
            }
            private PanReadyScene(XScenario scenario) : base(scenario) {
            }

            public override void handleKeyDown(KeyCode kc) {
            }

            public override void handleKeyUp(KeyCode kc) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(app, 
                            IGDDefaultScenario.ReadyScene.getSingleton(),
                            this.mReturnScene);
                    break;
                }
            }

            public override void handleMouseDown(Vector2 pt) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                
                XCmdToChangeScene.execute(app, 
                    IGDNavigateScenario.PanCameraScene.getSingleton(),
                    this.mReturnScene);
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