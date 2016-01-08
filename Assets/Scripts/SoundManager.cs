using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (this != GameManager.instance)
        {
            Destroy(this);
        }
    }



    public AudioSource bombeExplosion;
    public AudioSource bossDamages;
    public AudioSource bossDeath;
    public AudioSource crateExplosion;
	
}
