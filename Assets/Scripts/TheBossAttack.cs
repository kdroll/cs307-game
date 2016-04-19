using UnityEngine;
using System.Collections;

public class TheBossAttack : MonoBehaviour {

	// Use this for initialization
	LineRenderer line;
	LineRenderer line2;
	LineRenderer line3;
	LineRenderer line4;
	//int done = 0;
	void Start () {
		line = gameObject.GetComponent<LineRenderer> ();
		line.sortingOrder = 20;
		line2 = gameObject.GetComponent<LineRenderer> ();
		line2.sortingOrder = 20;
		line3 = gameObject.GetComponent<LineRenderer> ();
		line3.sortingOrder = 20;
		line4 = gameObject.GetComponent<LineRenderer> ();
		line4.sortingOrder = 20;
		line.enabled = false;
		line2.enabled = false;
		line3.enabled = false;
		line4.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag ("TheBoss").GetComponent<Animator> ().GetBool ("IfLaserAttack") == true) {
			StopCoroutine (FireLaser ());
			StartCoroutine (FireLaser ());
		}
	
	}

	IEnumerator FireLaser() {
		line.enabled = true;
		line2.enabled = true;
		line3.enabled = true;
		line4.enabled = true;

		while((GameObject.FindGameObjectWithTag ("TheBoss").GetComponent<Animator> ().GetBool ("IfLaserAttack") == true)) {
			//if (done == 0) {
				Ray2D ray = new Ray2D (transform.position, new Vector2 (0, -400));
				Ray2D ray2 = new Ray2D (transform.position, new Vector2 (0, 410));
				Ray2D ray3 = new Ray2D (transform.position, new Vector2 (600, 0));
				Ray2D ray4 = new Ray2D (transform.position, new Vector2 (-600, 0));
				RaycastHit2D hit = new RaycastHit2D ();
				RaycastHit2D hit2 = new RaycastHit2D ();
				RaycastHit2D hit3 = new RaycastHit2D ();
				RaycastHit2D hit4 = new RaycastHit2D ();
				//line2.SetPosition (0, ray2.origin);
				line.SetPosition (0, ray.origin);
				line2.SetPosition (1, ray2.origin);
				line3.SetPosition (2, ray3.origin);
				line4.SetPosition (3, ray4.origin);
				//line2.SetPosition(1, ray.GetPoint(100));
				hit = Physics2D.BoxCast (new Vector2 (transform.position.x - 0.6f, transform.position.y), new Vector2 (3, 1), 0f, new Vector2 (0, -400), 100);
				//print (hit.collider.tag);
				hit2 = Physics2D.BoxCast (new Vector2 (transform.position.x - 0.5f, transform.position.y), new Vector2 (4, 1), 0f, new Vector2 (0, 400), 100);
				hit3 = Physics2D.BoxCast (new Vector2 (transform.position.x, transform.position.y - 1.8f), new Vector2 (1, 4), 0f, new Vector2 (-400, 0), 100);
				hit4 = Physics2D.BoxCast (new Vector2 (transform.position.x, transform.position.y - 1.8f), new Vector2 (1, 4), 0f, new Vector2 (400, 0), 100);
				Debug.DrawRay (hit3.point, transform.position, Color.green);
				Debug.DrawRay (hit4.point, transform.position, Color.green);
			if ((hit.collider.tag == "Player" || hit2.collider.tag == "Player" || hit3.collider.tag == "Player" || hit4.collider.tag == "Player") && PlayerHealth.locked == 1) {
					print ("in here");
					StartCoroutine (PlayerHealth.takeDamage ());
				}

				
				line.SetPosition (0, ray.GetPoint (500));
				line2.SetPosition (1, ray2.GetPoint (500));
				line3.SetPosition (2, ray3.GetPoint (500));
				line4.SetPosition (3, ray4.GetPoint (500));
				//done = 1;
				yield return null;
			//}
		}
			line.enabled = false;
		line2.enabled = false;
		line3.enabled = false;
		line4.enabled = false;
			}
}
