namespace XAppObject {
    public abstract class XAppObject2D : XAppObject {
        // contructor
        public XAppObject2D(string name) : base(name) {
            this.mGameObject.layer = 5; // UI layer.
        }
    }
}