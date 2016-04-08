using UnityEngine;
using System.Collections;

public class NunchuckWeapon : MonoBehaviour {
	Animator anim;
	int lastMovement;
	// Use this for initialization
	void Start () {
		anim = this.transform.GetComponent<Animator> ();
		anim.SetBool ("ifNunchucksEquipped", false);
	}

	// Update is called once per frame
	void Update () {
		anim.SetBool ("ifAttacking", false);
		if ((Input.GetKey (KeyCode.F) || Input.GetKey(KeyCode.JoystickButton3))  && WeaponPickup.getNunchuck() == true) {
			WeaponPickup.setNunchuck (false);
			WeaponPickup.setPickedUp (false);
			anim.SetBool ("ifNunchucksEquipped", false);
			GameObject.FindGameObjectWithTag ("Nunchuck").GetComponent<Collider2D> ().isTrigger = false;
			GameObject.FindGameObjectWithTag ("Nunchuck").transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
			GameObject.FindGameObjectWithTag ("Nunchuck").GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((Random.value * 2) - 1, (Random.value * 2) - 1).normalized * 100.0f);
		}
		if (WeaponPickup.getPickedUp () == true) {
			if (WeaponPickup.getNunchuck () == true) {
				anim.SetBool ("ifNunchucksEquipped", true);
			} else {
				anim.SetBool ("ifNunchucksEquipped", false);
			}
		} else {
			anim.SetBool ("ifNunchucksEquipped", false);
		}
		if (WeaponPickup.getNunchuck () == true && (Input.GetButtonDown ("attack") || Input.GetButtonDown("B"))) {
			anim.SetBool ("ifAttacking", true);
			lastMovement = PlayerMovement.getLastMovementDirection ();
			if (lastMovement == 1) {
				anim.SetFloat ("AttackY", 1.0f);
				anim.SetFloat ("AttackX", 0.0f);
			} else if (lastMovement == 2) {
				anim.SetFloat ("AttackY", -1.0f);
				anim.SetFloat ("AttackX", 0.0f);
			} else if (lastMovement == 3) {
				anim.SetFloat ("AttackY", 0.0f);
				anim.SetFloat ("AttackX", -1.0f);
			} else if (lastMovement == 4) {
				anim.SetFloat ("AttackY", 0.0f);
				anim.SetFloat ("AttackX", 1.0f);
			}
		}

	}
}
