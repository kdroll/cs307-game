using UnityEngine;
using System.Collections;

public class Soundtrack : MonoBehaviour {
	public AudioClip level0Clip;
	public AudioClip level1Clip;
	public AudioClip level2Clip;
	static bool menuMusic = true;
	private static Soundtrack instance = null;
	public static Soundtrack GetInstance
	{
		get { return instance; }
	}
	public void Awake() {
		singleton ();
	}
	public void OnLevelWasLoaded(int level) {
		if (level == 0 && !menuMusic) {
			menuMusic = true;
			AudioSource aud = GetComponent<AudioSource> ();
			aud.clip = level0Clip;
			aud.Play ();
			singleton ();
		}
		if (level == 1) {
			menuMusic = false;
			AudioSource aud = GetComponent<AudioSource> ();
			aud.clip = level1Clip;
			aud.Play ();
			singleton ();
		}
		if (level == 10) {
			AudioSource aud = GetComponent<AudioSource> ();
			aud.clip = level2Clip;
			aud.Play ();
			singleton ();
		}
	}

	void singleton() {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad (gameObject);
	}
}
