using UnityEngine;
using System.Collections;

public class CameraView : MonoBehaviour {

    int playerCount;
    Vector3 centerCam;
    public GameObject sphere;
    GameObject test;

    void Start()
    {
        playerCount = PlayerManager.instance.players.Count;
        test = Instantiate(sphere, Vector3.zero, Quaternion.identity) as GameObject;
    }

    void Update()
    {
        if(GameManager.instance.gameState == GameManager.GameState.Tuto)
        {
            CalculateCenter();
        }
        else if(GameManager.instance.gameState == GameManager.GameState.Game)
        {
            transform.position = new Vector3(11, 22, 10.5f);
            Camera.main.orthographicSize = 11;
        }
        

    }
	void CalculateCenter()
    {
        playerCount = PlayerManager.instance.players.Count;
        if (playerCount == 2)
        {
            Vector3 centerCam = (PlayerManager.instance.players[0].transform.position + PlayerManager.instance.players[1].transform.position) / 2;
            float distance = Vector3.Distance(PlayerManager.instance.players[0].transform.position, PlayerManager.instance.players[1].transform.position);
            transform.position = new Vector3(centerCam.x , 22, centerCam.z);
            float cameraSize = Camera.main.orthographicSize;
            if (cameraSize <6 )
            {
                cameraSize = 6;
            }
            else
            {
                cameraSize = 6 + distance / 4;
            }
            Camera.main.orthographicSize = cameraSize;


        }
        else if(playerCount == 3)
        {
            Vector3 betweenPlayers1_2 = (PlayerManager.instance.players[0].transform.position + PlayerManager.instance.players[1].transform.position) / 2;
            Vector3 betweenPlayers1_3 = (PlayerManager.instance.players[0].transform.position + PlayerManager.instance.players[2].transform.position) / 2;


            Vector3 betweenPlayers2_3 = (PlayerManager.instance.players[1].transform.position + PlayerManager.instance.players[2].transform.position) / 2;


            Vector3 betweenCenterPlayer1 = (betweenPlayers2_3 + PlayerManager.instance.players[1].transform.position) / 2;
            Vector3 betweenCenters= (betweenPlayers1_2 + betweenPlayers1_3) / 2;

            centerCam = (betweenCenterPlayer1 + betweenCenters) / 2;
            float distance = Vector3.Distance(PlayerManager.instance.players[0].transform.position, betweenPlayers2_3);
            transform.position = new Vector3(centerCam.x, 22, centerCam.z);
            float cameraSize = Camera.main.orthographicSize;
            if (cameraSize < 6)
            {
                cameraSize = 6;
            }
            else
            {
                cameraSize = 6 + distance / 4;
            }
            Camera.main.orthographicSize = cameraSize;

        }
        else if (playerCount == 4)
        {
            Vector3 betweenPlayers1_2 = (PlayerManager.instance.players[0].transform.position + PlayerManager.instance.players[1].transform.position) / 2;
            Vector3 betweenPlayers3_4 = (PlayerManager.instance.players[2].transform.position + PlayerManager.instance.players[3].transform.position) / 2;

            centerCam = (betweenPlayers1_2 + betweenPlayers3_4) / 2;
            float distance = Vector3.Distance(betweenPlayers1_2, betweenPlayers3_4);
            transform.position = new Vector3(centerCam.x, 22, centerCam.z);
            float cameraSize = Camera.main.orthographicSize;
            if (cameraSize < 6)
            {
                cameraSize = 6;
            }
            else
            {
                cameraSize = 6 + distance/4;
            }
            Camera.main.orthographicSize = cameraSize;
        }
        else
        {
            if (PlayerManager.instance.players.Count > 0)
            {
                centerCam = PlayerManager.instance.players[0].transform.position;
                transform.position = new Vector3(centerCam.x, 22, centerCam.z);
                Camera.main.orthographicSize = 6;
            }
        }
    }
}
