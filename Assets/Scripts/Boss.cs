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
    public int nbBomb;
    public int interruptor;
    private AudioSource bossDamages;
    private AudioSource bossDeath;

    void Start()
    {
        quart = 4;
        lifeMax = 1000;
        life = lifeMax;
       shieldMax = 150;
        shield = shieldMax;
        currentState = State.Pattern1;
        prevState = currentState;
        timeToAttack = 0f;
        delayAttack = 5.0f;
        hasPlatforming = false;
        timeToShield = 0f;
        nbBomb = 20;

        delayShield = 0.5f;
        interruptor = 4;

        bossDamages = SoundManager.instance.bossDamages.GetComponent<AudioSource>();
        bossDeath = SoundManager.instance.bossDeath.GetComponent<AudioSource>();
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
                TileMapGenerator.instance.CleanLevelSpawnInterruptor();
                quart = 3;
                currentState = State.Platform;
            }
            else if (hasPlatforming)
            {
                PlayerManager.instance.Revive();
                currentState = State.Pattern2;
                hasPlatforming = false;
            }
        }
        else if (life >= (lifeMax / 4))//3e quart
        {
            if (quart == 3 && prevState == State.Pattern2)
            {
                //TileMapGenerator.instance.CleanLevelSpawnInterruptor();
                quart = 2;
                currentState = State.Platform;
            }
            else if (hasPlatforming)
            {
                PlayerManager.instance.Revive();
                currentState = State.Pattern3;
                hasPlatforming = false;
            }
        }
        else //4e quart
        {
            if (quart == 2 && prevState == State.Pattern3)
            {
                TileMapGenerator.instance.CleanLevelSpawnInterruptor();
                quart = 1;
                currentState = State.Platform;
            }
            else if (hasPlatforming)
            {
                PlayerManager.instance.Revive();
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
        if (timeToAttack >= delayAttack)
        {
            timeToAttack = 0f;
            SpawnBomb();
        }
    }

    void Pattern2()
    {
        if (timeToAttack >= delayAttack)
        {
            timeToAttack = 0f;
            SpawnBomb();
        }
    }

    void Pattern3()
    {
        if (timeToAttack >= delayAttack)
        {
            timeToAttack = 0f;
            SpawnBomb();
        }
    }

    void SpawnBomb()
    {
        for (int i = 0; i < nbBomb; i++)
        {
            Vector3 pos = TileMapGenerator.instance.GetRandomBombPlace().GetPosition();
            Instantiate(bomb, pos, Quaternion.identity);
        }
    }

    void Platforming()
    {
        timeToShield += Time.deltaTime;
        if (timeToShield >= delayShield)
        {
            timeToShield = 0f;
            shield++;
            UIManager.instance.ActutaliseBossShield(shield);
        }
        if (shield >= shieldMax)
        {
            shield = shieldMax;
            CompletePlatforming();
        }
        if (timeToAttack >= delayAttack)
        {
            timeToAttack = 0f;
            SpawnBomb();
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
                // son dégats boss
                bossDamages.Play();

                shield -= theDamage;
                UIManager.instance.ActutaliseBossShield(shield);
                if (shield < 0)
                    shield = 0;
            }
            else
            {
                life -= theDamage;
                 UIManager.instance.ActutaliseBoss(life);
            }
            if (life <= 0)
            {
                //son mort boss
                bossDeath.Play();

                currentState = State.Dead;
            }
            ChooseState();
        }
    }

    public void OneInterruptorHit()
    {
        interruptor--;
        if (interruptor == 0)
        {
            CompletePlatforming();
            TileMapGenerator.instance.DestructibleBlockGeneration();
            interruptor = 4;
        }
    }
}
