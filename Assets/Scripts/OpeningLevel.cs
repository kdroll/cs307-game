using UnityEngine;
using System.Collections;


public class OpeningLevel : MonoBehaviour {
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
    void Start () {
        time = Time.time;
        if (LoadOnClick.difficultySet) {
        
        } else {
            difficulty = 2;
        }
        density = 60 / difficulty;
        positions = new Point[density * maxWallLength];
        spawnRate = 300;
        Time.timeScale = 1;
        PerkMenu.inPerkMenu = false;
        StatsMenu.inStatsMenu = false;
		player = GameObject.FindGameObjectWithTag("Player");
		pitchfork = GameObject.FindGameObjectWithTag ("Pitchfork");
		sword = GameObject.FindGameObjectWithTag ("Sword");
		enemy = GameObject.FindGameObjectWithTag("Enemy");
        Pepper = GameObject.FindGameObjectWithTag("Pepper");
        ToxicWaste = GameObject.FindGameObjectWithTag("ToxicWaste");
        Bacon = GameObject.FindGameObjectWithTag("Bacon");
        Cupcake = GameObject.FindGameObjectWithTag("Cupcake");        PlayerHealth.isDead = false;
        EnemyAi.numEnemiesDestroyed = 0;
        EnemyAi.goldBonus = 0;
		levelHeight = levelTexture.height;
		levelWidth = levelTexture.width;
		for (int i = 0; i < 64; i++) {
			for (int j = 0; j < 64; j++) {
				walls [i,j] = 0;
			}
		}
        if (PlayerHealth.perks[0] == 1) {
            EnemyAi.goldBonus += 3;
        }
		loadLevel ();

        updateCountSpawnNum = 0;
        //print("time = " + time + "   spawnRate " + spawnRate);
        //print("" + LoadOnClick.difficultySet);

    }

	// Update is called once per frame
	void Update () {
		System.Random randy = new System.Random();
		amount = GameObject.FindGameObjectsWithTag ("Enemy");

		if(Time.time - time > difficulty*3)
        {
            spawnRate -= 10;
            time = Time.time;
            //print("time = " + time + "   spawnRate " + spawnRate);
            if(spawnRate <= (20*difficulty - 10))
            {
                spawnRate = (20 * difficulty - 10);
            }
        }
        if(updateCountSpawnNum % spawnRate == 0)
        {
            int choice = 1;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 0)
            {
                float maxDist = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, new Vector3(10, 54, 0));
                if (maxDist < Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, new Vector3(54, 54, 0)))
                {
                    maxDist = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, new Vector3(54, 54, 0));
                    choice = 2;
                }
                if (maxDist < Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, new Vector3(54, 10, 0)))
                {
                    maxDist = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, new Vector3(54, 10, 0));
                    choice = 3;
                }
                if (maxDist < Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, new Vector3(10, 10, 0)))
                {
                    maxDist = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, new Vector3(10, 10, 0));
                    choice = 4;
                }
                Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                if (choice == 1)
                {
                    //Instantiate(enemy, new Vector3(10,54,0), Quaternion.identity);
                    GameObject enemyClone = GameObject.FindGameObjectWithTag("Enemy");
                    
                    Instantiate(enemyClone, new Vector3(playerPos.x - 7, playerPos.y + 7, 0), Quaternion.identity);
                    print("spawned enemy at top left");
                }
                else if (choice == 2)
                {
                    //Instantiate(enemy, new Vector3(54, 54, 0), Quaternion.identity);
                    GameObject enemyClone = GameObject.FindGameObjectWithTag("Enemy");
                   
                    Instantiate(enemyClone, new Vector3(playerPos.x + 7, playerPos.y + 7, 0), Quaternion.identity);
                    print("spawned enemy at top right");
                }
                else if (choice == 3)
                {
                    //Instantiate(enemy, new Vector3(54, 10, 0), Quaternion.identity);
                    GameObject enemyClone = GameObject.FindGameObjectWithTag("Enemy");
                 
                    Instantiate(enemyClone, new Vector3(playerPos.x + 7, playerPos.y - 7, 0), Quaternion.identity);
                    print("spawned enemy at bottom right");
                }
                else if (choice == 4)
                {
                    //Instantiate(enemy, new Vector3(10, 10, 0), Quaternion.identity);
                   GameObject enemyClone = GameObject.FindGameObjectWithTag("Enemy");
                  
                    Instantiate(enemyClone, new Vector3(playerPos.x - 7, playerPos.y - 7, 0), Quaternion.identity);
                    print("spawned enemy at bottom left");
                }
            }
        }

        updateCountSpawnNum++;
        if(updateCountSpawnNum > 3600)
        {
            updateCountSpawnNum = 0;
        }

	}

    //return false if position is filled
    bool checkIfPosEmpty(Vector3 target)
    {
        int hit = 0;
        for(int i = 0; i < pointIndex; i++)
        {
            if(positions[i].X == target.x && positions[i].Y == target.y)
            {
                hit = 1;
                break;
            }
        }
        if(hit == 1) 
        {

            return false;
        }
        return true;
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
				Instantiate(wallTile, new Vector3 ( leftBW, leftBH), Quaternion.identity);
			}
		}

		//boundary right
		for(int rightBW = 0; rightBW < 7; rightBW++) {
			for(int rightBH = 0; rightBH < 64; rightBH++) {
				Instantiate(wallTile, new Vector3 ( 64 - rightBW, rightBH), Quaternion.identity);
			}
		}

		//boundary down
		for(int downBW = 3; downBW < 62; downBW++) {
			for(int downBH = 0; downBH < 7; downBH++) {
				Instantiate(wallTile, new Vector3 ( downBW, downBH), Quaternion.identity);
			}
		}

		//boundary up
		for(int upBW = 3; upBW < 62; upBW++) {
			for(int upBH = 57; upBH < 64; upBH++) {
				Instantiate(wallTile, new Vector3 ( upBW, upBH), Quaternion.identity);
			}
		}
        //random boundaries
        int randx, randy, adjacent, howMany, randGrass;
		int divisor = 9;
		
		for (int a = 0; a < density; a++) { 
			randx = rand.Next (levelWidth / divisor, ((divisor - 1) * levelWidth) / divisor);
			randy = rand.Next (levelWidth / divisor, ((divisor - 1) * levelWidth) / divisor);
            if (randx <= 9 || randx >= 55 || randy <= 9 || randy >= 55)
                continue;
			adjacent = rand.Next (0, 3);
			howMany = rand.Next (1, maxWallLength);
            int placeMore = 0;
            if (checkIfPosEmpty(new Vector3(randx + 1, randy)) && checkIfPosEmpty(new Vector3(randx - 1, randy)) && checkIfPosEmpty(new Vector3(randx, randy - 1)) && checkIfPosEmpty(new Vector3(randx, randy + 1))
                && checkIfPosEmpty(new Vector3(randx + 2, randy)) && checkIfPosEmpty(new Vector3(randx - 2, randy)) && checkIfPosEmpty(new Vector3(randx, randy - 2)) && checkIfPosEmpty(new Vector3(randx, randy + 2))
                && checkIfPosEmpty(new Vector3(randx + 1, randy - 1)) && checkIfPosEmpty(new Vector3(randx - 1, randy - 1)) && checkIfPosEmpty(new Vector3(randx - 1, randy - 1)) && checkIfPosEmpty(new Vector3(randx - 1, randy + 1))
                && checkIfPosEmpty(new Vector3(randx + 2, randy - 1)) && checkIfPosEmpty(new Vector3(randx - 2, randy - 1)) && checkIfPosEmpty(new Vector3(randx - 1, randy - 2)) && checkIfPosEmpty(new Vector3(randx - 1, randy + 2))
                && checkIfPosEmpty(new Vector3(randx + 1, randy + 1)) && checkIfPosEmpty(new Vector3(randx - 1, randy + 1)) && checkIfPosEmpty(new Vector3(randx + 1, randy - 1)) && checkIfPosEmpty(new Vector3(randx + 1, randy + 1))
                && checkIfPosEmpty(new Vector3(randx + 2, randy + 1)) && checkIfPosEmpty(new Vector3(randx - 2, randy + 1)) && checkIfPosEmpty(new Vector3(randx + 1, randy - 2)) && checkIfPosEmpty(new Vector3(randx + 1, randy + 2))
                && checkIfPosEmpty(new Vector3(randx, randy)))
            {
                /*Instantiate(stoneTile, new Vector3(randx, randy), Quaternion.identity);
				walls [(int)randx,(int)randy] = 1;
                Point p = new Point();
                p.X = randx;
                p.Y = randy;
                positions[pointIndex] = p;
                pointIndex++;*/
                placeMore = 1;
            }
            if (placeMore == 1)
            {
                Point p = new Point();
                int atLeastPlacedOne = 0;
                if (adjacent == 0)
                {
                    //left
                    for (int i = 1; i <= howMany; i++)
                    {
                        if (checkIfPosEmpty(new Vector3(randx - i - 1, randy)) && checkIfPosEmpty(new Vector3(randx - i - 2, randy))
                            && checkIfPosEmpty(new Vector3(randx, randy + 1)) && checkIfPosEmpty(new Vector3(randx, randy - 1))
                            && checkIfPosEmpty(new Vector3(randx, randy + 2)) && checkIfPosEmpty(new Vector3(randx, randy - 2))
                            && checkIfPosEmpty(new Vector3(randx - 1, randy + 1)) && checkIfPosEmpty(new Vector3(randx - 1, randy - 1))
                            && checkIfPosEmpty(new Vector3(randx - 1, randy + 2)) && checkIfPosEmpty(new Vector3(randx - 1, randy - 2))
                            && checkIfPosEmpty(new Vector3(randx - i, randy)))
                        {
                            p = new Point(); //Point p = new Point();
                            p.X = randx;
                            p.Y = randy;
                            positions[pointIndex] = p;
                            pointIndex++;
                            atLeastPlacedOne++;
                            Instantiate(stoneTile, new Vector3(randx - i, randy), Quaternion.identity);
							walls [(int)(randx - i),(int)randy] = 1;
                        }
                    }
                }
                else if (adjacent == 1)
                {
                    //right
                    for (int i = 1; i <= howMany; i++)
                    {
                        if (checkIfPosEmpty(new Vector3(randx + i + 1, randy)) && checkIfPosEmpty(new Vector3(randx + i + 2, randy))
                            && checkIfPosEmpty(new Vector3(randx, randy + 1)) && checkIfPosEmpty(new Vector3(randx, randy - 1))
                            && checkIfPosEmpty(new Vector3(randx, randy + 2)) && checkIfPosEmpty(new Vector3(randx, randy - 2))
                            && checkIfPosEmpty(new Vector3(randx + 1, randy + 1)) && checkIfPosEmpty(new Vector3(randx + 1, randy - 1))
                            && checkIfPosEmpty(new Vector3(randx + 1, randy + 2)) && checkIfPosEmpty(new Vector3(randx + 1, randy - 2))
                            && checkIfPosEmpty(new Vector3(randx + i, randy)))
                        {
                            p = new Point();
                            p.X = randx;
                            p.Y = randy;
                            positions[pointIndex] = p;
                            pointIndex++;
                            atLeastPlacedOne++;
                            Instantiate(stoneTile, new Vector3(randx + i, randy), Quaternion.identity);
							walls [(int)(randx + i),(int)randy] = 1;
                        }
                    }
                }
                else if (adjacent == 2)
                {
                    //down
                    for (int i = 1; i <= howMany; i++)
                    {
                        if (checkIfPosEmpty(new Vector3(randx, randy - i - 1)) && checkIfPosEmpty(new Vector3(randx, randy - i - 2))
                            && checkIfPosEmpty(new Vector3(randx - 1, randy)) && checkIfPosEmpty(new Vector3(randx + 1, randy))
                            && checkIfPosEmpty(new Vector3(randx - 2, randy)) && checkIfPosEmpty(new Vector3(randx + 2, randy))
                            && checkIfPosEmpty(new Vector3(randx - 1, randy - 1)) && checkIfPosEmpty(new Vector3(randx + 1, randy - 1))
                            && checkIfPosEmpty(new Vector3(randx - 2, randy - 1)) && checkIfPosEmpty(new Vector3(randx + 2, randy - 1))
                            && checkIfPosEmpty(new Vector3(randx, randy - i)))
                        {
                            p = new Point();
                            p.X = randx;
                            p.Y = randy;
                            positions[pointIndex] = p;
                            pointIndex++;
                            atLeastPlacedOne++;
                            Instantiate(stoneTile, new Vector3(randx, randy - i), Quaternion.identity);
							walls [(int)randx,(int)(randy - i)] = 1;
                        }
                    }
                }
                else if (adjacent == 3)
                {
                    //up
                    for (int i = 1; i <= howMany; i++)
                    {
                        if (checkIfPosEmpty(new Vector3(randx, randy + i + 1)) && checkIfPosEmpty(new Vector3(randx, randy + i + 2))
                            && checkIfPosEmpty(new Vector3(randx - 1, randy)) && checkIfPosEmpty(new Vector3(randx + 1, randy))
                            && checkIfPosEmpty(new Vector3(randx - 2, randy)) && checkIfPosEmpty(new Vector3(randx + 2, randy))
                            && checkIfPosEmpty(new Vector3(randx - 1, randy + 1)) && checkIfPosEmpty(new Vector3(randx + 1, randy + 1))
                            && checkIfPosEmpty(new Vector3(randx - 2, randy + 1)) && checkIfPosEmpty(new Vector3(randx + 2, randy + 1))
                            && checkIfPosEmpty(new Vector3(randx, randy + i)))
                        {
                            p = new Point();
                            p.X = randx;
                            p.Y = randy;
                            positions[pointIndex] = p;
                            pointIndex++;
                            atLeastPlacedOne++;
                            Instantiate(stoneTile, new Vector3(randx, randy + i), Quaternion.identity);
							walls [(int)randx,(int)(randy + i)] = 1;
                        }
                    }
                }
                if (atLeastPlacedOne > 0)
                {
                    Instantiate(stoneTile, new Vector3(randx, randy), Quaternion.identity);
                    walls[(int)randx, (int)randy] = 1;
                    p = new Point();
                    p.X = randx;
                    p.Y = randy;
                    positions[pointIndex] = p;
                    pointIndex++;
                }
            }
		}
        

        /*for(int i = 0; i < pointIndex; i++)
        {
            print("point " + i + " : x = " + positions[i].X + "   y = " + positions[i].Y);
        }*/

        // Dispersion of grass/flower tiles. Higher number means less flowers.
        int grassDispersion = 60;
		for (int y = 0; y < levelHeight; y++) {
			for (int x = 0; x < levelWidth; x++) {
                randGrass = rand.Next(1, grassDispersion);
                if (tileColors [x + y * levelWidth] == grassColor) {
                    if (randGrass == 2)
                        Instantiate(grassTile2, new Vector3(x, y), Quaternion.identity);
                    else if (randGrass == 3)
                        Instantiate(grassTile3, new Vector3(x, y), Quaternion.identity);
                    else if (randGrass == 4)
                        Instantiate(grassTile4, new Vector3(x, y), Quaternion.identity);
                    else
                        Instantiate(grassTile1, new Vector3(x, y), Quaternion.identity);
                }

				else if (tileColors [(x + y * levelWidth)] == stoneColor) {
                    if (randGrass == 2)
                        Instantiate(grassTile2, new Vector3(x, y), Quaternion.identity);
                    else if (randGrass == 3)
                        Instantiate(grassTile3, new Vector3(x, y), Quaternion.identity);
                    else if (randGrass == 4)
                        Instantiate(grassTile4, new Vector3(x, y), Quaternion.identity);
                    else
                        Instantiate(grassTile1, new Vector3(x, y), Quaternion.identity);
                }

				else if (tileColors[x + y * levelWidth] == spawnPointColor) {
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
				else if (tileColors[x + y * levelWidth] == enemySpawnPointColor) {
                    if (randGrass == 2)
                        Instantiate(grassTile2, new Vector3(x, y), Quaternion.identity);
                    else if (randGrass == 3)
                        Instantiate(grassTile3, new Vector3(x, y), Quaternion.identity);
                    else if (randGrass == 4)
                        Instantiate(grassTile4, new Vector3(x, y), Quaternion.identity);
                    else
                        Instantiate(grassTile1, new Vector3(x, y), Quaternion.identity);
                    Vector2 pos1 = new Vector2(x,y);
					Vector2 pos2 = new Vector2 (x + 2f, y + 2f);
					Vector2 pos3 = new Vector2 (x + 3f, y + 3f);
					enemy.transform.position = new Vector3(0,0,0);  //before was equal to pos1
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