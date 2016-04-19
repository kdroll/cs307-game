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
        goldAmount.text = "" + EnemyAi.gold;
    }

    void handleEPS()
    {
        EPS.text = "" + OpeningLevel.spawnRateString;
    }
    void handleGrenades()
    {
        Grenades.text = "" + "Grenades: " + Grenade.numOfGrenades;
    }
}
