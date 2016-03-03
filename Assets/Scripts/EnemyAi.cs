using UnityEngine;
using System.Collections;

public class EnemyAi : MonoBehaviour {

	Transform target;
	Transform enemyTransform;
	public float speed = 3f;
	public float rotationSpeed=3f;
	public int distanceToAttack = 10;
	private int follow = 0;
	public static int damage = 10;


	void Start () {
		//obtain the game object Transform
		enemyTransform = this.GetComponent<Transform>();
	}

	void Update(){

		target = GameObject.FindWithTag ("Player").transform;


		if (Vector3.Distance (target.position, enemyTransform.position) <= distanceToAttack || follow == 1) {
			follow = 1;
			//rotate to look at the player

			//transform.rotation = Quaternion.LookRotation (targetDirection); // Converts target direction vector to Quaternion
			//transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);

			//move towards the player
			transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

		}


	}
}
