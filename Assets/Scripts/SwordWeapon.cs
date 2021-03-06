﻿using UnityEngine;
using System.Collections;

public class SwordWeapon : MonoBehaviour {
	Animator anim;
	int lastMovement;
	// Use this for initialization
	void Start () {
		anim = this.transform.GetComponent<Animator> ();
		anim.SetBool ("ifSwordEquipped", false);
	}

	// Update is called once per frame
	void Update () {
		anim.SetBool ("ifAttacking", false);
		if ((Input.GetKey (KeyCode.F) || Input.GetKey(KeyCode.JoystickButton3)) && WeaponPickup.getSword() == true) {
			WeaponPickup.setSword (false);
			WeaponPickup.setPickedUp (false);
			anim.SetBool ("ifSwordEquipped", false);
			GameObject.FindGameObjectWithTag ("Sword").GetComponent<Collider2D> ().isTrigger = false;
			GameObject.FindGameObjectWithTag ("Sword").transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
			GameObject.FindGameObjectWithTag ("Sword").GetComponent<Rigidbody2D> ().AddForce (new Vector2 ((Random.value * 2) - 1, (Random.value * 2) - 1).normalized * 100.0f);
		}
		if (WeaponPickup.getPickedUp () == true) {
			if (WeaponPickup.getSword () == true) {
				anim.SetBool ("ifSwordEquipped", true);
			} else {
				anim.SetBool ("ifSwordEquipped", false);
			}
		} else {
			anim.SetBool ("ifSwordEquipped", false);
		}
		if (WeaponPickup.getSword () == true && (Input.GetButtonDown ("attack") || Input.GetButtonDown("B"))) {
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
