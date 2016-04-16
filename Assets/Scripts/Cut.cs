using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cut : MonoBehaviour {
	public Texture2D[] images;
	Camera cam = Camera.main;
	public GameObject canvas;
	float height;
	float width;
	int i;
	// Use this for initialization
	void Start () {
		RectTransform size = canvas.GetComponent<RectTransform> ();
		height = 2f * cam.orthographicSize;
		width = height * cam.aspect;

		gameObject.GetComponent<Image> ().GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width, Screen.height);

		//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), images[0], ScaleMode.StretchToFill);
	}
	
	// Update is called once per frame
	void Update () {
		//height = 2f * cam.orthographicSize;
		//width = height * cam.aspect;

		//gameObject.GetComponent<Image> ().GetComponent<RectTransform> ().sizeDelta = new Vector2 (width, height);

	
	}
	/*void OnGUI() {
		for(i=0;i<images.Length;i++)
		{
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), images[i]);
		}
	}*/
}
