using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemDisplayScript : MonoBehaviour {

    public static Text baconDisplay, cupcakeDisplay, pepperDisplay, toxicWasteDisplay, fireballDisplay, coinDisplay,
        oilSpillDisplay, lightningDisplay, grenadesDisplay, swordDisplay, pitchforkDisplay, nunchuckDisplay, bowDisplay;

	// Use this for initialization
	void Start () {
        baconDisplay = GameObject.Find("Bacon Description").GetComponent<Text>();
        baconDisplay.GetComponent<Text>().enabled = false;
        cupcakeDisplay = GameObject.Find("Cupcake Description").GetComponent<Text>();
        cupcakeDisplay.GetComponent<Text>().enabled = false;
        pepperDisplay = GameObject.Find("Pepper Description").GetComponent<Text>();
        pepperDisplay.GetComponent<Text>().enabled = false;
        toxicWasteDisplay = GameObject.Find("ToxicWaste Description").GetComponent<Text>();
        toxicWasteDisplay.GetComponent<Text>().enabled = false;
        fireballDisplay = GameObject.Find("Fireball Description").GetComponent<Text>();
        fireballDisplay.GetComponent<Text>().enabled = false;
        coinDisplay = GameObject.Find("Coin Description").GetComponent<Text>();
        coinDisplay.GetComponent<Text>().enabled = false;
        oilSpillDisplay = GameObject.Find("OilSpill Description").GetComponent<Text>();
        oilSpillDisplay.GetComponent<Text>().enabled = false;
        lightningDisplay = GameObject.Find("Lightning Description").GetComponent<Text>();
        lightningDisplay.GetComponent<Text>().enabled = false;
        grenadesDisplay = GameObject.Find("Grenades Description").GetComponent<Text>();
        grenadesDisplay.GetComponent<Text>().enabled = false;
        swordDisplay = GameObject.Find("Sword Description").GetComponent<Text>();
        swordDisplay.GetComponent<Text>().enabled = false;
        pitchforkDisplay = GameObject.Find("Pitchfork Description").GetComponent<Text>();
        pitchforkDisplay.GetComponent<Text>().enabled = false;
        nunchuckDisplay = GameObject.Find("Nunchuck Description").GetComponent<Text>();
        nunchuckDisplay.GetComponent<Text>().enabled = false;
        bowDisplay = GameObject.Find("Bow Description").GetComponent<Text>();
        bowDisplay.GetComponent<Text>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (baconDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(baconDisplay));
        }
        if (cupcakeDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(cupcakeDisplay));
        }
        if (pepperDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(pepperDisplay));
        }
        if (toxicWasteDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(toxicWasteDisplay));
        }
        if (fireballDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(fireballDisplay));
        }
        if (coinDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(coinDisplay));
        }
        if (oilSpillDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(oilSpillDisplay));
        }
        if (lightningDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(lightningDisplay));
        }
        if (grenadesDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(grenadesDisplay));
        }
        if (swordDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(swordDisplay));
        }
        if (pitchforkDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(pitchforkDisplay));
        }
        if (nunchuckDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(nunchuckDisplay));
        }
        if (bowDisplay.GetComponent<Text>().enabled == true) {
            StartCoroutine(waitForFadeOut(bowDisplay));
        }
    }

    private IEnumerator waitForFadeOut(Text item) {
        yield return new WaitForSeconds(1.1f);
        item.GetComponent<Text>().enabled = false;
    }
}
