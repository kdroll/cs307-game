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
	GameObject enemy;

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
		//int hello = 0;
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		player = GameObject.FindGameObjectWithTag("Player");
		pitchfork = GameObject.FindGameObjectWithTag ("Pitchfork");
		levelHeight = levelTexture.height;
		levelWidth = levelTexture.width;
		loadLevel ();



	}

	// Update is called once per frame
	void Update () {

	}

    //return false if position is filled
    bool checkIfPosEmpty(Vector3 target)
    {
        /*var hit = Physics.OverlapSphere(target, 1);
        print("hit = " + hit.Length);
        if(hit.Length > 0)
        {
            print("target = " + target);
            return false;
        }
        return true;*/
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
            print("got hit");
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
                        }
                    }
                }
            }
		}

        /*for(int i = 0; i < pointIndex; i++)
        {
            print("point " + i + " : x = " + positions[i].X + "   y = " + positions[i].Y);
        }*/


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
					Vector2 pos2 = new Vector2 (x + 2f, y + 2f);
					enemy.transform.position = pos1;
					pitchfork.transform.position = pos2;
				}
			}
		}


	}
}