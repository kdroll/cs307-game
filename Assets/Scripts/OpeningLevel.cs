using UnityEngine;
using System.Collections;


public class OpeningLevel : MonoBehaviour {


	private int levelHeight;
	private int levelWidth;

	public Color grassColor;
	public Color stoneColor;
	public Color spawnPointColor;
	public Color enemySpawnPointColor;

	public Transform grassTile;
	public Transform stoneTile;

	private Color[] tileColors;

	public Texture2D levelTexture;

	GameObject player;
	GameObject pitchfork;

	// Use this for initialization
	void Start () {
		int high = 0;
		player = GameObject.FindGameObjectWithTag("Player");
		pitchfork = GameObject.FindGameObjectWithTag ("Pitchfork");
		levelHeight = levelTexture.height;
		levelWidth = levelTexture.width;
		loadLevel ();



	}

	// Update is called once per frame
	void Update () {

	}

	void loadLevel() {
		tileColors = new Color[(levelWidth * levelHeight)];
		tileColors = levelTexture.GetPixels ();
		System.Random rand = new System.Random ();
		int randx, randy;
		for (int a = 0; a < 1000; a++) { 
			randx = rand.Next (levelWidth / 4, (3 * levelWidth) / 4);
			randy = rand.Next (levelWidth / 4, (3 * levelWidth) / 4);
			Instantiate (stoneTile, new Vector3 (randx, randy), Quaternion.identity);
		}
		for (int y = 0; y < levelHeight; y++) {
			for (int x = 0; x < levelWidth; x++) {

				if (tileColors [x + y * levelWidth] == grassColor) {
					Instantiate (grassTile, new Vector3 (x, y), Quaternion.identity);
				}

				else if (tileColors [(x + y * levelWidth)] == stoneColor) {
					Instantiate (grassTile, new Vector3 (x, y), Quaternion.identity);
				}

				else if (tileColors[x + y * levelWidth] == spawnPointColor) {
					Instantiate (grassTile, new Vector3 (x, y), Quaternion.identity);
					Vector2 pos = new Vector2(x,y);
					player.transform.position = pos;

				}
				else if (tileColors[x + y * levelWidth] == enemySpawnPointColor) {
					Instantiate (grassTile, new Vector3 (x, y), Quaternion.identity);
					Vector2 pos1 = new Vector2(x,y);
					pitchfork.transform.position = pos1;
				}
			}
		}


	}
}