using UnityEngine;
using System.Collections;

public class Bombes : MonoBehaviour {

    int degats;
    int porte;
    float delayExplode;
    float timeToExplode;
    int posX;
    int posY;
    GameObject parent = null;

    private AudioSource bombe;

    void Start()
    {
        degats = 10;
        porte = 3;
        delayExplode = 1.0f;
        timeToExplode = 0.0f;

        bombe = SoundManager.instance.bombeExplosion.GetComponent<AudioSource>();
    }

    void Pose(int X, int Y)
    {
        posX = X;
        posY = Y;
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
            parent.GetComponent<playerScript>().SetPeutPoser(true);
            // play bombe explosion
            bombe.Play();
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
            /*if(hit.transform.GetComponent<Player>() != null)
            {
                hit.transform.GetComponent<Player>().TakeDamage(degats);
            }*/
            if (hit.transform.GetComponent<Boss>() != null)
            {
                hit.transform.GetComponent<Boss>().TakeDamage(degats);
            }
        }
        if (Physics.Raycast(transform.position, -Vector3.forward * porte, out hit, porte))
        {
            //print("There is something in back of the object!");
            /*if(hit.transform.GetComponent<Player>() != null)
            {
                hit.transform.GetComponent<Player>().TakeDamage(degats);
            }*/
            if (hit.transform.GetComponent<Boss>() != null)
            {
                hit.transform.GetComponent<Boss>().TakeDamage(degats);
            }
        }
        if (Physics.Raycast(transform.position, Vector3.right * porte, out hit, porte))
        {
            //print("There is something in right of the object!");
            /*if(hit.transform.GetComponent<Player>() != null)
            {
                hit.transform.GetComponent<Player>().TakeDamage(degats);
            }*/
            if (hit.transform.GetComponent<Boss>() != null)
            {
                hit.transform.GetComponent<Boss>().TakeDamage(degats);
            }
        }
        if (Physics.Raycast(transform.position, -Vector3.right * porte, out hit, porte))
        {
            //print("There is something in left of the object!");
            /*if(hit.transform.GetComponent<Player>() != null)
            {
                hit.transform.GetComponent<Player>().TakeDamage(degats);
            }*/
            if (hit.transform.GetComponent<Boss>() != null)
            {
                hit.transform.GetComponent<Boss>().TakeDamage(degats);
            }
        }
    }
}
