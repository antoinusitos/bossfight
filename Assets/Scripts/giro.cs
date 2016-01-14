using UnityEngine;
using System.Collections;

public class giro : MonoBehaviour {

    public Material green;

	void Update()
    {
        transform.Rotate(Vector3.up, 50.0f * Time.deltaTime);
    }

    public void Green()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.GetComponent<Renderer>().material = green;
        }
    }
}
