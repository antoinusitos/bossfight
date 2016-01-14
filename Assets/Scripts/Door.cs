using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    float currentTime = 0;
    public bool move = false;

    void Start()
    {
        move = false;
        transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
    }

    void Update()
    {

        if (move)
        {
            transform.position -= new Vector3(0.0f, Time.deltaTime, 0.0f);
            currentTime += Time.deltaTime;

            if (currentTime >= 4.0f)
            {
                move = false;
            }
        }
    }
}
