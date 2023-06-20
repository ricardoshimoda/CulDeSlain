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

    public float speed = 1f;

    private void Update()
    {
        // Calculate the texture offset based on camera rotation and position
        float offsetX = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        float offsetY = transform.rotation.eulerAngles.x * Mathf.Deg2Rad;

        float offsetXNormalized = offsetX / (2f * Mathf.PI);
        float offsetYNormalized = offsetY / (2f * Mathf.PI);

        Vector2 offset = new Vector2(offsetXNormalized, offsetYNormalized) * speed;

        // Set the texture offset on the material
        effectMaterial.SetTextureOffset("_DitherTex", offset);
    }
}
