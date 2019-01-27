using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{

	private Material _material;
	
	// Use this for initialization
	void Start ()
	{
		_material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		_material.SetTextureOffset("_MainTex", _material.GetTextureOffset("_MainTex") + Vector2.left*Time.deltaTime);
	}
}
