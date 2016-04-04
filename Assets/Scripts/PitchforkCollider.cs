using UnityEngine;
using System.Collections;

public class PitchforkCollider : MonoBehaviour {
	GameObject player;
	Vector3 rotate;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rotate = new Vector3 (0, 0, 0);
		//this.GetComponent<EdgeCollider2D> ().enabled = false;
	}

	// Update is called once per frame
	void Update () {
		//this.GetComponent<EdgeCollider2D> ().enabled = false;
		//if (Input.GetButtonDown("attack")) {
		//	this.GetComponent<EdgeCollider2D>().enabled = true;
		//}
		if (PlayerMovement.lastMovementDirection == 1) {
			transform.rotation = Quaternion.Euler (0, 0, 90);
		} else if (PlayerMovement.lastMovementDirection == 2) {
			transform.rotation = Quaternion.Euler (0, 0, 270);
		} else if (PlayerMovement.lastMovementDirection == 3) {
			transform.rotation = Quaternion.Euler (0, 0, 180);
		} else {
			transform.rotation = Quaternion.Euler (0, 0, 0);
		}
	}
}
