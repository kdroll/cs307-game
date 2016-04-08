using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageCostScript : MonoBehaviour {

    public Text damageCost;

	// Use this for initialization
	void Start () {
        damageCost = GameObject.Find("Damage Cost").GetComponent<Text>();
        damageCost.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        handleGold();
	}

    void handleGold() {
        damageCost.text = "" + (50 + (50 * PlayerAttack.numDamageUpgrades));
    }
}
