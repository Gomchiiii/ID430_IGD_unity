using UnityEditor;
using UnityEngine;
using XAppObject;

// Use next site for importing mathematical notations:
// https://editor.codecogs.com/home
namespace XPlot {
    public class XPlotImage2D : XAppNoGeom2D {
        // constants
        private static readonly float DEFAULT_SCALE = 100f;
        
        // constructor
        public XPlotImage2D(string name, string filePath, Vector2 pos)
            : base($"{name}/Image2D") {
            Texture2D texture = Resources.Load<Texture2D>(filePath);
            
            // By default, the texture width and height are scaled as power of 2. 
            // Below code will resize the texture to the original size.
            string assetPath = AssetDatabase.GetAssetPath(texture);
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) 
                as TextureImporter;
            textureImporter.npotScale = TextureImporterNPOTScale.None;
            textureImporter.SaveAndReimport();
            
            this.mGameObject.AddComponent<SpriteRenderer>();
            SpriteRenderer sr = this.mGameObject.GetComponent<SpriteRenderer>();
            sr.sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
            
            this.setPosition(pos);
            this.setScale(1f);
        }
        
        // methods
        public void setScale(float scale) {
            Vector3 defaultScale =
                new Vector3(DEFAULT_SCALE, DEFAULT_SCALE, DEFAULT_SCALE);
            this.mGameObject.transform.localScale = defaultScale * scale;
        }
    }
}