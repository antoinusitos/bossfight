using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

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
        PlayerManager.instance.SpawnPlayers();
        bossInstance = (GameObject)Instantiate(bossPrefab, TileMapGenerator.instance.GetMiddleTile().GetPosition(), Quaternion.identity);
    }

    public void HitInterruptor()
    {
        bossInstance.GetComponent<Boss>().OneInterruptorHit();
    }
}
