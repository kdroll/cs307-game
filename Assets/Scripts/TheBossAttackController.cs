using UnityEngine;
using System.Collections;

public class TheBossAttackController : MonoBehaviour {
	int attack;
	float attackCoolodwnStart; //timer
	float attackCooldown; 	//cooldown length
	Animator attackAnimator;
	public AnimationClip circle;
	public AnimationClip laser;
	public AnimationClip fire;
	public AnimationClip vulnerable;
	public static bool onlyOnce;
	// Use this for initialization
	void Start () {
		attackCoolodwnStart = 0f;
		attackCooldown = 3f;
		onlyOnce = false;
		attackAnimator = gameObject.GetComponent<Animator> ();
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;

	}
	IEnumerator wait() {
		yield return new WaitForSeconds (1.5f);
	}
	IEnumerator waitVulnerable() {
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		yield return new WaitForSeconds (vulnerable.length);
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		attackAnimator.SetBool ("IfVulnerable", false);
		attackAnimator.SetBool ("IfIdle", true);
	}
	IEnumerator waitLaser() {
		//yield return new WaitForSeconds (1.5f);
		attackAnimator.SetBool ("IfIdle", false);
		attackAnimator.SetBool ("IfLaserAttack", true);
		yield return new WaitForSeconds (laser.length);
		attackAnimator.SetBool ("IfLaserAttack",false);
		attackAnimator.SetBool ("IfIdle", true);
	}
	IEnumerator waitCircle() {
		//yield return new WaitForSeconds (1.5f);
		attackAnimator.SetBool ("IfIdle", false);
		attackAnimator.SetBool ("IfCircleAttack", true);	
		yield return new WaitForSeconds (circle.length);
		attackAnimator.SetBool ("IfCircleAttack", false);
		onlyOnce = false;
		attackAnimator.SetBool ("IfIdle", true);
	}
	IEnumerator waitFire() {
		//yield return new WaitForSeconds (1.5f);
		attackAnimator.SetBool ("IfIdle", false);
		attackAnimator.SetBool ("IfFireAttack", true);
		yield return new WaitForSeconds (fire.length);
		attackAnimator.SetBool ("IfFireAttack", false);
		attackAnimator.SetBool ("IfIdle", true);

	}

	//attackAnimator.SetBool ("IfLaserAttack", false);

	// Update is called once per frame
	void Update () { 

		// then where you call the spell or whatever it is you call
		// check that Time.time is greater than fireSpellStart
		// + fireSpellCooldown
		// and if it is allow that spell to be cast but record the start time

		if (Time.time > attackCoolodwnStart + attackCooldown) {
			attackAnimator.SetBool ("IfCircleAttack", false);
			attackAnimator.SetBool ("IfLaserAttack",false);
			attackAnimator.SetBool ("IfFireAttack", false);
			attackAnimator.SetBool ("IfIdle", true);
			attackCoolodwnStart = Time.time;
			attack = Random.Range (1, 6);
			//attack = 2;
			print (attack);
			if (attack == 1 && attackAnimator.GetBool("IfVulnerable") == false) {
				//attackAnimator.SetBool ("IfIdle", false);
				//attackAnimator.SetBool ("IfLaserAttack", true);
				StartCoroutine (waitLaser ());
			} else if (attack == 2 && attackAnimator.GetBool("IfVulnerable") == false) {
				//attackAnimator.SetBool ("IfIdle", false);
				//attackAnimator.SetBool ("IfCircleAttack", true);	
				StartCoroutine (waitCircle ());
			} else if (attack == 3 && attackAnimator.GetBool("IfVulnerable") == false) {
				//attackAnimator.SetBool ("IfIdle", false);
				//attackAnimator.SetBool ("IfFireAttack", true);
				StartCoroutine (waitFire ());
			} else if (attack == 4 && attackAnimator.GetBool("IfVulnerable") == false) {
				attackAnimator.SetBool ("IfIdle", false);
				attackAnimator.SetBool ("IfVulnerable", true);
				StartCoroutine (waitVulnerable ());
			} else {
				attackAnimator.SetBool ("IfCircleAttack", false);
				attackAnimator.SetBool ("IfLaserAttack",false);
				attackAnimator.SetBool ("IfFireAttack", false);
				attackAnimator.SetBool ("IfIdle", true);
			}

		}
	}
}
