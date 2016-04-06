using UnityEngine;
using System.Collections;

public class ConsumablePickup : MonoBehaviour {

	static bool nothing = true;
	static bool pepper = false;
	static bool pickedUp = false;

	Animator anim;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static bool getPepper() {
		return pepper;
	}

	public static void setPepper(bool input) {
		pepper = input;
	}

	public static void setPickedUp(bool input) {
		pickedUp = input;
	}

	public static bool getPickedUp() {
		return pickedUp;
	}

	public static bool getNothing() {
		return nothing;
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Pepper") {
			PlayerMovement.speedModifier *= 1.2f;
			Destroy (GameObject.FindGameObjectWithTag ("Pepper"));
			print ("Picked up: Chili Pepper -- 1.2x Speed");
		} else if (collision.gameObject.tag == "Cupcake") {
			PlayerMovement.speedModifier *= .85f;
			PlayerAttack.damageModifier *= 1.1f;
			if ((PlayerHealth.health + 20) > PlayerHealth.startHealth) {
				PlayerHealth.health = PlayerHealth.startHealth;
			} else {
				PlayerHealth.health += 20;
			}
			Destroy (GameObject.FindGameObjectWithTag ("Cupcake"));
			print ("Picked up: Cupcake -- .85x Speed, 1.1x Damage, +20 Health");
		} else if (collision.gameObject.tag == "ToxicWaste") {
			PlayerMovement.speedModifier *= 1.2f;
			PlayerAttack.damageModifier *= 1.2f;
			Destroy (GameObject.FindGameObjectWithTag ("ToxicWaste"));
			print ("Picked up: ToxicWaste -- 1.2x Speed, 1.2x Damage");
		} else if (collision.gameObject.tag == "Bacon") {
			PlayerHealth.healthModifier += 30;
			PlayerHealth.startHealth += 30;
			PlayerHealth.health += 30;
			Destroy (GameObject.FindGameObjectWithTag ("Bacon"));
			print ("Picked up: Bacon -- +30 Max Health");
		}
	}
}
