using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject P1Slider;
    public GameObject P2Slider;
    public GameObject P3Slider;
    public GameObject P4Slider;
    public GameObject BossSlider;
    public GameObject BossSield;
    public GameObject Decompte;
    public GameObject skipPanel;

    static UIManager mInst;
    static public UIManager instance { get { return mInst; } }

    void Awake()
    {
        if (mInst == null) mInst = this;
        DontDestroyOnLoad(this);
        P1Slider.SetActive(false);
        P2Slider.SetActive(false);
        P3Slider.SetActive(false);
        P4Slider.SetActive(false);
    }

    void Start()
    {
        StartCoroutine(TimeToSkip());
    }

    IEnumerator TimeToSkip()
    {
        yield return new WaitForSeconds(10);
        HideSkip();
    }

    public void HideSkip()
    {
        skipPanel.SetActive(false);
    }

    public void UpdateCompteur(int l)
    {
        Decompte.GetComponent<Text>().text = l.ToString();
    }

    public void ActutaliseP1(int life)
    {
        P1Slider.GetComponent<Slider>().value = (float)life / 100.0f;
    }

    public void ActutaliseP2(int life)
    {
        P2Slider.GetComponent<Slider>().value = (float)life / 100.0f;
    }

    public void ActutaliseP3(int life)
    {
        P3Slider.GetComponent<Slider>().value = (float)life / 100.0f;
    }

    public void ActutaliseP4(int life)
    {
        P4Slider.GetComponent<Slider>().value = (float)life / 100.0f;
    }

    public void ActutaliseBoss(int life, int lifeMax)
    {
        BossSlider.GetComponent<Slider>().value = (float)life / lifeMax;
    }

    public void ActutaliseBossShield(int shield, int ShieldMax)
    {
        BossSield.GetComponent<Slider>().value = (float)shield / (float)ShieldMax;
    }

    public void ShowP1Life()
    {
        P1Slider.SetActive(true);
    }

    public void ShowP2Life()
    {
        P2Slider.SetActive(true);
    }

    public void ShowP3Life()
    {
        P3Slider.SetActive(true);
    }

    public void ShowP4Life()
    {
        P4Slider.SetActive(true);
    }
}
