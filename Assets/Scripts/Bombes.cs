using UnityEngine;
using System.Collections;

public class Bombes : MonoBehaviour {

    int degats;
    int degatsBlocks;
    int porte;
    float delayExplode;
    float timeToExplode;
    GameObject parent = null;

    void Start()
    {
        degats = 30;
        degatsBlocks = 1;
        porte = 5;
        delayExplode = 1.0f;
        timeToExplode = 0.0f;
    }

    public void SetParent(GameObject newParent)
    {
        parent = newParent;
    }

    void Update()
    {
        timeToExplode += Time.deltaTime;
        if (timeToExplode >= delayExplode)
        {
            sendDegat();
            parent.GetComponent<PlayerScript>().SetPeutPoser(true);
            Destroy(gameObject);
        }
    }

    void sendDegat()
    {
        //board = Manager.instance.board;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.forward * porte, out hit, porte))
        {
            //print("There is something in front of the object!");
            if (hit.transform.GetComponent<PlayerScript>() != null)
            {
                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<Boss>() != null)
            {
                hit.transform.GetComponent<Boss>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<DestructibleBlock>() != null)
            {
                hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<DestructibleBlockV2>() != null)
            {
                hit.transform.GetComponent<DestructibleBlockV2>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<Interruptor>() != null)
            {
                hit.transform.GetComponent<Interruptor>().TakeDamage();
            }
        }
        if (Physics.Raycast(transform.position, -Vector3.forward * porte, out hit, porte))
        {
            //print("There is something in back of the object!");
            if (hit.transform.GetComponent<PlayerScript>() != null)
            {
                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<Boss>() != null)
            {
                hit.transform.GetComponent<Boss>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<DestructibleBlock>() != null)
            {
                hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<DestructibleBlockV2>() != null)
            {
                hit.transform.GetComponent<DestructibleBlockV2>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<Interruptor>() != null)
            {
                hit.transform.GetComponent<Interruptor>().TakeDamage();
            }
        }
        if (Physics.Raycast(transform.position, Vector3.right * porte, out hit, porte))
        {
            //print("There is something in right of the object!");
            if (hit.transform.GetComponent<PlayerScript>() != null)
            {
                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<Boss>() != null)
            {
                hit.transform.GetComponent<Boss>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<DestructibleBlock>() != null)
            {
                hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<DestructibleBlockV2>() != null)
            {
                hit.transform.GetComponent<DestructibleBlockV2>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<Interruptor>() != null)
            {
                hit.transform.GetComponent<Interruptor>().TakeDamage();
            }
        }
        if (Physics.Raycast(transform.position, -Vector3.right * porte, out hit, porte))
        {
            //print("There is something in left of the object!");
            if (hit.transform.GetComponent<PlayerScript>() != null)
            {
                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<Boss>() != null)
            {
                hit.transform.GetComponent<Boss>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<DestructibleBlock>() != null)
            {
                hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<DestructibleBlockV2>() != null)
            {
                hit.transform.GetComponent<DestructibleBlockV2>().TakeDamage(degatsBlocks);
            }
            if (hit.transform.GetComponent<Interruptor>() != null)
            {
                hit.transform.GetComponent<Interruptor>().TakeDamage();
            }
        }
    }
}
