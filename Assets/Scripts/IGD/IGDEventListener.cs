using UnityEngine;

namespace IGD {
    public class IGDEventListener {
        // fields
        private IGDApp mApp = null;

        // constructor
        public IGDEventListener(IGDApp app) {
            this.mApp = app;
        }

        // methods
        public void keyPressed(KeyCode kc) {
            IGDScene curScene = 
                (IGDScene) this.mApp.getScenarioMgr().getCurScene();
            curScene.handleKeyDown(kc);
        }
        public void keyReleased(KeyCode kc) {
            IGDScene curScene = 
                (IGDScene) this.mApp.getScenarioMgr().getCurScene();
            curScene.handleKeyUp(kc);
        }
        public void mouseMoved(Vector2 pt) {
            // update the cursor.
            this.mApp.getCursor().setScreenPos(pt);
        }
        public void mouseLeftPressed(Vector2 pt) {
            // update the cursor.
            this.mApp.getCursor().setScreenPos(pt);

            if (this.mApp.getPenMarkMgr().penDown(pt)) {
                IGDScene curScene = 
                    (IGDScene) this.mApp.getScenarioMgr().getCurScene();
                curScene.handleMouseDown(pt);
            }
        }
        public void mouseLeftDragged(Vector2 pt) {
            // update the cursor.
            this.mApp.getCursor().getGameObject().transform.position = pt;

            if (this.mApp.getPenMarkMgr().penDrag(pt)) {
                IGDScene curScene = 
                    (IGDScene) this.mApp.getScenarioMgr().getCurScene();
                curScene.handleMouseDrag(pt);
            }
        }
        public void mouseLeftReleased(Vector2 pt) {
            // update the cursor.
            this.mApp.getCursor().getGameObject().transform.position = pt;

            if (this.mApp.getPenMarkMgr().penUp(pt)) {
                IGDScene curScene = 
                    (IGDScene) this.mApp.getScenarioMgr().getCurScene();
                curScene.handleMouseUp(pt);
            }
        }
        public void mouseRightPressed(Vector2 pt) {
        }
        public void mouseRightDragged(Vector2 pt) {
        }
        public void mouseRightReleased(Vector2 pt) {
        }
    }
}