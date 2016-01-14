using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

    public enum state
    {
        play,
        quit
    }

    state currentState;

    public GameObject thePlay;
    public GameObject theEnd;

    public GameObject APlay;
    public GameObject AQuit;

    void Start()
    {
        currentState = state.play;
        UpdateUI();
    }

    void UpdateUI()
    {
        if(currentState == state.play)
        {
            thePlay.GetComponent<Text>().color = new Color(0, 1, 0);
            thePlay.GetComponent<Text>().fontStyle = FontStyle.Bold;
            theEnd.GetComponent<Text>().color = new Color(1, 1, 1);
            theEnd.GetComponent<Text>().fontStyle = FontStyle.Normal;
            APlay.SetActive(true);
            AQuit.SetActive(false);
        }
        else if (currentState == state.quit)
        {
            thePlay.GetComponent<Text>().color = new Color(1, 1, 1);
            thePlay.GetComponent<Text>().fontStyle = FontStyle.Normal;
            theEnd.GetComponent<Text>().fontStyle = FontStyle.Bold;
            theEnd.GetComponent<Text>().color = new Color(0, 1, 0);
            APlay.SetActive(false);
            AQuit.SetActive(true);
        }
    }

    void ChangeState(state theState)
    {
        currentState = theState;
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetAxis("L_YAxis_1") < -0)
        {
            ChangeState(state.play);
        }
        else if (Input.GetAxis("L_YAxis_1") > 0)
        {
            ChangeState(state.quit);
        }

        if (Input.GetButtonDown("Jump_1"))
        {
            if (currentState == state.play)
                Application.LoadLevel("Selection");
            else
                Application.Quit();
        }
    }

}
