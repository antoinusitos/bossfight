using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public GameObject player2;
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
        SpawnEntities();
    }

	public void SpawnEntities()
	{
        if(MenuManager.instance.GetReady(1))
		    Instantiate (player, new Vector3 (TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f), Quaternion.identity);
        if (MenuManager.instance.GetReady(2))
            Instantiate (player2, new Vector3 (TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 1.5f), Quaternion.identity);
        bossInstance = (GameObject)Instantiate(bossPrefab, TileMapGenerator.instance.GetMiddleTile().GetPosition() /*+ new Vector3(1f, 0, 1f)*/, Quaternion.identity);
    }

    public void HitInterruptor()
    {
        bossInstance.GetComponent<Boss>().OneInterruptorHit();
    }
}
