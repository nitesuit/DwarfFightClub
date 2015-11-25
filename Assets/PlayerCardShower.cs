using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class PlayerCardShower : MonoBehaviour {

	public GameObject PlayerCard;
	void Awake() {
		ShowPlayers ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowPlayers() {


		foreach (var elem in GameManager.PlayerLevelPoints) {
		
			GameObject go = GameObject.Instantiate(PlayerCard,new Vector3(),new Quaternion()) as GameObject;
			go.transform.SetParent(this.gameObject.transform);
			go.GetComponentsInChildren<Image>()[1].sprite = GameManager.PlayerIdentifierSprite[elem.Key];
			go.GetComponentInChildren<Text>().text  = elem.Value + " pts";
		}
	

	}
}
