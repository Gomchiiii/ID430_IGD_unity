using UnityEngine;

namespace XGeom {
    public abstract class XGeom {
        public static readonly Vector2 VECTOR2_NAN =
            new Vector2(float.NaN, float.NaN); 
        public static readonly Vector3 VECTOR3_NAN =
            new Vector3(float.NaN, float.NaN, float.NaN);
        public static readonly Vector4 VECTOR4_NAN =
            new Vector4(float.NaN, float.NaN, float.NaN, float.NaN);
    }
}