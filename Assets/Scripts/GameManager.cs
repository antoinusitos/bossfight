using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject boss;
    public GameObject door;

	static GameManager mInst;
    public GameObject panelEndGame;
	static public GameManager instance { get { return mInst; } }
	void Awake()
	{
		if (mInst == null) mInst = this;
		DontDestroyOnLoad(this);
	}

	public enum GameState {Tuto, Game, End, Menu}

	public GameState gameState;
    public GameObject bossPrefab;
    public GameObject bossInstance;

    AudioSource porte;

    float currentTime;
    float maxTime;

	// Use this for initialization
	void Start () 
    {
        maxTime = 11.0f;
        currentTime = maxTime;
        TileMapGenerator.instance.Init();
        SpawnEntities();
        //door.transform.position = new Vector3(door.transform.position.x, 0.0f, door.transform.position.z);
        //Debug.Log("start game manager");
        door = TileMapGenerator.instance.tutoPrefabInstance.GetComponent<SpawnBomb>().Door;

        porte = SoundManager.instance.ouverturePorte.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(gameState == GameState.End)
        {
            if(Input.GetButtonDown("Start_1"))
            {
                Application.LoadLevel("Game");
            }
            else if (Input.GetButtonDown("Start_2"))
            {
                Application.LoadLevel("Game");
            }
            else if (Input.GetButtonDown("Start_3"))
            {
                Application.LoadLevel("Game");
            }
            else if (Input.GetButtonDown("Start_4"))
            {
                Application.LoadLevel("Game");
            }
            else
            {
                if(currentTime >= 0.0f)
                {
                    currentTime -= Time.deltaTime;
                    UIManager.instance.UpdateCompteur((int)currentTime);
                }
                else
                {
                    currentTime = maxTime;
                    gameState = GameState.Menu;
                    Application.LoadLevel("MainMenu");
                }
            }
        }
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

    public void ActiveEndGameVictory()
    {
        if(gameState != GameState.End)
        {
            gameState = GameState.End;
            panelEndGame.SetActive(true);
            panelEndGame.transform.GetChild(0).gameObject.SetActive(true);
        }
       
    }

    public void ActiveEndGameDefeat()
    {
        if (gameState != GameState.End)
        {
            gameState = GameState.End;
            panelEndGame.SetActive(true);
            panelEndGame.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    


}
