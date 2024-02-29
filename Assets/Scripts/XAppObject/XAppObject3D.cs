namespace XAppObject {
    public abstract class XAppObject3D : XAppObject {
        // contructor
        public XAppObject3D(string name) : base(name) {
            this.mGameObject.layer = 0; // default layer.
        }
    }
}