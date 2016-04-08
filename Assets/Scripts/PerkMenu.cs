using UnityEngine;
using System.Collections;

public class PerkMenu : MonoBehaviour {

	public GameObject perkMenuCanvas;
    public static bool inPerkMenu = false;

	void Start() {
		perkMenuCanvas.SetActive(false);
	}

    public void setTrue() {
        inPerkMenu = true;
        perkMenuCanvas.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
        if (inPerkMenu == true) {
			perkMenuCanvas.SetActive (true);
            Time.timeScale = 0f;
		} else {
            perkMenuCanvas.SetActive(false);
        }
	}
}
