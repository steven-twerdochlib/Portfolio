using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class CreateMapSceneScript : MonoBehaviour {

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
					MapArray [RandomXCoordinate, RandomYCoordinate] = x + 5;	//x represents an enemy
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
				if (MapArray[x, y] == 0)
				{
					Floor.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 1)
				{   
					Wall.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 2)
				{
					PlayerPrefab.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
					PlayerVision.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 3)
				{
					InitialiseWall.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 4)
				{
					InitialiseFloor.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else
				{
					Floor.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
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
					Instantiate(Floor);
					Floor.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 1)
				{   
					Instantiate(Wall);
					Wall.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 2)
				{
					Instantiate(InitialiseFloor);
					InitialiseFloor.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
					Instantiate(PlayerPrefab);
					PlayerPrefab.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
					Instantiate(PlayerVision);
					PlayerVision.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 3)
				{
					Instantiate(InitialiseWall);
					InitialiseWall.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else if (MapArray[x, y] == 4)
				{
					Instantiate(InitialiseFloor);
					InitialiseFloor.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
				else
				{
					Instantiate(Floor);
					Floor.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
					Instantiate(EnemyPrefab);
					EnemyPrefab.transform.position = new Vector2(-120 + (25 * x), -170 + (25 * y));
				}
			}
		}
	}

	public static CreateMapSceneScript CreateMapScene;
	public GameObject PlayerVision;
	public GameObject PlayerPrefab;
	public GameObject EnemyPrefab;
	public GameObject Wall;
	public GameObject InitialiseWall;
	public GameObject InitialiseFloor;
	public GameObject Floor;
	public int[,] MapArray;
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
		NumOfEnemies = Level;
		int MapRowSize = 25;        //Needs to be an odd number
		int MapColumnSize = 15;     //Needs to be an odd number
		for (int x = 0; x < NumOfEnemies; x++)
		{
			LastEnemyDirections.Add (5);
		}
		MapArray = CreateMapBorder(MapRowSize, MapColumnSize);
		MapArray = CreateMapInside(MapArray);
		for (int x = 0; x < MapArray.GetLength(0); x++)
		{
			for (int y = 0; y < MapArray.GetLength(1); y++)
			{
				MapArrayList.Add (MapArray[x,y]);
			}
		}
		Debug.Log (Level);
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
			Destroy (GameObject.FindWithTag("Player"));
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

    void EnemyMovement(int PlayerXPos, int PlayerYPos)
    {
        var AllEnemies = GameObject.FindGameObjectsWithTag("Enemy");    //Removes all clones on the map
        int NumOfLoops = 0;
        foreach (var clone in AllEnemies)
        {
            int CloneXPos = (Mathf.RoundToInt(clone.transform.position.x) + 120) / 25;
            int CloneYPos = (Mathf.RoundToInt(clone.transform.position.y) + 170) / 25;
            //(-120 + (25 * x)
            //(-170 + (25 * y));
            bool EnemyNextToPlayer = false;
            bool EnemyInvisible = false;
            if (clone.GetComponent<Renderer>().material.color == Color.grey)    //Is the enemy invisible?
            {
                EnemyInvisible = true;
            }
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
            if (EnemyNextToPlayer == true & EnemyInvisible == true)     //if enemy is next to player and enemy is invisible, do nothing
            {
            }
            else if (EnemyNextToPlayer == true & EnemyInvisible == false)     //if enemy is next to player and enemy is visible
            {
                MoveEnemy(CloneXPos, CloneYPos, PlayerXPos, PlayerYPos, NumOfLoops);
            }
            else if (EnemyNextToPlayer == false & EnemyInvisible == true)
            {
                MoveEnemy(CloneXPos, CloneYPos, PlayerXPos, PlayerYPos, NumOfLoops);
            }
            else if (EnemyNextToPlayer == false & EnemyInvisible == false)
            {
                MoveEnemy(CloneXPos, CloneYPos, PlayerXPos, PlayerYPos, NumOfLoops);
            }
            Destroy(clone);
            NumOfLoops++;
        }
    }

	void MoveEnemy(int CloneXPos, int CloneYPos, int PlayerXPos, int PlayerYPos, int NumOfLoops)
	{
		List<int> PossibleEnemyDirections = new List<int>();
		int LastEnemyDirectionIndex = MapArray[CloneXPos, CloneYPos] - 5;
		int EnemyNum = MapArray[CloneXPos, CloneYPos];
		if ((MapArray[CloneXPos, CloneYPos + 1] == 0 || MapArray[CloneXPos, CloneYPos + 1] == 2))
		{
			PossibleEnemyDirections.Add(1);
		}
		if ((MapArray[CloneXPos, CloneYPos - 1] == 0 || MapArray[CloneXPos, CloneYPos - 1] == 2))
		{
			PossibleEnemyDirections.Add(2);
		}
		if ((MapArray[CloneXPos + 1, CloneYPos] == 0 || MapArray[CloneXPos + 1, CloneYPos] == 2))
		{
			PossibleEnemyDirections.Add(3);
		}
		if ((MapArray[CloneXPos - 1, CloneYPos] == 0 || MapArray[CloneXPos - 1, CloneYPos] == 2))
		{
			PossibleEnemyDirections.Add(4);
		}
		for (int x = 0; x < PossibleEnemyDirections.Count; x++)
		{
			if (PossibleEnemyDirections[x] == LastEnemyDirections[EnemyNum - 5])
			{
				PossibleEnemyDirections.Remove(PossibleEnemyDirections[x]);
			}
		}
		if (PossibleEnemyDirections.Count == 0)
		{
			PossibleEnemyDirections.Add(LastEnemyDirections[EnemyNum - 5]); //allows the player to move back to the previous space
		}
		int RandomDirectionAvailableIndex = UnityEngine.Random.Range(0, PossibleEnemyDirections.Count);
		if (PossibleEnemyDirections[RandomDirectionAvailableIndex] == 1)
		{
			MapArray[CloneXPos, CloneYPos + 1] = EnemyNum;
			MapArray[CloneXPos, CloneYPos] = 0;
			LastEnemyDirections[EnemyNum - 5] = 2;
		}
		else if (PossibleEnemyDirections[RandomDirectionAvailableIndex] == 2)
		{
			MapArray[CloneXPos, CloneYPos - 1] = EnemyNum;
			MapArray[CloneXPos, CloneYPos] = 0;
			LastEnemyDirections[EnemyNum - 5] = 1;
		}
		else if (PossibleEnemyDirections[RandomDirectionAvailableIndex] == 4)
		{
			MapArray[CloneXPos - 1, CloneYPos] = EnemyNum;
			MapArray[CloneXPos, CloneYPos] = 0;
			LastEnemyDirections[EnemyNum - 5] = 3;
		}
		else if (PossibleEnemyDirections[RandomDirectionAvailableIndex] == 3)
		{
			MapArray[CloneXPos + 1, CloneYPos] = EnemyNum;
			MapArray[CloneXPos, CloneYPos] = 0;
			LastEnemyDirections[EnemyNum - 5] = 4;
		}
		else if (PossibleEnemyDirections[RandomDirectionAvailableIndex] == 5)
		{
			MapArray[CloneXPos, CloneYPos] = EnemyNum;
			LastEnemyDirections[EnemyNum - 5] = 5;
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
					Destroy (GameObject.FindWithTag("Player"));

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
		PlayerPrefab = GameObject.FindWithTag ("Player");
        LevelCompleted ();
		int PlayerXPos = 0;
		int PlayerYPos = 0;
        if (NumOfTurnsLeftForSuperPower <= 0)
        {
            PowerReady = true;
        }
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
		if (PlayerXPos == 0 & PlayerYPos == 0) 
		{ 	//If player is touching enemy then calls game over function.
			GameOver ();
		} 
		else 
		{ 	//Decides whether player can move and moves them.
			for (int x = PlayerXPos - 1; x <= PlayerXPos + 1; x++) 
			{
				for (int y = PlayerYPos - 1; y <= PlayerYPos + 1; y++) 
				{
					if (MapArray [x, y] == 4) 
					{
						MapArray [x, y] = 0; //4 represensts the floors surrounding the player when the map is initalized
					}
				}
			}
			if (Input.GetKeyDown ("down") & MapArray [PlayerXPos, PlayerYPos - 1] == 0) 
			{
				MapArray [PlayerXPos, PlayerYPos - 1] = 2;
				MapArray [PlayerXPos, PlayerYPos] = 0;
				EnemyMovement (PlayerXPos, PlayerYPos);
				DisplayCharacters (MapArray);
				NumOfTurnsLeftForSuperPower--;
			}
			else if (Input.GetKeyDown ("up") & MapArray [PlayerXPos, PlayerYPos + 1] == 0) 
			{
				MapArray [PlayerXPos, PlayerYPos + 1] = 2;
				MapArray [PlayerXPos, PlayerYPos] = 0;
				EnemyMovement (PlayerXPos, PlayerYPos);
				DisplayCharacters (MapArray);
				NumOfTurnsLeftForSuperPower--;
			}
			else if (Input.GetKeyDown ("left") & MapArray [PlayerXPos - 1, PlayerYPos] == 0) 
			{
				MapArray [PlayerXPos - 1, PlayerYPos] = 2;
				MapArray [PlayerXPos, PlayerYPos] = 0;
				EnemyMovement (PlayerXPos, PlayerYPos);
				DisplayCharacters (MapArray);
				NumOfTurnsLeftForSuperPower--;
			} 
			else if (Input.GetKeyDown ("right") & MapArray [PlayerXPos + 1, PlayerYPos] == 0) 
			{
				MapArray [PlayerXPos + 1, PlayerYPos] = 2;
				MapArray [PlayerXPos, PlayerYPos] = 0;
				EnemyMovement (PlayerXPos, PlayerYPos);
				DisplayCharacters (MapArray);
				NumOfTurnsLeftForSuperPower--;
			} 
			else if (Input.GetKeyDown ("down") & MapArray [PlayerXPos, PlayerYPos - 1] > 4) 
			{
				PlayerPrefab.transform.position = new Vector2(-120 + (25 * PlayerXPos), -170 + (25 * (PlayerYPos - 1)));
				NumOfTurnsLeftForSuperPower--;
				GameOver ();
			}
			else if (Input.GetKeyDown ("up") & MapArray [PlayerXPos, PlayerYPos + 1] > 4) 
			{
				PlayerPrefab.transform.position = new Vector2(-120 + (25 * PlayerXPos), -170 + (25 * (PlayerYPos + 1)));
				NumOfTurnsLeftForSuperPower--;
				GameOver ();
			}
			else if (Input.GetKeyDown ("left") & MapArray [PlayerXPos - 1, PlayerYPos] > 4) 
			{
				PlayerPrefab.transform.position = new Vector2(-120 + (25 * (PlayerXPos - 1)), -170 + (25 * PlayerYPos));
				NumOfTurnsLeftForSuperPower--;
				GameOver ();
			} 
			else if (Input.GetKeyDown ("right") & MapArray [PlayerXPos + 1, PlayerYPos] > 4) 
			{
				PlayerPrefab.transform.position = new Vector2(-120 + (25 * (PlayerXPos + 1)), -170 + (25 * PlayerYPos));
				NumOfTurnsLeftForSuperPower--;
				GameOver ();
			} 
			else if(Input.GetKey("space") & PowerReady == true)				//Uses super power if available
			{
				bool CoordinatesFound = false;
				int x = 2;
				if (Input.GetKeyDown ("down"))
				{
					while (CoordinatesFound == false)
					{
						if (MapArray [PlayerXPos, PlayerYPos - x] == 0) 
						{
							MapArray [PlayerXPos, PlayerYPos - x] = 2;
							MapArray [PlayerXPos, PlayerYPos] = 0;
							EnemyMovement (PlayerXPos, PlayerYPos);
							DisplayCharacters (MapArray);
							CoordinatesFound = true;
							NumOfTurnsLeftForSuperPower = 15;
							PowerReady = false;
							LastPlayerDirection = 2;
						}
						else if (x == 0)
						{
							CoordinatesFound = true;
						}
						else 
						{
							x++;
						}
					}
				} 
				else if (Input.GetKeyDown ("up"))
				{
					while(CoordinatesFound == false)
					{
						if (MapArray [PlayerXPos, PlayerYPos + x] == 0) 
						{
							MapArray [PlayerXPos, PlayerYPos + x] = 2;
							MapArray [PlayerXPos, PlayerYPos] = 0;
							EnemyMovement (PlayerXPos, PlayerYPos);
							DisplayCharacters (MapArray);
							CoordinatesFound = true;
							NumOfTurnsLeftForSuperPower = 15;
							PowerReady = false;
							LastPlayerDirection = 1;
						}
						else if (x == MapArray.GetLength(1) - 1)
						{
							CoordinatesFound = true;
						}
						else 
						{
							x++;
						}
					}
				} 
				else if (Input.GetKeyDown ("left"))
				{
					while(CoordinatesFound == false)
					{
						if (MapArray [PlayerXPos - x, PlayerYPos] == 0) 
						{
							MapArray [PlayerXPos - x, PlayerYPos] = 2;
							MapArray [PlayerXPos, PlayerYPos] = 0;
							EnemyMovement (PlayerXPos, PlayerYPos);
							DisplayCharacters (MapArray);
							CoordinatesFound = true;
							NumOfTurnsLeftForSuperPower = 15;
							PowerReady = false;
							LastPlayerDirection = 4;
						}
						else if (x == 0)
						{
							CoordinatesFound = true;
						}
						else 
						{
							x++;
						}
					}
				} 
				else if (Input.GetKeyDown ("right"))
				{
					while(CoordinatesFound == false)
					{
						if (MapArray [PlayerXPos + x, PlayerYPos] == 0) 
						{
							MapArray [PlayerXPos + x, PlayerYPos] = 2;
							MapArray [PlayerXPos, PlayerYPos] = 0;
							EnemyMovement (PlayerXPos, PlayerYPos);
							DisplayCharacters (MapArray);
							CoordinatesFound = true;
							NumOfTurnsLeftForSuperPower = 15;
							PowerReady = false;
							LastPlayerDirection = 3;
						}
						else if (x == MapArray.GetLength(0) - 1)
						{
							CoordinatesFound = true;
						}
						else 
						{
							x++;
						}
					}
				}
			}
			else if (Input.GetKeyDown ("down") || Input.GetKeyDown ("up") || Input.GetKeyDown ("left") || Input.GetKeyDown ("right")) 
			{
				EnemyMovement (PlayerXPos, PlayerYPos);
				DisplayCharacters (MapArray);
				NumOfTurnsLeftForSuperPower--;
				LastPlayerDirection = 5;
			}
		}
	}
}
