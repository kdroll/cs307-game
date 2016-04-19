using UnityEngine;
using System.Collections;

public class Soundtrack : MonoBehaviour {
	public AudioClip newClip;
	private static Soundtrack instance = null;
	public static Soundtrack GetInstance
	{
		get { return instance; }
	}
	public void Awake() {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (gameObject);
	}
	public void OnLevelWasLoaded(int level) {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (gameObject);
		if (Application.loadedLevel == 1) {
			AudioSource audio = GetComponent<AudioSource> ();
			audio.clip = newClip;
		}
	}
}
