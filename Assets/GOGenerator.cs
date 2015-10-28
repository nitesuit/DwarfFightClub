using UnityEngine;
using System.Collections;


public class GOGenerator : MonoBehaviour {

	public GameObject objectToThrow;
	private GameObject instantiatedObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Hazard" || other.gameObject.tag == "Battle_axe")
			InstantiateObject ();
		Debug.Log ("kolizija");
	}
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Hazard" || other.gameObject.tag == "Battle_axe")
			InstantiateObject ();
		Debug.Log ("trigeris");
	}

	private void InstantiateObject() {

		if (instantiatedObject == null)
			InstantiateAtPosition ();
		else if (!instantiatedObject.transform.IsChildOf (this.transform)) {
			InstantiateAtPosition();

		}
		

	}

	private void InstantiateAtPosition() {
			Vector2 position  = new Vector2 (this.transform.position.x,this.transform.position.y);
			GameObject go = Instantiate (objectToThrow, position, Quaternion.identity) as GameObject;
			go.transform.parent = this.transform;
			instantiatedObject = go;

		}
}
