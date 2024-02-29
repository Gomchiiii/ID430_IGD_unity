using X;
using UnityEngine;

namespace IGD.Scenario {
    public partial class IGDEmptyScenario {
        public class EmptyScene : IGDScene {
            // singleton pattern 
            private static EmptyScene mSingleton = null;
            public static EmptyScene getSingleton() {
                Debug.Assert(EmptyScene.mSingleton != null);
                return EmptyScene.mSingleton;
            }
            public static EmptyScene createSingleton(XScenario scenario) {
                Debug.Assert(EmptyScene.mSingleton == null);
                EmptyScene.mSingleton = new EmptyScene(scenario);
                return EmptyScene.mSingleton;
            }
            private EmptyScene(XScenario scenario) : 
                base(scenario) {
            }

            public override void handleKeyDown(KeyCode kc) {
            }
            public override void handleKeyUp(KeyCode kc) {
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