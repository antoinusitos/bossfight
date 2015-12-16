﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMapGenerator : MonoBehaviour {

    static TileMapGenerator mInst;
    static public TileMapGenerator instance { get { return mInst; } }
    void Awake()
    {
        if (mInst == null) mInst = this;
        DontDestroyOnLoad(this);
    }

    public TileType[] tileType;
	public GameObject canon;
    public GameObject cam;
    public GameObject player;
    private GameObject pl;
    public GameObject t ;

	private int indexTileMap = 0;
	public Tile[] tileMap;
	public Tile[] tileMapCorridor;
    public int tileMapSize = 0;
	public int CorridorLarger = 0;
    public List<Tile> listBossBomb;
    public List<Tile> listOfDestructibleBlock;
	Tile tile;

    public void Init()
    {
        listBossBomb = new List<Tile>();
        listOfDestructibleBlock = new List<Tile>();
        indexTileMap = 0;
        cam.transform.position = new Vector3(tileMapSize / 2, tileMapSize, tileMapSize / 2);
        InitMapGeneration();
        DoListOfBomb(GetMiddleTile().GetPosition().x, GetMiddleTile().GetPosition().z, 4);
        Generation();
        DestructibleBlockGeneration();

    }
	
	// Update is called once per frame
	void Update ()
    {

	}


    void DoListOfBomb(float x, float y, int range)
    {
        for (int i = (int)x - range; i <= x + range; ++i)
        {
            for (int j = (int)y - range; j <= y + range; ++j)
            {
                if (!((i>= x-1 && i<= x+1) && (j >= y-1 && j <= y+1)))
                {
                    listBossBomb.Add(tileMap[CoordToIndex(i, j)]);
                   // tileMap[CoordToIndex(i, j)].type = 1;
                }
                
            }
        }
    }

    public Tile GetRandomBombPlace()
    {
        int index = Random.Range(0, listBossBomb.Count);
        return listBossBomb[index];
    }

    public Tile GetMiddleTile()
    {
        return tileMap[(tileMapSize * ((tileMapSize / 2) + 1)) - ((tileMapSize / 2) + 1)];
    }

    public Tile GetTileWithCoord(float x, float y)
    {
        return tileMap[CoordToIndex(x, y)];
    }

    public int CoordToIndex(float x, float y)
    {
        int index = (int)(x+0.5f) + ((int)(y+0.5f) * TileMapGenerator.instance.tileMapSize);

        return index;
    }

    void InitMapGeneration()
    {
        tileMap = new Tile[tileMapSize*tileMapSize];
		tileMapCorridor = new Tile[CorridorLarger * ((tileMapSize/2)+3)];

        for (int y = 0; y < tileMapSize; ++y)
        {
            for (int x = 0; x < tileMapSize; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
                if(x == 0 || x == tileMapSize-1 || y == 0 || y == tileMapSize-1)
                {
					tileMap[indexTileMap] = new Tile();
					tileMap[indexTileMap].SetTile(x,y,1);
                }
                else
                {
					tileMap[indexTileMap] = new Tile();
					tileMap[indexTileMap].SetTile(x,y,0);
                }
            }
        }
		//ouverture sur couloir
		if (CorridorLarger >2) 
		{
            tileMap[CoordToIndex(tileMapSize-1, tileMapSize/2 -1)].type = 2;
            tileMap[CoordToIndex(tileMapSize-1, tileMapSize/2 )].type = 2;
            tileMap[CoordToIndex(tileMapSize - 1, tileMapSize / 2 + 1)].type = 2;

			CorridorGeneration();
		}

		/*************************/
       // BlockGeneration();

        


    }

    void Generation()
    {
        GameObject parent = GameObject.Find("LD");
        
        if (!GameObject.Find("LD"))
        {
            parent = new GameObject("LD");
        }
        
        

        for(int y =0; y < tileMapSize; ++y)
        {
            for (int x = 0; x < tileMapSize; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
				TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
				t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                t.transform.parent = parent.transform;
            }
        }
    }

    void CorridorGeneration()
    {
		GameObject parent = GameObject.Find("LDCorridor");
		
		if (!GameObject.Find("LDCorridor"))
		{
			parent = new GameObject("LDCorridor");
		}
        int index = 0;
        for (int y = tileMapSize/2+2; y >=0; --y)
        {
			for (int x = tileMapSize; x < tileMapSize + CorridorLarger; ++x)
            {
				if(((x >= tileMapSize && x < tileMapSize + CorridorLarger-1) && (y <= tileMapSize/2+1 && y > tileMapSize/2-2)) ||
				    ((x >= tileMapSize + CorridorLarger-4 && x < tileMapSize + CorridorLarger-1) && (y <= tileMapSize/2-2 && y > 0)))
				{
					tileMapCorridor[index] = new Tile();
					tileMapCorridor[index].SetTile(x, y, 0);
				}
				else
				{
					tileMapCorridor[index] = new Tile();
					tileMapCorridor[index].SetTile(x, y, 1);
				}
				index++;

            }
        }
		/*************************/
		index = 0;
		for (int y = tileMapSize/2+2; y >=0; --y)
		{
			for (int x = tileMapSize; x < tileMapSize + CorridorLarger; ++x)
			{
				TileType tileT = tileType[tileMapCorridor[index].GetTypeAtCoord()];
				t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
				t.transform.parent = parent.transform;
				index++;
			}
		}

        
    }

   	void DestructibleBlockGeneration()
    {
        GameObject parent = GameObject.Find("LD");
        // grand tour
        // bas
        for (int y = 2; y < 3; ++y)
        {
            for (int x = 0; x < tileMapSize-2; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
				if (x % 2 == 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
					tileMap[indexTileMap].SetTile(x,y,2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                }
            }
        }
        // haut
        for (int y = tileMapSize - 3; y < tileMapSize - 2; ++y)
        {
            for (int x = 2; x < tileMapSize; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
				if (x % 2 != 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
					tileMap[indexTileMap].SetTile(x,y,2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                }
            }
        }
        // gauche
        for (int y = 3; y < tileMapSize - 2; ++y)
        {
            for (int x = 2; x < 3; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
				if (y % 2 == 0 && tileMap[indexTileMap].GetTypeAtCoord() !=1 )
                {
					tileMap[indexTileMap].SetTile(x,y,2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                }
            }
        }
        // droite
        for (int y = 2; y < tileMapSize; ++y)
        {
            for (int x = tileMapSize - 3; x < tileMapSize - 2; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
				if (y % 2 != 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
					tileMap[indexTileMap].SetTile(x,y,2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                }
            }
        }


        // petit tour
        // bas
        for (int y = 4; y < 5; ++y)
        {
            for (int x = 4; x < tileMapSize - 4; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
				if (x % 2 != 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
					tileMap[indexTileMap].SetTile(x,y,2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                }
            }
        }
        // haut
        for (int y = tileMapSize - 5; y < tileMapSize - 4; ++y)
        {
            for (int x = 4; x < tileMapSize-4; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
				if (x % 2 == 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
					tileMap[indexTileMap].SetTile(x,y,2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                }
            }
        }
        // gauche
        for (int y = 5; y < tileMapSize - 4; ++y)
        {
            for (int x = 4; x < 5; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
				if (y % 2 != 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
					tileMap[indexTileMap].SetTile(x,y,2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                }
            }
        }
        // droite
        for (int y = 4; y < tileMapSize-4; ++y)
        {
            for (int x = tileMapSize - 5; x < tileMapSize - 4; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
				if (y % 2 == 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
					tileMap[indexTileMap].SetTile(x,y,2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                }
            }
        }

    }
}
