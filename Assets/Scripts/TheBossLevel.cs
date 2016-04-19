using UnityEngine;
using System.Collections;


public class TheBossLevel : MonoBehaviour
{
	private int levelHeight;
	private int levelWidth;

	public Color grassColor;
	public Color stoneColor;
	public Color spawnPointColor;
	public Color enemySpawnPointColor;

	public Transform grassTile1;
	public Transform grassTile2;
	public Transform grassTile3;
	public Transform grassTile4;
	public Transform stoneTile;
	public Transform wallTile;
	public static string spawnRateString;
	private Color[] tileColors;

	public Texture2D levelTexture;

	public static GameObject Bacon;
	public GameObject pitchforkclone;
	GameObject Cupcake;
	GameObject Pepper;
	GameObject ToxicWaste;


	static public GameObject player;
	GameObject pitchfork;
	static public GameObject enemy;
	GameObject sword;
	public GameObject newEnemy;
	float time;
	GameObject[] amount;
	int spawnRate;
	int updateCountSpawnNum;
	public static int difficulty; //3= easiest    1 = hardest
	public static int[,] walls = new int[64, 64];
	float truncatedspawn;
	float roundedspawn;

	// Use this for initialization

	public struct Point
	{
		public int X { get; set; }
		public int Y { get; set; }
	}
	static int density;
	static int maxWallLength = 4;
	Point[] positions;
	int pointIndex = 0;

	// Use this for initialization
	void Start()
	{
		time = Time.time;
		if (LoadOnClick.difficultySet)
		{

		}
		else {
			difficulty = 2;
		}
		density = 30 / difficulty;
		positions = new Point[density * maxWallLength];
		spawnRate = 300;
		spawnRateString = (3600 / spawnRate) + " enemies/min";
		Time.timeScale = 1;
		PerkMenu.inPerkMenu = false;
		StatsMenu.inStatsMenu = false;
		player = GameObject.FindGameObjectWithTag("Player");
		pitchfork = GameObject.FindGameObjectWithTag("Pitchfork");
		sword = GameObject.FindGameObjectWithTag("Sword");
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		Pepper = GameObject.FindGameObjectWithTag("Pepper");
		ToxicWaste = GameObject.FindGameObjectWithTag("ToxicWaste");
		Bacon = GameObject.FindGameObjectWithTag("Bacon");
		Cupcake = GameObject.FindGameObjectWithTag("Cupcake"); PlayerHealth.isDead = false;
		EnemyAi.numEnemiesDestroyed = 0;
		EnemyAi.goldBonus = 0;
		levelHeight = levelTexture.height;
		levelWidth = levelTexture.width;
		for (int i = 0; i < 64; i++)
		{
			for (int j = 0; j < 64; j++)
			{
				walls[i, j] = 0;
			}
		}
		if (PlayerHealth.perks[0] == 1)
		{
			EnemyAi.goldBonus += 3;
		}
		loadLevel();

		updateCountSpawnNum = 0;
		//print("time = " + time + "   spawnRate " + spawnRate);
		//print("" + LoadOnClick.difficultySet);

	}

	// Update is called once per frame
	void Update()
	{
		System.Random randy = new System.Random();
		amount = GameObject.FindGameObjectsWithTag("Enemy");



	}

	//return false if position is filled

	void loadLevel()
	{
		tileColors = new Color[(levelWidth * levelHeight)];
		tileColors = levelTexture.GetPixels();
		System.Random rand = new System.Random();

		//print ("levelHeight = " + levelHeight);
		//print ("levelWidth = " + levelWidth);


		//boundary left
		for (int leftBW = 0; leftBW < 7; leftBW++)
		{
			for (int leftBH = 0; leftBH < 64; leftBH++)
			{
				Instantiate(wallTile, new Vector3(leftBW, leftBH), Quaternion.identity);
			}
		}

		//boundary right
		for (int rightBW = 0; rightBW < 7; rightBW++)
		{
			for (int rightBH = 0; rightBH < 64; rightBH++)
			{
				Instantiate(wallTile, new Vector3(64 - rightBW, rightBH), Quaternion.identity);
			}
		}

		//boundary down
		for (int downBW = 3; downBW < 62; downBW++)
		{
			for (int downBH = 0; downBH < 7; downBH++)
			{
				Instantiate(wallTile, new Vector3(downBW, downBH), Quaternion.identity);
			}
		}

		//boundary up
		for (int upBW = 3; upBW < 62; upBW++)
		{
			for (int upBH = 57; upBH < 64; upBH++)
			{
				Instantiate(wallTile, new Vector3(upBW, upBH), Quaternion.identity);
			}
		}
		//random boundaries
		int randx, randy, adjacent, howMany, randGrass;

		/*for(int i = 0; i < pointIndex; i++)
        {
            print("point " + i + " : x = " + positions[i].X + "   y = " + positions[i].Y);
        }*/

		// Dispersion of grass/flower tiles. Higher number means less flowers.
		int grassDispersion = 60;
		for (int y = 0; y < levelHeight; y++)
		{
			for (int x = 0; x < levelWidth; x++)
			{
				randGrass = rand.Next(1, grassDispersion);
				if (tileColors[x + y * levelWidth] == grassColor)
				{
					if (randGrass == 2)
						Instantiate(grassTile2, new Vector3(x, y), Quaternion.identity);
					else if (randGrass == 3)
						Instantiate(grassTile3, new Vector3(x, y), Quaternion.identity);
					else if (randGrass == 4)
						Instantiate(grassTile4, new Vector3(x, y), Quaternion.identity);
					else
						Instantiate(grassTile1, new Vector3(x, y), Quaternion.identity);
				}

				else if (tileColors[(x + y * levelWidth)] == stoneColor)
				{
					if (randGrass == 2)
						Instantiate(grassTile2, new Vector3(x, y), Quaternion.identity);
					else if (randGrass == 3)
						Instantiate(grassTile3, new Vector3(x, y), Quaternion.identity);
					else if (randGrass == 4)
						Instantiate(grassTile4, new Vector3(x, y), Quaternion.identity);
					else
						Instantiate(grassTile1, new Vector3(x, y), Quaternion.identity);
				}

				else if (tileColors[x + y * levelWidth] == spawnPointColor)
				{
					if (randGrass == 2)
						Instantiate(grassTile2, new Vector3(x, y), Quaternion.identity);
					else if (randGrass == 3)
						Instantiate(grassTile3, new Vector3(x, y), Quaternion.identity);
					else if (randGrass == 4)
						Instantiate(grassTile4, new Vector3(x, y), Quaternion.identity);
					else
						Instantiate(grassTile1, new Vector3(x, y), Quaternion.identity);


					/*if (walls[x, y] == 0)
                    {
                        Vector2 pos = new Vector2(x, y);
                        player.transform.position = pos;
                    } else
                    {
                        int add = 1;
                        while(walls[x+add,y] != 0 && walls[x+add,y+3] != 0)
                        {
                            add++;
                        }
                        Vector2 pos = new Vector2(x+add, y);
                        Vector2 pos1 = new Vector2(x + add, y + 3);
                        player.transform.position = pos;
                        GameObject pitchforkclone = GameObject.FindGameObjectWithTag("Pitchfork");
                        Instantiate(pitchforkclone, pos1, Quaternion.identity);
                        print("instatiated pitchfork");
                        //pitchfork.transform.position = pos1;

                    }*/

				}
				else if (tileColors[x + y * levelWidth] == enemySpawnPointColor)
				{
					if (randGrass == 2)
						Instantiate(grassTile2, new Vector3(x, y), Quaternion.identity);
					else if (randGrass == 3)
						Instantiate(grassTile3, new Vector3(x, y), Quaternion.identity);
					else if (randGrass == 4)
						Instantiate(grassTile4, new Vector3(x, y), Quaternion.identity);
					else
						Instantiate(grassTile1, new Vector3(x, y), Quaternion.identity);
					Vector2 pos1 = new Vector2(x, y);
					Vector2 pos2 = new Vector2(x + 2f, y + 2f);
					Vector2 pos3 = new Vector2(x + 3f, y + 3f);
					enemy.transform.position = new Vector3(0, 0, 0);  //before was equal to pos1
					pitchfork.transform.position = pos2;
					sword.transform.position = pos3;


				}
			}
		}


		int add = 0;
		while (walls[32 + add, 32] != 0 && walls[32 + add, 32 + 3] != 0)
		{
			add++;
		}
		Vector2 pos = new Vector2(32 + add, 32);
		Vector2 pospitchfork = new Vector2(32 + add, 32 + 3);
		player.transform.position = pos;
		//print("player put onto position " + (32 + add) + "," + 32);
		//Instantiate(pitchforkclone, pospitchfork, Quaternion.identity);
		//print("instatiated pitchfork");
		//pitchfork.transform.position = pos1;


		//for(int i = 0; i < 5; i++)
		//{
		//    Instantiate(enemy, new Vector3((levelWidth/2 + i*5), (levelHeight/2 + i*5)), Quaternion.identity);
		//}


	}
}