using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    static PlayerManager mInst;
    static public PlayerManager instance { get { return mInst; } }
    void Awake()
    {
        if (mInst == null) mInst = this;
        DontDestroyOnLoad(this);
    }

    public int nbPlayer = 0;
    public int currentNbPlayer = 0;

    public GameObject player;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject playerInstance;
    public GameObject player2Instance;
    public GameObject player3Instance;
    public GameObject player4Instance;

    public List<GameObject> players;
    public List<GameObject> playersDeath;

    public void SpawnPlayers()
    {
        if (MenuManager.instance.GetReady(1))
        {
            playerInstance = (GameObject)Instantiate(player, new Vector3(/*TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f*/50.0f, 0f, 9.0f /*TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f*/), Quaternion.identity);
            UIManager.instance.ShowP1Life();
            nbPlayer++;
            players.Add(playerInstance);
        }
        if (MenuManager.instance.GetReady(2))
        {
            //player2Instance = (GameObject)Instantiate(player2, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 1.5f), Quaternion.identity);
            player2Instance = (GameObject)Instantiate(player2, new Vector3(/*TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f*/47.0f, 0f, 9.0f /*TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f*/), Quaternion.identity);
            UIManager.instance.ShowP1Life();
            UIManager.instance.ShowP2Life();
            nbPlayer++;
            players.Add(player2Instance);
        }
        if (MenuManager.instance.GetReady(3))
        {
            //player3Instance = (GameObject)Instantiate(player3, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 1.5f), Quaternion.identity);
            player3Instance = (GameObject)Instantiate(player3, new Vector3(/*TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f*/50.0f, 0f, 7.0f /*TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f*/), Quaternion.identity);
            UIManager.instance.ShowP1Life();
            UIManager.instance.ShowP3Life();
            nbPlayer++;
            players.Add(player3Instance);
        }
        if (MenuManager.instance.GetReady(4))
        {
            //player4Instance = (GameObject)Instantiate(player4, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 1.5f), Quaternion.identity);
            player4Instance = (GameObject)Instantiate(player4, new Vector3(/*TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f*/47.0f, 0f, 7.0f /*TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f*/), Quaternion.identity);
            UIManager.instance.ShowP1Life();
            UIManager.instance.ShowP4Life();
            nbPlayer++;
            players.Add(player4Instance);
        }
        currentNbPlayer = nbPlayer;
    }

    public void PlayerDead(int player)
    {
        if (player == 1)
        {
            currentNbPlayer--;
            players.Remove(playerInstance);
        }
        else if (player == 2)
        {
            currentNbPlayer--;
            players.Remove(player2Instance);

        }
        else if (player == 3)
        {
            currentNbPlayer--;
            players.Remove(player3Instance);

        }
        else if (player == 4)
        {
            currentNbPlayer--;
            players.Remove(player4Instance);

        }

        if (currentNbPlayer == 0)
        {
            GameManager.instance.ActiveEndGameDefeat();
            GameManager.instance.gameState = GameManager.GameState.End;
        }
    }

    public void Revive()
    {
        GameObject inst = TileMapGenerator.instance.tutoPrefabInstance.gameObject;
        GameObject reviveInst = TileMapGenerator.instance.tutoPrefabInstance.gameObject.GetComponent<SpawnBomb>().RespawnZone;
        if (playerInstance == null && MenuManager.instance.GetReady(1))
        {
            playerInstance = (GameObject)Instantiate(player, new Vector3(21.0f, 0f, reviveInst.transform.position.z), Quaternion.identity);
            currentNbPlayer++;
            UIManager.instance.ShowP1Life();
            players.Add(playerInstance);
          //  Debug.Log(playerInstance.transform.position);
        }
        if (player2Instance == null && MenuManager.instance.GetReady(2))
        {
            player2Instance = (GameObject)Instantiate(player2, new Vector3(21.0f, 0f, reviveInst.transform.position.z + 1.0f), Quaternion.identity);
            currentNbPlayer++;
            UIManager.instance.ShowP2Life();
            players.Add(player2Instance);
          //  Debug.Log(player2Instance.transform.position);

        }
        if (player3Instance == null && MenuManager.instance.GetReady(3))
        {
            player3Instance = (GameObject)Instantiate(player3, new Vector3(21.0f, 0f, reviveInst.transform.position.z - 1.0f), Quaternion.identity);
            currentNbPlayer++;
            UIManager.instance.ShowP3Life();
            players.Add(player3Instance);
           // Debug.Log(player3Instance.transform.position);

        }
        if (player4Instance == null && MenuManager.instance.GetReady(4))
        {
            player4Instance = (GameObject)Instantiate(player4, new Vector3(21.0f - 1.0f, 0f, reviveInst.transform.position.z), Quaternion.identity);
            currentNbPlayer++;
            UIManager.instance.ShowP4Life();
            players.Add(player4Instance);
           // Debug.Log(player4Instance.transform.position);

        }
    }
    public void Tepe()
    {
        GameObject reviveInst = TileMapGenerator.instance.tutoPrefabInstance.gameObject.GetComponent<SpawnBomb>().RespawnZone;
        if (playerInstance != null && MenuManager.instance.GetReady(1))
        {
            playerInstance.transform.position = new Vector3(21.0f, 0f, reviveInst.transform.position.z);
        }
        if (player2Instance != null && MenuManager.instance.GetReady(2))
        {
            player2Instance.transform.position = new Vector3(21.0f, 0f, reviveInst.transform.position.z + 1.0f);
        }
        if (player3Instance != null && MenuManager.instance.GetReady(3))
        {
            player3Instance.transform.position = new Vector3(21.0f, 0f, reviveInst.transform.position.z - 1.0f);
        }
        if (player4Instance != null && MenuManager.instance.GetReady(4))
        {
            player4Instance.transform.position = new Vector3(21.0f - 1.0f, 0f, reviveInst.transform.position.z);
        }
    }

}
