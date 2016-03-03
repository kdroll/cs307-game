using UnityEngine;
using System.Collections;

public class Pitchfork : MonoBehaviour {
	Animator anim;
	int lastMovement;
	// Use this for initialization
	void Start () {
		anim = this.transform.GetComponent<Animator> ();
		anim.SetBool ("ifPitchforkEquipped", false);
	}

	// Update is called once per frame
	void Update () {
		anim.SetBool ("ifAttacking", false);
		if (Input.GetKey (KeyCode.F)) {
			WeaponPickup.setPitchfork (false);
			WeaponPickup.setPickedUp (false);
			anim.SetBool ("ifPitchforkEquipped", false);
			GameObject.FindGameObjectWithTag ("Pitchfork").GetComponent<Collider2D> ().isTrigger = false;
			GameObject.FindGameObjectWithTag ("Pitchfork").transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
			GameObject.FindGameObjectWithTag ("Pitchfork").GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((Random.value * 2) - 1, (Random.value * 2) - 1).normalized * 100.0f);
		}
		if (WeaponPickup.getPickedUp () == true) {
			if (WeaponPickup.getPitchfork () == true) {
				anim.SetBool ("ifPitchforkEquipped", true);
			} else {
				anim.SetBool ("ifPitchforkEquipped", false);
			}
		} else {
			anim.SetBool ("ifPitchforkEquipped", false);
		}
		if (WeaponPickup.getPitchfork () == true && Input.GetButtonDown ("attack")) {
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
