using UnityEngine;
using System.Collections;

[System.Serializable]
public class MovementControls
{
    public string playerIdentifier;
    public string vertical;
    public string horizontal;
}
public class PlayerMover : MonoBehaviour {

    public MovementControls controls;
    public float speed = 2.5f;
    public float acceleration = 0.1f;
    public float beginAccelerationPercent = 0.1f;
    public float speedBoost = 2f;
    public float buffTimeLimit = 5f;

    private float maxSpeed;
    private Rigidbody2D rb;
    private float accelerationPercent = 0.2f;
    private LifeController lifeC;
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lifeC = GetComponent<LifeController>();
        maxSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {

    }

    void FixedUpdate()
    {
        //Debug.Log("Immobile: " + lifeC.Immobile);
        if(!lifeC.Immobile){

            float moveHorizontal = Input.GetAxis(controls.playerIdentifier + controls.horizontal);
            float moveVertical = Input.GetAxis(controls.playerIdentifier + controls.vertical);

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
            //Debug.Log("Acceleration percent: (" + accelerationPercent + ")");
            Vector3 movement = new Vector3(moveHorizontal * accelerationPercent, moveVertical * accelerationPercent, 0.0f);
            rb.velocity = movement * speed;

            if (moveHorizontal != 0 || moveVertical != 0)
            {
                anim.SetBool("walking", true);


                if (moveHorizontal > 0)
                {
                    anim.SetFloat("moveHorizontal", 1f);
                }
                else if (moveHorizontal < 0)
                {
                    anim.SetFloat("moveHorizontal", -1f);
                }
                else
                {
                    anim.SetFloat("moveHorizontal", 0f);
                }

                if (moveVertical > 0)
                {
                    anim.SetFloat("moveVertical", 1f);
                }
                else if (moveVertical < 0)
                {
                    anim.SetFloat("moveVertical", -1f);
                }
                else
                {
                    anim.SetFloat("moveVertical", 0f);
                }

            }
            else
            {
                anim.SetBool("walking", false);
            }
            anim.SetFloat("moveHorizontal", moveHorizontal);
            anim.SetFloat("moveVertical", moveVertical);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "SpeedBoost")
        {
            Destroy(other.gameObject);
            StartCoroutine(SpeedBoost(speedBoost, buffTimeLimit));
        }
    }

    private IEnumerator SpeedBoost(float boost, float time)
    {
        speed += boost;
        yield return new WaitForSeconds(time);
        speed -= boost;
    }
}
