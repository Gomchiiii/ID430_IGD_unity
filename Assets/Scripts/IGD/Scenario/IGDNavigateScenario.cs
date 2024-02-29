using X;
using UnityEngine;
using XAppObject;

namespace IGD.Scenario {
    public partial class IGDNavigateScenario : XScenario {
        // singleton pattern
        private static IGDNavigateScenario mSingleton = null;
        public static IGDNavigateScenario getSingleton() {
            Debug.Assert(IGDNavigateScenario.mSingleton != null);
            return IGDNavigateScenario.mSingleton;
        }
        public static IGDNavigateScenario createSingleton(XApp app) {
            Debug.Assert(IGDNavigateScenario.mSingleton == null);
            IGDNavigateScenario.mSingleton = new IGDNavigateScenario(app);
            return IGDNavigateScenario.mSingleton;
        }
        private IGDNavigateScenario(XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(
                IGDNavigateScenario.RotateReadyScene.createSingleton(this));
            this.addScene(
                IGDNavigateScenario.TumbleCameraScene.createSingleton(this));
            this.addScene(
                IGDNavigateScenario.TranslateReadyScene.createSingleton(this));
            this.addScene(
                IGDNavigateScenario.DollyCameraScene.createSingleton(this));
            this.addScene(
                IGDNavigateScenario.ZoomReadyScene.createSingleton(this));
            this.addScene(
                IGDNavigateScenario.ZoomCameraScene.createSingleton(this));
            this.addScene(
                IGDNavigateScenario.PanReadyScene.createSingleton(this));
            this.addScene(
                IGDNavigateScenario.PanCameraScene.createSingleton(this));
        }
    }
}