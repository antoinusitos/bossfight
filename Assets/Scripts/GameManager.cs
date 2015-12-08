using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject boss;

	static GameManager mInst;
	static public GameManager instance { get { return mInst; } }
	void Awake()
	{
		if (mInst == null) mInst = this;
		DontDestroyOnLoad(this);
	}

	public enum GameState {Tuto, Game}

	public GameState gameState;
    public GameObject bossPrefab;
    public GameObject bossInstance;

	// Use this for initialization
	void Start () 
    {
        TileMapGenerator.instance.Init();
        bossInstance = (GameObject)Instantiate(bossPrefab, TileMapGenerator.instance.GetMiddleTile().GetPosition() /*+ new Vector3(1f, 0, 1f)*/, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

	public void SpawnEntities()
	{
		Instantiate (player, new Vector3 (TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f), Quaternion.identity);
		Instantiate (boss, new Vector3 (TileMapGenerator.instance.tileMapSize / 2.0f, 0f, TileMapGenerator.instance.tileMapSize / 2.0f), Quaternion.identity);
	}
}
