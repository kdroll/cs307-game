using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UpgradeMenu : MonoBehaviour {

	public GameObject upgradeMenuCanvas;
    public static bool inUpgradeMenu = false;
    public Text damageAmount, speedAmount, healthAmount;
    float roundedDamage, roundedSpeed, roundedHealth, roundedMaxHealth;

    void Start() {
		upgradeMenuCanvas.SetActive(false);
	}

    public void setFalse() {
        upgradeMenuCanvas.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		if (PlayerHealth.isDead == true && PerkMenu.inPerkMenu == false && StatsMenu.inStatsMenu == false) {
            Time.timeScale = 0f;
            upgradeMenuCanvas.SetActive (true);
            roundedDamage = (float)System.Math.Round(PlayerAttack.damageModifier, 2);
            damageAmount = GameObject.Find("Damage Mod Display").GetComponent<Text>();
            damageAmount.text = "" + roundedDamage + "x";
            roundedSpeed = (float)System.Math.Round(PlayerMovement.speedModifier, 2);
            speedAmount = GameObject.Find("Speed Mod Display").GetComponent<Text>();
            speedAmount.text = "" + roundedSpeed + "x";
            roundedMaxHealth = (float)System.Math.Round((PlayerHealth.startHealth), 2);
            healthAmount = GameObject.Find("Health Mod Display").GetComponent<Text>();
            healthAmount.text = "" + (roundedMaxHealth + PlayerHealth.healthModifier);
        } else
        {
            upgradeMenuCanvas.SetActive(false);
        }
	}
}
