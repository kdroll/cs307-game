using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	Animator anim;
	//bool isAttacking;
	// Use this for initialization
	public float cooldown = 0.0f;
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (cooldown <= Time.time) {
			GameObject.FindGameObjectWithTag ("Pitchfork").GetComponent<Collider2D> ().isTrigger = true;
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			//isAttacking = true;
		}
		/*handleAttack ();
		if (isAttacking) {
			if (anim.GetBool ("ifPitchforkEquipped") && anim.GetBool ("isWalking")) {
				if (anim.GetFloat ("MoveX") != 0.0f) {
					anim.SetFloat ("AttackX", anim.GetFloat ("MoveX"));
					anim.SetFloat ("AttackY", 0.0f);
				} else {
					anim.SetFloat ("AttackY", anim.GetFloat ("MoveY"));
					anim.SetFloat ("AttackX", 0.0f);
				}
				//if (playerMovement.lastMovementDirection == 
			}
		}
		if (anim.GetBool ("ifPitchforkEquipped")) {
			if (anim.GetFloat ("IdleX") != 0.0f) {
				anim.SetFloat ("AttackX", anim.GetFloat ("IdleX"));
				anim.SetFloat ("AttackY", 0.0f);
			} else {
				anim.SetFloat ("AttackY", anim.GetFloat ("IdleY"));
				anim.SetFloat ("AttackX", 0.0f);
			}
		}*/
		if (Input.GetKeyDown (KeyCode.F)) {
			cooldown = Time.time + 2.0f;
			anim.SetBool ("ifPitchforkEquipped", false);
			GameObject.FindGameObjectWithTag ("Pitchfork").GetComponent<Collider2D> ().isTrigger = false;
			GameObject.FindGameObjectWithTag ("Pitchfork").transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
			GameObject.FindGameObjectWithTag ("Pitchfork").GetComponent<Rigidbody2D> ().AddForce (new Vector2(Random.value, Random.value).normalized * 300.0f);
			//isAttacking = false;
		}
		//anim.SetBool ("isAttacking", false);
	}

	/*public void handleAttack() {
		if (isAttacking) {
			anim.SetTrigger ("isAttacking");
			isAttacking = false;
		}
	}*/
}
