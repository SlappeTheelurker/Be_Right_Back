using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shotgun : SemiAutomatic
{
    public int bulletAmountPerShot;
    public float spreadMax, bulletSpeedMin, bulletSpeedMax;

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

    protected override void FireWeapon()
    {
        if (GameObject.Find("SafeHouse").GetComponent<SafeHouseController>().CraftMenuOpen() == false && shotCounter <= 0)//if (Craft menu open && done reloading)
        {
            shotCounter = firingRate;
            if (transform.parent.GetComponent<PlayerController>().resourceAmount >= resourcePerShot)
            {
                animator.SetFloat("runMultiplier", fireRateAnim);
                animator.Play("Reload");

                for (int iBullet = 0; iBullet < bulletAmountPerShot; iBullet++)
                {
                    Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
                    newBullet.transform.Rotate(0, 0, Random.Range(-spreadMax, spreadMax));
                    newBullet.speed = Random.Range(bulletSpeedMin, bulletSpeedMax);
                }

                transform.parent.GetComponent<PlayerController>().resourceAmount -= resourcePerShot;
                GameObject amountChanged = Instantiate(Resources.Load("Prefabs/AmountChangeText"), GameObject.Find("Resource_Amount").transform) as GameObject;
                amountChanged.GetComponent<Text>().text = "-" + resourcePerShot;

                source.PlayOneShot(shoot);
            }
            else
            {
                source.PlayOneShot(emptyGun);
            }
        }

        if (transform.parent.GetComponent<PlayerController>().resourceAmount < resourcePerShot)
        {
            animator.Play("Empty");
        }
    }
}
