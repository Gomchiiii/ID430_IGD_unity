using System.Collections.Generic;
using UnityEngine;

namespace IGD {
    public class IGDPenMarkMgr {
        // constants
        public static readonly int MAX_NUM_PEN_MARKS = 10;

        // fields
        private List<IGDPenMark> mPenMarks = null;
        public List<IGDPenMark> getPenMarks() {
            return this.mPenMarks;
        }

        // constructor
        public IGDPenMarkMgr() {
            this.mPenMarks = new List<IGDPenMark>();
        }

        // methods
        public void addPenMark(IGDPenMark penMark) {
            this.mPenMarks.Add(penMark);
            if (this.mPenMarks.Count > IGDPenMarkMgr.MAX_NUM_PEN_MARKS) {
                this.mPenMarks.RemoveAt(0);
                Debug.Assert(
                    this.mPenMarks.Count <= IGDPenMarkMgr.MAX_NUM_PEN_MARKS);
            }
        }

        public IGDPenMark getLastPenMark() {
            int size = this.mPenMarks.Count;
            if (size == 0) {
                return null;
            } else {
                return this.mPenMarks[size - 1];
            }
        }

        public IGDPenMark getRecentPenMark(int i) {
            int size = this.mPenMarks.Count;
            int index = size - 1- i;
            if (index < 0 || index >= size) {
                return null;
            } else {
                return this.mPenMarks[index];
            }
        }

        public bool penDown(Vector2 pt) {
            IGDPenMark penMark = new IGDPenMark(pt);
            this.addPenMark(penMark);
            return true;
        }

        public bool penDrag(Vector2 pt) {
            IGDPenMark penMark = this.getLastPenMark();
            if (penMark != null && penMark.addPt(pt)) {
                return true;
            } else {
                return false;
            }
        }

        public bool penUp(Vector2 pt) {
            return true;
        }
    }
}