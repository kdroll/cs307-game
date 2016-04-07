using UnityEngine;
using System.Collections;

public class OpeningMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
        EnemyAi.gold = 0;
        EnemyAi.numEnemiesDestroyed = 0;
        EnemyAi.totalScore = 0;
        PlayerHealth.health = 100;
        PlayerHealth.healthModifier = 0;
        PlayerHealth.numHealthUpgrades = 0;
        PlayerHealth.startHealth = 100;
        PlayerAttack.damageModifier = 1;
        PlayerAttack.numDamageUpgrades = 0;
        PlayerMovement.numSpeedUpgrades = 0;
        PlayerMovement.speedModifier = 1;
	}
	
	// Update is called once per frame
	void Update () {

    }
}
