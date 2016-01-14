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
    public int tier;
    public GameObject bomb;
    public GameObject bombSuiveuse;
    public GameObject canon;
    public int nbBomb;
    public int nbBombSuiveuse;
    public int interruptor;
    private AudioSource bossDamages;
    private AudioSource bossDeath;
    private bool spawnCanon = false;

    public float respiration = 0;
    public float respirationMax;
    public float respirationSpeed;
    public bool inspire = true;

    void Start()
    {
        tier = 3;
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
        nbBomb = 10;
        nbBombSuiveuse = 5;
        spawnCanon = false;

        delayShield = 0.5f;
        interruptor = 4;

        respirationMax = .03f;
        respirationSpeed = 0.5f;
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

        if(inspire)
        {
            respiration += 0.1f * Time.deltaTime * respirationSpeed;
            transform.GetChild(0).transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime * respirationSpeed;
            if(respiration >= respirationMax)
            {
                inspire = false;
            }
        }
        else
        {
            respiration -= 0.1f * Time.deltaTime * respirationSpeed;
            transform.GetChild(0).transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime * respirationSpeed;
            if (respiration <= -respirationMax)
            {
                inspire = true;
            }
        }
    }

    void ChooseState()
    {
        if (shield > 0)
        {
            if (hasPlatforming)
            {
                TileMapGenerator.instance.RemoveAllInterruptorsBlocks();
                PlayerManager.instance.Revive();
            }
            currentState = State.Pattern1;
        }
        else if (life >= (lifeMax / 3) * 2) //1er tier
        {
            currentState = State.Pattern1;
        }
        else if (life >= (lifeMax / 3)) //2e tier
        {
            if (tier == 3 && prevState == State.Pattern1)
            {
                TileMapGenerator.instance.CleanLevelSpawnInterruptor();
                tier = 2;
                currentState = State.Platform;
            }
            else if (hasPlatforming)
            {
               // Debug.Log("has platforming");
                
                currentState = State.Pattern2;
                hasPlatforming = false;
            }
        }
        else /*if (life >= (lifeMax / 3))*///3e tier
        {
            if (tier == 2 && prevState == State.Pattern2)
            {
                TileMapGenerator.instance.CleanLevelSpawnInterruptor();
                tier = 1;
                currentState = State.Platform;
            }
            else if (hasPlatforming)
            {
                currentState = State.Pattern3;
                hasPlatforming = false;
            }
        }
        /*else //4e tier
        {
            if (tier == 2 && prevState == State.Pattern3)
            {
                TileMapGenerator.instance.CleanLevelSpawnInterruptor();
                tier = 1;
                currentState = State.Platform;
            }
            else if (hasPlatforming)
            {
                PlayerManager.instance.Revive();
                currentState = State.Pattern3;
                hasPlatforming = false;
            }
        }*/
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
            SpawnBombSuiveuse();

        }
    }

    void Pattern3()
    {
        if (timeToAttack >= delayAttack)
        {
            timeToAttack = 0f;
            SpawnBomb();
            SpawnBombSuiveuse();
            if(!spawnCanon)
            {
                SpawnCanon();
                spawnCanon = true;
            }
            
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

    void SpawnBombSuiveuse()
    {
        for (int i = 0; i < nbBombSuiveuse; i++)
        {
            Vector3 pos = TileMapGenerator.instance.GetRandomBombPlace().GetPosition();
            Instantiate(bombSuiveuse, pos, Quaternion.identity);
        }
    }

    void SpawnCanon()
    {
        GameObject canon1 = Instantiate(canon, new Vector3(2.0f, 0.0f, 1.0f), Quaternion.Euler(0, 180, 0)) as GameObject;

        GameObject canon2 = Instantiate(canon, new Vector3(1.0f, 0.0f, TileMapGenerator.instance.tileMapSize -3), Quaternion.Euler(0, 270, 0)) as GameObject;

        GameObject canon3 = Instantiate(canon, new Vector3(TileMapGenerator.instance.tileMapSize - 3, 0.0f, TileMapGenerator.instance.tileMapSize - 2), Quaternion.identity) as GameObject;

        GameObject canon4 = Instantiate(canon, new Vector3(TileMapGenerator.instance.tileMapSize - 2, 0.0f, 2.0f), Quaternion.Euler(0, 90, 0)) as GameObject;
        
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
            if(prevState == State.Pattern2)
            {
                TileMapGenerator.instance.DestructibleBlockGeneration();
            }
            interruptor = 4;
        }
    }
}
