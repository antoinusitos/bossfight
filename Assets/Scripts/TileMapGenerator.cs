using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMapGenerator : MonoBehaviour
{

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
	public GameObject prefabInterruptor;
    public Transform corridorPrefab;

    private int indexTileMap = 0;
    public Tile[] tileMap;
    public Tile[] tileMapCorridor;
    public int tileMapSize = 0;
    public int CorridorLarger = 0;
    public List<Tile> listBossBomb;
    public List<Tile> listOfDestructibleBlock;
	public List<Tile> listOfInterruptorBlock;
	private List<GameObject> destructibleBlock;
	private List<GameObject> interruptorBlock;
	Tile tile;


    public Transform tutoPrefabInstance;

    public void Init()
    {
        listBossBomb = new List<Tile>();
        listOfDestructibleBlock = new List<Tile>();
        listOfInterruptorBlock = new List<Tile>();
        destructibleBlock = new List<GameObject>();
        interruptorBlock = new List<GameObject>();
        indexTileMap = 0;
        cam.transform.position = new Vector3(tileMapSize / 2, tileMapSize, tileMapSize / 2-0.5f);
        InitMapGeneration();
        DoListOfBomb(GetMiddleTile().GetPosition().x, GetMiddleTile().GetPosition().z, 4);
        Generation();
        DestructibleBlockGeneration();
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoListOfInterruptorBlock(2);
            InstanciateInterruptorBlock();
        }

    }

    public void CleanLevelSpawnInterruptor()
    {
        DoListOfInterruptorBlock(2);
        InstanciateInterruptorBlock();
    }


    void DoListOfBomb(float x, float y, int range)
    {
        for (int i = (int)x - range; i <= x + range; ++i)
        {
            for (int j = (int)y - range; j <= y + range; ++j)
            {
                if (!((i >= x - 1 && i <= x + 1) && (j >= y - 1 && j <= y + 1)))
                {
                    listBossBomb.Add(tileMap[CoordToIndex(i, j)]);
                }

            }
        }
    }

    void DoListOfInterruptorBlock(int range)
    {
        #region DoList
        for (int index = 0; index < 4; ++index)
        {
            if (index == 0)
            {
                for (int x = 1; x <= 1 + range; ++x)
                {
                    for (int y = 1; y <= 1 + range; ++y)
                    {
                        if (!(x == 1 && y == 1))
                        {
                            listOfInterruptorBlock.Add(tileMap[CoordToIndex(x, y)]);
                        }

                    }
                }
            }
            else if (index == 1)
            {
                for (int x = tileMapSize - 2; x >= tileMapSize - 2 - range; --x)
                {
                    for (int y = 1; y <= 1 + range; ++y)
                    {
                        if (!(x == tileMapSize - 2 && y == 1))
                        {
                            listOfInterruptorBlock.Add(tileMap[CoordToIndex(x, y)]);
                        }

                    }
                }
            }
            else if (index == 2)
            {
                for (int x = tileMapSize - 2; x >= tileMapSize - 2 - range; --x)
                {
                    for (int y = tileMapSize - 2; y >= tileMapSize - 2 - range; --y)
                    {
                        if (!(x == tileMapSize - 2 && y == tileMapSize - 2))
                        {
                            listOfInterruptorBlock.Add(tileMap[CoordToIndex(x, y)]);
                        }

                    }
                }
            }
            else if (index == 3)
            {
                for (int x = 1; x <= 1 + range; ++x)
                {
                    for (int y = tileMapSize - 2; y >= tileMapSize - 2 - range; --y)
                    {
                        if (!(x == 1 && y == tileMapSize - 2))
                        {
                            listOfInterruptorBlock.Add(tileMap[CoordToIndex(x, y)]);
                        }

                    }
                }
            }

        }
        #endregion

    }

    void InstanciateInterruptorBlock()
    {
        //spawn des 4 interrupteurs
        GameObject interruptor1 = Instantiate(prefabInterruptor, new Vector3(1, 0, 1), Quaternion.identity)as GameObject;
        GameObject interruptor2 = Instantiate(prefabInterruptor, new Vector3(tileMapSize - 2, 0, 1), Quaternion.identity)as GameObject;
        GameObject interruptor3 = Instantiate(prefabInterruptor, new Vector3(1, 0, tileMapSize - 2), Quaternion.identity)as GameObject;
        GameObject interruptor4 = Instantiate(prefabInterruptor, new Vector3(tileMapSize - 2, 0, tileMapSize - 2), Quaternion.identity)as GameObject;
        interruptorBlock.Add(interruptor1);
        interruptorBlock.Add(interruptor2);
        interruptorBlock.Add(interruptor3);
        interruptorBlock.Add(interruptor4);

        RemoveAllDestructiblesBlocks();
        GameObject parent = GameObject.Find("LD");
        for (int i = 0; i < listOfInterruptorBlock.Count; ++i)
        {
            TileType tileT = tileType[2];
            t = (GameObject)Instantiate(tileT.tile, new Vector3(listOfInterruptorBlock[i].x, 0, listOfInterruptorBlock[i].z), Quaternion.identity);
            t.transform.parent = parent.transform;
            interruptorBlock.Add(t);
        }

    }

    public void RemoveAllInterruptorsBlocks()
    {
        for (int i = 0; i < interruptorBlock.Count; ++i)
        {
            Destroy(interruptorBlock[i]);

        }
        listOfInterruptorBlock.Clear();
        interruptorBlock.Clear();
    }

    void RemoveAllDestructiblesBlocks()
    {
        for (int i = 0; i < listOfDestructibleBlock.Count; ++i)
        {
            Destroy(destructibleBlock[i]);

        }
        listOfDestructibleBlock.Clear();
        destructibleBlock.Clear();
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
        int index = (int)(x + 0.5f) + ((int)(y + 0.5f) * TileMapGenerator.instance.tileMapSize);

        return index;
    }

    void InitMapGeneration()
    {
        tileMap = new Tile[tileMapSize * tileMapSize];
        tileMapCorridor = new Tile[CorridorLarger * ((tileMapSize / 2) + 3)];

        for (int y = 0; y < tileMapSize; ++y)
        {
            for (int x = 0; x < tileMapSize; ++x)
            {
                indexTileMap = x + (y * tileMapSize);
                if (x == 0 || x == tileMapSize - 1 || y == 0 || y == tileMapSize - 1)
                {
                    tileMap[indexTileMap] = new Tile();
                    tileMap[indexTileMap].SetTile(x, y, 1);
                }
                else
                {
                    tileMap[indexTileMap] = new Tile();
                    tileMap[indexTileMap].SetTile(x, y, 0);
                }
            }
        }

        tileMap[CoordToIndex(tileMapSize - 1, tileMapSize / 2 - 1)].type = 0;
        tileMap[CoordToIndex(tileMapSize - 1, tileMapSize / 2)].type = 0;
        tileMap[CoordToIndex(tileMapSize - 1, tileMapSize / 2 + 1)].type = 0;

        tutoPrefabInstance = (Transform)Instantiate(corridorPrefab, new Vector3(tileMapSize + 27.0f, 0.0f, tileMapSize / 2 - 4.0f), Quaternion.identity);

    }

    void Generation()
    {
        GameObject parent = GameObject.Find("LD");

        if (!GameObject.Find("LD"))
        {
            parent = new GameObject("LD");
        }



        for (int y = 0; y < tileMapSize; ++y)
        {
            for (int x = 0; x < tileMapSize; ++x)
            {
                indexTileMap = x + (y * tileMapSize);
                TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                t.transform.parent = parent.transform;
            }
        }
    }


    public void DestructibleBlockGeneration()
    {
        if (listOfInterruptorBlock.Count> 0)
        {
            RemoveAllInterruptorsBlocks();
        }
        
        GameObject parent = GameObject.Find("LD");
        // grand tour
        // bas
        for (int y = 2; y < 3; ++y)
        {
            for (int x = 0; x < tileMapSize - 2; ++x)
            {
                indexTileMap = x + (y * tileMapSize);
                if (x % 2 == 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
                    tileMap[indexTileMap].SetTile(x, y, 2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                    destructibleBlock.Add(t);
                }
            }
        }
        // haut
        for (int y = tileMapSize - 3; y < tileMapSize - 2; ++y)
        {
            for (int x = 2; x < tileMapSize; ++x)
            {
                indexTileMap = x + (y * tileMapSize);
                if (x % 2 != 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
                    tileMap[indexTileMap].SetTile(x, y, 2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                    destructibleBlock.Add(t);
                }
            }
        }
        // gauche
        for (int y = 3; y < tileMapSize - 2; ++y)
        {
            for (int x = 2; x < 3; ++x)
            {
                indexTileMap = x + (y * tileMapSize);
                if (y % 2 == 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
                    tileMap[indexTileMap].SetTile(x, y, 2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                    destructibleBlock.Add(t);
                }
            }
        }
        // droite
        for (int y = 2; y < tileMapSize; ++y)
        {
            for (int x = tileMapSize - 3; x < tileMapSize - 2; ++x)
            {
                indexTileMap = x + (y * tileMapSize);
                if (y % 2 != 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
                    tileMap[indexTileMap].SetTile(x, y, 2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                    destructibleBlock.Add(t);
                }
            }
        }


        // petit tour
        // bas
        for (int y = 4; y < 5; ++y)
        {
            for (int x = 4; x < tileMapSize - 4; ++x)
            {
                indexTileMap = x + (y * tileMapSize);
                if (x % 2 != 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
                    tileMap[indexTileMap].SetTile(x, y, 2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                    destructibleBlock.Add(t);
                }
            }
        }
        // haut
        for (int y = tileMapSize - 5; y < tileMapSize - 4; ++y)
        {
            for (int x = 4; x < tileMapSize - 4; ++x)
            {
                indexTileMap = x + (y * tileMapSize);
                if (x % 2 == 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
                    tileMap[indexTileMap].SetTile(x, y, 2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                    destructibleBlock.Add(t);
                }
            }
        }
        // gauche
        for (int y = 5; y < tileMapSize - 4; ++y)
        {
            for (int x = 4; x < 5; ++x)
            {
                indexTileMap = x + (y * tileMapSize);
                if (y % 2 != 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
                    tileMap[indexTileMap].SetTile(x, y, 2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                    destructibleBlock.Add(t);
                }
            }
        }
        // droite
        for (int y = 4; y < tileMapSize - 4; ++y)
        {
            for (int x = tileMapSize - 5; x < tileMapSize - 4; ++x)
            {
                indexTileMap = x + (y * tileMapSize);
                if (y % 2 == 0 && tileMap[indexTileMap].GetTypeAtCoord() != 1)
                {
                    tileMap[indexTileMap].SetTile(x, y, 2);
                    TileType tileT = tileType[tileMap[indexTileMap].GetTypeAtCoord()];
                    t = (GameObject)Instantiate(tileT.tile, new Vector3(x, 0, y), Quaternion.identity);
                    t.transform.parent = parent.transform;

                    listOfDestructibleBlock.Add(tileMap[CoordToIndex(x, y)]);
                    destructibleBlock.Add(t);
                }
            }
        }

    }
}
