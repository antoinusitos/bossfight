using UnityEngine;
using System.Collections;

public class ChangePhaseTuto : MonoBehaviour {

    Vector3 CheckPoint;
    void Start()
    {
        CheckPoint = transform.position;
    }


    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "TriggerPhase2")
        {
            Tuto.instance.phaseTuto = Tuto.PhaseTuto.PhaseTuto2;
            CheckPoint = collider.transform.position;
        }
        if (collider.gameObject.name == "TriggerPhase3")
        {
            CheckPoint = collider.transform.position;
            Tuto.instance.phaseTuto = Tuto.PhaseTuto.PhaseTuto3;
        }
        if (collider.gameObject.name == "TriggerEndTuto")
        {
            CheckPoint = collider.transform.position;
            GameManager.instance.gameState = GameManager.GameState.Game;
            Tuto.instance.phaseTuto = Tuto.PhaseTuto.PhaseTuto3;
        }
    }
}
