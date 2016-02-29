using UnityEngine;
using System.Collections;

public class Hands : MonoBehaviour {

	Animator anim;
	//WeaponPickup weapon;

	// Use this for initialization
	void Start () {
		anim = this.transform.GetComponent<Animator> ();
		anim.SetBool("ifNothingPickedUp", true);
		//weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponPickup>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (WeaponPickup.getPickedUp() == true) {
			anim.SetBool ("ifNothingPickedUp", false);
		}
		else {
			anim.SetBool ("ifNothingPickedUp", true);
	}
}
}
