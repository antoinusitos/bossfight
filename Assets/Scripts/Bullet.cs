using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(MoveBullet(9.0f));
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(0, 0, 1)*Time.deltaTime;
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
}
