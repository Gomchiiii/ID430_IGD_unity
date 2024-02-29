using X;
using UnityEngine;
using IGD.Cmd;

namespace IGD.Scenario {
    public partial class IGDNavigateScenario {
        public class ZoomReadyScene : IGDScene {
            // singleton pattern 
            private static ZoomReadyScene mSingleton = null;
            public static ZoomReadyScene getSingleton() {
                Debug.Assert(ZoomReadyScene.mSingleton != null);
                return ZoomReadyScene.mSingleton;
            }
            public static ZoomReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(ZoomReadyScene.mSingleton == null);
                ZoomReadyScene.mSingleton = new ZoomReadyScene(scenario);
                return ZoomReadyScene.mSingleton;
            }
            private ZoomReadyScene(XScenario scenario) : base(scenario) {
            }

            public override void handleKeyDown(KeyCode kc) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
            }

            public override void handleKeyUp(KeyCode kc) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(app, this.mReturnScene, null);
                        break;
                    case KeyCode.LeftShift:
                        XCmdToChangeScene.execute(app,
                            IGDNavigateScenario.RotateReadyScene.getSingleton(),
                            this.mReturnScene);
                        break;
                }
            }

            public override void handleMouseDown(Vector2 pt) {
                IGDApp app = (IGDApp) this.mScenario.getApp();

                XCmdToChangeScene.execute(app, 
                    IGDNavigateScenario.ZoomCameraScene.getSingleton(),
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