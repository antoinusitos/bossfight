﻿using UnityEngine;
using System.Collections;

public class BombBoss : MonoBehaviour
{

    int degats;
	int degatsBlocks;
    int porte;
    float delayExplode;
    float timeToExplode;

    private AudioSource bombe;

    public GameObject fx;
    public GameObject explosionFx;

    void Start()
    {
        degats = 15;
		degatsBlocks = 1;
        porte = 5;
        delayExplode = Random.Range(0.5f,1.5f);
        timeToExplode = 0.0f;

        bombe = SoundManager.instance.bombeExplosion.GetComponent<AudioSource>();
    }

    void Update()
    {
        timeToExplode += Time.deltaTime;
        if (timeToExplode >= delayExplode)
        {
            sendDegat();
            bombe.Play();
            Instantiate(explosionFx, transform.position, Quaternion.identity);
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
            if (hit.transform.GetComponent<PlayerScript>() != null)
            {
                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<DestructibleBlock>() != null)
            {
                hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
            }
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.forward, Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.forward, porte);
        }
        if (Physics.Raycast(transform.position, -Vector3.forward * porte, out hit, porte))
        {
            //print("There is something in back of the object!");
            if (hit.transform.GetComponent<PlayerScript>() != null)
            {
                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
            }
            if (hit.transform.GetComponent<DestructibleBlock>() != null)
			{
				hit.transform.GetComponent<DestructibleBlock>().TakeDamage(degatsBlocks);
			}
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(-Vector3.forward, Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(-Vector3.forward, porte);
        }
        if (Physics.Raycast(transform.position, Vector3.right * porte, out hit, porte))
        {
            // print("There is something in right of the object!");
            if (hit.transform.GetComponent<PlayerScript>() != null)
            {
                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
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
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.right, Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.right, porte);
        }
        if (Physics.Raycast(transform.position, -Vector3.right * porte, out hit, porte))
        {
            // print("There is something in left of the object!");
            if (hit.transform.GetComponent<PlayerScript>() != null)
            {
                hit.transform.GetComponent<PlayerScript>().TakeDamage(degats);
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
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.left, Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            GameObject theFx = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
            theFx.GetComponent<Trainees>().SetDirection(Vector3.left, porte);
        }
    }
}
