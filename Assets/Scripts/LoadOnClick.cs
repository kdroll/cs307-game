using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
    public static bool difficultySet = false;
	public static int karma = 0;
	public static int amountDone = 0;

    // public GameObject loadingImage;

	public void karmaChoice(int karmaChoice) {
		karma = karma + karmaChoice;
		amountDone++;
	}
    public void LoadScene(int level)
    {
        // loadingImage.SetActive(true);
		if (level == 0) {
			karma = 0;
			amountDone = 0;
		}
		Application.LoadLevel(level);
    }

    public void LoadLevel(int level)
    {
		//karma = -3;
		//amountDone = 3;
		//print (amountDone);
        // loadingImage.SetActive(true);
		if (level == 0) {
			karma = 0;
			amountDone = 0;
		}
		//print (EndGameMenu.playerDied);
		//print (amountDone);
		//print (level);
		if (amountDone == 3 && level == 2) {
			print ("in if statement");
			if (EndGameMenu.playerDied == true) {
				Application.LoadLevel (23); //find value for death scene
			} else {

				if (karma == 3) {
					Application.LoadLevel (19); //find value for best ending
				} else if (karma == 1) {
					Application.LoadLevel (20);
				} else if (karma == -1) {
					Application.LoadLevel (21);
				} else {
					Application.LoadLevel (22);
				}
			}
		} else {
			print (amountDone);
			Application.LoadLevel (level);
		}
        //UpgradeMenu.upgradeMenuCanvas.SetActive(false);
    }

    public void advanceToPerkMenu() {
        PerkMenu.inPerkMenu = true;
        UpgradeMenu.inUpgradeMenu = false;
    }

    public void advanceToStatsMenu() {
        Time.timeScale = 0f;
        PerkMenu.inPerkMenu = false;
        StatsMenu.inStatsMenu = true;
    }

    public void ExitScene()
    {
        Application.Quit();
    }

    public void acquirePerk(int perkNumber) {
        PlayerHealth.perks[perkNumber] = 1;
        print("perk" + perkNumber + " = " + PlayerHealth.perks[perkNumber]);
    }

    public void increaseDamage()
    {
        if (EnemyAi.gold < 50 + (50 * PlayerAttack.numDamageUpgrades))
        {

        }
        else {
            PlayerAttack.damageModifier += .2f;
            EnemyAi.gold -= 50 + (50 * PlayerAttack.numDamageUpgrades);
            PlayerAttack.numDamageUpgrades++;
            print("Damage modifier: " + PlayerAttack.damageModifier);
            print("Num damageUpgrades: " + PlayerAttack.numDamageUpgrades);
        }
    }

    public void increaseSpeed()
    {
 
        if (EnemyAi.gold < 50 + (50 * PlayerMovement.numSpeedUpgrades))
        {

        }
        else {
            if (PlayerMovement.speedModifier >= PlayerMovement.maxSpeedMod || PlayerMovement.speedModifier + .02f >= PlayerMovement.maxSpeedMod) {
                PlayerMovement.speedModifier = PlayerMovement.maxSpeedMod;
            } else {
                PlayerMovement.speedModifier += .02f;
                EnemyAi.gold -= 50 + (50 * PlayerMovement.numSpeedUpgrades);
                PlayerMovement.numSpeedUpgrades++;
                print("Speed modifier: " + PlayerMovement.speedModifier);
                print("Num speedUpgrades: " + PlayerMovement.numSpeedUpgrades);
            }
        }
    }

    public void increaseHealth()
    {
        if (EnemyAi.gold < 50 + (50 * PlayerHealth.numHealthUpgrades))
        {

        }
        else {
            PlayerHealth.healthModifier += 5;
            EnemyAi.gold -= 50 + (50 * PlayerHealth.numHealthUpgrades);
            PlayerHealth.numHealthUpgrades++;
            print("Health modifier: " + PlayerHealth.healthModifier);
            print("Num healthUpgrades: " + PlayerHealth.numHealthUpgrades);
        }
    }

    public void setDifficulty(int difficulty) {
        OpeningLevel.difficulty = difficulty;
        print(OpeningLevel.difficulty);
        difficultySet = true;
    }
}

