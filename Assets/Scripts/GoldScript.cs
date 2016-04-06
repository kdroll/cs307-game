using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldScript : MonoBehaviour {

    public Text goldAmount;


    // Use this for initialization
    void Start() {
        goldAmount = GameObject.Find("Gold Amount").GetComponent<Text>();
        goldAmount.text = "";
    }

    // Update is called once per frame
    void Update() {
        handleGold();
    }

    void handleGold() {
        goldAmount.text = "" + EnemyAi.gold;
    }
}
