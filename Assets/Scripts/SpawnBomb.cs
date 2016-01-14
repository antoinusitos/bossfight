using UnityEngine;
using System.Collections;

public class SpawnBomb : MonoBehaviour {

    public GameObject spawnBombFixe1;
    public GameObject spawnBombFixe2;
    public GameObject spawnBlock1;
    public GameObject spawnBlock2;
    public GameObject spawnBlock3;

    public GameObject spawnBombMovable1;
    public GameObject spawnBombMovable2;
    public GameObject spawnBombMovable3;
    public GameObject spawnBombMovable4;
    public GameObject spawnBombMovable5;

    public GameObject canon;

    public GameObject Door;

    public GameObject RespawnZone;

    public GameObject block1;
    public GameObject block2;
    public GameObject block3;

    public void Block()
    {
        block1.SetActive(true);
        block2.SetActive(true);
        block3.SetActive(true);
    }
}
