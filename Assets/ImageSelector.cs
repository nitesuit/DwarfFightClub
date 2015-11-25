using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageSelector : MonoBehaviour {

	public Sprite[] Players;
	public string Axis;
	public Image playerImage;
	private int _selectedIndex=0;
	private int _maxIndex;
	public Button forwardButton;
	public Button backButton;
	public GameObject Disable;
	public GameObject Ready;
	private string _horizontalAxis;
	// Use this for initialization
	private string _fireAxis;
	void Awake () {
		_horizontalAxis = Axis + "Horizontal";
		_fireAxis = Axis + "Fire";
		GameManager.PlayerIdentifierSprite = new Dictionary<string, Sprite> ();
	
	}
	void Start() {
		playerImage.sprite = Players [_selectedIndex];
	}
	// Update is called once per frame
	void Update () {

		if (!Disable.activeSelf&&!Ready.activeSelf) {
			ChangePlayer ();
			SelectPlayer();
		}
		else {
		if (Input.GetAxisRaw(_horizontalAxis)==1) {
				Disable.SetActive(false);

			} 
			}
	
	}

	private void ChangePlayer() {

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
		
		if (_selectedIndex > Players.Length - 1 ) {
			_selectedIndex = 0;
		}
		if (_selectedIndex <0) {
			_selectedIndex = Players.Length-1;
		}
		playerImage.sprite = Players [_selectedIndex];
	}
	private void SelectPlayer() {

		if (Input.GetButtonDown(_fireAxis)) {

			if (!GameManager.SelectedPlayerIndex.Contains(_selectedIndex)) {
				GameManager.SelectedPlayerIndex.Add(_selectedIndex);
				GameManager.Players[_selectedIndex].GetComponent<PlayerMover>().controls.playerIdentifier = Axis;
				GameManager.Players[_selectedIndex].GetComponent<PlayerAttacker>().controls.playerIdentifier = Axis;
				GameManager.PlayerIdentifierSprite.Add(Axis,Players[_selectedIndex]);
				Ready.SetActive(true);
			}
		}
	}

	public void UpImage() {
		_selectedIndex ++;
		
		if (_selectedIndex > Players.Length - 1 ) {
			_selectedIndex = 0;
		}
		if (_selectedIndex <0) {
			_selectedIndex = Players.Length-1;
		}
		playerImage.sprite = Players [_selectedIndex];
	}
	public void DownImage() {
		_selectedIndex--;
		
		if (_selectedIndex > Players.Length - 1 ) {
			_selectedIndex = 0;
		}
		if (_selectedIndex <0) {
			_selectedIndex = Players.Length-1;
		}
		playerImage.sprite = Players [_selectedIndex];
	}
}
