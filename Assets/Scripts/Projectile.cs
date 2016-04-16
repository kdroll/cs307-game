using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public GameObject projectile;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision (GameObject.FindGameObjectWithTag ("Player").GetComponent<BoxCollider2D> (), projectile.GetComponent<BoxCollider2D> ());
	
	}

	// Update is called once per frame
	void Update () {
		
	}
	private IEnumerator wait() {
		yield return new WaitForSeconds (0.1f);
	}

	void OnCollisionEnter2D(Collision2D collider) {
		//StartCoroutine (wait ());
		ArrowSpawn.numOfArrows = 0;
		Destroy (gameObject);
		print ("arrow destroyed");
	}
}
