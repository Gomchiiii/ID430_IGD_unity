using X;
using UnityEngine;

namespace IGD.Scenario {
    public partial class IGDDefaultScenario : XScenario {
        // singleton pattern
        private static IGDDefaultScenario mSingleton = null;
        public static IGDDefaultScenario getSingleton() {
            Debug.Assert(IGDDefaultScenario.mSingleton != null);
            return IGDDefaultScenario.mSingleton;
        }
        public static IGDDefaultScenario createSingleton(XApp app) {
            Debug.Assert(IGDDefaultScenario.mSingleton == null);
            IGDDefaultScenario.mSingleton = new IGDDefaultScenario(app);
            return IGDDefaultScenario.mSingleton;
        }
        private IGDDefaultScenario(XApp app) : base(app) {
        }
        
        protected override void addScenes() {
            this.addScene(IGDDefaultScenario.ReadyScene.createSingleton(this));
        }
    }
}