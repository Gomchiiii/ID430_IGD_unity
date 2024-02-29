using System.Collections.Generic;
using UnityEngine;

namespace IGD {
    public class IGDKeyEventSource {
        // constants
        private static readonly List<KeyCode> WATCHING_KEYCODE = 
            new List<KeyCode>() {
                KeyCode.LeftControl, // for rotating
                KeyCode.LeftAlt, // for translation and panning
                KeyCode.Return, // for creating a standing card
                KeyCode.LeftShift // for zooming
            };
        
        // fields
        private IGDEventListener mEventListener = null;
        public void setEventListener(IGDEventListener eventListener) {
            this.mEventListener = eventListener;
        }

        // constructor
        public IGDKeyEventSource() {
        }

        // methods
        public void update() {
            foreach (KeyCode kc in IGDKeyEventSource.WATCHING_KEYCODE) {
                if (Input.GetKeyDown(kc)) {
                    this.mEventListener.keyPressed(kc);
                }
                if (Input.GetKeyUp(kc)) {
                    this.mEventListener.keyReleased(kc);
                }
            }
        }
    }
}