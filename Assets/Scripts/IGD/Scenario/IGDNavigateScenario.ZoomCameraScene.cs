using X;
using UnityEngine;
using IGD.Cmd;

namespace IGD.Scenario {
    public partial class IGDNavigateScenario {
        public class ZoomCameraScene : IGDScene {
            // singleton pattern 
            private static ZoomCameraScene mSingleton = null;
            public static ZoomCameraScene getSingleton() {
                Debug.Assert(ZoomCameraScene.mSingleton != null);
                return ZoomCameraScene.mSingleton;
            }
            public static ZoomCameraScene createSingleton(XScenario scenario) {
                Debug.Assert(ZoomCameraScene.mSingleton == null);
                ZoomCameraScene.mSingleton = new ZoomCameraScene(scenario);
                return ZoomCameraScene.mSingleton;
            }
            private ZoomCameraScene(XScenario scenario) : base(scenario) {
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
                            IGDNavigateScenario.TumbleCameraScene.
                                getSingleton(), this.mReturnScene);
                        break;

                }
            }

            public override void handleMouseDown(Vector2 pt) {
            }

            public override void handleMouseDrag(Vector2 pt) {
                IGDApp app = (IGDApp)this.mScenario.getApp();
                IGDCmdToZoomCamera.execute(app);
            }

            public override void handleMouseUp(Vector2 pt) {
                IGDApp app = (IGDApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(app,
                    IGDNavigateScenario.ZoomReadyScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }

    }
}