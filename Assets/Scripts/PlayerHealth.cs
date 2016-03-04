using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int health;

	// Use this for initialization
	void Start () {
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		takeDamage ();
	}

	private IEnumerator takeDamage() {
		health -= 10;
		print (health);
		yield return null;
	}
		

	public void OnTriggerStay2D(Collider2D collision) {
		if (collision.gameObject.tag == "Enemy") {
			StartCoroutine (takeDamage ());
		}
	}

}
