using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public static float startHealth;
    public static float health;
    public static float healthModifier = 0;
    public static int numHealthUpgrades = 0;
    public static int numTimesHit = 0;
    public static double locked = 0f;
    public static bool isDead = false;
	static int difficulty;
    GameObject player;
    GameObject[] enemy;
	public AudioSource playerHurtAudio;
	static AudioSource _source;
    public static bool regen;


    // perks array has size of the total number of perks
    // perks[i] = 0 means player does not have the 'i'th perk
    // perks[i] = 1 means player has the 'i'th perk
    public static int[] perks = new int[6] { 0,0,0,0,0,0 };

    // Use this for initialization
    void Start() {
        regen = false;
        numTimesHit = 0;
        startHealth = 100 + healthModifier;
        health = 100 + healthModifier;
        player = GameObject.FindGameObjectWithTag("Player");
		//difficulty = OpeningLevel.difficulty;
		difficulty = 2;
		getHurtAudio ();
    }

    // Update is called once per frame
    void Update() {
        if (regen == false && perks[4] == 1) 
            StartCoroutine(healthRegen());
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
            //EnemyAi.totalScore += (int)(System.Math.Truncate(TimeScript.roundedTime));
            if (Application.loadedLevel == 1)
            {
                EnemyAi.totalScore = ((EnemyAi.numEnemiesDestroyed * 5) + EnemyAi.totalScore - (PlayerHealth.numTimesHit * 10) + (int)(System.Math.Truncate(TimeScript.roundedTime)));
                PlayerPrefs.SetInt("Score", EnemyAi.totalScore);
            } else if (Application.loadedLevel == 10)
            {
                EnemyAILevel2.totalScore = ((EnemyAILevel2.numEnemiesDestroyed * 5) + EnemyAILevel2.totalScore - (PlayerHealth.numTimesHit * 10) + (int)(System.Math.Truncate(TimeScript.roundedTime)));
                PlayerPrefs.SetInt("Score", EnemyAILevel2.totalScore);
            }
            else if (Application.loadedLevel == 11)
            {
                EnemyAILevel3.totalScore = ((EnemyAILevel3.numEnemiesDestroyed * 5) + EnemyAILevel3.totalScore - (PlayerHealth.numTimesHit * 10) + (int)(System.Math.Truncate(TimeScript.roundedTime)));
                PlayerPrefs.SetInt("Score", EnemyAILevel3.totalScore);
            }
            else if (Application.loadedLevel == 12)
            {
                TheBossLevel.totalScore = (TheBossLevel.totalScore - (PlayerHealth.numTimesHit * 10) + (int)(System.Math.Truncate(TimeScript.roundedTime)));
                PlayerPrefs.SetInt("Score", TheBossLevel.totalScore);
            }
            OpeningLevel.changeEnemyHealth = 0;
            Level2Manager.changeEnemyHealth = 0;
            Level3Manager.changeEnemyHealth = 0;
            Destroy(OpeningLevel.player);
            Destroy(Level2Manager.player);
            Destroy(Level3Manager.player);
            Destroy (TheBossLevel.player);
        }
    }

	public static IEnumerator takeDamage() {
        if (!PauseMenu.isPaused) {
			health -= ((5*(difficulty)*(difficulty)) - (30*difficulty) + 55);
            print(health);
            numTimesHit++;
            locked = 0;
			_source.Play();
            yield return null;
        }
    }

    public static IEnumerator healthRegen()
    {
        regen = true;
        PlayerHealth.health += 1;
        yield return new WaitForSeconds(1f);
        regen = false;
    }


    void getHurtAudio() {
		_source = playerHurtAudio;
	}

    public void OnCollisionEnter2D(Collision2D collision) {
		if ((collision.gameObject.tag == "Enemy"  || collision.gameObject.tag == "PenguinEnemy" )&& locked == 1 && !Input.GetButtonDown("attack") && !Input.GetButtonDown("B")) {
            StartCoroutine(takeDamage());
        }

    }
    public void OnCollisionStay2D(Collision2D coll) {
		if ((coll.gameObject.tag == "Enemy"  || coll.gameObject.tag == "PenguinEnemy" || coll.gameObject.tag == "SkeletonEnemy")&& locked == 1 && !Input.GetButtonDown("attack") && !Input.GetButtonDown("B")) {
            StartCoroutine(takeDamage());
        }

    }
	public void OnParticleCollision(GameObject other) {
		other.SetActive (false);
	}

}
