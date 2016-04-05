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
    Animator anim2;
	public GameObject enemy;
    public AnimationClip hitAnimation;
	bool ifThereIsAnything = false;
	RaycastHit2D hit;

    int firstRunUpdate = 0;
    int playerDied = 0;
	int enemyHealth;
	float locked;
    public static int gold = 0;


    public void Start () {
		//obtain the game object Transform
		enemyTransform = this.GetComponent<Transform>();
		anim = this.GetComponent<Animator> ();
		enemyHealth = 30;
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		locked = 0f;
	}

	IEnumerator wait() {
		yield return new WaitForSeconds (0.3f);
        gold += 10;
		Destroy(this.gameObject);
	}

    IEnumerator waitHit()
    {
        yield return new WaitForSeconds(0.2f);
		anim.SetBool("ifHit", false);
    }

    private IEnumerator takeDamage() {
        enemyHealth -= 10;
        print("enemyHealth = " + enemyHealth);
        locked = 0;
       // anim.SetBool("ifHit", false);
		yield return null;
	} 

	void OnTriggerStay2D(Collider2D collision) {
        //print(Input.GetButtonDown("attack"));
		if (collision.tag == "SwordCollider" && (Input.GetButtonDown("attack") || Input.GetButtonDown("B")) && WeaponPickup.getHands() == false && locked == 1 && WeaponPickup.getSword() == true) {
            anim.SetBool("ifHit", true);
            if (anim.GetFloat("MoveY") == 1.0f) {
                anim.SetFloat("HitY", 1.0f);
            } else if (anim.GetFloat("MoveY") == -1.0f) {
                anim.SetFloat("HitY", -1.0f);
            } else if (anim.GetFloat("MoveX") == 1.0f) {
                anim.SetFloat("HitX", 1.0f);
            } else if (anim.GetFloat("MoveX") == -1.0f) {
                anim.SetFloat("HitX", -1.0f);
            }
            StartCoroutine(takeDamage ());
            StartCoroutine(waitHit());
   

		}
		if (collision.tag == "PitchforkCollider" && (Input.GetButtonDown("attack") || Input.GetButtonDown("B")) && WeaponPickup.getHands() == false && locked == 1 && WeaponPickup.getPitchfork() == true) {
			anim.SetBool("ifHit", true);
			if (anim.GetFloat("MoveY") == 1.0f) {
				anim.SetFloat("HitY", 1.0f);
			} else if (anim.GetFloat("MoveY") == -1.0f) {
				anim.SetFloat("HitY", -1.0f);
			} else if (anim.GetFloat("MoveX") == 1.0f) {
				anim.SetFloat("HitX", 1.0f);
			} else if (anim.GetFloat("MoveX") == -1.0f) {
				anim.SetFloat("HitX", -1.0f);
			}
			StartCoroutine(takeDamage ());
			StartCoroutine(waitHit());
		}
	}

    void Update() {
		if(enemyHealth <= 0)
		{
			StartCoroutine (wait());
		}

        if (firstRunUpdate == 0) {
            target = GameObject.FindWithTag("Player").transform;
        }

        if(firstRunUpdate != 0 && target == null) {
            playerDied = 1;
            print("Game Over");
        }

        if (locked >= 1)
        {
            locked = 1;
        }
        else
        {
            locked += .02f;
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
				transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
				/*Transform leftRay = transform;
				Transform rightRay = transform;
				//Use Phyics.RayCast to detect the obstacle
				if (Physics.Raycast (enemyTransform.position, new Vector3(1,0,0), 10) || Physics.Raycast(enemyTransform.position, new Vector3(1,0,0), 10)) {
					if (hit.collider.gameObject.CompareTag("Obstacle")){
						ifThereIsAnything = true;
						//transform.position = Vector2.MoveTowards (transform.position, new Vector2(transform.position.x + 7f, transform.position.y), speed * Time.deltaTime);
						transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
					}
				}*/
				//Debug.DrawRay (hit, transform.position, Color.red);

                //StartCoroutine(wait());
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
            }

        }
	}
}
