using UnityEngine;
using System.Collections;

public class Cut : MonoBehaviour {
	public Texture2D[] images;
	int i;
	// Use this for initialization
	void Start () {
		
		//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), images[0], ScaleMode.StretchToFill);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI() {
		for(i=0;i<images.Length;i++)
		{
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), images[i]);
		}
	}
}
