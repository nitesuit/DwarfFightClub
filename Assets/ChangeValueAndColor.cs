using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeValueAndColor : MonoBehaviour {

	private LifeController lc;
	private Slider slider;
	public Image image;
	// Use this for initialization
	void Awake () {
		lc = GetComponentInParent<LifeController> ();
		slider = GetComponent<Slider> ();

		slider.maxValue = lc.lives;
		slider.value = lc.lives;
		slider.minValue = 0;
	}
	
	// Update is called once per frame
	void Update () {

		slider.value = lc.lives;
		Debug.Log (slider.value);
	
	}

	public void ChangeColor(Slider slider) {

		image.color = new Color (Mathf.Abs (1 - slider.value/slider.maxValue), slider.value / slider.maxValue, image.color.b, image.color.a);


	}
}
