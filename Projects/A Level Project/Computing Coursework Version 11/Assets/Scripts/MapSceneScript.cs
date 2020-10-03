using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapSceneScript : MonoBehaviour
{

    int[,] CreateMapBorder(int MapRowSize, int MapColumnSize)
    {
        int[,] MapArray = new int[MapRowSize, MapColumnSize];
        for (int x = 0; x < MapRowSize; x++)
        {
            MapArray[x, 0] = 1;
            MapArray[x, MapArray.GetLength(1) - 1] = 1;
        }
        for (int y = 0; y < MapColumnSize; y++)
        {
            MapArray[0, y] = 1;
            MapArray[MapArray.GetLength(0) - 1, y] = 1;
        }
        return MapArray;
    }

    int[,] CreateMapInside(int[,] MapArray)
    {
        int RowLength = MapArray.GetLength(0) - 1;
        int ColumnLength = MapArray.GetLength(1) - 1;
        int NumberOfWallsInside = (RowLength - ((RowLength - 1)/2)) - 2;
        List<int> ListOfYCoorindates = new List<int>();
        int RandomYCoordinateOne = 0;
        int RandomYCoordinateTwo = 0;
        for (int x = 0; x < RowLength; x = x + 2)  //This creates verticle walls every even x value in the MapArray
        {
            for (int y = 0; y < ColumnLength; y++)
            {
                MapArray[x, y] = 1;
            }
        }
        for (int x = 0; x < NumberOfWallsInside; x = x + 1)   //This picks the gaps to be made (haven't been made yet)
        {
            RandomYCoordinateOne = UnityEngine.Random.Range(1, ColumnLength);  //int min inclusive, int max exclusive
            RandomYCoordinateTwo = UnityEngine.Random.Range(1, ColumnLength);  //int min inclusive, int max exclusive
			if (RandomYCoordinateOne == RandomYCoordinateTwo || 
                RandomYCoordinateOne == RandomYCoordinateTwo - 1 || 
				RandomYCoordinateOne == RandomYCoordinateTwo + 1 || 
                RandomYCoordinateOne == RandomYCoordinateTwo - 2 || 
                RandomYCoordinateOne == RandomYCoordinateTwo + 2)
            {
                x = x - 1;
            }
            else
            {
                ListOfYCoorindates.Add(RandomYCoordinateOne);
                ListOfYCoorindates.Add(RandomYCoordinateTwo);
            }
        }
        int XCoordinate = 2;
        for (int x = 0; x < ListOfYCoorindates.Count; x = x + 2)    //This creates the gaps chosen previously
        {
            MapArray[XCoordinate, ListOfYCoorindates[x]] = 0;
            MapArray[XCoordinate, ListOfYCoorindates[x + 1]] = 0;
            XCoordinate = XCoordinate + 2;
        }
        int XCoordinateWallGaps = 1;
		int RandomYPointGaps = 0;
        for (int x = 0; x < ListOfYCoorindates.Count; x = x + 2)    //This creates a wall between the two gaps
        {
			if (ListOfYCoorindates [x] + 1 < ListOfYCoorindates [x + 1]) {
				RandomYPointGaps = UnityEngine.Random.Range ((ListOfYCoorindates [x] + 1), ListOfYCoorindates [x + 1]);
			} 
			else 
			{
				RandomYPointGaps = UnityEngine.Random.Range((ListOfYCoorindates[x + 1]) + 1, ListOfYCoorindates[x]);	
			}
            while (MapArray[(XCoordinateWallGaps - 1), RandomYPointGaps] == 0)
            {
				if (ListOfYCoorindates [x] + 1 < ListOfYCoorindates [x + 1]) {
					RandomYPointGaps = UnityEngine.Random.Range ((ListOfYCoorindates [x] + 1), ListOfYCoorindates [x + 1]);
				} 
				else 
				{
					RandomYPointGaps = UnityEngine.Random.Range((ListOfYCoorindates[x + 1]) + 1, ListOfYCoorindates[x]);	//int min inclusive, int max inclusive
				}
            }
            MapArray[XCoordinateWallGaps, RandomYPointGaps] = 1;
            if (x == ListOfYCoorindates.Count - 1)
            {
                continue;

           }
            else
            {
                XCoordinateWallGaps = XCoordinateWallGaps + 2;
            }
        }
        bool PlayerCoordinateFound = false;
        while (PlayerCoordinateFound == false)
        {
             int RandomXCoordinate = UnityEngine.Random.Range(1, RowLength - 1);   //min inclusive, max exclusive
             int RandomYCoordinate = UnityEngine.Random.Range(1, ColumnLength - 1);   //min inclusive, max exclusive
             if (MapArray[RandomXCoordinate, RandomYCoordinate] == 0)
             {
                 PlayerCoordinateFound = true;
                 MapArray[RandomXCoordinate, RandomYCoordinate] = 2;
				 for (int x = RandomXCoordinate - 1; x <= RandomXCoordinate + 1; x++) 
				 {
				 	for (int y = RandomYCoordinate - 1; y <= RandomYCoordinate + 1; y++) 
			 		{
						if (MapArray [x, y] == 1) 
						{
							MapArray [x, y] = 3;	//3 represensts the walls surrounding the player when the map is initalized
						}
						else if (MapArray [x, y] == 0) 
						{
							MapArray [x, y] = 4;	//4 represensts the floors surrounding the player when the map is initalized
						}
					}
				 }
             }
        }
		for (int x = 0; x < NumOfEnemies; x++) 
		{
			bool EnemyCoordinateFound = false;
			while (EnemyCoordinateFound == false) 
			{
				int RandomXCoordinate = UnityEngine.Random.Range (1, RowLength - 1);   //min inclusive, max exclusive
				int RandomYCoordinate = UnityEngine.Random.Range (1, ColumnLength - 1);   //min inclusive, max exclusive
				if (MapArray [RandomXCoordinate, RandomYCoordinate] == 0) 
				{
					EnemyCoordinateFound = true;
					MapArray [RandomXCoordinate, RandomYCoordinate] = (x + 4);	//x represents an enemy
				}
			}
		}
        return MapArray;
    }

	void DisplayMap(int[,] MapArray)
	{
		for (int x = 0; x < MapArray.GetLength(0); x++)
		{
			for (int y = 0; y < MapArray.GetLength(1); y++)
			{
				if (MapArray [x, y] == 0) {
					Instantiate (Floor);
					Floor.transform.position = new Vector2 (-120 + (25 * x), -170 + (25 * y));
				} else if (MapArray [x, y] == 1) {   
					Instantiate (Wall);
					Wall.transform.position = new Vector2 (-120 + (25 * x), -170 + (25 * y));
				} else if (MapArray [x, y] == 2) {   
					Instantiate (Floor);
					Floor.transform.position = new Vector2 (-120 + (25 * x), -170 + (25 * y));
					PlayerVision.transform.position = new Vector2 (-120 + (25 * x), -170 + (25 * y));
					PlayerPrefab.transform.position = new Vector2 (-120 + (25 * x), -170 + (25 * y));
				} else if (MapArray [x, y] == 3) {
					Instantiate (InitialiseWall);
					InitialiseWall.transform.position = new Vector2 (-120 + (25 * x), -170 + (25 * y));
				} else {
					Instantiate (Floor);
					Floor.transform.position = new Vector2 (-120 + (25 * x), -170 + (25 * y));
					Instantiate (EnemyPrefab);
					EnemyPrefab.transform.position = new Vector2 (-120 + (25 * x), -170 + (25 * y));
				}
			}
		}
		for (int x = 0; x < MapArray.GetLength(0); x++)
		{
			for (int y = 0; y < MapArray.GetLength(1); y++)
			{
				if (MapArray[x, y] == 2)
				{ 
					Instantiate(PlayerVision);
					PlayerVision.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
					Instantiate(PlayerPrefab);
					PlayerPrefab.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 3)
				{   
					Instantiate(InitialiseWall);
					InitialiseWall.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
			}
		}
	}

    public GameObject PlayerVision;
    public GameObject PlayerPrefab;
	public GameObject EnemyPrefab;
    public GameObject Wall;
	public GameObject InitialiseWall;
	public GameObject InitialiseFloor;
	public GameObject Floor;
    public int [,] MapArray;
	private string LastEnemyDirection;
	public List<int> MapArrayList;
	public List<int> LastEnemyDirections;
	public int LastPlayerDirection = 5;
	public int NumOfEnemies;
	public int Level = 1;
	public int NumOfTurnsLeftForSuperPower = 0;
	public bool PowerReady = true;
	public bool SceneLoaded = false;

    public void Main()         
    {
		for (int x = 0; x < NumOfEnemies; x++)
		{
			LastEnemyDirections.Add (5);
		}
        int MapRowSize = 25;        //Needs to be an odd number
        int MapColumnSize = 15;     //Needs to be an odd number
        MapArray = CreateMapBorder(MapRowSize, MapColumnSize);
        MapArray = CreateMapInside(MapArray);
        DisplayMap(MapArray);
    }

	void LevelCompleted()
	{
		if (GameObject.FindGameObjectsWithTag ("InvisibleWall").Length == 0 & GameObject.FindGameObjectsWithTag ("InvisibleFloor").Length == 0)
		{
			Level++;
			var AllEnemies = GameObject.FindGameObjectsWithTag ("Enemy"); 	//Removes all clones on the map
			foreach (var clone in AllEnemies) 
			{ 
				Destroy (clone);
			}
			var InvisibleWalls = GameObject.FindGameObjectsWithTag ("InvisibleWall"); 	//Removes all clones on the map
			foreach (var clone in InvisibleWalls) 
			{ 
				Destroy (clone);
			}
			var InvisibleFloors = GameObject.FindGameObjectsWithTag ("InvisibleFloor"); 	//Removes all clones on the map
			foreach (var clone in InvisibleFloors) 
			{ 
				Destroy (clone);
			}
			var VisibleWalls = GameObject.FindGameObjectsWithTag ("VisibleWall"); 	//Removes all clones on the map
			foreach (var clone in VisibleWalls) 
			{ 
				Destroy (clone);
			}
			var VisibleFloors = GameObject.FindGameObjectsWithTag ("VisibleFloor"); 	//Removes all clones on the map
			foreach (var clone in VisibleFloors) 
			{ 
				Destroy (clone);
			}
			Destroy (GameObject.Find("PlayerPrefab(Clone)"));
			Destroy (GameObject.Find("PlayerVision(Clone)"));
			Main ();
		}
	}

	void GameOver()
	{
		var AllEnemies = GameObject.FindGameObjectsWithTag ("Enemy"); 	//Removes all clones on the map
		foreach (var clone in AllEnemies) 
		{ 
			clone.GetComponent<Renderer> ().material.color = Color.red;
		}
		var AllWalls = GameObject.FindGameObjectsWithTag ("InvisibleWall"); 	//Removes all clones on the map
		foreach (var clone in AllWalls) 
		{ 
			clone.GetComponent<Renderer> ().material.color = Color.white; 
		}
		var AllFloors = GameObject.FindGameObjectsWithTag ("InvisibleFloor"); 	//Removes all clones on the map
		foreach (var clone in AllFloors) 
		{ 
			clone.GetComponent<Renderer> ().material.color = Color.black; 
		}
	}

    void EnemyMovement()
    {
        Debug.Log("HHHHHHHHHHHHHHHHHHH");
            var AllEnemies = GameObject.FindGameObjectsWithTag("Enemy");    //Removes all clones on the map
            foreach (var clone in AllEnemies)
            {
                Destroy(clone);
                int CloneXPos = (Mathf.RoundToInt(clone.transform.position.x) + 120) / 25;
                int CloneYPos = (Mathf.RoundToInt(clone.transform.position.y) + 170) / 25;
                //(-120 + (25 * x)
                //(-170 + (25 * y));
                bool EnemyNextToPlayer = false;
                if (clone.GetComponent<Renderer>().material.color == Color.grey)    //Is the enemy invisible?
                {
                    Debug.Log("Hello");
                    for (int x = -1; x < 2; x++)    //Is the player next to the enemy?
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            if (MapArray[CloneXPos + x, CloneYPos + y] == 2)
                            {
                                EnemyNextToPlayer = true;
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Not");
                    MoveEnemy(CloneXPos, CloneYPos);
                }
                if (EnemyNextToPlayer == true)
                {
                    MoveEnemy(CloneXPos, CloneYPos);
                }
                EnemyNextToPlayer = false;
            }
    }

    void MoveEnemy(int CloneXPos, int CloneYPos)
    {
        List<int> PossibleEnemyDirections = new List<int>();
        if (MapArray[CloneXPos, CloneYPos + 1] == 0 || MapArray[CloneXPos, CloneYPos + 1] == 2)     //Checks if spaces around enemies are available and adds them to list
        {
            PossibleEnemyDirections.Add(1); //1 represents up
        }
        if (MapArray[CloneXPos, CloneYPos - 1] == 0 || MapArray[CloneXPos, CloneYPos - 1] == 2)
        {
            PossibleEnemyDirections.Add(2); //2 represents down
        }
        if (MapArray[CloneXPos + 1, CloneYPos] == 0 || MapArray[CloneXPos + 1, CloneYPos] == 2)
        {
            PossibleEnemyDirections.Add(3); //3 represents right
        }
        if (MapArray[CloneXPos - 1, CloneYPos] == 0 || MapArray[CloneXPos - 1, CloneYPos] == 2)
        {
            PossibleEnemyDirections.Add(4); //4 represents left
        }
        int RandomDirectionAvailableIndex = UnityEngine.Random.Range(0, PossibleEnemyDirections.Count);     //Randomly picks a direction
        if (PossibleEnemyDirections[RandomDirectionAvailableIndex] == 1)
        {
            MapArray[CloneXPos, CloneYPos + 1] = MapArray[CloneXPos, CloneYPos];
            MapArray[CloneXPos, CloneYPos] = 0;
        }
        if (PossibleEnemyDirections[RandomDirectionAvailableIndex] == 2)
        {
            MapArray[CloneXPos, CloneYPos - 1] = MapArray[CloneXPos, CloneYPos];
            MapArray[CloneXPos, CloneYPos] = 0;
        }
        if (PossibleEnemyDirections[RandomDirectionAvailableIndex] == 4)
        {
            MapArray[CloneXPos - 1, CloneYPos] = MapArray[CloneXPos, CloneYPos];
            MapArray[CloneXPos, CloneYPos] = 0;
        }
        if (PossibleEnemyDirections[RandomDirectionAvailableIndex] == 3)
        {
            MapArray[CloneXPos + 1, CloneYPos] = MapArray[CloneXPos, CloneYPos];
            MapArray[CloneXPos, CloneYPos] = 0;
        }
    }

    void DisplayCharacters(int[,] MapArray)
	{
		for (int x = 0; x < MapArray.GetLength(0); x++)
		{
			for (int y = 0; y < MapArray.GetLength(1); y++)
			{
				if (MapArray[x, y] == 0)
				{
				}
				else if (MapArray[x, y] == 1)
				{   
				}
				else if (MapArray[x, y] == 2)
				{
					Destroy (GameObject.Find("PlayerPrefab(Clone)"));
					PlayerPrefab.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 3)
				{
				}
				else if (MapArray[x, y] == 4)
				{
				}
				else
				{
					EnemyPrefab.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
			}
		}
		for (int x = 0; x < MapArray.GetLength(0); x++)
		{
			for (int y = 0; y < MapArray.GetLength(1); y++)
			{
				if (MapArray[x, y] == 0)
				{
				}
				else if (MapArray[x, y] == 1)
				{
				}
				else if (MapArray[x, y] == 2)
				{
					Instantiate(PlayerPrefab);
					PlayerPrefab.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 3)
				{
				}
				else if (MapArray[x, y] == 4)
				{
				}
				else
				{
					Instantiate(EnemyPrefab);
					EnemyPrefab.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
			}
		}
	}

    void Update()
    {
        Debug.Log("JJJJJJJJJJJJJJJJJJJ");
        LevelCompleted();
        int PlayerXPos = 0;
        int PlayerYPos = 0;
        for (int x = 0; x < MapArray.GetLength(0); x++)     //Finds the players position 
        {
            for (int y = 0; y < MapArray.GetLength(1); y++)
            {
                if (MapArray[x, y] == 2)
                {
                    PlayerXPos = x;
                    PlayerYPos = y;
                }
            }
        }
        if (PlayerXPos == 0 & PlayerYPos == 0)  //If player is touching enemy then calls game over function.
        {
            GameOver();
        }
        else    //Decides whether player can move and moves them.
        {
            for (int x = PlayerXPos - 1; x <= PlayerXPos + 1; x++)
            {
                for (int y = PlayerYPos - 1; y <= PlayerYPos + 1; y++)
                {
                    if (MapArray[x, y] == 4)
                    {
                        MapArray[x, y] = 0; //4 represensts the floors surrounding the player when the map is initalized
                    }
                }
            }
            if (Input.GetKeyDown("down") & MapArray[PlayerXPos, PlayerYPos - 1] == 0)
            {
                MapArray[PlayerXPos, PlayerYPos - 1] = 2;
                MapArray[PlayerXPos, PlayerYPos] = 0;
                Debug.Log("IIIIIIIIIIIIIIII");
                EnemyMovement();
                DisplayCharacters(MapArray);
            }
            else if (Input.GetKeyDown("up") & MapArray[PlayerXPos, PlayerYPos + 1] == 0)
            {
                MapArray[PlayerXPos, PlayerYPos + 1] = 2;
                MapArray[PlayerXPos, PlayerYPos] = 0;
                EnemyMovement();
                DisplayCharacters(MapArray);
            }
            else if (Input.GetKeyDown("left") & MapArray[PlayerXPos - 1, PlayerYPos] == 0)
            {
                MapArray[PlayerXPos - 1, PlayerYPos] = 2;
                MapArray[PlayerXPos, PlayerYPos] = 0;
                EnemyMovement();
                DisplayCharacters(MapArray);
            }
            else if (Input.GetKeyDown("right") & MapArray[PlayerXPos + 1, PlayerYPos] == 0)
            {
                MapArray[PlayerXPos + 1, PlayerYPos] = 2;
                MapArray[PlayerXPos, PlayerYPos] = 0;
                EnemyMovement();
                DisplayCharacters(MapArray);
            }
            else if (Input.GetKeyDown("down") || Input.GetKeyDown("up") || Input.GetKeyDown("left") || Input.GetKeyDown("right"))
            {
                EnemyMovement();
                DisplayCharacters(MapArray);
            }
        }
    }
}
