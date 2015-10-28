using UnityEngine;
using System.Collections;

public class PineappleRotator : MonoBehaviour {
    private Rigidbody2D rb;
    public float velocity;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = -velocity;
    }

    public void Update()
    {
        if (transform.rotation.z < -0.8)
        {
            rb.angularVelocity = velocity;
        }
        else if (transform.rotation.z >= 0)
        {
            rb.angularVelocity = -velocity;
        }
    }
}
