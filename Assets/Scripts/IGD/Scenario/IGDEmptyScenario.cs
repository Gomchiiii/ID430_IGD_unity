using X;
using UnityEngine;

namespace IGD.Scenario {
    public partial class IGDEmptyScenario : XScenario {
        // singleton pattern
        private static IGDEmptyScenario mSingleton = null;
        public static IGDEmptyScenario getSingleton() {
            Debug.Assert(IGDEmptyScenario.mSingleton != null);
            return IGDEmptyScenario.mSingleton;
        }
        public static IGDEmptyScenario createSingleton(XApp app) {
            Debug.Assert(IGDEmptyScenario.mSingleton == null);
            IGDEmptyScenario.mSingleton = new IGDEmptyScenario(app);
            return IGDEmptyScenario.mSingleton;
        }
        private IGDEmptyScenario(XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(IGDEmptyScenario.EmptyScene.createSingleton(this));
        }
    }
}