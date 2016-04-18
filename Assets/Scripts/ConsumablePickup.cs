using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConsumablePickup : MonoBehaviour {

    public Text buffTimer, buffTimerDescription;
    static bool nothing = true;
    static bool pepper = false;
    static bool pickedUp = false;
    bool isLightningBuffed = false;
    public float buffTime = 0f;
    float truncatedTime, roundedTime;
    public float buffTimeLeft = 0f;
    public float time;

    CircleCollider2D blastRadius;
    public GameObject fireball;
    public static Transform fireballPosition;
    GameObject waffle;
    GameObject effect;
    public GameObject explosionParticle;
    public static Vector3 position;

    Animator anim;

    // Use this for initialization
    void Start() {
        buffTimer = GameObject.Find("Buff Time Amount").GetComponent<Text>();
        buffTimerDescription = GameObject.Find("Buff Time Description").GetComponent<Text>();
        buffTimer.GetComponent<Text>().enabled = false;
        buffTimerDescription.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update() {
        //buffTimeLeft = (buffTime - Mathf.Abs(time - Time.time));
        truncatedTime = (float)(System.Math.Truncate(((double)buffTime - Mathf.Abs(time - Time.time)) * 100.0) / 100.0);
        buffTimeLeft = (float)(System.Math.Round((double)truncatedTime, 2));
        if (buffTimeLeft < 0f) {
            buffTimer.text = "" + 0;
        } else {
            buffTimer.text = "" + (buffTimeLeft);
        }
    }

    public static bool getPepper() {
        return pepper;
    }

    public static void setPepper(bool input) {
        pepper = input;
    }

    public static void setPickedUp(bool input) {
        pickedUp = input;
    }

    public static bool getPickedUp() {
        return pickedUp;
    }

    public static bool getNothing() {
        return nothing;
    }

    private IEnumerator lightningBuffTimer(float oldSpeed) {
        if (PlayerMovement.speedModifier >= PlayerMovement.maxSpeedMod || PlayerMovement.speedModifier * 1.75f >= PlayerMovement.maxSpeedMod) {
            isLightningBuffed = true;
            PlayerMovement.speedModifier = PlayerMovement.maxSpeedMod;
        } else {
            isLightningBuffed = true;
            PlayerMovement.speedModifier *= 1.75f;
        }
        time = Time.time;
        buffTimer.GetComponent<Text>().enabled = true;
        buffTimerDescription.GetComponent<Text>().enabled = true;

        buffTime = 3f;
        yield return new WaitForSeconds(3f);

        buffTimer.GetComponent<Text>().enabled = false;
        buffTimerDescription.GetComponent<Text>().enabled = false;

        if (oldSpeed >= PlayerMovement.maxSpeedMod) {
            PlayerMovement.speedModifier = PlayerMovement.maxSpeedMod;
            isLightningBuffed = false;
        } else {
            PlayerMovement.speedModifier = oldSpeed;
            isLightningBuffed = false;
        }
    }

    private IEnumerator fireballExplosionTimer() {
        yield return new WaitForSeconds(0.01f);
        waffle.SetActive(false);

        waffle.GetComponent<CircleCollider2D>().enabled = false;
        Destroy(waffle);

        effect.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Pepper") {
            if (PlayerMovement.speedModifier >= PlayerMovement.maxSpeedMod || PlayerMovement.speedModifier * 1.04f >= PlayerMovement.maxSpeedMod) {
                PlayerMovement.speedModifier = PlayerMovement.maxSpeedMod;
            } else {
                PlayerMovement.speedModifier *= 1.04f;
            }
            //Destroy(GameObject.FindGameObjectWithTag("Pepper"));
            Destroy(collision.gameObject);
            print("Picked up: Chili Pepper -- 1.04x Speed");
        } else if (collision.gameObject.tag == "Cupcake") {
            PlayerMovement.speedModifier *= .95f;
            PlayerAttack.damageModifier *= 1.02f;
            if ((PlayerHealth.health + 20) > PlayerHealth.startHealth) {
                PlayerHealth.health = PlayerHealth.startHealth;
            } else {
                PlayerHealth.health += 20;
            }
            //Destroy(GameObject.FindGameObjectWithTag("Cupcake"));
            Destroy(collision.gameObject);
            print("Picked up: Cupcake -- .95x Speed, 1.02x Damage, +20 Health");
        } else if (collision.gameObject.tag == "ToxicWaste") {
            if (PlayerMovement.speedModifier >= PlayerMovement.maxSpeedMod || PlayerMovement.speedModifier * 1.03f >= PlayerMovement.maxSpeedMod) {
                PlayerMovement.speedModifier = PlayerMovement.maxSpeedMod;
            } else {
                PlayerMovement.speedModifier *= 1.03f;
            }
            PlayerAttack.damageModifier *= 1.03f;
            //Destroy(GameObject.FindGameObjectWithTag("ToxicWaste"));
            Destroy(collision.gameObject);
            print("Picked up: ToxicWaste -- 1.03x Speed, 1.03x Damage");
        } else if (collision.gameObject.tag == "Bacon") {
            PlayerHealth.healthModifier += 10;
            PlayerHealth.startHealth += 10;
            PlayerHealth.health += 10;
            //Destroy(GameObject.FindGameObjectWithTag("Bacon"));
            Destroy(collision.gameObject);
            print("Picked up: Bacon -- +10 Max Health");
        } else if (collision.gameObject.tag == "Fireball") {

            fireballPosition = collision.transform;

            Destroy(collision.gameObject);
            effect = (GameObject)Instantiate(explosionParticle, fireballPosition.position, Quaternion.identity);
            waffle = (GameObject)Instantiate(fireball, fireballPosition.position, Quaternion.identity);

            waffle.transform.position = fireballPosition.position;
            effect.transform.position = fireballPosition.position;

            Instantiate(explosionParticle, waffle.transform.position, Quaternion.identity);
            waffle.GetComponent<CircleCollider2D>().enabled = true;
            effect.SetActive(true);
            waffle.SetActive(true);
            position = waffle.transform.position;
            StartCoroutine(fireballExplosionTimer());

            print("Picked up: Fireball -- Explosion!!!");
        } else if (collision.gameObject.tag == "Coin") {
            EnemyAi.gold += 100;
            Destroy(collision.gameObject);
            print("Picked up: Coin -- +100 Gold");
        } else if (collision.gameObject.tag == "OilSpill") {
            PlayerMovement.speedModifier *= .75f;
            Destroy(collision.gameObject);
            print("Picked up: Oil Spill -- .75x Speed");
        } else if (collision.gameObject.tag == "Lightning") {
            Destroy(collision.gameObject);
            float oldSpeedMod = PlayerMovement.speedModifier;
            if (isLightningBuffed == false) {
                StartCoroutine(lightningBuffTimer(oldSpeedMod));
            }

            print("Picked up: Lightning -- 1.75x Speed for 3 Seconds");
        }
    }
}
