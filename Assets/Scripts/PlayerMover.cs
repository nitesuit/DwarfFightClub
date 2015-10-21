using UnityEngine;
using System.Collections;

[System.Serializable]
public class Controls
{
    public string attackButton;
    public string vertical;
    public string horizontal;
}
public class PlayerMover : MonoBehaviour {

    public Controls controls;
    public float speed = 2.5f;
    public float acceleration = 0.1f;
    public float beginAccelerationPercent = 0.1f;

    private float currSpeed=0f;
    private Rigidbody2D rb;
    private float accelerationPercent = 0.2f;



    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis(controls.horizontal);
        float moveVertical = Input.GetAxis(controls.vertical);
        //Debug.Log("inputXY: (" + moveVertical + ", " + moveHorizontal + ")");
        if (accelerationPercent < 1)
        {
            accelerationPercent += acceleration * Time.deltaTime;
        }
        else
        {
            accelerationPercent = 1;
        }

        if (moveVertical == 0 && moveHorizontal == 0)
        {
            accelerationPercent = beginAccelerationPercent;
        }
        Debug.Log("Acceleration percent: (" + accelerationPercent + ")");
        Vector3 movement = new Vector3(moveHorizontal * accelerationPercent, moveVertical*accelerationPercent, 0.0f);
        rb.velocity = movement * speed;
    }
}
