using UnityEngine;
using System.Collections;

public class ChangePhase : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Tuto.instance.phaseTuto = Tuto.PhaseTuto.PhaseTuto2;
        }
    }
}
