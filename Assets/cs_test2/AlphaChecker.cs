using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChecker : MonoBehaviour {

	public ComputeShader _shader;
	public Texture2D texture;

	private int divCount = 10;
	
	// Use this for initialization
	void Start () {
		
		//_shader = ComputeShader.Instantiate(Resources.Load<ComputeShader>("Shaders/HitAreaDetector"));

		// 分割数で算出された一区画に対するピクセル数をさらに奇数に補正する
		int pixelPerDivW = texture.width / divCount;
		pixelPerDivW = pixelPerDivW - (1 - pixelPerDivW % 2);

		int pixelPerDivH = texture.height / divCount;
		pixelPerDivH = pixelPerDivH - (1 - pixelPerDivH % 2);

		int num = divCount * divCount;
		ComputeBuffer buffer = new ComputeBuffer(num, sizeof(float));

		int kernelID = _shader.FindKernel("CSMain");

		_shader.SetBuffer(kernelID, "Result", buffer);
		_shader.SetTexture(kernelID, "Texture", texture);
		_shader.SetInt("Length", divCount);
		_shader.SetInt("Width", pixelPerDivW);
		_shader.SetInt("Height", pixelPerDivH);

		float[] rawData = new float[num];

		_shader.Dispatch(kernelID, divCount, divCount, 1);
		buffer.GetData(rawData);

		buffer.Release();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
