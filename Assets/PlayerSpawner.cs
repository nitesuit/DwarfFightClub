using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Spawn (GameManager.PlayingPlayers);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void Spawn(GameObject []players) {

		Transform [] transforms = GetComponentsInChildren<Transform> ();
		for (int i=0; i<players.Length; i++) {
		
			Instantiate(players[i],transforms[i+1].position,transforms[i+1].rotation);
		}
	} 
}
