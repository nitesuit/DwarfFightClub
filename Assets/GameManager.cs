using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject[] PlayersArray;
	public static GameObject[] Players;
	public static List<int> SelectedPlayerIndex;
	public static GameObject[] PlayingPlayers;
	private int _selectedGameMode;
	public string [] Scenes;
	// Use this for initialization
	public int GameMode;

	void Awake() {
		SelectedPlayerIndex = new List<int> ();
		Players = PlayersArray;
	}

	void Start () {

		GameObject.DontDestroyOnLoad (gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StarGame() {

		PlayingPlayers = new GameObject[SelectedPlayerIndex.Count];

		if (PlayingPlayers.Length > 0) {

			for (int i=0; i<PlayingPlayers.Length; i++) {
		
				PlayingPlayers [i] = Players [SelectedPlayerIndex [i]];
			}

			if (GameMode == 0) {
		
				Application.LoadLevel (Scenes [0]);


			} else {
				Application.LoadLevel (Scenes [1]);
			}
		}
	}

	public void SetGameMode(int mode) {

		GameMode = mode;
	}


}
