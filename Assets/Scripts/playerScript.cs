using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

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
       /* //move left
        if (Input.GetAxis("L_XAxis_" + playerNumber) < -deadZone)
        {
            //transform.Translate(-speed, 0f, 0f);
            cc.SimpleMove(new Vector3(-speed * Time.deltaTime, 0f, 0f));
        }
       
        //move right
     if (Input.GetAxis("L_XAxis_" + playerNumber) > deadZone)
          {
        // transform.Translate(speed, 0f, 0f);
         cc.SimpleMove(new Vector3(speed * Time.deltaTime, 0f, 0f));
         }*/
       

        //move up move up, rough comme une louve
       //if (Input.GetAxis("L_YAxis_" + playerNumber) > deadZone)
            //{
              //  Debug.Log("up");
          // transform.Translate(0f, 0f, -speed);
                //cc.SimpleMove(new Vector3(0f, 0f, -speed * Time.deltaTime));
        
        if (cc.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("L_XAxis_" + playerNumber), 0, -Input.GetAxis("L_YAxis_" + playerNumber));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
         //  }
     

         //move down
      // if (Input.GetAxis("L_YAxis_" + playerNumber) < -deadZone)
          //  {
              //  Debug.Log("down");
                /*Vector3 forward = transform.TransformDirection(Vector3.forward);
                float curSpeed = speed * Input.GetAxis("Vertical");
                cc.SimpleMove(-forward * curSpeed);*/
          //  }

       /*if (Input.GetButton("Attack_" + playerNumber))
       {
           
       }*/
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
        if (life <= 0)
        {
            //mort
        }
    }

}
