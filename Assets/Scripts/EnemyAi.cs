using UnityEngine;
using System.Collections;
using System;

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
	float enemyHealth;
	float locked;
    public static int gold = 100000;


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
	IEnumerator waitsleep(Transform transform, Vector3 go) {
		transform.position = Vector2.MoveTowards (transform.position, go, speed * Time.deltaTime);
		yield return new WaitForSeconds (1f);
	}

    IEnumerator waitHit()
    {
        yield return new WaitForSeconds(0.2f);
		anim.SetBool("ifHit", false);
    }

    private IEnumerator takeDamage() {
        enemyHealth -= 10 * PlayerAttack.damageModifier;
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
            //print("Game Over");
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


				//Use Phyics.RayCast to detect the obstacle
				//print(transform.position.ToString());
				//print (target.position.ToString());
				Vector3 dir = target.position - transform.position;
				//print((Physics2D.Raycast (transform.position, dir,hit, 100f)));
				hit = Physics2D.Raycast (transform.position, dir.normalized, 100f);
				//print ("enemypos = " + transform.position + " hit = " + hit.point);
				if (hit.collider != null) {
					//print (hit.point);
					//print ("hello");
					//Debug.DrawRay (hit.transform.position, transform.position, Color.red);
					Vector3 debugLine = transform.position - new Vector3(hit.point.x, hit.point.y, 0);
					Debug.DrawRay (hit.point, debugLine, Color.red);
					if (hit.collider.gameObject.CompareTag ("Obstacle")) {
						
						ifThereIsAnything = true;
						//transform.position = Vector2.MoveTowards (transform.position, new Vector2(transform.position.x + 7f, transform.position.y), speed * Time.deltaTime);
						//transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
						Vector3 go = transform.position;
                       
                        float hitx = (float)Math.Round(hit.point.x, 0, MidpointRounding.AwayFromZero);
                        float hity = (float)Math.Round(hit.point.y, 0, MidpointRounding.AwayFromZero);
                        //print(hitx + " " + hity);

                        Vector2 p1 = new Vector2(3, 3);
                        Vector2 p2 = new Vector2(2, 4);
                        float angle = Mathf.Atan2(transform.position.y - hit.point.y, transform.position.x - hit.point.x) * 180 / Mathf.PI;

                        //print("------------------------------------angle = " + angle);

                        //if (hity <= transform.position.y) {
                        if(angle >= 45 && angle <= 135) { // above
							int left = 0;
							int right = 0;
                            int i;
                            for (i = (int)hitx; OpeningLevel.walls[i, (int)hity] != 0; i++)
                            {
                                right++;
                            }
                            //print("enemy pos = " + transform.position.x + "," + transform.position.y + "  wall at that pos and up one = " + ((int)(transform.position.x + 1)) + "," + ((int)(hity + 1)));
                            //print("wall = " + OpeningLevel.walls[(int)transform.position.x + 1, (int)hity + 1]);
                            if (OpeningLevel.walls[(int)transform.position.x + 1, (int)hity + 1] == 1)
                            {
                                right = 100;
                            }
                            for (i = (int)hitx; OpeningLevel.walls[i, (int)hity] != 0; i--)
                            {
                                left++;
                            }
                            if (OpeningLevel.walls[(int)transform.position.x - 1, (int)hity + 1] == 1)
                            {
                                left = 100;
                            }
                          //  print ("right = " + right + " left = " + left);
							if (left > right-1) {
								go.Set (go.x + 10, go.y, go.z);
							//	print ("moving right");
							} else if (right > left-1) {
								go.Set (go.x - 10, go.y, go.z);
							//	print ("moving left");
							} else {
                                System.Random rnd = new System.Random();
                                int month = rnd.Next(1, 2);
                                if (month == 1)
                                {
                                    go.Set(go.x + 10, go.y, go.z);
                              //      print("going right");
                                } else
                                {
                                    go.Set(go.x - 10, go.y, go.z);
                                //    print("going left");
                                }
							}
						}
                        //if (hity >= transform.position.y) {
                        if(angle <= -45 && angle >= -135) {//below
                            int left = 0;
                            int right = 0;
                            int i;
                            for (i = (int)hitx; OpeningLevel.walls[i, (int)hity] != 0; i++)
                            {
                                right++;
                            }
                            //print("enemy pos = " + transform.position.x + "," + transform.position.y + "  wall at that pos and up one = " + ((int)(transform.position.x + 1)) + "," + ((int)(hity - 1)));
                            //print("wall = " + OpeningLevel.walls[(int)transform.position.x + 1, (int)hity - 1]);
                            if (OpeningLevel.walls[(int)transform.position.x+1, (int)hity - 1] == 1)
                            {
                                right = 100;
                            }
                            for (i = (int)hitx; OpeningLevel.walls[i, (int)hity] != 0; i--)
                            {
                                left++;
                            }
                            if (OpeningLevel.walls[(int)transform.position.x - 1, (int)hity - 1] == 1)
                            {
                                left = 100;
                            }

                            //print("right = " + right + " left = " + left);
                            if (left > right - 1)
                            {
                                go.Set(go.x + 10, go.y, go.z);
                              //  print("moving right");
                            }
                            else if (right > left - 1)
                            {
                                go.Set(go.x - 10, go.y, go.z);
                                //print("moving left");
                            }
                            else {
                                System.Random rnd = new System.Random();
                                int month = rnd.Next(1, 2);
                                if (month == 1)
                                {
                                    go.Set(go.x + 10, go.y, go.z);
                                  //  print("going right");
                                }
                                else
                                {
                                    go.Set(go.x - 10, go.y, go.z);
                                    //print("going left");
                                }
                            }
                        }
                        //if (hitx <= transform.position.x) {
                        if( angle < 45 && angle > -45) { //right
                            int left = 0;
                            int right = 0;
                            int i;
                            for (i = (int)hity; OpeningLevel.walls[(int)hitx, i] != 0; i++)
                            {
                                right++;
                            }
                            //print("enemy pos = " + transform.position.x + "," + transform.position.y + "  wall at that pos and up one = " + ((int)(transform.position.x + 1)) + "," + ((int)(hity + 1)));
                            //print("wall = " + OpeningLevel.walls[(int)transform.position.x + 1, (int)hity + 1]);
                            if (OpeningLevel.walls[(int)hitx + 1, (int)transform.position.y + 1] == 1)
                            {
                                right = 100;
                            }
                            for (i = (int)hity; OpeningLevel.walls[(int)hitx,i] != 0; i--)
                            {
                                left++;
                            }
                            if (OpeningLevel.walls[(int)hitx + 1, (int)transform.position.y - 1] == 1)
                            {
                                left = 100;
                            }
                           // print("right = " + right + " left = " + left);
                            if (left > right - 1)
                            {
                                go.Set(go.x, go.y + 10, go.z);
                             //   print("moving right");
                            }
                            else if (right > left - 1)
                            {
                                go.Set(go.x, go.y - 10, go.z);
                              //  print("moving left");
                            }
                            else {
                                System.Random rnd = new System.Random();
                                int month = rnd.Next(1, 2);
                                if (month == 1)
                                {
                                    go.Set(go.x, go.y + 10, go.z);
                                //    print("going right");
                                }
                                else
                                {
                                    go.Set(go.x, go.y - 10, go.z);
                                  //  print("going left");
                                }
                            }
                        }
                        //if (hitx >= transform.position.x) {
                        if(angle > 135 || angle < -135) { //left
                            int left = 0;
                            int right = 0;
                            int i;
                            for (i = (int)hity; OpeningLevel.walls[(int)hitx, i] != 0; i++)
                            {
                                right++;
                            }
                            //print("enemy pos = " + transform.position.x + "," + transform.position.y + "  wall at that pos and up one = " + ((int)(transform.position.x + 1)) + "," + ((int)(hity + 1)));
                            //print("wall = " + OpeningLevel.walls[(int)transform.position.x + 1, (int)hity + 1]);
                            if (OpeningLevel.walls[(int)hitx + 1, (int)transform.position.y - 1] == 1)
                            {
                                right = 100;
                            }
                            for (i = (int)hity; OpeningLevel.walls[(int)hitx, i] != 0; i--)
                            {
                                left++;
                            }
                            if (OpeningLevel.walls[(int)hitx - 1, (int)transform.position.y - 1] == 1)
                            {
                                left = 100;
                            }
                            //print("right = " + right + " left = " + left);
                            if (left > right - 1)
                            {
                                go.Set(go.x, go.y + 10, go.z);
                              //  print("moving right");
                            }
                            else if (right > left - 1)
                            {
                                go.Set(go.x, go.y - 10, go.z);
                                //print("moving left");
                            }
                            else {
                                System.Random rnd = new System.Random();
                                int month = rnd.Next(1, 2);
                                if (month == 1)
                                {
                                    go.Set(go.x, go.y + 10, go.z);
                                  //  print("going right");
                                }
                                else
                                {
                                    go.Set(go.x, go.y - 10, go.z);
                                    //print("going left");
                                }
                            }
                        }
                        if(angle == 45)
                        {
                            go.Set(go.x, -64, 0);
                        } else if(angle == -45)
                        {
                            go.Set(go.x, 64, 0);
                        } else if(angle == -135)
                        {
                            go.Set(go.x, 64, 0);
                        } else if(angle == 135)
                        {
                            go.Set(go.x, -64, 0);
                        }                    
                        transform.position = Vector2.MoveTowards (transform.position, go, speed/40);

					}
				}
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
