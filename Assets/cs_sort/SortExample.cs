using UnityEngine;
using System.Collections;

public class SortExample : MonoBehaviour
{
    public ComputeShader shader;

    RenderTexture tex;

    [SerializeField] private Texture2D _src;
    [SerializeField,Range(0,5)] private float _th = 0.5f;
    void Start ()
    {

        tex = new RenderTexture(256, 256, 0);
        tex.enableRandomWrite = true;
        tex.Create();
        Graphics.Blit(_src,tex);

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

    void Update(){

        Graphics.Blit(_src,tex);    
        shader.SetFloat("th",_th);
        shader.SetFloat("w", tex.width);
        shader.SetFloat("h", tex.height);
        shader.SetTexture(0, "tex", tex);
        shader.SetFloat("th",_th);

        //_th = Mathf.Sin(Time.realtimeSinceStartup) * 0.7f + 1f;

        shader.Dispatch(0,  1, 1, 1);
        
    }

}