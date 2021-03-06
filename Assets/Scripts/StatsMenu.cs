﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsMenu : MonoBehaviour {

    public GameObject statsMenuCanvas;
    public static bool inStatsMenu = false;
    public Text timeStat, enemiesKilledStat, timesHitStat, totalScoreStat;
    float truncatedTime;
    float roundedTime;

    void Start() {
        statsMenuCanvas.SetActive(false);
    }

    public void setTrue() {
        inStatsMenu = true;
        PerkMenu.inPerkMenu = false;
        statsMenuCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        if (inStatsMenu == true && UpgradeMenu.inUpgradeMenu == false && PerkMenu.inPerkMenu == false) {
            statsMenuCanvas.SetActive(true);
            UpgradeMenu.inUpgradeMenu = false;
            PerkMenu.inPerkMenu = false;
            Time.timeScale = 0f;
            truncatedTime = (float)(System.Math.Truncate((double)Time.timeSinceLevelLoad * 100.0) / 100.0);
            roundedTime = (float)(System.Math.Round((double)truncatedTime, 2));

            timeStat = GameObject.Find("Time Value").GetComponent<Text>();
            timeStat.text = "";
            enemiesKilledStat = GameObject.Find("Enemies Value").GetComponent<Text>();
            enemiesKilledStat.text = "";
            timesHitStat = GameObject.Find("Times Hit Value").GetComponent<Text>();
            timesHitStat.text = "";
            totalScoreStat = GameObject.Find("Total Score Value").GetComponent<Text>();
            totalScoreStat.text = "";

            timeStat.text = "" + roundedTime + " = " + (float)(System.Math.Truncate(Time.time));
            if (Application.loadedLevel == 1)
            {
                enemiesKilledStat.text = "" + EnemyAi.numEnemiesDestroyed;
                timesHitStat.text = "" + PlayerHealth.numTimesHit;
                totalScoreStat.text = "" + EnemyAi.totalScore;
            } else if (Application.loadedLevel == 10)
            {
                enemiesKilledStat.text = "" + EnemyAILevel2.numEnemiesDestroyed;
                timesHitStat.text = "" + PlayerHealth.numTimesHit;
                totalScoreStat.text = "" + EnemyAILevel2.totalScore;
            }
            else if (Application.loadedLevel == 11)
            {
                enemiesKilledStat.text = "" + EnemyAILevel3.numEnemiesDestroyed;
                timesHitStat.text = "" + PlayerHealth.numTimesHit;
                totalScoreStat.text = "" + EnemyAILevel3.totalScore;
            }
        } else {
            statsMenuCanvas.SetActive(false);
        }
    }
}