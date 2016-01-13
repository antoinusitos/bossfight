using UnityEngine;
using System.Collections;

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

    public void SpawnPlayers()
    {
        if (MenuManager.instance.GetReady(1))
        {
            playerInstance = (GameObject)Instantiate(player, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f), Quaternion.identity);
            UIManager.instance.ShowP1Life();
            nbPlayer++;
        }
        if (MenuManager.instance.GetReady(2))
        {
            player2Instance = (GameObject)Instantiate(player2, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 1.5f), Quaternion.identity);
            UIManager.instance.ShowP2Life();
            nbPlayer++;
        }
        if (MenuManager.instance.GetReady(3))
        {
            player3Instance = (GameObject)Instantiate(player3, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 1.5f), Quaternion.identity);
            UIManager.instance.ShowP3Life();
            nbPlayer++;
        }
        if (MenuManager.instance.GetReady(4))
        {
            player4Instance = (GameObject)Instantiate(player4, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 1.5f), Quaternion.identity);
            UIManager.instance.ShowP4Life();
            nbPlayer++;
        }
        currentNbPlayer = nbPlayer;
    }

    public void PlayerDead(int player)
    {
        if (player == 1)
        {
            currentNbPlayer--;
        }
        else if (player == 2)
        {
            currentNbPlayer--;
        }
        else if (player == 3)
        {
            currentNbPlayer--;
        }
        else if (player == 4)
        {
            currentNbPlayer--;
        }

        if (currentNbPlayer == 0)
        {
            // GameOver
        }
    }

    public void Revive()
    {
        if(playerInstance == null && MenuManager.instance.GetReady(1))
        {
            playerInstance = (GameObject)Instantiate(player, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f), Quaternion.identity);
            UIManager.instance.ShowP1Life();
        }
        if (player2Instance == null && MenuManager.instance.GetReady(2))
        {
            player2Instance = (GameObject)Instantiate(player2, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f), Quaternion.identity);
            UIManager.instance.ShowP2Life();
        }
        if (player3Instance == null && MenuManager.instance.GetReady(3))
        {
            player3Instance = (GameObject)Instantiate(player3, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f), Quaternion.identity);
            UIManager.instance.ShowP3Life();
        }
        if (player4Instance == null && MenuManager.instance.GetReady(4))
        {
            player4Instance = (GameObject)Instantiate(player4, new Vector3(TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f, 0f, TileMapGenerator.instance.tileMapSize / 3.0f + 0.5f), Quaternion.identity);
            UIManager.instance.ShowP4Life();
        }
    }
}
