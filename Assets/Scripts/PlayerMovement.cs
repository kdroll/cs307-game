﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private Vector3 movementVector;
    private CharacterController characterController;

    public static float speedModifier = 1;
    public static int numSpeedUpgrades = 0;
    public static float maxSpeedMod = 2;
    public float sprintModifier;
	private int currentSpeed;
	Animator anim;
	private bool sprinting;
	private bool attacking;
	Vector3 direction = new Vector3 (0, 0, 0);
	public static int lastMovementDirection;
	int layer;
    public bool changedMaxSpeed = false;

	// Use this for initialization
	void Start () {
        //characterController = GetComponent<CharacterController>();
        sprintModifier = 3;
        anim = this.transform.GetComponent<Animator>();
		sprinting = false;
	}

	public static int getLastMovementDirection() {
		return lastMovementDirection;
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (changedMaxSpeed == false && PlayerHealth.perks[2] == 1) {
            maxSpeedMod = maxSpeedMod + (PlayerHealth.perks[2] * (.2f));
            changedMaxSpeed = true;
        }

        direction = new Vector3 (0, 0, 0);
		//lastMovementDirection = 0;
		//anim.SetFloat("speed", 0f);
		//anim.SetBool ("running", false);
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow) || Input.GetAxis("LeftJoystickY") < 0) {
			direction += Vector3.up;
			anim.SetFloat ("MoveY", 1.0f);
			anim.SetFloat ("MoveX", 0.0f);
			lastMovementDirection = 1;
		}

		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow) || Input.GetAxis("LeftJoystickY") > 0) {
			direction += Vector3.down;
			anim.SetFloat ("MoveY", -1.0f);
			anim.SetFloat ("MoveX", 0.0f);
			lastMovementDirection = 2;
		}

		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow) || Input.GetAxis("LeftJoystickX") < 0) {
			direction += Vector3.left;
			anim.SetFloat ("MoveX", -1.0f);
			anim.SetFloat ("MoveY", 0.0f);
			lastMovementDirection = 3;
		}

		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow) || Input.GetAxis("LeftJoystickX") > 0) {
			direction += Vector3.right;
			anim.SetFloat ("MoveX", 1.0f);
			anim.SetFloat ("MoveY", 0.0f);
			lastMovementDirection = 4;
		}

		if (direction != new Vector3 (0, 0, 0)) {
			anim.SetBool ("isWalking", true);
			if (!Running ()) {
                //anim.SetBool("running", false);
                //GetComponent<CharacterController>().transform.position += direction.normalized * speedMultiplier * Time.deltaTime;
				//GetComponent<Rigidbody2D>().transform.position += direction.normalized * speedMultiplier * Time.deltaTime;
				transform.Translate(direction.normalized * sprintModifier * speedModifier * Time.deltaTime);
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
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.JoystickButton4)) {
            //GetComponent<Rigidbody2D>().transform.position += direction.normalized * (speedMultiplier + 2) * Time.deltaTime;
			transform.Translate(direction.normalized * (sprintModifier + 2) * speedModifier * Time.deltaTime);
            //GetComponent<CharacterController>().transform.position += direction.normalized * speedMultiplier * Time.deltaTime;

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