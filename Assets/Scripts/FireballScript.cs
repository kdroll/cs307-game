using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	CircleCollider2D blastRadius;
	public GameObject fireball;
	public Transform spawnGrenade;
	GameObject waffle;
	GameObject effect;
	public GameObject explosionParticle;
	public static Vector3 position;
	int isThereAFireball = 0;
	// Use this for initialization

	private IEnumerator fireballTimer() {
		yield return new WaitForSeconds(2f);
		Instantiate (explosionParticle, waffle.transform.position, Quaternion.identity);
		waffle.GetComponent<CircleCollider2D> ().enabled = true;
		effect.SetActive (true);
		position = waffle.transform.position;
		yield return new WaitForSeconds (0.01f);
		waffle.SetActive (false);
		effect.SetActive (false);
		waffle.GetComponent<CircleCollider2D> ().enabled = false;
		isThereAFireball = 0;
		//print ("made it past here");
		//blastRadius.enabled = true;
		//StartCoroutine (explosionTimer ());
		//Destroy (GameObject.FindGameObjectWithTag("Grenade"));
		//isThereAGrenade = 0;
	}
		

	void Start () {
		effect = (GameObject)Instantiate (explosionParticle, spawnGrenade.position, Quaternion.identity);
		effect.SetActive (false);
		waffle = (GameObject)Instantiate (fireball, spawnGrenade.position, Quaternion.identity);
		waffle.GetComponent<CircleCollider2D> ().enabled = false;
		waffle.SetActive (false);
	}

	void Detonate() {
		StartCoroutine (fireballTimer ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.G) && isThereAFireball == 0) {
			waffle.transform.position = spawnGrenade.position;
			effect.transform.position = spawnGrenade.position;
			waffle.SetActive (true);
			isThereAFireball = 1;
			StartCoroutine (fireballTimer ());

		}
	}
}
