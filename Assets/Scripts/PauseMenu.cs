using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Text damageAmount, speedAmount, healthAmount;
    public string mainMenu;
    public static bool isPaused;
    public GameObject pauseMenuCanvas;
    float roundedDamage, roundedSpeed, roundedHealth, roundedMaxHealth;
	
	// Update is called once per frame
	void Update () {
	    if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            roundedDamage = (float)System.Math.Round(PlayerAttack.damageModifier, 2);
            damageAmount = GameObject.Find("Paused Damage Mod").GetComponent<Text>();
            damageAmount.text = "" + roundedDamage + "x";
            roundedSpeed = (float)System.Math.Round(PlayerMovement.speedModifier, 2);
            speedAmount = GameObject.Find("Paused Speed Mod").GetComponent<Text>();
            speedAmount.text = "" + roundedSpeed + "x";
            roundedHealth = (float)System.Math.Round(PlayerHealth.health, 2);
            roundedMaxHealth = (float)System.Math.Round((PlayerHealth.startHealth), 2);
            healthAmount = GameObject.Find("Paused Health Mod").GetComponent<Text>();
            healthAmount.text = "" + roundedHealth + "/" + roundedMaxHealth;
            Time.timeScale = 0f;
        } else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            isPaused = !isPaused;
        }
	}

    public void Resume()
    {
        isPaused = false;
    }

    public void Quit()
    {
	    Application.Quit();
    }

	public void LoadScene(int level)
	{
		Application.LoadLevel(level);
	}
}
