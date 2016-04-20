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
		times = 1;
		circleAttack.SetActive (true);
		yield return new WaitForSeconds (3f);
		times = 0;
		circleAttack.SetActive (false);
		//print ("its false");
	}
	
	// Update is called once per frame
	void Update () {
		//print (times);
		if (GameObject.FindGameObjectWithTag ("TheBoss").GetComponent<Animator> ().GetBool ("IfCircleAttack") == true && times != 1) {
			circleAttack.transform.position = gameObject.transform.position;
			//StopCoroutine (fireballs ());
			//print("in if statement");
			StartCoroutine (fireballs ());

			//StopCoroutine (wait ());
			//StartCoroutine (wait ());
		}

	
	}
}
