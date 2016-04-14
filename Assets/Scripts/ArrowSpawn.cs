using UnityEngine;
using System.Collections;

public class ArrowSpawn : MonoBehaviour {
	GameObject player;
	Vector3 rotate;
	public GameObject bowPlacement;
	public GameObject arrow;
	GameObject projectile;
	Vector3 dir;
	public static int numOfArrows = 0;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rotate = new Vector3 (0, 0, 0);
		//this.GetComponent<EdgeCollider2D> ().enabled = false;
	}

	void attack() {
		if ((Input.GetButtonDown("attack") || Input.GetButtonDown("B")) && WeaponPickup.getBow() == true && numOfArrows == 0) {
			projectile = (GameObject)Instantiate(arrow, bowPlacement.transform.position + dir, transform.rotation);
			numOfArrows = 1;
			projectile.SetActive (true);
			projectile.GetComponent<Rigidbody2D> ().AddForce (dir * 5000f);
		}

	}

	// Update is called once per frame
	void Update () {
		//this.GetComponent<EdgeCollider2D> ().enabled = false;
		//if (Input.GetButtonDown("attack")) {
		//	this.GetComponent<EdgeCollider2D>().enabled = true;
		//}
		if (PlayerMovement.lastMovementDirection == 1) {
			transform.rotation = Quaternion.Euler (0, 0, 90);
			//transform.position = new Vector3 (transform.position.x, transform.position.y + 0.3f, 0);
			dir = new Vector3 (0, 0.3f, 0);
			attack ();
		} else if (PlayerMovement.lastMovementDirection == 2) {
			transform.rotation = Quaternion.Euler (0, 0, 270);
			dir = new Vector3(0, -0.3f, 0);
			attack ();
		} else if (PlayerMovement.lastMovementDirection == 3) {
			transform.rotation = Quaternion.Euler (0, 0, 180);
			dir = new Vector3(-0.3f, 0, 0);
			attack ();
		} else {
			transform.rotation = Quaternion.Euler (0, 0, 0);
			dir = new Vector3 (0.3f, 0, 0);
			attack ();
		}
	}
}

