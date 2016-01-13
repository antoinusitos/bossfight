using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float speed = 10f;
    public float deadZone = 0.5f;
    public int playerNumber;
    public float gravity = 20.0F;
    public int life = 100;

    bool peutPoser = true;

    public GameObject prefabBombe;
    public CharacterController cc;
    Vector3 moveDirection;


    public enum state
    {
        dos,
        face,
        droite,
        gauche,
        idle
    }

    public state currentState;

    public bool move;

    // Use this for initialization
    void Start () {
		cc = GetComponent<CharacterController>();
        currentState = state.face;
    }
	
	// Update is called once per frame
	void Update ()
    { 
       
        if (cc.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("L_XAxis_" + playerNumber), 0, -Input.GetAxis("L_YAxis_" + playerNumber));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);

        move = false;

        if (Input.GetAxis("L_XAxis_" + playerNumber) < -deadZone)
        {
            move = true;
            if (currentState != state.gauche)
            {
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("gauche");
                currentState = state.gauche;
            }
        }
        else if (Input.GetAxis("L_XAxis_" + playerNumber) > deadZone)
        {
            move = true;
            if (currentState != state.droite)
            {
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("droite");
                currentState = state.droite;
            }
        }

        if (Input.GetAxis("L_YAxis_" + playerNumber) < -deadZone)
        {
            move = true;
            if (currentState != state.dos)
            {
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("dos");
                currentState = state.dos;
            }
        }
        else if (Input.GetAxis("L_YAxis_" + playerNumber) > deadZone)
        {
            move = true;
            if (currentState != state.face)
            {
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("face");
                currentState = state.face;
            }
        }

        if( move == false && currentState != state.idle)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("idle");
            currentState = state.idle;
        }



        if (Input.GetButtonDown("Attack_" + playerNumber) && peutPoser)
       {
            //Vector3 bombPosition = new Vector3(TileMapGenerator.instance.tileMap[TileMapGenerator.instance.CoordToIndex(transform.position.x, transform.position.z)].x, transform.position.y, TileMapGenerator.instance.tileMap[TileMapGenerator.instance.CoordToIndex(transform.position.x, transform.position.z)].z);
            Vector3 bombPosition = new Vector3(transform.position.x > 0 ? (int)(transform.position.x+0.5f): (int)(transform.position.x - 0.5f), 0.0f, transform.position.z > 0 ? (int)(transform.position.z + 0.5f) : (int)(transform.position.z - 0.5f));
            GameObject bombe = Instantiate(prefabBombe, bombPosition, Quaternion.identity) as GameObject;
            bombe.GetComponent<Bombes>().SetParent(gameObject);
            peutPoser = false;
       }
	}

    public void SetPeutPoser(bool newBool)
    {
        peutPoser = newBool;   
    }

    public void TakeDamage(int theDamage)
    {
        life -= theDamage;
        if (playerNumber == 1)
            UIManager.instance.ActutaliseP1(life);
        else if (playerNumber == 2)
            UIManager.instance.ActutaliseP2(life);
        else if (playerNumber == 3)
            UIManager.instance.ActutaliseP3(life);
        else if (playerNumber == 4)
            UIManager.instance.ActutaliseP4(life);
        if (life <= 0)
        {
            PlayerManager.instance.PlayerDead(playerNumber);
            Destroy(gameObject);
        }
    }

}
