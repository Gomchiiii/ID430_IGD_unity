using UnityEngine;
using X;

namespace IGD {
    public abstract class IGDScene : XScene {
        // constructor 
        protected IGDScene(XScenario scenario) : base(scenario) {
        }

        // methods
        public abstract void handleKeyDown(KeyCode kc);
        public abstract void handleKeyUp(KeyCode kc);
        public abstract void handleMouseDown(Vector2 pt);
        public abstract void handleMouseDrag(Vector2 pt);
        public abstract void handleMouseUp(Vector2 pt);
    }
}