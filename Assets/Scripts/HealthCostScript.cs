using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthCostScript : MonoBehaviour {

    public Text healthCost;

    // Use this for initialization
    void Start() {
        healthCost = GameObject.Find("Health Cost").GetComponent<Text>();
        healthCost.text = "";
    }

    // Update is called once per frame
    void Update() {
        handleGold();
    }

    void handleGold() {
        healthCost.text = "" + (50 + (50 * PlayerHealth.numHealthUpgrades));
    }
}
