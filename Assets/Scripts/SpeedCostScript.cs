using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedCostScript : MonoBehaviour {

    public Text speedCost;

    // Use this for initialization
    void Start() {
        speedCost = GameObject.Find("Speed Cost").GetComponent<Text>();
        speedCost.text = "";
    }

    // Update is called once per frame
    void Update() {
        handleGold();
    }

    void handleGold() {
        speedCost.text = "" + (50 + (50 * PlayerMovement.numSpeedUpgrades));
    }
}
