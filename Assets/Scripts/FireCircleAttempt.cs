using UnityEngine;
using System.Collections;

public class FireCircleAttempt : MonoBehaviour {
	public GameObject fireball;
	GameObject fireballSpawned;
	int numberOfFireballs;
	Vector2 centerPosition;
	// Use this for initialization
	void Start () {
		numberOfFireballs = 30;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag ("TheBoss").GetComponent<Animator> ().GetBool ("IfCircleAttack") == true && TheBossAttackController.onlyOnce == false) {
			centerPosition = gameObject.transform.position;
			int pointNum;
			for (pointNum = 0; pointNum < numberOfFireballs; pointNum++) {
				float i = (pointNum * 1.0f) / numberOfFireballs;
				float angle = i * Mathf.PI * 2;
				float x = Mathf.Sin (angle);
				float y = Mathf.Cos (angle);
				Vector2 pos = new Vector2 (x, y) +centerPosition;
				fireballSpawned = (GameObject)Instantiate (fireball, pos, Quaternion.identity);
				fireballSpawned.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (x, y) * 500);
			}
			TheBossAttackController.onlyOnce = true;
		}
	
	}
}
