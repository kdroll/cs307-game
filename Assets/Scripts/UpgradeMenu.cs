using UnityEngine;
using System.Collections;

public class UpgradeMenu : MonoBehaviour {

	public GameObject upgradeMenuCanvas;

	void Start() {
		upgradeMenuCanvas.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		if (PlayerHealth.isDead == true) {
			upgradeMenuCanvas.SetActive (true);
		} else
        {
            upgradeMenuCanvas.SetActive(false);
        }
	}
}
