using UnityEngine;
using System.Collections;

public class Trainees : MonoBehaviour {

    public float porte = 0;
    Vector3 direction;
    Vector3 pos;

	public void SetDirection(Vector3 dir, float port)
    {
        porte = port;
        direction = dir;
        pos = transform.position;
    }

    void Update()
    {
        transform.position += direction;
        if(Vector3.Distance(pos, transform.position) > porte)
        {
            direction = Vector3.zero;
            Destroy(gameObject, 1);
        }
    }
}
