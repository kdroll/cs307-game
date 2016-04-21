using UnityEngine;
using System.Collections;

public class EndGameMenu : MonoBehaviour {

	public GameObject endGameCanvas;
	public static bool playerDied;
	// Use this for initialization
	void Start () {
		endGameCanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerHealth.isDead == true || TheBossHealth.enemyHealth <= 0) {
			if (PlayerHealth.isDead == true) {
				playerDied = true;
			} else {
				playerDied = false;
			}
			endGameCanvas.SetActive (true);
		}
	
	}
}
