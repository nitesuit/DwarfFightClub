using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class GameManager : MonoBehaviour {

	public GameObject[] PlayersArray;
	public static GameObject[] Players;
	public static Dictionary<string,Sprite> PlayerIdentifierSprite;
	public static List<int> SelectedPlayerIndex;
	public static GameObject[] PlayingPlayers;
	public static List<string> DeadPlayerIdentificators;
	public static Dictionary <string,int> PlayerPoints;
	public static Dictionary <string,int> PlayerLevelPoints;
	private static List<string> _playingLevelList;
	private int _selectedGameMode;
	private static string _nextLevel;
	public static string LevelWinnerIdentifier;
	public string [] Scenes;
	public static string[] ScenesArray;
	public GameObject NextLevelScreen;
	public static GameObject NextLevelCanvas;
	public GameObject EndGameScreen;
	public static GameObject EndGameCanvas;
	// Use this for initialization
	public int GameMode;

	void Awake() {
		SelectedPlayerIndex = new List<int> ();
		Players = PlayersArray;
		ScenesArray = Scenes;
		NextLevelCanvas = NextLevelScreen;
		EndGameCanvas = EndGameScreen;
	}

	void Start () {

		GameObject.DontDestroyOnLoad (gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StarGame() {


		int numberOfPlayers = SelectedPlayerIndex.Count;
		DeadPlayerIdentificators = new List<string> ();
		PlayingPlayers = new GameObject[numberOfPlayers];
		PlayerPoints = new Dictionary<string, int> ();
		_playingLevelList = new List<string>();
		SetLevelPoints ();
		if (PlayingPlayers.Length > 1) {

			for (int i=0; i<numberOfPlayers; i++) {
		
				PlayingPlayers [i] = Players [SelectedPlayerIndex [i]];
				PlayerPoints.Add(PlayingPlayers[i].GetComponent<PlayerAttacker>().controls.playerIdentifier,0);
			}

			if (GameMode == 0) {
			
				StartFirstMode();

			} else if (GameMode==1) {


				StartSecondMode();
			}
			else if (GameMode==2) {

				StartThirdMode();
			}
		}
	}

	public void SetGameMode(int mode) {

		GameMode = mode;
	}

	public void ExitGame() {
		Application.Quit ();
	}

	public static void PlayerDied(string playerIdentifier) {

		DeadPlayerIdentificators.Add (playerIdentifier);
		int points = PlayingPlayers.Length - DeadPlayerIdentificators.Count;
		PlayerLevelPoints [playerIdentifier] -= points;
		if (DeadPlayerIdentificators.Count  == PlayingPlayers.Length-1) {
		

			foreach (var go in GameObject.FindGameObjectsWithTag("Player")) {
				go.SetActive(false);
			}
			ContinueMode();
			//next level
			//end game
		}

	}
	public static void SetNextLevel(string levelName) {

		_nextLevel = levelName;
	}

	private void StartFirstMode() {

		Application.LoadLevel (_nextLevel);
	}
	private void StartSecondMode() {
		int numberOfLevels = 4;
		if (Scenes.Length < numberOfLevels) {
			numberOfLevels = Scenes.Length;
		}
		for (int i=0;i<numberOfLevels;i++) {
			_playingLevelList.Add(ScenesArray[i]);
		}

		StartNextLevel ();
	}

	private void StartThirdMode() {
		int numberOfLevels = 6;
		if (Scenes.Length < numberOfLevels) {
			numberOfLevels = Scenes.Length;
		}
		for (int i=0;i<numberOfLevels;i++) {
			_playingLevelList.Add(ScenesArray[i]);
		}

		StartNextLevel ();
	}

	private static void ContinueMode() {

		if (_playingLevelList.Count == 0) {
			//game end
			ConvertLevelPoints();
			SumPoints();
			ConvertPoints();
			ShowEndGameScreen();
		}
		else {
		
			ConvertLevelPoints();
			SumPoints();
			ShowNextLevelScreen();
		}

	}

	public static void SetLevelPoints() {
		int numberOfPlayers = SelectedPlayerIndex.Count;
		PlayerLevelPoints = new Dictionary<string, int> ();
		for (int i=0; i<numberOfPlayers; i++) {
			
			PlayingPlayers [i] = Players [SelectedPlayerIndex [i]];
			PlayerLevelPoints.Add(PlayingPlayers[i].GetComponent<PlayerAttacker>().controls.playerIdentifier,0);
		}
	}

	private static void ShowNextLevelScreen() {
		GameObject go = GameObject.Instantiate (NextLevelCanvas, new Vector3(0f,0f,0f),new Quaternion()) as GameObject;
		go.GetComponentInChildren<LevelSelector> ();
	//	Sprite[] levels = new Sprite[go.GetComponentInChildren<LevelSelector> ().Levels.Length];
		List <Sprite> list = new List<Sprite> ();
		foreach (var lvl in  go.GetComponentInChildren<LevelSelector> ().Levels) {
		
			if (_playingLevelList.Contains(lvl.name)) list.Add(lvl);
		}
		go.GetComponentInChildren<LevelSelector> ().Levels = list.ToArray ();

	}

	private static void SumPoints() {

		foreach (var key in PlayerLevelPoints.Keys) {
		
			PlayerPoints[key] += PlayerLevelPoints[key];
		}
	}
	private static void ConvertLevelPoints() {

		Dictionary<string,int> newDic = new Dictionary<string,int> ();

		var items = from pair in PlayerLevelPoints
			orderby pair.Value descending
				select pair;
		
		int points = PlayerLevelPoints.Count;
		foreach (KeyValuePair<string, int> pair in items)
		{
			newDic.Add (pair.Key,points);
			points--;
		}

		PlayerLevelPoints = newDic;

	}

	private static void ConvertPoints() {
		Dictionary<string,int> newDic = new Dictionary<string,int> ();
		
		var items = from pair in PlayerPoints
			orderby pair.Value descending
				select pair;
		
		//int points = PlayerPoints.Count;
		foreach (KeyValuePair<string, int> pair in items)
		{
			newDic.Add (pair.Key,pair.Value);
		}
		
		PlayerPoints = newDic;
	}
	public static void StartNextLevel() {
		_playingLevelList.Remove (_nextLevel);
		DeadPlayerIdentificators = new List<string> ();
		SetLevelPoints ();
		Application.LoadLevel (_nextLevel);
	}

	private static void ShowEndGameScreen() {
		GameObject go = GameObject.Instantiate (EndGameCanvas, new Vector3(0f,0f,0f),new Quaternion()) as GameObject;
		
	}
	

	}

