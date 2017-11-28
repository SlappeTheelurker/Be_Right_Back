using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
    public Bullet bullet;
    public float bulletSpeed, firingRate;
    public Transform firePoint;
    public AudioClip shot;


    private bool firing;
    private float fireCounter;
    private Ray2D ray;
    private RaycastHit2D rayHit;
    private AudioSource source;

	void Start () {
        ray = new Ray2D();
        rayHit = new RaycastHit2D();
        source = GetComponent<AudioSource>();
	}

    private void FixedUpdate()
    {
        if (firing)
        {
            if (fireCounter <= 0)
            {
                fireCounter = firingRate;

                Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
                newBullet.speed = bulletSpeed;
                
                source.PlayOneShot(shot);
            }
            firing = false;
        }
        fireCounter -= Time.deltaTime;
    }

    public void TargetAt(Vector3 targetPos)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPos - transform.position);

        ray.origin = transform.position;
        ray.direction = transform.forward;

        rayHit = Physics2D.Raycast(transform.forward, targetPos - transform.position);

        firing = true;
    }
}