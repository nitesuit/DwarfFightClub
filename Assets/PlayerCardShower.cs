using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class PlayerCardShower : MonoBehaviour {
	public bool IsEndGame;
	public GameObject PlayerCard;
    public Text TotalPoints;
    public Text LevelPoints;
	void Awake() {

		if (!IsEndGame) ShowPlayers ();
		if (IsEndGame)
			ShowEndPlayers ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowPlayers() {

		int i = 0;
		foreach (var elem in GameManager.PlayerLevelPoints) {
		
			if (i==0) GameManager.LevelWinnerIdentifier = elem.Key;
			GameObject go = GameObject.Instantiate(PlayerCard,new Vector3(),new Quaternion()) as GameObject;
			go.transform.SetParent(this.gameObject.transform);
			go.GetComponentsInChildren<Image>()[1].sprite = GameManager.PlayerIdentifierSprite[elem.Key];

            Text[] texts = go.GetComponentsInChildren<Text>();
            foreach (var text in texts)
            {
                if (text.gameObject.name == "LevelScore")
                {
                    text.text = elem.Value + " pts";
                }
                if (text.gameObject.name == "TotalScore")
                {
                    text.text = GameManager.PlayerPoints[elem.Key] + "pts";
                }
            }
			i++;
		}
	

	}

	public void ShowEndPlayers() {
		foreach (var elem in GameManager.PlayerPoints) {
			
			GameObject go = GameObject.Instantiate(PlayerCard,new Vector3(),new Quaternion()) as GameObject;
			go.transform.SetParent(this.gameObject.transform);
			go.GetComponentsInChildren<Image>()[1].sprite = GameManager.PlayerIdentifierSprite[elem.Key];

            Text[] texts = go.GetComponentsInChildren<Text>();
            foreach (var text in texts)
            {
                if (text.gameObject.name == "LevelScore")
                {
                    text.text = GameManager.PlayerLevelPoints[elem.Key] + "pts";
                }
                if (text.gameObject.name == "TotalScore")
                {
                    text.text = elem.Value + " pts";
                }
            }

		}
	}
	public void LoadHome() {
		Application.LoadLevel ("HomeScreen");
	}
}
