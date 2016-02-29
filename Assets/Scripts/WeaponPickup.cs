using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {

	static bool pitchfork = false;
	static bool hands = true;
	static bool pickedUp = false;
	Animator anim;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void setPitchfork(bool input) {
		pitchfork = input;
	}

	public static void setPickedUp(bool input) {
		pickedUp = input;
	}

	public static bool getPickedUp() {
		return pickedUp;
	}

	public static bool getHands() {
		return hands;
	}

	public static bool getPitchfork() {
		return pitchfork;
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Pitchfork") {
			pickedUp = true;
			pitchfork = true;
			hands = false;
			GameObject.FindGameObjectWithTag ("Pitchfork").transform.position = new Vector2 (-20f, -20f);
		}
	}
}
