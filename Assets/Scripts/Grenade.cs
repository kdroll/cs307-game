using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

	CircleCollider2D blastRadius;
	public GameObject grenade;
	public Transform spawnGrenade;
	int isThereAGrenade = 0;
	// Use this for initialization

	private IEnumerator grenadeTimer() {
		yield return new WaitForSeconds(2f);
		blastRadius.enabled = true;
		Destroy (GameObject.FindGameObjectWithTag("Grenade"));
		isThereAGrenade = 0;
		print ("made it past here");
	}

	void Start () {
	
	}

	void Detonate() {
		StartCoroutine (grenadeTimer ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.G) && isThereAGrenade == 0) {
			Instantiate (grenade, spawnGrenade.position, Quaternion.identity);
			blastRadius = grenade.GetComponent<CircleCollider2D> ();
			blastRadius.enabled = false;
			isThereAGrenade = 1;
			Detonate ();

		}
	}
}
