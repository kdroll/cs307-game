using UnityEngine;
using System.Collections;

public class TheBossAttack : MonoBehaviour {

	// Use this for initialization
	LineRenderer line;
	void Start () {
		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag ("TheBoss").GetComponent<Animator> ().GetBool ("IfLaserAttack") == true) {
			StopCoroutine (FireLaser ());
			StartCoroutine (FireLaser ());
		}
	
	}

	IEnumerator FireLaser() {
		print ("in here");
		line.enabled = true;
		while((GameObject.FindGameObjectWithTag ("TheBoss").GetComponent<Animator> ().GetBool ("IfLaserAttack") == true)) {
			Ray2D ray = new Ray2D(transform.position, new Vector2(0,4));
			print("in laser");
			line.SetPosition(0, ray.origin);
			line.SetPosition(1, ray.GetPoint(100));
			yield return null;
		}
			line.enabled = false;
			}
}
