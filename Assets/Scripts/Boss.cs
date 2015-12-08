using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public enum State
    {
        Pattern1 = 1, //destruction shield
        Pattern2 = 2, //plus agressif
        Pattern3 = 3, //saut sur les coins
        Platform = 4, //regagne son shield
        Dead = 5,

    }

    public int life;
    public int lifeMax;
    public int shield;
    public int shieldMax;
    public State currentState;
    public State prevState;
    public float timeToAttack;
    public float delayAttack;
    public float timeToShield;
    public float delayShield;
    public bool hasPlatforming;
    public int quart;
    public GameObject bomb;

    void Start()
    {
        quart = 4;
        lifeMax = 1000;
        life = lifeMax;
        shieldMax = 20;
        shield = shieldMax;
        currentState = State.Pattern1;
        prevState = currentState;
        timeToAttack = 0f;
        delayAttack = 5.0f;
        hasPlatforming = false;
        timeToShield = 0f;
        delayShield = 1.0f;
    }

    void Update()
    {
        if (currentState != State.Dead)
        {
            IncreaseTimers();
            ApplyState();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(10);
        }
    }

    void ChooseState()
    {
        if (shield > 0)
        {
            currentState = State.Pattern1;
        }
        else if (life >= (lifeMax / 4) * 3) //1er quart
        {
            currentState = State.Pattern2;
        }
        else if (life >= (lifeMax / 4) * 2) //2e quart
        {
            if (quart == 4 && prevState == State.Pattern2)
            {
                quart = 3;
                currentState = State.Platform;
            }
            else if (hasPlatforming)
            {
                currentState = State.Pattern2;
                hasPlatforming = false;
            }
        }
        else if (life >= (lifeMax / 4))//3e quart
        {
            if (quart == 3 && prevState == State.Pattern2)
            {
                quart = 2;
                currentState = State.Platform;
            }
            else if (hasPlatforming)
            {
                currentState = State.Pattern3;
                hasPlatforming = false;
            }
        }
        else //4e quart
        {
            if (quart == 2 && prevState == State.Pattern3)
            {
                quart = 1;
                currentState = State.Platform;
            }
            else if (hasPlatforming)
            {
                currentState = State.Pattern3;
                hasPlatforming = false;
            }
        }
    }

    void ApplyState()
    {
        switch (currentState)
        {
            case State.Pattern1:
                Pattern1();
                break;
            case State.Pattern2:
                Pattern2();
                break;
            case State.Pattern3:
                Pattern3();
                break;
            case State.Platform:
                Platforming();
                break;
        }
        prevState = currentState;
    }

    void IncreaseTimers()
    {
        timeToAttack += Time.deltaTime;
    }

    void Pattern1()
    {
        //Debug.Log ("pattern1");
        if (timeToAttack >= delayAttack)
        {
            timeToAttack = 0f;
            //Debug.Log ("attaque pattern1 !");
            //Instantiate(bomb, transform.position + new Vector3(2.0f, 0, 2.0f), Quaternion.identity);
            SpawnBomb();
        }
    }

    void Pattern2()
    {
        //Debug.Log ("pattern2");
        if (timeToAttack >= delayAttack)
        {
            timeToAttack = 0f;
            //Debug.Log ("attaque pattern2 !");
            //Instantiate(bomb, transform.position + new Vector3(2.0f, 0, 2.0f), Quaternion.identity);
            SpawnBomb();
        }
    }

    void Pattern3()
    {
        //Debug.Log ("pattern3");
        if (timeToAttack >= delayAttack)
        {
            timeToAttack = 0f;
            //Debug.Log ("attaque pattern3 !");
            //Instantiate(bomb, transform.position + new Vector3(2.0f, 0, 2.0f), Quaternion.identity);
            SpawnBomb();
        }
    }

    void SpawnBomb()
    {
        for (int i = 0; i < 5; i++)
        {
            float rand = Random.Range(0.0f, 1.0f);
            int value = Random.Range(2, 4);
            float rand2 = Random.Range(0.0f, 1.0f);
            int value2 = Random.Range(2, 4);
            if (rand >= 0.5f)
                value = value * -1;
            if (rand2 >= 0.5f)
                value2 = value2 * -1;
            //Debug.Log("rand:" + rand + " rand2:" + rand2);
            Instantiate(bomb, transform.position + new Vector3(value, 0, value2), Quaternion.identity);
        }
    }

    void Platforming()
    {
        timeToShield += Time.deltaTime;
        if (timeToShield >= delayShield)
        {
            timeToShield = 0f;
            shield++;
        }
        if (shield >= shieldMax)
        {
            shield = shieldMax;
            CompletePlatforming();
        }
    }

    public void CompletePlatforming()
    {
        hasPlatforming = true;
        ChooseState();
    }

    public void TakeDamage(int theDamage)
    {
        if (currentState != State.Platform)
        {
            if (shield > 0)
            {
                shield -= theDamage;
                if (shield < 0)
                    shield = 0;
            }
            else
            {
                life -= theDamage;
            }
            if (life <= 0)
            {
                currentState = State.Dead;
            }
            ChooseState();
        }
    }
}
