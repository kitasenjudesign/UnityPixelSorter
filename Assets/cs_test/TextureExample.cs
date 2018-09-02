using UnityEngine;
using System.Collections;

public class TextureExample : MonoBehaviour
{
    public ComputeShader shader;

    RenderTexture tex;

    [SerializeField] private Texture2D _src;

    void Start ()
    {

        tex = new RenderTexture(512, 512, 0);
        tex.enableRandomWrite = true;
        tex.Create();
        Graphics.Blit(_src,tex);

        shader.SetFloat("w", tex.width);
        shader.SetFloat("h", tex.height);
        shader.SetTexture(0, "tex", tex);
        shader.Dispatch(0, tex.width/8, tex.height/8, 1);

    }

    void OnGUI()
    {
        int w = Screen.width/2;
        int h = Screen.height/2;
        int s = 512;

        GUI.DrawTexture(new Rect(w-s/2,h-s/2,s,s), tex);
    }

    void OnDestroy()
    {
        tex.Release();
    }
}