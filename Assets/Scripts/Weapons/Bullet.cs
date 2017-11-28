using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed, totalLifeTime;
    public AudioClip hit;
    private AudioSource source;
    private float lifeTime, dieCounter;
    private bool dying;

	// Use this for initialization
	void Start () {
        lifeTime = 0;
        dying = false;
        source = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
	}

    // Update for physics shtuff
    private void FixedUpdate()
    {
        lifeTime++;
        if (lifeTime >= totalLifeTime) Destroy(gameObject);

        if (dying)
        {
            GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<BoxCollider2D>().enabled = false;
            dieCounter++;
            if (dieCounter > 6)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            source.PlayOneShot(hit);
            dying = true;
        }
        if (other.CompareTag("Rock") || other.CompareTag("House"))
        {
            source.PlayOneShot(hit);
            dying = true;
        }
    }
}
