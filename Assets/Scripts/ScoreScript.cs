using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public Text Score;

    // Use this for initialization
    void Start() {
        Score = GameObject.Find("Score").GetComponent<Text>();
        Score.text = "";
    }

    // Update is called once per frame
    void Update() {
        handleScore();
    }

	IEnumerator wait() {
		yield return new WaitForSeconds (0.3f);
	}

    void handleScore() {
        print(EnemyAi.totalScore);
        print(EnemyAi.numEnemiesDestroyed * 5);
        print((EnemyAi.numEnemiesDestroyed * 5) + EnemyAi.totalScore);
        Score.text = "" + ((EnemyAi.numEnemiesDestroyed*5) + EnemyAi.totalScore);
		wait ();
    }
}
