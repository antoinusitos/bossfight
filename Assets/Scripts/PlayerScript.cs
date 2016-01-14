using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public float speed = 10f;
    public float deadZone = 0.15f;
    public int playerNumber;
    public float gravity = 20.0F;
    public int life = 100;

    bool peutPoser = true;

    public GameObject prefabBombe;
    public CharacterController cc;
    Vector3 moveDirection;

    float multAnim = 2.0f;

    public enum state
    {
        dos,
        face,
        droite,
        gauche,
        idle
    }

    public GameObject death;

    public state currentState;
    public state prevState;

    public bool move;

    // Use this for initialization
    void Start () {
		cc = GetComponent<CharacterController>();
        currentState = state.face;
        prevState = state.idle;

        if (playerNumber == 1)
            UIManager.instance.ActutaliseP1(life);
        else if (playerNumber == 2)
            UIManager.instance.ActutaliseP2(life);
        else if (playerNumber == 3)
            UIManager.instance.ActutaliseP3(life);
        else if (playerNumber == 4)
            UIManager.instance.ActutaliseP4(life);
    }
	
	// Update is called once per frame
	void Update ()
    { 
       if(GameManager.instance.gameState == GameManager.GameState.Tuto)
        {
            if(life < 100)
            {
                life ++;
                if (playerNumber == 1)
                    UIManager.instance.ActutaliseP1(life);
                else if (playerNumber == 2)
                    UIManager.instance.ActutaliseP2(life);
                else if (playerNumber == 3)
                    UIManager.instance.ActutaliseP3(life);
                else if (playerNumber == 4)
                    UIManager.instance.ActutaliseP4(life);
            }

            if (Input.GetButtonDown("Back_1"))
            {
                PlayerManager.instance.Tepe();
                TileMapGenerator.instance.tutoPrefabInstance.GetComponent<SpawnBomb>().Block();
            }
        }
        if (cc.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("L_XAxis_" + playerNumber), 0, -Input.GetAxis("L_YAxis_" + playerNumber));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);

        move = false;

        if (Input.GetAxis("L_XAxis_" + playerNumber) < -0)
        {
            move = true;
            transform.GetChild(0).GetComponent<Animator>().speed = Mathf.Abs(Input.GetAxis("L_XAxis_" + playerNumber)) * multAnim;
            if (currentState != state.gauche)
            {
                currentState = state.gauche;
            }
        }
        else if (Input.GetAxis("L_XAxis_" + playerNumber) > 0)
        {
            transform.GetChild(0).GetComponent<Animator>().speed = Mathf.Abs(Input.GetAxis("L_XAxis_" + playerNumber)) * multAnim;
            move = true;
            if (currentState != state.droite)
            {
                currentState = state.droite;
            }
        }

        if (Input.GetAxis("L_YAxis_" + playerNumber) < -0)
        {
            transform.GetChild(0).GetComponent<Animator>().speed = Mathf.Abs(Input.GetAxis("L_YAxis_" + playerNumber)) * multAnim;
            move = true;
            if (currentState != state.dos)
            {
                currentState = state.dos;
            }
        }
        else if (Input.GetAxis("L_YAxis_" + playerNumber) > 0)
        {
            transform.GetChild(0).GetComponent<Animator>().speed = Mathf.Abs(Input.GetAxis("L_YAxis_" + playerNumber)) * multAnim;
            move = true;
            if (currentState != state.face)
            {
                currentState = state.face;
            }
        }

        if( move == false && currentState != state.idle)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("idle");
            currentState = state.idle;
        }


        if (currentState == state.face && prevState != state.face)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("face");
            prevState = state.face;
        }
        else if (currentState == state.dos && prevState != state.dos)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("dos");
            prevState = state.dos;
        }
        else if (currentState == state.droite && prevState != state.droite)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("droite");
            prevState = state.droite;
        }
        else if (currentState == state.gauche && prevState != state.gauche)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("gauche");
            prevState = state.gauche;
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
            Instantiate(death, transform.position - new Vector3(0, 0.5f, 0), death.transform.rotation);
            SoundManager.instance.Death.GetComponent<AudioSource>().PlayDelayed(.5f);
            Destroy(gameObject);
        }
    }

}
