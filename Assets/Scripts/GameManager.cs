using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	static GameManager mInst;
	static public GameManager instance { get { return mInst; } }
	void Awake()
	{
		if (mInst == null) mInst = this;
		DontDestroyOnLoad(this);
	}

	public enum GameState {Tuto, Game}

	public GameState gameState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
