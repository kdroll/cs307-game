using UnityEngine;
using System.Collections;

public class BowWeapon : MonoBehaviour {
	Animator anim;
	int lastMovement;
	// Use this for initialization
	void Start () {
		anim = this.transform.GetComponent<Animator> ();
		anim.SetBool ("ifBowEquipped", false);
		GameObject.FindGameObjectWithTag ("Bow").transform.position = new Vector3 (10, 54, 0);
	}

	// Update is called once per frame
	void Update () {
		anim.SetBool ("ifAttacking", false);
		if ((Input.GetKey (KeyCode.F) || Input.GetKey(KeyCode.JoystickButton3))  && WeaponPickup.getBow() == true) {
			WeaponPickup.setBow (false);
			WeaponPickup.setPickedUp (false);
			anim.SetBool ("ifBowEquipped", false);
			GameObject.FindGameObjectWithTag ("Bow").GetComponent<Collider2D> ().isTrigger = false;
			GameObject.FindGameObjectWithTag ("Bow").transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
			GameObject.FindGameObjectWithTag ("Bow").GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((Random.value * 2) - 1, (Random.value * 2) - 1).normalized * 100.0f);
		}
		if (WeaponPickup.getPickedUp () == true) {
			if (WeaponPickup.getBow () == true) {
				anim.SetBool ("ifBowEquipped", true);
			} else {
				anim.SetBool ("ifBowEquipped", false);
			}
		} else {
			anim.SetBool ("ifBowEquipped", false);
		}
		//if (WeaponPickup.getBow () == true && (Input.GetButtonDown ("attack") || Input.GetButtonDown("B"))) {
		//	anim.SetBool ("ifAttacking", true);
			lastMovement = PlayerMovement.getLastMovementDirection ();
			if (lastMovement == 1) {
				anim.SetFloat ("MoveY", 1.0f);
				anim.SetFloat ("MoveX", 0.0f);
			//gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y -0.3f, 0);
			} else if (lastMovement == 2) {
				anim.SetFloat ("MoveY", -1.0f);
				anim.SetFloat ("MoveX", 0.0f);
			//transform.position = new Vector3 (0, 0.3f, 0);
			} else if (lastMovement == 3) {
				anim.SetFloat ("MoveY", 0.0f);
				anim.SetFloat ("MoveX", -1.0f);
			//transform.position = new Vector3 (0.3f, 0, 0);
			} else if (lastMovement == 4) {
				anim.SetFloat ("MoveY", 0.0f);
				anim.SetFloat ("MoveX", 1.0f);
			//transform.position = new Vector3 (0.3f, 0, 0);
			}
		//}

	}
}
