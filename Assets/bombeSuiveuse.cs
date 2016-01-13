using UnityEngine;
using System.Collections;

public class bombeSuiveuse : MonoBehaviour {

    /*private Vector3 player1position;
    private Vector3 player2position;
    private Vector3 player3position;
    private Vector3 player4position; 

    private Transform player1;
    private Transform player2;
    private Transform player3;
    private Transform player4;

    private Transform target;*/
    
    private AudioSource bombe;

    GameObject nearestPlayer;

    int degats;
    int degatsBlocks;
    int porte;
    float speed;
    float delayExplode;
    float timeToExplode;

    public GameObject fx;
    public GameObject explosionFx;

    

	// Use this for initialization
	void Start () 
    {
        degats = 30;
        degatsBlocks = 1;
        porte = 3;
        speed = 5;
        delayExplode = 10.0f;
        timeToExplode = 0.0f;



       /* player1 = PlayerManager.instance.playerInstance.transform;
        player1 = PlayerManager.instance.player2Instance.transform;
        player1 = PlayerManager.instance.player3Instance.transform;
        player1 = PlayerManager.instance.player4Instance.transform; 

        player1position = PlayerManager.instance.playerInstance.transform.position;
        player2position = PlayerManager.instance.player2Instance.transform.position;
        player3position = PlayerManager.instance.player3Instance.transform.position;
        player4position = PlayerManager.instance.player4Instance.transform.position; */

        bombe = SoundManager.instance.bombeExplosion.GetComponent<AudioSource>();
        
	
	}
	
	// Update is called once per frame
	void Update () 
    {

        timeToExplode += Time.deltaTime;

        if (timeToExplode >= delayExplode)
        {
            sendDegat();
            Instantiate(explosionFx, transform.position, Quaternion.identity);
            bombe.Play();
            Destroy(gameObject);
        }

        FollowPlayer();
	
	}

        void sendDegat()
    {
        //board = Manager.instance.board;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.forward * porte, out hit, porte))
        {
            //print("There is something in front of the object!");

            if (hit.transform.GetComponent<PlayerScript>() != null)
            {
                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
            }
            
            if (hit.transform.GetComponent<DestructibleBlock>() != null)
            {
                hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<DestructibleBlockV2>() != null)
            {
                hit.transform.GetComponent<DestructibleBlockV2>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<Interruptor>() != null)
            {
                hit.transform.GetComponent<Interruptor>().TakeDamage();
            }

            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.forward, Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.forward, porte);
        }
        if (Physics.Raycast(transform.position, -Vector3.forward * porte, out hit, porte))
        {
            //print("There is something in back of the object!");

            if (hit.transform.GetComponent<PlayerScript>() != null)
            {

                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<DestructibleBlock>() != null)
            {
                hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<DestructibleBlockV2>() != null)
            {
                hit.transform.GetComponent<DestructibleBlockV2>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<Interruptor>() != null)
            {
                hit.transform.GetComponent<Interruptor>().TakeDamage();
            }

            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(-Vector3.forward, Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(-Vector3.forward, porte);
        }
        if (Physics.Raycast(transform.position, Vector3.right * porte, out hit, porte))
        {
            //print("There is something in right of the object!");

            if (hit.transform.GetComponent<PlayerScript>() != null)
            {

                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<DestructibleBlock>() != null)
            {
                hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<DestructibleBlockV2>() != null)
            {
                hit.transform.GetComponent<DestructibleBlockV2>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<Interruptor>() != null)
            {
                hit.transform.GetComponent<Interruptor>().TakeDamage();
            }

            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.right, Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.right, porte);
        }
        if (Physics.Raycast(transform.position, -Vector3.right * porte, out hit, porte))
        {
            //print("There is something in left of the object!");

            if (hit.transform.GetComponent<PlayerScript>() != null)
            {

                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
            }
            
            if (hit.transform.GetComponent<DestructibleBlock>() != null)
            {
                hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<DestructibleBlockV2>() != null)
            {
                hit.transform.GetComponent<DestructibleBlockV2>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<Interruptor>() != null)
            {
                hit.transform.GetComponent<Interruptor>().TakeDamage();
            }

            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.left, Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.left, porte);
        }
    }


    void calculDistance()
        {
            float distance = 9999999.0f;
            float distanceBombePlayer;
           

            for (int i = 0; i < PlayerManager.instance.players.Count; i++)
            {
                distanceBombePlayer = Vector3.Distance(gameObject.transform.position, PlayerManager.instance.players[i].transform.position);
                if (distanceBombePlayer < distance)
                {
                    distance = distanceBombePlayer;
                    nearestPlayer = PlayerManager.instance.players[i].gameObject;
                    
                }

            
            }

        }


    void FollowPlayer()
    {

        transform.LookAt(nearestPlayer.transform.position);
        transform.position = Vector3.MoveTowards(gameObject.transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);

    }


}
