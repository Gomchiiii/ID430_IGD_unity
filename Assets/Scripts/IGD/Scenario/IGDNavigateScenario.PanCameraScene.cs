using X;
using UnityEngine;
using IGD.Cmd;

namespace IGD.Scenario {
    public partial class IGDNavigateScenario {
        public class PanCameraScene : IGDScene {
            // singleton pattern 
            private static PanCameraScene mSingleton = null;
            public static PanCameraScene getSingleton() {
                Debug.Assert(PanCameraScene.mSingleton != null);
                return PanCameraScene.mSingleton;
            }
            public static PanCameraScene createSingleton(
                XScenario scenario) {
                Debug.Assert(PanCameraScene.mSingleton == null);
                PanCameraScene.mSingleton = 
                    new PanCameraScene(scenario);
                return PanCameraScene.mSingleton;
            }
            private PanCameraScene(XScenario scenario) : base(scenario) {
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
            }

            public override void handleMouseDrag(Vector2 pt) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                IGDCmdToPanCamera.execute(app);
            }

            public override void handleMouseUp(Vector2 pt) {
                IGDApp app = (IGDApp)this.mScenario.getApp();

                XCmdToChangeScene.execute(app,
                    IGDNavigateScenario.PanReadyScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }
    }
}