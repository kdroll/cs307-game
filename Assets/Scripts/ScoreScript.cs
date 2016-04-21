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
        if (Application.loadedLevel == 1)
        {
            Score.text = "" + EnemyAi.totalScore;
        } else if (Application.loadedLevel == 10)
        {
            Score.text = "" + EnemyAILevel2.totalScore;
        }
        else if (Application.loadedLevel == 11)
        {
            Score.text = "" + EnemyAILevel3.totalScore;
        }
        else if (Application.loadedLevel == 12)
        {
            Score.text = "" + TheBossLevel.totalScore;
        }
        //Score.text = "" + ((EnemyAi.numEnemiesDestroyed * 5) + EnemyAi.totalScore);
        //Score.text = "" + ((EnemyAi.numEnemiesDestroyed * 5) + EnemyAi.totalScore - (PlayerHealth.numTimesHit * 10));
    }
}
