using X;
using UnityEngine;
using IGD.Cmd;

namespace IGD.Scenario {
    public partial class IGDNavigateScenario {
        public class RotateReadyScene : IGDScene {
            // singleton pattern 
            private static RotateReadyScene mSingleton = null;
            public static RotateReadyScene getSingleton() {
                Debug.Assert(RotateReadyScene.mSingleton != null);
                return RotateReadyScene.mSingleton;
            }
            public static RotateReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(RotateReadyScene.mSingleton == null);
                RotateReadyScene.mSingleton = new RotateReadyScene(scenario);
                return RotateReadyScene.mSingleton;
            }
            private RotateReadyScene(XScenario scenario) : base(scenario) {
            }

            public override void handleKeyDown(KeyCode kc) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(app, 
                            IGDNavigateScenario.TranslateReadyScene.getSingleton(),
                            this.mReturnScene);
                        break;
                    case KeyCode.LeftShift:
                        XCmdToChangeScene.execute(app, 
                            IGDNavigateScenario.ZoomReadyScene.getSingleton(),
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
                IGDApp app = (IGDApp) this.mScenario.getApp();

                XCmdToChangeScene.execute(app, 
                    IGDNavigateScenario.TumbleCameraScene.getSingleton(),
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