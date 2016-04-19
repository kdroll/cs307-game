using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoresScript : MonoBehaviour {
    public Text highScore1, highScore2, highScore3;
    bool uploadedScore = false;
    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("1stHighScore", 0);
        PlayerPrefs.SetInt("2ndHighScore", 0);
        PlayerPrefs.SetInt("3rdHighScore", 0);

        highScore1 = GameObject.Find("1st High Scores Value").GetComponent<Text>();
        if (PlayerPrefs.HasKey("1stHighScore")) {
            highScore1.text = "" + PlayerPrefs.GetInt("1stHighScore");
        } else {
            highScore1.text = "------------";
        }

        highScore2 = GameObject.Find("2nd High Scores Value").GetComponent<Text>();
        if (PlayerPrefs.HasKey("2ndHighScore")) {
            highScore2.text = "" + PlayerPrefs.GetInt("2ndHighScore");
        } else {
            highScore2.text = "------------";
        }

        highScore3 = GameObject.Find("3rd High Scores Value").GetComponent<Text>();
        if (PlayerPrefs.HasKey("3rdHighScore")) {
            highScore3.text = "" + PlayerPrefs.GetInt("3rdHighScore");
        } else {
            highScore3.text = "------------";
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (PlayerPrefs.HasKey("Score")) {
            int score = PlayerPrefs.GetInt("Score");
            if (score > PlayerPrefs.GetInt("1stHighScore") && uploadedScore == false) {
                uploadedScore = true;
                PlayerPrefs.SetInt("3rdHighScore", PlayerPrefs.GetInt("2ndHighScore"));
                PlayerPrefs.SetInt("2ndHighScore", PlayerPrefs.GetInt("1stHighScore"));
                PlayerPrefs.SetInt("1stHighScore", score);
            } else if (score > PlayerPrefs.GetInt("2ndHighScore") && uploadedScore == false) {
                uploadedScore = true;
                PlayerPrefs.SetInt("3rdHighScore", PlayerPrefs.GetInt("2ndHighScore"));
                PlayerPrefs.SetInt("2ndHighScore", score);
            } else if (score > PlayerPrefs.GetInt("3rdHighScore") && uploadedScore == false) {
                uploadedScore = true;
                PlayerPrefs.SetInt("3rdHighScore", score);
            }
        }

        highScore1.text = "" + PlayerPrefs.GetInt("1stHighScore");
        highScore2.text = "" + PlayerPrefs.GetInt("2ndHighScore");
        highScore3.text = "" + PlayerPrefs.GetInt("3rdHighScore");
    }
}
