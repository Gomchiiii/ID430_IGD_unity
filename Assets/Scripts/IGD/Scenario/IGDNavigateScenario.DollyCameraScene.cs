using X;
using IGD.Cmd;
using UnityEngine;

namespace IGD.Scenario {
    public partial class IGDNavigateScenario {
        public class DollyCameraScene : IGDScene {
            // singleton pattern 
            private static DollyCameraScene mSingleton = null;
            public static DollyCameraScene getSingleton() {
                Debug.Assert(DollyCameraScene.mSingleton != null);
                return DollyCameraScene.mSingleton;
            }
            public static DollyCameraScene createSingleton(XScenario scenario) {
                Debug.Assert(DollyCameraScene.mSingleton == null);
                DollyCameraScene.mSingleton = new DollyCameraScene(scenario);
                return DollyCameraScene.mSingleton;
            }
            private DollyCameraScene(XScenario scenario) : base(scenario) {
            }

            public override void handleKeyDown(KeyCode kc) {
            }

            public override void handleKeyUp(KeyCode kc) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(app, this.mReturnScene, null);
                    break;
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(app, 
                            IGDNavigateScenario.TumbleCameraScene.getSingleton(),
                            this.mReturnScene);
                    break;
                }
            }

            public override void handleMouseDown(Vector2 pt) {
            }

            public override void handleMouseDrag(Vector2 pt) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                IGDCmdToDollyCamera.execute(app);
            }

            public override void handleMouseUp(Vector2 pt) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                XCmdToChangeScene.execute(app, 
                    IGDNavigateScenario.TranslateReadyScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void getReady() {
            }

            public override void wrapUp() {
            }
        }
    }
}