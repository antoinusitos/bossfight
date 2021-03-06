﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Vector3 direction;
	// Use this for initialization
	void Start () {
        StartCoroutine(MoveBullet(20.0f));
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position -= new Vector3(0, 0, 1)*Time.deltaTime;
        transform.position += direction * Time.deltaTime;
	}

    IEnumerator MoveBullet(float timer)
    {
        float currentTime = 0;
        while (currentTime < timer)
        {
            
            yield return new WaitForSeconds(0.1f);
            currentTime += 0.1f;
        }

        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name != "Bullet(Clone)")
        {
            Debug.Log(collider.gameObject.tag);
            if (collider.gameObject.tag == "Destructible")
            {
                Debug.Log("destroy");
                Destroy(collider.gameObject);
            }
            Destroy(this.gameObject);
            
        }
    }
}
