using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    private Rigidbody2D rb;
    public float velocity;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        if (rb.velocity.x > 0 || rb.velocity.y < 0) rb.angularVelocity = -velocity;
        else rb.angularVelocity = velocity;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            rb.angularVelocity = 0;
        }
    }
}
