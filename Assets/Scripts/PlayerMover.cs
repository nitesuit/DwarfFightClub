﻿using UnityEngine;
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
    private AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip powerUpSound;

    
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
        audioSource = GetComponent<AudioSource>();
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
            //Vector3 movement = new Vector3(moveHorizontal * accelerationPercent, moveVertical * accelerationPercent, 0.0f);
            Vector2 movement;
            if (Mathf.Abs(moveVertical) > 0.1f || Mathf.Abs(moveHorizontal) > 0.1f)
            {
                movement = new Vector2(moveHorizontal, moveVertical).normalized * accelerationPercent;
            }else
            {
                movement = Vector2.zero;
            }
            rb.velocity = movement * speed;

            if (moveHorizontal != 0 || moveVertical != 0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = walkSound;
                    audioSource.Play();
                }
                anim.SetBool("walking", true);


                if (moveHorizontal > 0)
                {
                    anim.SetFloat("moveHorizontal", 1f);
                    anim.SetFloat("idleHorizontal", 1f);

                }
                else if (moveHorizontal < 0)
                {
                    anim.SetFloat("moveHorizontal", -1f);
                    anim.SetFloat("idleHorizontal", -1f);

                }
                else
                {
                    anim.SetFloat("moveHorizontal", 0f);
                    anim.SetFloat("idleHorizontal", 0f);

                }

                if (moveVertical > 0)
                {
                    anim.SetFloat("moveVertical", 1f);
                    anim.SetFloat("idleVertical", 1f);

                }
                else if (moveVertical < 0)
                {
                    anim.SetFloat("moveVertical", -1f);
                    anim.SetFloat("idleVertical", -1f);

                }
                else
                {
                    anim.SetFloat("moveVertical", 0f);
                    anim.SetFloat("idleVertical", 0f);
                }

            }
            else
            {
                //Debug.Log(moveVertical + " " + moveHorizontal);
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
            audioSource.clip = powerUpSound;
            audioSource.Play();
            Destroy(other.gameObject);
            StartCoroutine(SpeedBoost(speedBoost, buffTimeLimit));
			if (Application.loadedLevelName=="Level4") {
				GameManager.PlayerLevelPoints[controls.playerIdentifier]++;
			}
        }
    }

    private IEnumerator SpeedBoost(float boost, float time)
    {
        speed += boost;
        yield return new WaitForSeconds(time);
        speed -= boost;
    }
}
