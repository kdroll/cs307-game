using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public static int numTimesHit;
    public static float startHealth;
    public static float health;
    public static float healthModifier = 0;
    public static int numHealthUpgrades = 0;
    double locked = 0f;
    public static bool isDead = false;
    GameObject player;
    GameObject[] enemy;

    // perks array has size of the total number of perks
    // perks[i] = 0 means player does not have the 'i'th perk
    // perks[i] = 1 means player has the 'i'th perk
    public static int[] perks = new int[2];

    // Use this for initialization
    void Start() {
        numTimesHit = 0;
        startHealth = 100 + healthModifier;
        health = 100 + healthModifier;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        //print(Vector2.Distance(player.transform.position, enemy.transform.position));
        for (int i = 0; i < enemy.Length; i++) {
            if (Vector2.Distance(player.transform.position, enemy[i].transform.position) < 1f && locked == 1 && !Input.GetButtonDown("attack") && !Input.GetButtonDown("B")) {
                StartCoroutine(takeDamage());
            }
        }
        if (locked >= 1) {
            locked = 1;
        } else {
            locked += .02;
        }
        if (health <= 0) {
            isDead = true;
            EnemyAi.totalScore += (int)(System.Math.Truncate(TimeScript.roundedTime));
            Destroy(OpeningLevel.player);
        }
    }

    private IEnumerator takeDamage() {
        if (!PauseMenu.isPaused) {
            health -= ((5*(OpeningLevel.difficulty)*(OpeningLevel.difficulty)) - (30*OpeningLevel.difficulty) + 55);
            numTimesHit++;
            print(health);
            locked = 0;
            yield return null;
        }
    }



    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" && locked == 1 && !Input.GetButtonDown("attack") && !Input.GetButtonDown("B")) {
            StartCoroutine(takeDamage());
        }

    }
    public void OnCollisionStay2D(Collision2D coll) {
        if (coll.gameObject.tag == "Enemy" && locked == 1 && !Input.GetButtonDown("attack") && !Input.GetButtonDown("B")) {
            StartCoroutine(takeDamage());
        }
    }

}
