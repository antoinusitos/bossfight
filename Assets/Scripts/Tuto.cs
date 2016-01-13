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

    public GameObject tutoPrefab;

    public GameObject Block;
    public GameObject bombFixePrefab;
    public GameObject bombMovablePrefab;

    bool phase1 = true;
    bool phase2 = true;

    public enum PhaseTuto
    {
        PhaseTuto1, 
        PhaseTuto2,
        PhaseTuto3,
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
        if(GameManager.instance.gameState == GameManager.GameState.Tuto)
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
                    CancelInvoke();
                    tutoPrefab.GetComponent<SpawnBomb>().canon.SetActive(true);
                    InvokeRepeating("SpawnBombMovable", 0.0f, 3.0f);
                }
            }
            else if (phaseTuto == PhaseTuto.PhaseTuto3)
            {
                CancelInvoke();
            }
        }
        

	}
    
    void SpawnBombFixe()
    {
        phase1 = false;
        Vector3 pos = TileMapGenerator.instance.tutoPrefabInstance.transform.position;

        GameObject b1 = (GameObject)Instantiate(Block, tutoPrefab.GetComponent<SpawnBomb>().spawnBlock1.transform.position + pos, Quaternion.identity);
        Instantiate(Block, tutoPrefab.GetComponent<SpawnBomb>().spawnBlock2.transform.position + pos, Quaternion.identity);
        Instantiate(Block, tutoPrefab.GetComponent<SpawnBomb>().spawnBlock3.transform.position + pos, Quaternion.identity);

        Instantiate(bombFixePrefab, tutoPrefab.GetComponent<SpawnBomb>().spawnBombFixe1.transform.position + pos, Quaternion.identity);
        Instantiate(bombFixePrefab, tutoPrefab.GetComponent<SpawnBomb>().spawnBombFixe2.transform.position + pos, Quaternion.identity);


        Instantiate(bombMovablePrefab, tutoPrefab.GetComponent<SpawnBomb>().spawnBombMovable1.transform.position + pos, Quaternion.identity);
        Instantiate(bombMovablePrefab, tutoPrefab.GetComponent<SpawnBomb>().spawnBombMovable2.transform.position + pos, Quaternion.identity);
    }

    void SpawnBombMovable()
    {
        phase2 = false;
        Vector3 pos = TileMapGenerator.instance.tutoPrefabInstance.transform.position;
        Instantiate(bombMovablePrefab, tutoPrefab.GetComponent<SpawnBomb>().spawnBombMovable3.transform.position + pos, Quaternion.identity);
        Instantiate(bombMovablePrefab, tutoPrefab.GetComponent<SpawnBomb>().spawnBombMovable4.transform.position + pos, Quaternion.identity);
        Instantiate(bombMovablePrefab, tutoPrefab.GetComponent<SpawnBomb>().spawnBombMovable5.transform.position + pos, Quaternion.identity);
    }
}
