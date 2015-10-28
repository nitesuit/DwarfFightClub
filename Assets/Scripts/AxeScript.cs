using UnityEngine;
using System.Collections;

public class AxeScript : MonoBehaviour {
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public AudioClip axeFly;
    public AudioClip axeHit;

    private bool hit = false;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (tag != "Battle_axe")
        {
            audioSource.loop = true;
            audioSource.clip = axeFly;
            audioSource.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit2D(Collider2D other)
    {
       // tag = "Hazard";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hit && other.gameObject.transform != transform.parent)
        {
            hit = true;
            audioSource.Stop();
            audioSource.clip = axeHit;
            audioSource.loop = false;
            audioSource.Play();
        }
        if (other.gameObject.tag != "Player" )
        {
            tag = "Battle_axe";
            transform.SetParent(null);
            rb.velocity = Vector3.zero;
        }
        
    }
}
