using X;
using UnityEngine;
using IGD.Cmd;

namespace IGD.Scenario {
    public partial class IGDNavigateScenario {
        public class TranslateReadyScene : IGDScene {
            // singleton pattern 
            private static TranslateReadyScene mSingleton = null;
            public static TranslateReadyScene getSingleton() {
                Debug.Assert(TranslateReadyScene.mSingleton != null);
                return TranslateReadyScene.mSingleton;
            }
            public static TranslateReadyScene createSingleton(
                XScenario scenario) {
                Debug.Assert(TranslateReadyScene.mSingleton == null);
                TranslateReadyScene.mSingleton = 
                    new TranslateReadyScene(scenario);
                return TranslateReadyScene.mSingleton;
            }
            private TranslateReadyScene(XScenario scenario) : base(scenario) {
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
                            IGDNavigateScenario.RotateReadyScene.getSingleton(),
                            this.mReturnScene);
                    break;
                }
            }

            public override void handleMouseDown(Vector2 pt) {
                IGDApp app = (IGDApp) this.mScenario.getApp();
                
                XCmdToChangeScene.execute(app, 
                    IGDNavigateScenario.DollyCameraScene.getSingleton(),
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