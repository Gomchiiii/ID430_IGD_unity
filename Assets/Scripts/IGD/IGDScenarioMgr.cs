using X;
using IGD.Scenario;

namespace IGD {
    public class IGDScenarioMgr : XScenarioMgr {
        // constructor
        public IGDScenarioMgr(XApp app) : base(app) {
        }

        // methods
        protected override void addScenarios() { 
            IGDApp app = (IGDApp) this.mApp;
            this.addScenario(IGDDefaultScenario.createSingleton(app));
            this.addScenario(IGDNavigateScenario.createSingleton(app));
        }
        
        protected override void setInitCurScene() {
            this.setCurScene(IGDDefaultScenario.ReadyScene.getSingleton());
        }
    }
}