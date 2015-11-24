using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject Prefab;
	float _nextSpawn = 0;
	public float SpawnRate;
	public float SpawnRateAmplitude;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	

		if (Time.time > _nextSpawn) {
		
			float rate = Random.Range(SpawnRate-SpawnRateAmplitude,SpawnRate+SpawnRateAmplitude);
			_nextSpawn = rate+Time.time;
			Spawn ();
		}

	}

	private void Spawn() {

	 GameObject go = GameObject.Instantiate (Prefab, gameObject.transform.position,gameObject.transform.rotation) as GameObject;
	
		go.GetComponent<GoblinController> ().range = 50;
	}

}
