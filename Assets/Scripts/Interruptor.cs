using UnityEngine;
using System.Collections;

public class Interruptor : MonoBehaviour {

    bool canBeDestoyed = true;
    public Material hit;

    public void TakeDamage()
    {
        if(canBeDestoyed)
        {
            canBeDestoyed = false;
            GetComponent<Renderer>().material = hit;

            GameManager.instance.HitInterruptor();
        }
    }

}
