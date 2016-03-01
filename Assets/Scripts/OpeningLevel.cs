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

		print ("levelHeight = " + levelHeight);
		print ("levelWidth = " + levelWidth);

		//boundary left
		for(int leftBW = 0; leftBW < 7; leftBW++) {
			for(int leftBH = 0; leftBH < 64; leftBH++) {
				Instantiate(stoneTile, new Vector3 ( leftBW, leftBH), Quaternion.identity);
			}
		}

		//boundary right
		for(int rightBW = 0; rightBW < 7; rightBW++) {
			for(int rightBH = 0; rightBH < 64; rightBH++) {
				Instantiate(stoneTile, new Vector3 ( 64 - rightBW, rightBH), Quaternion.identity);
			}
		}

		//boundary down
		for(int downBW = 3; downBW < 62; downBW++) {
			for(int downBH = 0; downBH < 7; downBH++) {
				Instantiate(stoneTile, new Vector3 ( downBW, downBH), Quaternion.identity);
			}
		}

		//boundary up
		for(int upBW = 3; upBW < 62; upBW++) {
			for(int upBH = 57; upBH < 64; upBH++) {
				Instantiate(stoneTile, new Vector3 ( upBW, upBH), Quaternion.identity);
			}
		}
			

		//random boundaries
		int randx, randy, adjacent, howMany;
		int divisor = 9;
		int density = 70;
		int maxWallLength = 4;
		for (int a = 0; a < density; a++) { 
			randx = rand.Next (levelWidth / divisor, ((divisor - 1) * levelWidth) / divisor);
			randy = rand.Next (levelWidth / divisor, ((divisor - 1) * levelWidth) / divisor);
			adjacent = rand.Next (0, 3);
			howMany = rand.Next (1, maxWallLength);
			if (adjacent == 0) {
				//left
				for (int i = 1; i <= howMany; i++) {
					Instantiate (stoneTile, new Vector3 (randx - i, randy), Quaternion.identity);
				}
			} else if (adjacent == 1) {
				//right
				for (int i = 1; i <= howMany; i++) {
					Instantiate (stoneTile, new Vector3 (randx + i, randy), Quaternion.identity);
				}
			} else if (adjacent == 2) {
				//down
				for (int i = 1; i <= howMany; i++) {
					Instantiate (stoneTile, new Vector3 (randx, randy - i), Quaternion.identity);
				}
			} else if (adjacent == 3) {
				//up
				for (int i = 1; i <= howMany; i++) {
					Instantiate (stoneTile, new Vector3 (randx, randy + i), Quaternion.identity);
				}
			}
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