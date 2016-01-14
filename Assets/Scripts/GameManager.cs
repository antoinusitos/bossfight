using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject boss;
    public GameObject door;

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

    AudioSource porte;

	// Use this for initialization
	void Start () 
    {
        TileMapGenerator.instance.Init();
        SpawnEntities();
        //door.transform.position = new Vector3(door.transform.position.x, 0.0f, door.transform.position.z);
        //Debug.Log("start game manager");
        door = TileMapGenerator.instance.tutoPrefabInstance.GetComponent<SpawnBomb>().Door;

        porte = SoundManager.instance.ouverturePorte.GetComponent<AudioSource>();
    }

	public void SpawnEntities()
	{
        PlayerManager.instance.SpawnPlayers();
        
    }

    public void SpawnBoss()
    {
        bossInstance = (GameObject)Instantiate(bossPrefab, TileMapGenerator.instance.GetMiddleTile().GetPosition(), Quaternion.identity);
    }
    

    public void HitInterruptor()
    {   if(GameManager.instance.gameState == GameState.Tuto)
        {
            
            //StartCoroutine(OpenDoor(0.5f));
           
            door.gameObject.GetComponent<Door>().move = true;
            porte.Play();

        }
        else
        {
            bossInstance.GetComponent<Boss>().OneInterruptorHit();
        }
        
    }
    


}
