using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject boss;
    public Transform door;

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
    {   if(GameManager.instance.gameState == GameState.Tuto)
        {
            StartCoroutine(OpenDoor(0.5f));
        }
        else
        {
            bossInstance.GetComponent<Boss>().OneInterruptorHit();
        }
        
    }

    IEnumerator OpenDoor(float time)
    {
        float currentTime = 0;
        Vector3 doorPos = door.position;

        while(currentTime < time)
        {
            doorPos.y -= 0.21f;
            door.position = doorPos;
            currentTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }


}
