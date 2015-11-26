using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelPauser : MonoBehaviour {

	public Image image; 
	private Text text;
	// Use this for initialization
	void Start()
	{
		text = GetComponentInChildren<Text> ();
		StartCoroutine (Pause (2));

	}
	
	private IEnumerator Pause(int p)
	{
		Debug.Log ("paused");
		Time.timeScale = 0.1f;
		float pauseEndTime = Time.realtimeSinceStartup + 1f;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1;
		StartCoroutine (FadeOut());
		Debug.Log ("Started");
	}

	
	IEnumerator FadeOut(){
		while (image.color.a > 0){
				image.color = new Color(image.color.r,image.color.g,image.color.b,image.color.a-1f*Time.deltaTime*2f);
			text.color = new Color(text.color.r,text.color.g,text.color.b,text.color.a-1f*Time.deltaTime*2f);
			yield return 0;    
		}    
		Destroy (gameObject);
	}
}
