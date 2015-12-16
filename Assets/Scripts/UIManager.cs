using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject P1Slider;
    public GameObject P2Slider;
    public GameObject BossSlider;
    public GameObject BossSield;

    static UIManager mInst;
    static public UIManager instance { get { return mInst; } }

    void Awake()
    {
        if (mInst == null) mInst = this;
        DontDestroyOnLoad(this);
    }

    public void ActutaliseP1(int life)
    {
        P1Slider.GetComponent<Slider>().value = (float)life / 100.0f;
    }

    public void ActutaliseP2(int life)
    {
        P2Slider.GetComponent<Slider>().value = (float)life / 100.0f;
    }

    public void ActutaliseBoss(int life)
    {
        BossSlider.GetComponent<Slider>().value = (float)life / 1000.0f;
    }

    public void ActutaliseBossShield(int shield)
    {
        BossSield.GetComponent<Slider>().value = (float)shield / 150.0f;
    }
}
