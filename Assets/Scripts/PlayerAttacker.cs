using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CombatControls
{
    public string playerIdentifier; 
    public string punchButton;
    public string throwButton;
    public string throwHorizontalInput;
    public string throwVerticalInput;
    public bool canPunchWhileHolding;
    public bool canHaveMoreThanOneAmmo;
}
[System.Serializable]
public class Directions
{
    public Transform up;
    public Transform right;
    public Transform down;
    public Transform left;
}

public class PlayerAttacker : MonoBehaviour {
    public CombatControls controls;
    public int ammo = 0;
    public GameObject weapon;
    public GameObject punch;
    public float weaponSpeed;
    public float nextFire;
    public float fireRate;
    public Directions launchPoints;
    private LifeController life;
    public float punchDuration;
    public AudioClip pickUpSound;
    private AudioSource audioSource;
    // Use this for initialization
    void Start () {
        life = GetComponent<LifeController>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextFire && life.lives>0) {
            if (Input.GetButton(controls.playerIdentifier + controls.throwButton) && (weapon != null && ammo>0))
            {
                nextFire = Time.time + fireRate;

                Vector3 weaponSpawn = DetermineLaunchPoint().position;
                GameObject o = Instantiate(weapon, weaponSpawn, Quaternion.identity) as GameObject;
                o.name = weapon.name;
                Vector3 direction = weaponSpawn - transform.position;
                o.GetComponent<Rigidbody2D>().velocity = (weaponSpawn - transform.position) * weaponSpeed;
                if (direction.x > 0 || direction.y < 0) Flip(o);
                o.transform.parent = transform;
                o.tag = "Hazard";
                ammo--;
                if (ammo == 0) weapon = null;
            }
            else if(Input.GetButton(controls.playerIdentifier + controls.punchButton) && (weapon == null || controls.canPunchWhileHolding))
            {
                nextFire = Time.time + fireRate;

                Vector3 weaponSpawn = DetermineLaunchPoint().position;
                GameObject o = Instantiate(punch, weaponSpawn, Quaternion.identity) as GameObject;
                o.transform.parent = transform;
                o.tag = "Hazard";
                Destroy(o, punchDuration);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (weapon == null && other.gameObject.layer == LayerMask.NameToLayer("Weapon") && other.transform.parent!=transform && tag == "Player")//&& other.tag != "Hazard")
        {
            weapon = Resources.Load(other.gameObject.name) as GameObject;
            ammo += 1;
            Destroy(other.gameObject);
            audioSource.clip = pickUpSound;
            audioSource.Play();
        }
        else if(weapon != null && other.gameObject.layer == LayerMask.NameToLayer("Weapon") && controls.canHaveMoreThanOneAmmo && weapon.name == other.name && tag == "Player")
        {
            ammo += 1;
            Destroy(other.gameObject);
            audioSource.clip = pickUpSound;
            audioSource.Play();
        }
    }

    private Transform DetermineLaunchPoint()
    {
       
        float x = Input.GetAxis(controls.playerIdentifier + controls.throwHorizontalInput);
        float y = Input.GetAxis(controls.playerIdentifier + controls.throwVerticalInput);
        if (x == 0 && y == 0) return launchPoints.down;
        if (y > 0 && y >= x && y >= -x) return launchPoints.up;
        if (y < 0 && y <= x && y <= -x) return launchPoints.down;
        if (x < 0 && y > x && y < -x) return launchPoints.left;
        if (x > 0 && y < x && y > -x) return launchPoints.right;
        return launchPoints.down;
    }

    private void Flip(GameObject obj)
    {
        Vector3 theScale = obj.transform.localScale;
        theScale.x *= -1;
        obj.transform.localScale = theScale;
    }

    public void DropWeapon()
    {
        if (weapon != null)
        {
            GameObject o = Instantiate(weapon, transform.position, Quaternion.identity) as GameObject;
            o.name = weapon.name;
            weapon = null;
            o.tag = "Battle_axe";
        }
        ammo = 0;
    }
}
