using UnityEngine;

namespace Graphic
{
    [ExecuteInEditMode]
    public class CameraPostProcess : MonoBehaviour
    {
        #pragma warning disable CS0649
        [SerializeField]
        private Material material;
        #pragma warning restore CS0649

        [SerializeField]
        private bool postProcess = true;

        private void OnRenderImage(RenderTexture _src, RenderTexture _dest)
        {
            if (!postProcess)
                Graphics.Blit(_src, (RenderTexture)null);
            else
                Graphics.Blit(_src, null, material);
        }
    }
}
