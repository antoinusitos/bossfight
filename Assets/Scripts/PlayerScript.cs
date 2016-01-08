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

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
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

       if (Input.GetButtonDown("Attack_" + playerNumber) && peutPoser)
       {
           Vector3 bombPosition = new Vector3(TileMapGenerator.instance.tileMap[TileMapGenerator.instance.CoordToIndex(transform.position.x, transform.position.z)].x, transform.position.y, TileMapGenerator.instance.tileMap[TileMapGenerator.instance.CoordToIndex(transform.position.x, transform.position.z)].z);
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
