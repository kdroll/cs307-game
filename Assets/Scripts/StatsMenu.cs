using UnityEngine;
using System.Collections;

public class StatsMenu : MonoBehaviour {

	public GameObject statsMenuCanvas;
    public static bool inStatsMenu = false;

	void Start() {
		statsMenuCanvas.SetActive(false);
	}

    public void setTrue() {
        inStatsMenu = true;
        PerkMenu.inPerkMenu = false;
        statsMenuCanvas.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
        if (inStatsMenu == true && UpgradeMenu.inUpgradeMenu == false && PerkMenu.inPerkMenu == false) {
            statsMenuCanvas.SetActive (true);
            UpgradeMenu.inUpgradeMenu = false;
            PerkMenu.inPerkMenu = false;
            Time.timeScale = 0f;
        } else {
            statsMenuCanvas.SetActive(false);
        }
	}
}
