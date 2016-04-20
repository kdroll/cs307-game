using UnityEngine;
using System.Collections;

public class TheBossFollow : MonoBehaviour {
	Transform enemyTransform;
	GameObject player;
	Transform target;
	public float speed;

	// Use this for initialization
	void Start () {
		enemyTransform = gameObject.transform;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		target = player.transform;
		if (gameObject.GetComponent<Animator> ().GetBool ("IfLaserAttack") == false && gameObject.GetComponent<Animator> ().GetBool ("IfCircleAttack") == false && gameObject.GetComponent<Animator> ().GetBool ("IfFireAttack") == false && gameObject.GetComponent<Animator> ().GetBool ("IfVulnerable") == false) {
			Vector2 newPosition;
			transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);

			newPosition = enemyTransform.position;
		}



	}
}
