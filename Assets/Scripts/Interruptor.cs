using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interruptor : MonoBehaviour {

    bool canBeDestoyed = true;
    public Material hit;

    public void TakeDamage()
    {
        if(canBeDestoyed)
        {
            canBeDestoyed = false;
            GetComponent<Renderer>().material = hit;
            //transform.GetChild(0).transform.GetComponent<giro>().Green();
            GameManager.instance.HitInterruptor();
            if(transform.childCount>1)
            {
                transform.GetChild(1).transform.GetChild(0).transform.GetComponent<Text>().text = "Thanks !";
            }

        }
    }

}
