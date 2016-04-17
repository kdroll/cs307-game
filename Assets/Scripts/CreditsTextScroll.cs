using UnityEngine;

public class CreditsTextScroll : MonoBehaviour
{
	public float speed;
	public float length;

	private void Start()
	{
		transform.Translate(-300, (float) -1 * ((Screen.height / 2) + 150), 0);
	}

	private void Update()
	{
		// print (transform.position.y);
		// print (Screen.height + 150);
		if (transform.position.y - Screen.height * length > 3) {
			// print ("RESET");
			// print (transform.position.y - Screen.height + 150);
			transform.Translate(0, (float) -1 * (Screen.height + 500), 0);
		}
		transform.Translate(0,speed,0);
	}
}