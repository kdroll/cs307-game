 using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
    int doubleClick = 0;

    // public GameObject loadingImage;

    public void LoadScene(int level)
    {
        // loadingImage.SetActive(true);
        Application.LoadLevel(level);
    }

    public void LoadLevel(int level)
    {
        // loadingImage.SetActive(true);
        Application.LoadLevel(level);
        //UpgradeMenu.upgradeMenuCanvas.SetActive(false);
    }

    public void ExitScene()
    {
        Application.Quit();
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
            PlayerMovement.speedModifier += .02f;
            EnemyAi.gold -= 50 + (50 * PlayerMovement.numSpeedUpgrades);
            PlayerMovement.numSpeedUpgrades++;
            print("Speed modifier: " + PlayerMovement.speedModifier);
            print("Num speedUpgrades: " + PlayerMovement.numSpeedUpgrades);
        }
    }

    public void increaseHealth()
    {

        if (EnemyAi.gold < 50 + (50 * PlayerHealth.numHealthUpgrades))
        {

        }
        else {
            PlayerHealth.healthModifier += 10;
            EnemyAi.gold -= 50 + (50 * PlayerHealth.numHealthUpgrades);
            PlayerHealth.numHealthUpgrades++;
            print("Health modifier: " + PlayerHealth.healthModifier);
            print("Num healthUpgrades: " + PlayerHealth.numHealthUpgrades);
        }
    }
}

