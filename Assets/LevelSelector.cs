﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
public class LevelSelector : MonoBehaviour {

	public Sprite[] Levels;
	public Image LevelImage;
	private int _selectedIndex = 0;
	public Button forwardButton;
	public Button backButton;
	public GameObject Disabled;
	public bool IsNextLevelWindow;
	public Button StartButton;
	private int _counter=0;
    public float TimeBetweenClicks = 0.3f;
    private float _timeStamp = 0;
    // Use this for initialization
    void Start () {
		SetLevel ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (IsNextLevelWindow) {
			string _horizontalAxis = GameManager.LevelWinnerIdentifier + "Horizontal";
			ChangeLevel (_horizontalAxis);
			ButtonIvoke(GameManager.LevelWinnerIdentifier + "Fire");
		
		} else {
			if (Disabled.activeSelf) {
				ChangeLevel ("P1_Horizontal");
				if (_counter>0)ButtonIvoke("P1_Fire");
				_counter++;
			}
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

	private void ChangeLevel(string _horizontalAxis) {



		forwardButton.interactable = true;
		backButton.interactable = true;
        if (Time.time >= _timeStamp)
        {

            if (Input.GetAxisRaw(_horizontalAxis) == -1)
            {
                _selectedIndex--;
                backButton.interactable = false;
                _timeStamp = Time.time + TimeBetweenClicks;
            }

            else if (Input.GetAxisRaw(_horizontalAxis) == 1)
            {
                _selectedIndex++;
                forwardButton.interactable = false; // cheat to highlight button
                _timeStamp = Time.time + TimeBetweenClicks;
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

	private void ButtonIvoke(string btn) {
		if (Input.GetButtonDown (btn)) {
			
			ColorBlock cb = StartButton.colors;
			cb.normalColor = new Color(cb.normalColor.r,cb.normalColor.g,cb.normalColor.b,0.5f);
			StartButton.colors = cb;
			StartButton.onClick.Invoke();
		}
	}

}
