using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : SemiAutomatic {

    public Pistol(): base()
    {

    }

	// Use this for initialization
	void Start ()    {
        myRigidbody = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        positionBeforeForward = transform.position;
    }

    private void FixedUpdate()
    {
        PointWeapon();

        if (transform.parent.GetComponent<PlayerController>().isDead)
        {
            GetComponent<Weapon>().enabled = false;
        }

        shotCounter -= Time.deltaTime;
    }
}
