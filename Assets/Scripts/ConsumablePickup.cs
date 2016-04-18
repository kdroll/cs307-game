using UnityEngine;
using System.Collections;

public class ConsumablePickup : MonoBehaviour {

    static bool nothing = true;
    static bool pepper = false;
    static bool pickedUp = false;

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
    }

    // Update is called once per frame
    void Update() {

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

            //fireballPosition = GameObject.FindGameObjectWithTag("Fireball").transform;
            fireballPosition = collision.transform;

            //Destroy(GameObject.FindGameObjectWithTag("Fireball"));
            Destroy(collision.gameObject);
            effect = (GameObject)Instantiate(explosionParticle, fireballPosition.position, Quaternion.identity);
            waffle = (GameObject)Instantiate(fireball, fireballPosition.position, Quaternion.identity);
            //waffle.GetComponent<CircleCollider2D>().enabled = false;

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
            //Destroy(GameObject.FindGameObjectWithTag("Coin"));
            Destroy(collision.gameObject);
            print("Picked up: Coin -- +100 Gold");
        } else if (collision.gameObject.tag == "OilSpill") {
            PlayerMovement.speedModifier *= .75f;
            //Destroy(GameObject.FindGameObjectWithTag("Coin"));
            Destroy(collision.gameObject);
            print("Picked up: Oil Spill -- .75x Speed");
        }
    }
}
