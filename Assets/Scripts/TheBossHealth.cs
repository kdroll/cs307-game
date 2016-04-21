using UnityEngine;
using System.Collections;

public class TheBossHealth : MonoBehaviour {
	int grenadeTag;
	Animator anim;
	public static float enemyHealth;
	public static float startHealth;
	float locked;

	void Start () {
		grenadeTag = 0;
		anim = gameObject.GetComponent<Animator> ();
		startHealth = 20f;
		enemyHealth = 20f;
		locked = 0;

	}
	IEnumerator waitHit()
	{
		yield return new WaitForSeconds(0.2f);
		anim.SetBool("IfHit", false);
	}
	private IEnumerator takeDamage() {
		if (grenadeTag == 1) {
			enemyHealth -= 30 * PlayerAttack.damageModifier + (PlayerHealth.perks[5] * 20);
			grenadeTag = 0;
		} else {
			enemyHealth -= 10 * PlayerAttack.damageModifier;
		}
		print("enemyHealth = " + enemyHealth);
		locked = 0;
		// anim.SetBool("ifHit", false);
		if (enemyHealth <= 0) {
			gameObject.SetActive (false);
			print ("is dead");
			//death.Play ();
		} else {
			print (enemyHealth);
			//anim.SetBool ("IfHit", false);
			//hurt.Play ();
		}
		//Vector3 dir1 = -(target.position - transform.position);
		//enemyRigid.AddForce (dir1 * 400f);
		yield return null;
	} 
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Arrow") {
			anim.SetBool("IfHit", true);
			print ("in collision field");
			StartCoroutine (takeDamage ());
			StartCoroutine(waitHit());

		} 
	}

	/*void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Grenade") {
			anim.SetBool("ifHit", true);
			if (anim.GetFloat("MoveY") == 1.0f) {
				anim.SetFloat("HitY", 1.0f);
			} else if (anim.GetFloat("MoveY") == -1.0f) {
				anim.SetFloat("HitY", -1.0f);
			} else if (anim.GetFloat("MoveX") == 1.0f) {
				anim.SetFloat("HitX", 1.0f);
			} else if (anim.GetFloat("MoveX") == -1.0f) {
				anim.SetFloat("HitX", -1.0f);
			}
			grenadeTag = 1;
			StartCoroutine (takeDamage ());
			StartCoroutine(waitHit());
			Vector3 dir1 = -(Grenade.position - enemyTransform.position);
			enemyRigid.AddForce (dir1 * 600f);
		} else if (coll.tag == "FireballExplosion") {
			anim.SetBool("ifHit", true);
			if (anim.GetFloat("MoveY") == 1.0f) {
				anim.SetFloat("HitY", 1.0f);
			} else if (anim.GetFloat("MoveY") == -1.0f) {
				anim.SetFloat("HitY", -1.0f);
			} else if (anim.GetFloat("MoveX") == 1.0f) {
				anim.SetFloat("HitX", 1.0f);
			} else if (anim.GetFloat("MoveX") == -1.0f) {
				anim.SetFloat("HitX", -1.0f);
			}
			StartCoroutine(takeDamage());
			StartCoroutine(waitHit());
			Vector3 dir1 = -(GameObject.FindGameObjectWithTag("Player").transform.position - enemyTransform.position);
			fireballTag = 1;
			enemyRigid.AddForce(dir1 * 600f);
		}
	}*/
	void OnTriggerStay2D(Collider2D collision) {
		//print(Input.GetButtonDown("attack"));
		if (collision.tag == "SwordCollider" && (Input.GetButtonDown("attack") || Input.GetButtonDown("B")) && WeaponPickup.getHands() == false && locked == 1 && WeaponPickup.getSword() == true) {
			anim.SetBool("IfHit", true);
			StartCoroutine(takeDamage ());
			StartCoroutine(waitHit());


		}
		if (collision.tag == "PitchforkCollider" && (Input.GetButtonDown("attack") || Input.GetButtonDown("B")) && WeaponPickup.getHands() == false && locked == 1 && WeaponPickup.getPitchfork() == true) {
			anim.SetBool("IfHit", true);

			StartCoroutine(takeDamage ());
			StartCoroutine(waitHit());
		}
		if (collision.tag == "NunchuckCollider" && (Input.GetButtonDown("attack") || Input.GetButtonDown("B")) && WeaponPickup.getHands() == false && WeaponPickup.getNunchuck() == true && locked > 0.5f) {
			anim.SetBool("IfHit", true);
			/*if (anim.GetFloat("MoveY") == 1.0f) {
				anim.SetFloat("HitY", 1.0f);
			} else if (anim.GetFloat("MoveY") == -1.0f) {
				anim.SetFloat("HitY", -1.0f);
			} else if (anim.GetFloat("MoveX") == 1.0f) {
				anim.SetFloat("HitX", 1.0f);
			} else if (anim.GetFloat("MoveX") == -1.0f) {
				anim.SetFloat("HitX", -1.0f);
			}*/
			StartCoroutine(takeDamage ());
			StartCoroutine(waitHit());
		}
		/*if (collision.tag == "NunchuckCollider" && (Input.GetButtonDown("attack") || Input.GetButtonDown("B")) && WeaponPickup.getHands() == false && locked == 1 && WeaponPickup.getNunchuck() == true) {
			anim.SetBool("ifHit", true);
			if (anim.GetFloat("MoveY") == 1.0f) {
				anim.SetFloat("HitY", 1.0f);
			} else if (anim.GetFloat("MoveY") == -1.0f) {
				anim.SetFloat("HitY", -1.0f);
			} else if (anim.GetFloat("MoveX") == 1.0f) {
				anim.SetFloat("HitX", 1.0f);
			} else if (anim.GetFloat("MoveX") == -1.0f) {
				anim.SetFloat("HitX", -1.0f);
			}
			StartCoroutine(takeDamage ());
			StartCoroutine(waitHit());
		}*/
	}

	void Update() {
		if(enemyHealth <= 0)
		{
			print ("dead");
			//StartCoroutine (wait());
		}

		//if (firstRunUpdate == 0) {
		//	target = GameObject.FindWithTag("Player").transform;
		//}

		//if(firstRunUpdate != 0 && target == null) {
		//	playerDied = 1;
			//print("Game Over");
		//}

		if (locked >= 1)
		{
			locked = 1;
		}
		else
		{
			locked += .02f;
		}
	
}
}