using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    private Rigidbody2D rb;
    public float velocity;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if(rb.velocity.x>0 || rb.velocity.y < 0) rb.angularVelocity = -velocity;
        else rb.angularVelocity = velocity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        rb.angularVelocity = 0;
    }
}
