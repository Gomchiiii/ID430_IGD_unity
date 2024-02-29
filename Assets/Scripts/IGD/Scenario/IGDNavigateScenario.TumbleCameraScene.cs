using X;
using IGD.Cmd;
using UnityEngine;

namespace IGD.Scenario {
    public partial class IGDNavigateScenario {
        public class TumbleCameraScene : IGDScene {
            // singleton pattern 
            private static TumbleCameraScene mSingleton = null;
            public static TumbleCameraScene getSingleton() {
                Debug.Assert(TumbleCameraScene.mSingleton != null);
                return TumbleCameraScene.mSingleton;
            }
            public static TumbleCameraScene createSingleton(XScenario scenario) {
                Debug.Assert(TumbleCameraScene.mSingleton == null);
                TumbleCameraScene.mSingleton = new TumbleCameraScene(scenario);
                return TumbleCameraScene.mSingleton;
            }
            private TumbleCameraScene(XScenario scenario) : base(scenario) {
            }

            public override void handleKeyDown(KeyCode kc) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(app, 
                            IGDNavigateScenario.DollyCameraScene.getSingleton(),
                            this.mReturnScene);
                        break;
                    case KeyCode.LeftShift:
                        XCmdToChangeScene.execute(app,
                            IGDNavigateScenario.ZoomCameraScene.getSingleton(),
                            this.mReturnScene);
                        break;
                }
            }

            public override void handleKeyUp(KeyCode kc) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(app, this.mReturnScene, null);
                    break;                    
                }
            }

            public override void handleMouseDown(Vector2 pt) {
            }

            public override void handleMouseDrag(Vector2 pt) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                IGDCmdToTumbleCamera.execute(app);
            }

            public override void handleMouseUp(Vector2 pt) { 
                IGDApp app = (IGDApp) this.mScenario.getApp();
                XCmdToChangeScene.execute(app, 
                    IGDNavigateScenario.RotateReadyScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }

    }
}