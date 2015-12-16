using UnityEngine;
using System.Collections;

public class BombBoss : MonoBehaviour
{

    int degats;
	int degatsBlocks;
    int porte;
    float delayExplode;
    float timeToExplode;

    void Start()
    {
        degats = 30;
		degatsBlocks = 1;
        porte = 3;
        delayExplode = 1.0f;
        timeToExplode = 0.0f;
    }

    void Update()
    {
        timeToExplode += Time.deltaTime;
        if (timeToExplode >= delayExplode)
        {
            sendDegat();
            Destroy(gameObject);
        }
    }

    void sendDegat()
    {
        //board = Manager.instance.board;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.forward * porte, out hit, porte))
        {
            // print("There is something in front of the object!");
            if (hit.transform.GetComponent<playerScript>() != null)
            {
                hit.transform.GetComponent<playerScript>().TakeDamage(degats);
            }
			if (hit.transform.GetComponent<DestructibleBlock>() != null)
            {
                hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
            }
        }
        if (Physics.Raycast(transform.position, -Vector3.forward * porte, out hit, porte))
        {
            //print("There is something in back of the object!");
            if (hit.transform.GetComponent<playerScript>() != null)
            {
                hit.transform.GetComponent<playerScript>().TakeDamage(degats);
            }
			if (hit.transform.GetComponent<DestructibleBlock>() != null)
			{
				hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
			}
        }
        if (Physics.Raycast(transform.position, Vector3.right * porte, out hit, porte))
        {
            // print("There is something in right of the object!");
            if (hit.transform.GetComponent<playerScript>() != null)
            {
                hit.transform.GetComponent<playerScript>().TakeDamage(degats);
            }
			if (hit.transform.GetComponent<DestructibleBlock>() != null)
			{
				hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
			}
            //if (hit.transform.GetComponent<Boss>() != null)
            //{
            //    print("boss take damage!");
            //    hit.transform.GetComponent<Boss>().TakeDamage(degats);
            //}
        }
        if (Physics.Raycast(transform.position, -Vector3.right * porte, out hit, porte))
        {
            // print("There is something in left of the object!");
            if (hit.transform.GetComponent<playerScript>() != null)
            {
                hit.transform.GetComponent<playerScript>().TakeDamage(degats);
            }
			if (hit.transform.GetComponent<DestructibleBlock>() != null)
			{
				hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
			}
            //if (hit.transform.GetComponent<Boss>() != null)
            //{
            //    print("boss take damage!");
            //    hit.transform.GetComponent<Boss>().TakeDamage(degats);
            //}
        }
    }
}
