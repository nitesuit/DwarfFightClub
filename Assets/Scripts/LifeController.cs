using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeController : MonoBehaviour {
    public int lives = 1;
    public float throwbackPower = 3f;
    public AudioClip takeHitSound;
    public AudioClip dieSound;
    private bool immune = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D[] colliders;
    private Color originalColor;
    private AudioSource audioSource;
    private Animator anim;

    public bool Immobile{get; set;}

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliders = gameObject.GetComponents<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        originalColor = spriteRenderer.material.color;
        Immobile = false;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnCollisionStay2D(Collision2D other)
    {
        if (!immune && (
            other.gameObject.tag == "Enemy" 
            || (other.gameObject.tag == "Hazard" 
            && (other.transform.parent == null || other.transform.parent.name != transform.name))))
        {
            lives--;
            immune = true;
            if (other.collider.name != "LavaPit") Immobile = true;
            if (tag == "Player")
            {
                audioSource.clip = takeHitSound;
                audioSource.Play();
                if (lives > 0) StartCoroutine(TakeDamage(other.contacts[0].point));
            }
            if (tag == "Goblin")
            {
                StartCoroutine(TakeDamage(other.contacts[0].point));
            }
            if (lives <= 0)
            {
                if (tag == "Breakable" || tag == "Goblin")
                {
                    GetComponent<Animator>().SetBool("IsAlive", false);
                    //spriteRenderer.sortingLayerName = "Background";
                    Destroy(gameObject, 0.55f);
                    return;
                }
                audioSource.clip = dieSound;
                audioSource.Play();
                if (tag == "Player") die();

            }

        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("other.gameObject.tag:" + other.gameObject.tag);

        if (!immune && (other.gameObject.tag == "Enemy" 
            ||((other.gameObject.tag == "Hazard" || other.gameObject.tag == "Punch")
            && (other.transform.parent == null || other.transform.parent.name != transform.name))
            ))
        {

            if (!(other.tag == "Punch" && tag == other.transform.parent.tag)) lives--;//tag == "Player" && other.transform.parent.tag == "Player"))lives--;
            immune = true;
            Immobile = true;
            if (tag == "Player")
            {
                audioSource.clip = takeHitSound;
                audioSource.Play();
                StartCoroutine(TakeDamage(other.transform.position));
            }
            if(tag == "Goblin")
            {
                StartCoroutine(TakeDamage(other.transform.position));
            }
            if (lives <= 0)
            {
                if(tag == "Breakable" || tag == "Goblin")
                {
                    GetComponent<Animator>().SetBool("IsAlive",false);
                    //spriteRenderer.sortingLayerName = "Background";
                    Destroy(gameObject, 0.55f);
                    return;
                }
                audioSource.clip = dieSound;
                audioSource.Play();
                if(tag == "Player") die();

            }
        }
    }

    IEnumerator TakeDamage(Vector3 collisionPoint)
    {
        Vector3 throwback = (collisionPoint - transform.position).normalized * -throwbackPower;
        for (float f = 3f; f >= 0; f -= 1f)
        {
            rb.velocity = throwback;
            //Vector3 tmp = other.transform.position - transform.position;
            //Vector3 throwback = new Vector3(Mathf.Sign(tmp.x), Mathf.Sign(tmp.y), Mathf.Sign(tmp.z)) * -throwbackPower;
            if (f % 2 == 0) spriteRenderer.material.color = Color.red;
            else spriteRenderer.material.color = originalColor;
            Color c = spriteRenderer.material.color;
            c.a = 0.9f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(.1f);
        }
        //Debug.Log("baiges spalvos");
        immune = false;
        Immobile = false;
        rb.velocity = Vector3.zero;
        spriteRenderer.material.color = originalColor;
        
    }
    private void die()
    {
        //Debug.Log("dydis:");
        //Debug.Log(colliders.Length);
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        anim.SetBool("death", true);
        Immobile = true;
        tag = "DeadPlayer";
        GetComponent<PlayerAttacker>().DropWeapon();
        Destroy(gameObject,1f);
    }
}
