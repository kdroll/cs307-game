using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public static int startHealth;
    public static int health;
    public static int healthModifier = 0;
    public static int numHealthUpgrades = 0;
    double locked = 0f;
	public static bool isDead = false;
	GameObject player;
	GameObject[] enemy;

	// Use this for initialization
	void Start () {
        startHealth = 100 + healthModifier;
        health = 100 + healthModifier;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

    // Update is called once per frame
    void Update()
    {
		enemy = GameObject.FindGameObjectsWithTag ("Enemy");
		//print(Vector2.Distance(player.transform.position, enemy.transform.position));
		for (int i = 0; i < enemy.Length; i++) {
			if (Vector2.Distance (player.transform.position, enemy[i].transform.position) < 1f && locked == 1 && !Input.GetButtonDown ("attack") && !Input.GetButtonDown ("B")) {
				StartCoroutine (takeDamage ());
			}
		}
        if(locked >= 1)
        {
            locked = 1;
        } else
        {
            locked += .02;
        }
        if(health <= 0)
        {
			isDead = true;
            Destroy(OpeningLevel.player);
        }
        //print("locked = " + locked);
    }

    private IEnumerator takeDamage() {
		health -= 10;
		print (health);
        locked = 0;
        yield return null;
	} 



   public void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Enemy" && locked == 1 && !Input.GetButtonDown("attack") && !Input.GetButtonDown("B")) {
			StartCoroutine (takeDamage ());
         } 

	}
	public void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy" && locked == 1 && !Input.GetButtonDown ("attack") && !Input.GetButtonDown("B")) {
			StartCoroutine (takeDamage ());
		}
	}

}
