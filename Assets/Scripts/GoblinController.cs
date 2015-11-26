using UnityEngine;
using System.Collections;

public class GoblinController : MonoBehaviour {

    public float speed = 20f;
    public float range = 20f;
    public float meleeRange = 1f;
    public float nextFire;
    public float fireRate;
    public float punchDuration;
    private bool targetLocked = false;
    private Animator anim;
    public GameObject weapon;
    private Rigidbody2D rb;
    private LifeController lc;
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        lc = GetComponent<LifeController>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate () {
        if (lc.Immobile) return;
        var players = GameObject.FindGameObjectsWithTag("Player");
        Vector2 direction = new Vector2(0,-range);
        targetLocked = false;
        if (players.Length != 0)
        {
            foreach (var player in players)
            {
                Vector2 vectorToPlayer = player.transform.position - transform.position;
                if (direction.sqrMagnitude > vectorToPlayer.sqrMagnitude)
                {
                    direction = vectorToPlayer;
                    targetLocked = true;
                }
            }
        }
        else
        {
            direction = new Vector2(0, 0);
        }
        if (!targetLocked) direction = direction = new Vector2(0, 0);
        if (direction.magnitude > meleeRange) rb.velocity = direction.normalized * speed;
        else 
        {
            rb.velocity = Vector2.zero;
            Debug.Log(direction);
            if (Time.time > nextFire && targetLocked)
            {
                nextFire = Time.time + fireRate;

                Vector2 weaponSpawn =  (Vector2)transform.position + direction.normalized/2;
                GameObject o = Instantiate(weapon, weaponSpawn, Quaternion.identity) as GameObject;
                o.transform.parent = transform;
                o.tag = "Hazard";
                Destroy(o, punchDuration);
            }
        }
        anim.SetBool("walking", true);

        if (direction.x <= 1 && direction.x >= 0 && direction.y >= 0 && direction.y <= 1)
	    {
            Debug.Log("lookin right");

            anim.SetFloat("moveHorizontal", 1f);
            anim.SetFloat("moveVertical", 0f);
	        return;
	    }

        if (direction.x >= -1 && direction.x <= 0 && direction.y >= 0 && direction.y <= 1)
        {
            Debug.Log("lookin left");

            anim.SetFloat("moveHorizontal", -1f);
            anim.SetFloat("moveVertical", 0f);
            return;
        }
    }
}
