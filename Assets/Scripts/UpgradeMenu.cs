using UnityEngine;
using System.Collections;

public class UpgradeMenu : MonoBehaviour {

	public GameObject upgradeMenuCanvas;

	void Start() {
		upgradeMenuCanvas.SetActive(false);
	}

    public void setFalse() {
        upgradeMenuCanvas.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		if (PlayerHealth.isDead == true && PerkMenu.inPerkMenu == false) {
			upgradeMenuCanvas.SetActive (true);
		} else
        {
            upgradeMenuCanvas.SetActive(false);
        }
	}
}
