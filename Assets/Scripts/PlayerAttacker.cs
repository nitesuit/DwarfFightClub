using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CombatControls
{
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
    // Use this for initialization
    void Start () {
        life = GetComponent<LifeController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextFire && life.lives>0) {
            if (Input.GetButton(controls.throwButton) && (weapon != null && ammo>0))
            {
                nextFire = Time.time + fireRate;

                Vector3 weaponSpawn = DetermineLaunchPoint().position;
                GameObject o = Instantiate(weapon, weaponSpawn, Quaternion.identity) as GameObject;
                Vector3 direction = weaponSpawn - transform.position;
                o.GetComponent<Rigidbody2D>().velocity = (weaponSpawn - transform.position) * weaponSpeed;
                if (direction.x > 0 || direction.y < 0) Flip(o);
                o.transform.parent = transform;
                o.tag = "Hazard";
                ammo--;
                if (ammo == 0) weapon = null;
            }
            else if(Input.GetButton(controls.punchButton) && (weapon == null || controls.canPunchWhileHolding))
            {
                nextFire = Time.time + fireRate;

                Vector3 weaponSpawn = DetermineLaunchPoint().position;
                Instantiate(punch, weaponSpawn, Quaternion.identity);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (weapon == null && other.gameObject.layer == LayerMask.NameToLayer("Weapon") && other.tag != "Hazard")
        {
            weapon = Resources.Load(other.tag) as GameObject;
            ammo += 1;
            Destroy(other.gameObject);
        }
        else if(weapon != null && other.gameObject.layer == LayerMask.NameToLayer("Weapon") && controls.canHaveMoreThanOneAmmo && weapon.tag == other.tag)
        {
            ammo += 1;
            Destroy(other.gameObject);
        }
    }

    private Transform DetermineLaunchPoint()
    {
        float x = Input.GetAxis(controls.throwHorizontalInput);
        float y = Input.GetAxis(controls.throwVerticalInput);
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
}
