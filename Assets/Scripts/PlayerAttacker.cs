﻿using UnityEngine;
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

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextFire) {
            if (Input.GetButton(controls.throwButton) && (weapon != null && ammo>0))
            {
                nextFire = Time.time + fireRate;

                Vector3 weaponSpawn = DetermineLaunchPoint().position;
                GameObject o = Instantiate(weapon, weaponSpawn, Quaternion.identity) as GameObject;
                o.GetComponent<Rigidbody2D>().velocity = (weaponSpawn-transform.position) * weaponSpeed;
                o.tag = tag;
                ammo--;
                if (ammo == 0) weapon = null;
            }
            else if(Input.GetButton(controls.punchButton) && (weapon == null || controls.canPunchWhileHolding))
            {
                nextFire = Time.time + fireRate;

                Vector3 weaponSpawn = DetermineLaunchPoint().position;
                GameObject o = Instantiate(punch, weaponSpawn, Quaternion.identity) as GameObject;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(weapon == null && (other.tag =="Axe"))
        {
            weapon = other.gameObject;
            ammo += 1;
            Destroy(other);
        }
        else if(controls.canHaveMoreThanOneAmmo && weapon.tag == other.tag)
        {
            ammo += 1;
            Destroy(other);
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

}
