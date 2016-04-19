using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

	CircleCollider2D blastRadius;
	public GameObject grenade;
	public Transform spawnGrenade;
	GameObject waffle;
	GameObject effect;
	public GameObject explosionParticle;
	public static Vector3 position;
	int isThereAGrenade = 0;
    public static int numOfGrenades;
	// Use this for initialization

	private IEnumerator grenadeTimer() {
		yield return new WaitForSeconds(2f);
		Instantiate (explosionParticle, waffle.transform.position, Quaternion.identity);
		waffle.GetComponent<CircleCollider2D> ().enabled = true;
		effect.SetActive (true);
		position = waffle.transform.position;
		yield return new WaitForSeconds (0.01f);
		waffle.SetActive (false);
		effect.SetActive (false);
		waffle.GetComponent<CircleCollider2D> ().enabled = false;
		isThereAGrenade = 0;
		//print ("made it past here");
		//blastRadius.enabled = true;
		//StartCoroutine (explosionTimer ());
		//Destroy (GameObject.FindGameObjectWithTag("Grenade"));
		//isThereAGrenade = 0;
	}
		

	void Start () {
		effect = (GameObject)Instantiate (explosionParticle, spawnGrenade.position, Quaternion.identity);
		effect.SetActive (false);
		waffle = (GameObject)Instantiate (grenade, spawnGrenade.position, Quaternion.identity);
		waffle.GetComponent<CircleCollider2D> ().enabled = false;
		waffle.SetActive (false);
        numOfGrenades = 5;
	}

	void Detonate() {
		StartCoroutine (grenadeTimer ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.G) && isThereAGrenade == 0 && numOfGrenades > 0) {
            numOfGrenades--;
			waffle.transform.position = spawnGrenade.position;
			effect.transform.position = spawnGrenade.position;
			waffle.SetActive (true);
			isThereAGrenade = 1;
			StartCoroutine (grenadeTimer ());

		}
	}
}
