using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {
	static bool nunchuck = false;
	static bool pitchfork = false;
	static bool hands = true;
	static bool pickedUp = false;
	static bool sword = false;
	Animator anim;
	public GameObject grenade;
	public Transform spawnGrenade;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			Instantiate (grenade, spawnGrenade.position, Quaternion.identity);
		}
		
	}
	public static bool getNunchuck() {
		return nunchuck;
	}

	public static void setNunchuck(bool input) {
		nunchuck = input;
	}
	public static bool getSword() {
		return sword;
	}

	public static void setSword(bool input) {
		sword = input;
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
		if (collision.gameObject.tag == "Pitchfork" && sword != true && nunchuck != true) {
			pickedUp = true;
			pitchfork = true;
			nunchuck = false;
			hands = false;
			GameObject.FindGameObjectWithTag ("Pitchfork").transform.position = new Vector2 (-20f, -20f);
		} else if (collision.gameObject.tag == "Sword" && pitchfork != true && nunchuck != true) {
			pickedUp = true;
			sword = true;
			nunchuck = false;
			pitchfork = false;
			hands = false;
			GameObject.FindGameObjectWithTag ("Sword").transform.position = new Vector2 (-40f, -40f);
		}
		else if (collision.gameObject.tag == "Nunchuck" && pitchfork != true && sword != true) {
			pickedUp = true;
			sword = false;
			nunchuck = true;
			pitchfork = false;
			hands = false;
			GameObject.FindGameObjectWithTag ("Nunchuck").transform.position = new Vector2 (-40f, -40f);
		}
	}
}
