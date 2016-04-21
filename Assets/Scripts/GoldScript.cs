using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoldScript : MonoBehaviour
{

    public Text goldAmount, EPS, Grenades;

    // Use this for initialization
    void Start()
    {
        goldAmount = GameObject.Find("Gold Amount").GetComponent<Text>();
        goldAmount.text = "";
        EPS = GameObject.Find("EPS").GetComponent<Text>();
        EPS.text = "";
        Grenades = GameObject.Find("Grenades").GetComponent<Text>();
        Grenades.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        handleGold();
        handleEPS();
        handleGrenades();
    }

    void handleGold()
    {
        if (Application.loadedLevel == 10)
        {
            goldAmount.text = "" + EnemyAILevel2.gold;
        }
        else if (Application.loadedLevel == 1)
        {
            goldAmount.text = "" + EnemyAi.gold;
        }
        else if (Application.loadedLevel == 11)
        {
            goldAmount.text = "" + EnemyAILevel3.gold;
        }
    }

    void handleEPS()
    {
        if (Application.loadedLevel == 10)
        {
            EPS.text = "" + Level2Manager.spawnRateString;
        }
        else if (Application.loadedLevel == 1)
        {
            EPS.text = "" + OpeningLevel.spawnRateString;
        } else if (Application.loadedLevel == 11)
        {
            EPS.text = "" + Level3Manager.spawnRateString;
        }
    }
    void handleGrenades()
    {
        Grenades.text = "" + "Grenades: " + Grenade.numOfGrenades;
    }
}
