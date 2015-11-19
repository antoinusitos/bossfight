using UnityEngine;
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
    GameObject t ;

	private int indexTileMap = 0;
	public Tile[] tileMap;
	public Tile[] tileMapCorridor;
    public int tileMapSize = 0;
	public int CorridorLarger = 0;
	Tile tile;
	private Vector3 playerPos;


	public GameObject player;
	public GameObject boss;
    

	// Use this for initialization
	void Start ()
    {
		indexTileMap = 0;

		playerPos = new Vector3(2.7f, 1.5f, 0f);
        cam.transform.position = new Vector3(tileMapSize / 2, tileMapSize, tileMapSize / 2);
        InitMapGeneration();
        Generation();
	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey(KeyCode.Space)){
            FindCoord(playerPos);
		}
	}

    void FindCoord(Vector3 pos)
    {
        for (int i = 0; i < tileMap.Length; ++i)
        {
            if (tileMap[i].x == Mathf.Floor(pos.x) && tileMap[i].y == Mathf.Floor(pos.y))
            {
                Debug.Log("Bombe en: " + tileMap[i].x + "," + tileMap[i].y);
            }
        }
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
			indexTileMap = (tileMapSize-1)+((tileMapSize/2 -1)*tileMapSize);
			tileMap[indexTileMap].SetTile(tileMapSize-1,tileMapSize/2 -1,0);
			
			indexTileMap = (tileMapSize-1)+((tileMapSize/2)*tileMapSize);
			tileMap[indexTileMap].SetTile(tileMapSize-1,tileMapSize/2,0);
			
			indexTileMap = (tileMapSize-1)+((tileMapSize/2 +1)*tileMapSize);
			tileMap[indexTileMap].SetTile(tileMapSize-1,tileMapSize/2 +1,0);

			CorridorGeneration();
		}

		/*************************/
        BlockGeneration();

        


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

		Debug.Log ("fin");
		Instantiate (player, new Vector3 (tileMapSize / 3, 0, tileMapSize / 3), Quaternion.identity);
		Instantiate (boss, new Vector3 (tileMapSize / 2, 0, tileMapSize / 2), Quaternion.identity);
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
				if(((x >= tileMapSize+1 && x < tileMapSize + CorridorLarger-1) && (y <= tileMapSize/2+1 && y > tileMapSize/2-2)) ||
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

   	void BlockGeneration()
    {

        // grand tour
        // bas
        for (int y = 2; y < 3; ++y)
        {
            for (int x = 0; x < tileMapSize-2; ++x)
            {
				indexTileMap = x+(y*tileMapSize);
				if (x % 2 == 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
					tileMap[indexTileMap].SetTile(x,y,1);
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
					tileMap[indexTileMap].SetTile(x,y,1);
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
					tileMap[indexTileMap].SetTile(x,y,1);
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
					tileMap[indexTileMap].SetTile(x,y,1);
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
					tileMap[indexTileMap].SetTile(x,y,1);
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
					tileMap[indexTileMap].SetTile(x,y,1);
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
					tileMap[indexTileMap].SetTile(x,y,1);
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
					tileMap[indexTileMap].SetTile(x,y,1);
                }
            }
        }

    }
}
