using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Rescale : MonoBehaviour {

	void Start()
    {
        GetComponent<RectTransform>().localScale = new Vector3((float)Screen.width / 4.0f,(float)Screen.height / 2, 0) ;
    }
}
