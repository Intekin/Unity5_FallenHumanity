using UnityEngine;
using System.Collections;

public class TexturePanner : MonoBehaviour {
	
	public float panX = 0;
	public float panY = 0;
	
	public float timeMultiplier = 0.1f;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float offsetX = Time.time * timeMultiplier * panX;
		float offsetY = Time.time * timeMultiplier * panY;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
	
	}
}
