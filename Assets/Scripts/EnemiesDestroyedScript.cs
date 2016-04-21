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
        if (Application.loadedLevel == 1)
        {
            if (EnemyAi.numEnemiesDestroyed >= 35)
            {
                numDestroyed.fontSize = 53;
            }
            else {
                numDestroyed.fontSize = numFontSize + EnemyAi.numEnemiesDestroyed;
            }
            numDestroyed.text = "" + EnemyAi.numEnemiesDestroyed;
        } else if (Application.loadedLevel == 10)
        {
            if (EnemyAILevel2.numEnemiesDestroyed >= 35)
            {
                numDestroyed.fontSize = 53;
            }
            else {
                numDestroyed.fontSize = numFontSize + EnemyAILevel2.numEnemiesDestroyed;
            }
            numDestroyed.text = "" + EnemyAILevel2.numEnemiesDestroyed;
        }
        else if (Application.loadedLevel == 11)
        {
            if (EnemyAILevel3.numEnemiesDestroyed >= 35)
            {
                numDestroyed.fontSize = 53;
            }
            else {
                numDestroyed.fontSize = numFontSize + EnemyAILevel3.numEnemiesDestroyed;
            }
            numDestroyed.text = "" + EnemyAILevel3.numEnemiesDestroyed;
        }
    }
}