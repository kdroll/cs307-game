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

	static public GameObject player;
	GameObject pitchfork;
	static public GameObject enemy;
	GameObject sword;
	public GameObject newEnemy;

	GameObject[] amount;

	public static int[,] walls = new int[64, 64];


	// Use this for initialization
	
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    static int density = 200;
    static int maxWallLength = 4;
    Point[] positions = new Point[density * maxWallLength];
    int pointIndex = 0;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        PerkMenu.inPerkMenu = false;
		player = GameObject.FindGameObjectWithTag("Player");
		pitchfork = GameObject.FindGameObjectWithTag ("Pitchfork");
		sword = GameObject.FindGameObjectWithTag ("Sword");
		enemy = GameObject.FindGameObjectWithTag("Enemy");
        PlayerHealth.isDead = false;
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



	}

	// Update is called once per frame
	void Update () {
		System.Random randy = new System.Random();
		amount = GameObject.FindGameObjectsWithTag ("Enemy");
		if (amount.Length < 6) {
			Instantiate (newEnemy, new Vector3 (10,10,0), Quaternion.identity);
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
                Instantiate(stoneTile, new Vector3(randx, randy), Quaternion.identity);
				walls [(int)randx,(int)randy] = 1;
                Point p = new Point();
                p.X = randx;
                p.Y = randy;
                positions[pointIndex] = p;
                pointIndex++;
                placeMore = 1;
            }
            if (placeMore == 1)
            {
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
                            Point p = new Point();
                            p.X = randx;
                            p.Y = randy;
                            positions[pointIndex] = p;
                            pointIndex++;
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
                            Point p = new Point();
                            p.X = randx;
                            p.Y = randy;
                            positions[pointIndex] = p;
                            pointIndex++;
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
                            Point p = new Point();
                            p.X = randx;
                            p.Y = randy;
                            positions[pointIndex] = p;
                            pointIndex++;
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
                            Point p = new Point();
                            p.X = randx;
                            p.Y = randy;
                            positions[pointIndex] = p;
                            pointIndex++;
                            Instantiate(stoneTile, new Vector3(randx, randy + i), Quaternion.identity);
							walls [(int)randx,(int)(randy + i)] = 1;
                        }
                    }
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

                    Vector2 pos = new Vector2(x,y);
					player.transform.position = pos;

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
					enemy.transform.position = pos1;
					pitchfork.transform.position = pos2;
					sword.transform.position = pos3;
				}
			}
		}

        for(int i = 0; i < 5; i++)
        {
            Instantiate(enemy, new Vector3((levelWidth/2 + i*5), (levelHeight/2 + i*5)), Quaternion.identity);
        }


	}
}