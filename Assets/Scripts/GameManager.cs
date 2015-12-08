using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

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
        bossInstance = (GameObject)Instantiate(bossPrefab, TileMapGenerator.instance.GetMiddleTile().GetPosition(), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
