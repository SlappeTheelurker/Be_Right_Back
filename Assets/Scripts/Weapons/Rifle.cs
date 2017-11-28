using UnityEngine;
using System.Collections;

public class Rifle : Automatic
{
    // Use this for initialization
    void Start()
    {
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
