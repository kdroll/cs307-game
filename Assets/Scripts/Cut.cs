using UnityEngine;
using System.Collections;

public class Cut : MonoBehaviour {
	public Texture2D image;
	// Use this for initialization
	void Start () {
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), image, ScaleMode.StretchToFill);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
