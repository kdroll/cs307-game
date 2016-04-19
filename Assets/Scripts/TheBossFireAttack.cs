using UnityEngine;
using System.Collections;

public class TheBossFireAttack : MonoBehaviour {

	public GameObject fireBreath;
	GameObject effect;
	int times = 0;
	// Use this for initialization
	void Start () {
		effect = (GameObject)Instantiate (fireBreath, gameObject.transform.position, Quaternion.identity);
		effect.SetActive (false);
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	
	}
	IEnumerator wait() {
		yield return new WaitForSeconds (0.5f);
		StartCoroutine (fireAttack ());
		times = 0;
	}
	IEnumerator fireAttack() {
		effect.SetActive (true);
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		yield return new WaitForSeconds (1f);
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		effect.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag ("TheBoss").GetComponent<Animator> ().GetBool ("IfFireAttack") == true) {
			effect.transform.position = gameObject.transform.position;
			StartCoroutine (wait ());
		}
	
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player" && PlayerHealth.locked == 1) {
			StartCoroutine (PlayerHealth.takeDamage ());
			print (times);
		}
	}
}
