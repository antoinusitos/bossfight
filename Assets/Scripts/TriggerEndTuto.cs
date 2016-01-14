﻿using UnityEngine;
using System.Collections;

public class TriggerEndTuto : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.instance.Revive();
        }
    }
}
