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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnEntities()
	{
		Instantiate (player, new Vector3 (TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f), Quaternion.identity);
		Instantiate (boss, new Vector3 (TileMapGenerator.instance.tileMapSize / 2.0f, 0f, TileMapGenerator.instance.tileMapSize / 2.0f), Quaternion.identity);
	}
}
