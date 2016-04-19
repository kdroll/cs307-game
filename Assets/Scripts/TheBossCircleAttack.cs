using UnityEngine;
using System.Collections;

public class TheBossCircleAttack : MonoBehaviour {

	public GameObject circleParticle;
	GameObject circleAttack;
	int times = 0;
	// Use this for initialization
	void Start () {
		circleAttack = (GameObject)Instantiate (circleParticle, gameObject.transform.position, Quaternion.identity);
		circleAttack.SetActive (false);
	}

	//IEnumerator wait() {
	//	yield return new WaitForSeconds (0.3f);
	//	StartCoroutine (fireballs ());
	//}
	IEnumerator fireballs() {
		circleAttack.SetActive (true);
		times = 1;
		yield return new WaitForSeconds (3f);
		circleAttack.SetActive (false);
		times = 0;
		print ("its false");
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag ("TheBoss").GetComponent<Animator> ().GetBool ("IfCircleAttack") == true && times != 1) {
			circleAttack.transform.position = gameObject.transform.position;
			StopCoroutine (fireballs ());
			StartCoroutine (fireballs ());

			//StopCoroutine (wait ());
			//StartCoroutine (wait ());
		}

	
	}
}
