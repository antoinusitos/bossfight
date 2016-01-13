using UnityEngine;
using System.Collections;

public class Tuto : MonoBehaviour {

    static Tuto mInst;
    static public Tuto instance { get { return mInst; } }
    void Awake()
    {
        if (mInst == null) mInst = this;
        DontDestroyOnLoad(this);
    }

    public GameObject bombFixePrefab;
    public GameObject Block;

    public GameObject spawnBombFixe1;
    public GameObject spawnBombFixe2;
    public GameObject spawnBlock1;
    public GameObject spawnBlock2;
    public GameObject spawnBlock3;

    bool phase1 = true;
    bool phase2 = true;

    public enum PhaseTuto
    {
        PhaseTuto1, 
        PhaseTuto2,
    }

    public PhaseTuto phaseTuto;
	// Use this for initialization
	void Start ()
    {
        phaseTuto = PhaseTuto.PhaseTuto1;
        phase1 = true;
        phase2 = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (phaseTuto == PhaseTuto.PhaseTuto1)
        {
            if (phase1)
            {
                InvokeRepeating("SpawnBombFixe", 0.0f, 3.0f);
            }

        }
        else if (phaseTuto == PhaseTuto.PhaseTuto2)
        {
            if (phase2)
            {
                InvokeRepeating("SpawnBombMovable", 0.0f, 3.0f);
            }
        }
        else
        {

        }

	}
    
    void SpawnBombFixe()
    {
        phase1 = false;
        Instantiate(Block, spawnBlock1.transform.position, Quaternion.identity);
        Instantiate(Block, spawnBlock2.transform.position, Quaternion.identity);
        Instantiate(Block, spawnBlock3.transform.position, Quaternion.identity);

        Instantiate(bombFixePrefab, spawnBombFixe1.transform.position, Quaternion.identity);
        Instantiate(bombFixePrefab, spawnBombFixe2.transform.position, Quaternion.identity);
    }

    void SpawnBombMovable()
    {
        phase2 = false;
        
    }
}
