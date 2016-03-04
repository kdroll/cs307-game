using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    int health = 100;
    double locked = 0f;

	// Use this for initialization
	void Start () {
		health = 100;
	}

    // Update is called once per frame
    void Update()
    {
        if(locked >= 1)
        {
            locked = 1;
        } else
        {
            locked += .016;
        }
        if(health <= 0)
        {
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



   public void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy" && locked == 1) {
			StartCoroutine (takeDamage ());
        }

	}

}
