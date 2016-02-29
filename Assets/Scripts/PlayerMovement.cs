using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speedMultiplier; 
	private int currentSpeed;
	Animator anim;
	private bool sprinting;
	private bool attacking;
	Vector3 direction = new Vector3 (0, 0, 0);
	public int lastMovementDirection;
	int layer;

	// Use this for initialization
	void Start () {
		anim = this.transform.GetComponent<Animator>();
		sprinting = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		direction = new Vector3 (0, 0, 0);
		//lastMovementDirection = 0;
		//anim.SetFloat("speed", 0f);
		//anim.SetBool ("running", false);
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			direction += Vector3.up;
			anim.SetFloat ("MoveY", 1.0f);
			anim.SetFloat ("MoveX", 0.0f);
			lastMovementDirection = 1;
		}

		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			direction += Vector3.down;
			anim.SetFloat ("MoveY", -1.0f);
			anim.SetFloat ("MoveX", 0.0f);
			lastMovementDirection = 2;
		}

		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			direction += Vector3.left;
			anim.SetFloat ("MoveX", -1.0f);
			anim.SetFloat ("MoveY", 0.0f);
			lastMovementDirection = 3;
		}

		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			direction += Vector3.right;
			anim.SetFloat ("MoveX", 1.0f);
			anim.SetFloat ("MoveY", 0.0f);
			lastMovementDirection = 4;
		}

		if (direction != new Vector3 (0, 0, 0)) {
			anim.SetBool ("isWalking", true);
			if (!Running ()) {
				//anim.SetBool("running", false);
				GetComponent<Rigidbody2D> ().transform.position += direction.normalized * speedMultiplier * Time.deltaTime;
				//anim.SetFloat("speed", Mathf.Abs(direction.x) + Mathf.Abs (direction.y));
			}
		} else {
			anim.SetBool ("isWalking", false);
			if (lastMovementDirection == 1) {
				anim.SetFloat ("IdleY", 1.0f);
				anim.SetFloat ("IdleX", 0.0f);
			}
			else if (lastMovementDirection == 4) {
				anim.SetFloat ("IdleX", 1.0f);
				anim.SetFloat ("IdleY", 0.0f);
			}
			else if (lastMovementDirection == 2) {
				anim.SetFloat ("IdleY", -1.0f);
				anim.SetFloat ("IdleX", 0.0f);
			}
			else if (lastMovementDirection == 3) {
				anim.SetFloat ("IdleX", -1.0f);
				anim.SetFloat ("IdleY", 0.0f);
			}
		}
	}


	bool Running() {
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			GetComponent<Rigidbody2D>().transform.position += direction.normalized * (speedMultiplier + 2) * Time.deltaTime;
			//anim.SetFloat("speed", Mathf.Abs(direction.x) + Mathf.Abs (direction.y));
			sprinting = true;
		}
		else {
			sprinting = false;
			//anim.SetBool("running", false);
		}
		if (sprinting) {
			//anim.SetBool("running", true);
			return true;
		}
		return false;
	}


}