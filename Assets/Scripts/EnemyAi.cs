using UnityEngine;
using System.Collections;

public class EnemyAi : MonoBehaviour {

	Transform target;
	Transform enemyTransform;
	public float speed = 3f;
	public float rotationSpeed=3f;
	public int distanceToAttack = 10;
	private int follow = 0;
    public static int damage = 10;
	Vector3 oldPosition;
	Vector3 newPosition;
	Animator anim;
	public GameObject enemy;

    int firstRunUpdate = 0;
    int playerDied = 0;
	int enemyHealth;
	float locked;


    public void Start () {
		//obtain the game object Transform
		enemyTransform = this.GetComponent<Transform>();
		anim = this.GetComponent<Animator> ();
		enemyHealth = 10;
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		locked = 0f;
	}

	IEnumerator wait() {
		yield return new WaitForSeconds (0.5f);
	}

	private IEnumerator takeDamage() {
		enemyHealth -= 10;
		yield return null;
	} 

	void OnTriggerStay2D(Collider2D collision) {
		if (collision.tag == "SwordCollider" && Input.GetButtonDown("attack") && WeaponPickup.getHands() == false) {
			StartCoroutine(takeDamage ());
		}
	}

    void Update() {
		if(enemyHealth <= 0)
		{
			Destroy(this.gameObject);
		}

        if (firstRunUpdate == 0) {
            target = GameObject.FindWithTag("Player").transform;
        }

        if(firstRunUpdate != 0 && target == null) {
            playerDied = 1;
            print("Game Over");
        }

        firstRunUpdate = 1;
        if (playerDied == 0)
        {
            target = GameObject.FindWithTag("Player").transform;
            if (Vector3.Distance(target.position, enemyTransform.position) <= distanceToAttack || follow == 1)
            {
                follow = 1;
                anim.SetBool("ifFollowing", true);
                oldPosition = enemyTransform.position;
                //move towards the player
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                StartCoroutine(wait());
                newPosition = enemyTransform.position;
                if (((newPosition.y - oldPosition.y) < -0.0001f) && (newPosition.y - oldPosition.y) > -0.1f)
                {
                    anim.SetFloat("MoveX", 0.0f);
                    anim.SetFloat("MoveY", -1.0f);
                }
                else if ((((newPosition.x - oldPosition.x) > 0.01f) && (newPosition.x - oldPosition.x) < 0.1f) || ((newPosition.x - oldPosition.x) > -0.1f && (newPosition.x - oldPosition.x) < -0.001f))
                {
                    if ((newPosition.x - oldPosition.x) > -0.1f && (newPosition.x - oldPosition.x) < -0.0001f)
                    {
                        anim.SetFloat("MoveX", -1.0f);
                        anim.SetFloat("MoveY", 0.0f);
                    }
                    else {
                        anim.SetFloat("MoveX", 1.0f);
                        anim.SetFloat("MoveY", 0.0f);
                    }
                }
                else if ((((newPosition.y - oldPosition.y) > 0.0001f) && (newPosition.y - oldPosition.y) < 0.1f) || ((newPosition.y - oldPosition.y) > -0.1f) && ((newPosition.y - oldPosition.y)) > -0.0001f)
                {
                    if ((newPosition.y - oldPosition.y) > -0.1f && (newPosition.y - oldPosition.y) > -0.0001f)
                    {
                        anim.SetFloat("MoveX", 0.0f);
                        anim.SetFloat("MoveY", 1.0f);
                    }
                    else {
                        anim.SetFloat("MoveX", 0.0f);
                        anim.SetFloat("MoveY", -1.0f);
                    }
                }
                /*if (PlayerMovement.lastMovementDirection == 1) {
                    anim.SetFloat ("MoveX", 0.0f);
                    anim.SetFloat ("MoveY", 1.0f);
                } else if (PlayerMovement.lastMovementDirection == 2) {
                    anim.SetFloat ("MoveX", 0.0f);
                    anim.SetFloat ("MoveY", -1.0f);
                } else if (PlayerMovement.lastMovementDirection == 3) {
                    anim.SetFloat ("MoveX", -1.0f);
                    anim.SetFloat ("MoveY", 0.0f);
                } else if (PlayerMovement.lastMovementDirection == 4) {
                    anim.SetFloat ("MoveX", 1.0f);
                    anim.SetFloat ("MoveY", 0.0f);
                }*/
            }

        }
	}
}
