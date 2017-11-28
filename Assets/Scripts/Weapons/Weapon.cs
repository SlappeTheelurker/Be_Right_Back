using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour {

    public bool isFiring;
    public int resourcePerShot;
    public float bulletSpeed, firingRate, fireRateAnim, yPosOffset, armLength;
    public Bullet bullet;
    public AudioClip shoot, emptyGun;
    public Transform firePoint;

    protected AudioSource source;
    protected Animator animator;
    protected Rigidbody2D myRigidbody;
    protected float shotCounter;
    protected Vector3 positionBeforeForward;

    // Use this for initialization
    void Start (){
        myRigidbody = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        positionBeforeForward = transform.position;
    }

    protected void PointWeapon()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + yPosOffset, -1f);

        // Raycast Mouse Looking, SOURCE: http://answers.unity3d.com/questions/599271/rotating-a-sprite-to-face-mouse-target.html
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        //have gun not rotate in place
        positionBeforeForward = transform.position;
        transform.position += transform.up * armLength;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        
        //flip gun if pointing left
        if (myRigidbody.rotation > 0)
        {
            transform.Rotate(Vector3.up, 180f);
        }
    }

    protected virtual void FireWeapon()
    {
        if (GameObject.Find("SafeHouse").GetComponent<SafeHouseController>().CraftMenuOpen() == false && shotCounter <= 0)//if (Craft menu open && done reloading)
        {
            shotCounter = firingRate;
            if (transform.parent.GetComponent<PlayerController>().resourceAmount >= resourcePerShot)
            {
                animator.SetFloat("runMultiplier", fireRateAnim);
                animator.Play("Reload");

                Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
                newBullet.speed = bulletSpeed;

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
