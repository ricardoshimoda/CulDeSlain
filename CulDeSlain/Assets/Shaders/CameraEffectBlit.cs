using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways, ImageEffectAllowedInSceneView, RequireComponent(typeof(Camera))]
public class CameraEffectBlit : MonoBehaviour
{
    public Material effectMaterial;
    public bool UseEffect = true;
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {

            RenderTexture rt = RenderTexture.GetTemporary(src.width, src.height);
            Graphics.Blit(src, rt, effectMaterial);
            Graphics.Blit(rt, dst);
            RenderTexture.ReleaseTemporary(rt);
        
    }
}
