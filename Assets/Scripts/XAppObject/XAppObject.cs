using System.Collections.Generic;
using UnityEngine;

namespace XAppObject {
    public abstract class XAppObject {
        // fields
        protected GameObject mGameObject = null;
        public GameObject getGameObject() {
            return this.mGameObject;
        }
        protected List<XAppObject> mChildren = null;
        public List<XAppObject> getChildren() {
            return this.mChildren;
        }

        // constructor
        public XAppObject(string name) {
            this.mGameObject = new GameObject(name);
            this.mChildren = new List<XAppObject>();
            this.addComponents();
        }

        // methods
        protected abstract void addComponents();
        public void addChild(XAppObject child) {
            this.mChildren.Add(child);
            GameObject childGameObject = child.getGameObject();

            Vector3 localPos = childGameObject.transform.localPosition;
            Quaternion localRot = childGameObject.transform.localRotation;
            Vector3 localScale = childGameObject.transform.localScale;

            childGameObject.transform.parent = this.mGameObject.transform;

            childGameObject.transform.localPosition = localPos;
            childGameObject.transform.localRotation = localRot;
            childGameObject.transform.localScale = localScale;
        }
        public void removeChild(XAppObject child) {
            this.mChildren.Remove(child);
            GameObject childGameObject = child.getGameObject();

            Vector3 localPos = childGameObject.transform.localPosition;
            Quaternion localRot = childGameObject.transform.localRotation;
            Vector3 localScale = childGameObject.transform.localScale;

            childGameObject.transform.parent = null;

            childGameObject.transform.localPosition = localPos;
            childGameObject.transform.localRotation = localRot;
            childGameObject.transform.localScale = localScale;
        }
        public void destroyGameObject() {
            GameObject.Destroy(this.mGameObject);
            foreach (XAppObject child in this.mChildren) {
                child.destroyGameObject();
            }
        }
        
        public void setPosition(Vector3 position) {
            this.mGameObject.transform.position = position;
        }
        public void setRotation(Quaternion rotation) {
            this.mGameObject.transform.rotation = rotation;
        }
        public void setLocalPosition(Vector3 localPosition) {
            this.mGameObject.transform.localPosition = localPosition;
        }
        public void setLocalRotation(Quaternion localRotation) {
            this.mGameObject.transform.localRotation = localRotation;
        }
        public void setLocalScale(Vector3 localScale) {
            this.mGameObject.transform.localScale = localScale;
        }
        
        public void setName(string name) {
            this.mGameObject.name = name;
        }
    }
}