using UnityEngine;
using System.Collections;

public class Pitchfork : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = this.transform.GetComponent<Animator> ();
		anim.SetBool ("ifPitchforkEquipped", false);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.F)) {
			WeaponPickup.setPitchfork (false);
			WeaponPickup.setPickedUp (false);
			anim.SetBool ("ifPitchforkEquipped", false);
		}
		if (WeaponPickup.getPickedUp () == true) {
			if (WeaponPickup.getPitchfork() == true) {
				anim.SetBool ("ifPitchforkEquipped", true);
			} else {
				anim.SetBool ("ifPitchforkEquipped", false);
			}
		} else {
			anim.SetBool ("ifPitchforkEquipped", false);
		}

	}
}
