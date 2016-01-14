using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GameObject player1UI;
    public GameObject player2UI;
    public GameObject player3UI;
    public GameObject player4UI;

    public GameObject player1Button;
    public GameObject player2Button;
    public GameObject player3Button;
    public GameObject player4Button;

    public GameObject player1Fond;
    public GameObject player2Fond;
    public GameObject player3Fond;
    public GameObject player4Fond;

    public Sprite ready;
    public Sprite notReady;

    public Material A;
    public Material B;

    public Material P1Mat;
    public Material P2Mat;
    public Material P3Mat;
    public Material P4Mat;
    public Material PDefault;

    bool player1Ready = false;
    bool player2Ready = false;
    bool player3Ready = false;
    bool player4Ready = false;

    bool Selection = true;

    static MenuManager mInst;
    static public MenuManager instance { get { return mInst; } }

    void Awake()
    {
        if (mInst == null) mInst = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        player1Fond.GetComponent<Image>().color = PDefault.color;
        player2Fond.GetComponent<Image>().color = PDefault.color;
        player3Fond.GetComponent<Image>().color = PDefault.color;
        player4Fond.GetComponent<Image>().color = PDefault.color;

        SoundManager.instance.transform.GetChild(2).gameObject.SetActive(false);
    }

    public bool GetReady(int p)
    {
        if (p == 1)
            return player1Ready;
        else if (p == 2)
            return player2Ready;
        else if (p == 3)
            return player3Ready;
        else
            return player4Ready;
    }

    void Update()
    {
        if (Selection)
        {
            if (Input.GetButtonDown("Jump_1"))
            {
                if (!player1Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    player1Ready = !player1Ready;
                    player1UI.GetComponent<Image>().sprite = ready;
                    player1Button.GetComponent<Image>().material = B;
                    player1Fond.GetComponent<Image>().color = P1Mat.color;
                }
            }
            if (Input.GetButtonDown("Cancel_1"))
            {
                if (player1Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    player1Ready = !player1Ready;
                    player1UI.GetComponent<Image>().sprite = notReady;
                    player1Button.GetComponent<Image>().material = A;
                    player1Fond.GetComponent<Image>().color = PDefault.color;
                }
            }

            if (Input.GetButtonDown("Jump_2"))
            {
                if (!player2Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    player2Ready = !player2Ready;
                    player2UI.GetComponent<Image>().sprite = ready;
                    player2Button.GetComponent<Image>().material = B;
                    player2Fond.GetComponent<Image>().color = P2Mat.color;
                }
            }
            if (Input.GetButtonDown("Cancel_2"))
            {
                if (player2Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    player2Ready = !player2Ready;
                    player2UI.GetComponent<Image>().sprite = notReady;
                    player2Button.GetComponent<Image>().material = A;
                    player2Fond.GetComponent<Image>().color = PDefault.color;
                }
            }

            if (Input.GetButtonDown("Jump_3"))
            {
                if (!player3Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    player3Ready = !player3Ready;
                    player3UI.GetComponent<Image>().sprite = ready;
                    player3Button.GetComponent<Image>().material = B;
                    player3Fond.GetComponent<Image>().color = P3Mat.color;
                }
            }
            if (Input.GetButtonDown("Cancel_3"))
            {
                if (player3Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    player3Ready = !player3Ready;
                    player3UI.GetComponent<Image>().sprite = notReady;
                    player3Button.GetComponent<Image>().material = A;
                    player3Fond.GetComponent<Image>().color = PDefault.color;
                }
            }

            if (Input.GetButtonDown("Jump_4"))
            {
                if (!player4Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    player4Ready = !player4Ready;
                    player4UI.GetComponent<Image>().sprite = ready;
                    player4Button.GetComponent<Image>().material = B;
                    player4Fond.GetComponent<Image>().color = P4Mat.color;
                }
            }
            if (Input.GetButtonDown("Cancel_4"))
            {
                if (player4Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    player4Ready = !player4Ready;
                    player4UI.GetComponent<Image>().sprite = notReady;
                    player4Button.GetComponent<Image>().material = A;
                    player4Fond.GetComponent<Image>().color = PDefault.color;
                }
            }

            if (Input.GetButtonDown("Start_1"))
            {
                if (player1Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    Selection = false;
                    Application.LoadLevel("Game");
                }
            }

            if (Input.GetButtonDown("Start_2"))
            {
                if (player2Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    Selection = false;
                    Application.LoadLevel("Game");
                }
            }

            if (Input.GetButtonDown("Start_3"))
            {
                if (player3Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    Selection = false;
                    Application.LoadLevel("Game");
                }
            }

            if (Input.GetButtonDown("Start_4"))
            {
                if (player4Ready)
                {
                    SoundManager.instance.validationMenu.Play();
                    Selection = false;
                    Application.LoadLevel("Game");
                }
            }
        }
    }
}
