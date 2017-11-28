using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed, totalLifeTime, hitSoundLength;
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
    
    private void FixedUpdate()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= totalLifeTime)
        {
            Destroy(gameObject);
        }

        if (dying)
        {
            GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<BoxCollider2D>().enabled = false;
            dieCounter += Time.deltaTime;
            if (dieCounter > hitSoundLength)
            {
                Destroy(gameObject);
            }
        }

        transform.Translate(Vector3.up * speed * Time.deltaTime);
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
