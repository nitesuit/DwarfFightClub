using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
public class LevelSelector : MonoBehaviour {

	public Sprite[] Levels;
	public Image LevelImage;
	private int _selectedIndex = 0;
	public Button forwardButton;
	public Button backButton;
	public bool IsNextLevelWindow;
	// Use this for initialization
	void Start () {
		SetLevel ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (IsNextLevelWindow) {
		
			ChangeLevel();
		}

	}

	public void UpImage() {
		_selectedIndex ++;
		
		if (_selectedIndex > Levels.Length - 1 ) {
			_selectedIndex = 0;
		}
		if (_selectedIndex <0) {
			_selectedIndex = Levels.Length-1;
		}
		LevelImage.sprite = Levels [_selectedIndex];
		SetLevel ();
	}
	public void DownImage() {
		_selectedIndex--;
		
		if (_selectedIndex > Levels.Length - 1 ) {
			_selectedIndex = 0;
		}
		if (_selectedIndex <0) {
			_selectedIndex = Levels.Length-1;
		}
		LevelImage.sprite = Levels [_selectedIndex];
		SetLevel ();
	}

	public void SetLevel() {

		GameManager.SetNextLevel (Levels[_selectedIndex].name);
		if (IsNextLevelWindow) GameManager.SetLevelPoints ();
	}

	private void ChangeLevel() {

		string _horizontalAxis = GameManager.PlayerLevelPoints.Keys.First () + "Horizontal";

		forwardButton.interactable = true;
		backButton.interactable = true;
		if (Input.GetButtonDown (_horizontalAxis)) {
			
			if (Input.GetAxisRaw(_horizontalAxis)==-1) {
				_selectedIndex--;
				backButton.interactable = false;
			}
			else {_selectedIndex++; 
				forwardButton.interactable = false; // cheat to highlight button
			}
		}
		
		if (_selectedIndex > Levels.Length - 1 ) {
			_selectedIndex = 0;
		}
		if (_selectedIndex <0) {
			_selectedIndex = Levels.Length-1;
		}
		LevelImage.sprite = Levels [_selectedIndex];
		SetLevel ();
	}
	public void StartNextLevel() {
		GameManager.StartNextLevel ();
	}

}
