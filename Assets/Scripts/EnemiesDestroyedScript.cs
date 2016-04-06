using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemiesDestroyedScript : MonoBehaviour {

    public Text numDestroyed;
    int numFontSize = 18;

    // Use this for initialization
    void Start() {
        numDestroyed = GameObject.Find("Enemies Destroyed").GetComponent<Text>();
        numDestroyed.text = "";
    }

    // Update is called once per frame
    void Update() {
        handleNumDestroyed();
    }

    void handleNumDestroyed() {
        if (EnemyAi.numEnemiesDestroyed >= 35) {
            numDestroyed.fontSize = 53;
        } else {
            numDestroyed.fontSize = numFontSize + EnemyAi.numEnemiesDestroyed;
        }
        numDestroyed.text = "" + EnemyAi.numEnemiesDestroyed;
    }
}
