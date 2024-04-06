using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using XPlot;

namespace IGD {
    public static partial class IGDPlotter {
        public static readonly string MATH_IMAGE_URL =
            "https://latex.codecogs.com/png.image?\\inline&space;\\huge&space;\\dpi{300}";

        // HOW TO USE: Input formula in LaTex format.
        public static void writeFormula2D(string formula, Vector2 pos,
            float scale, Color color) {
            IGDPlotter.mApp.StartCoroutine(downloadAndAddImage2D(formula, pos,
                scale, color));
        }
        public static void writeFormula2D(string formula, Vector2 pos,
            float scale = 1f) {
            IGDPlotter.writeFormula2D(formula, pos, scale, Color.black);
        }

        public static void writeFormula3D(string formula, Vector3 pos,
            float scale, Color color) {
            IGDPlotter.mApp.StartCoroutine(downloadAndAddImage3D(formula, pos,
                scale, color));
        }
        public static void writeFormula3D(string formula, Vector3 pos,
            float scale = 1f) {
            IGDPlotter.writeFormula3D(formula, pos, scale, Color.black);
        }
        
        private static IEnumerator downloadAndAddImage2D(string formula,
            Vector2 pos, float scale, Color color) {
            string url = MATH_IMAGE_URL + formula;
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) {
                Debug.LogWarning(request.error);
            } else {
                // Get downloaded texture
                Texture2D texture = DownloadHandlerTexture.GetContent(request);

                XPlotImage2D image2D = new XPlotImage2D(formula, texture, pos);
                image2D.setTintColor(color);
                image2D.setScale(scale);
                IGDPlotter.mImage2Ds.Add(image2D);
            }
        }
        private static IEnumerator downloadAndAddImage3D(string formula,
            Vector3 pos, float scale, Color color) {
            string url = MATH_IMAGE_URL + formula;
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) {
                Debug.LogWarning(request.error);
            } else {
                // Get downloaded texture
                Texture2D texture = DownloadHandlerTexture.GetContent(request);

                XPlotImage3D image3D = new XPlotImage3D(formula, texture, pos);
                image3D.setTintColor(color);
                image3D.setScale(scale);
                IGDPlotter.mImage3Ds.Add(image3D);
            }
        }
        
        
    }
}