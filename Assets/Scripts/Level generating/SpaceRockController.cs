using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRockController : MonoBehaviour {
    public int minResourceAmount, maxResourceAmount;
    public bool raided, collideWithRock, collideWithPlate, wellPlaced;
    public int health;
    public Sprite damage1, damage2;
    private AudioSource source;
    public AudioClip hit, explode;


    private int resourceAmount;
    private SpriteRenderer spriteRenderer;
    private bool exploding;
    private float deadCounter = 0f;
    
    // Use this for initialization
    void Start () {
        raided = false;
        exploding = false;
        health = 3;
        resourceAmount = Random.Range(minResourceAmount, maxResourceAmount);
        spriteRenderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }
    
    void FixedUpdate() {
        if (collideWithRock || collideWithPlate)
        {
            GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().generating = true;
        }

        if (!exploding)
        {
            switch (health)
            {
                case 3:
                    break;
                case 2:
                    if (spriteRenderer.sprite != damage1)
                    {
                        spriteRenderer.sprite = damage1;
                        source.PlayOneShot(hit);
                    }
                    break;
                case 1:
                    if (spriteRenderer.sprite != damage2)
                    {
                        spriteRenderer.sprite = damage2;
                        source.PlayOneShot(hit);
                    }
                    break;
                default:
                    exploding = true;
                    spriteRenderer.sprite = null;
                    source.PlayOneShot(explode);
                    break;
            }
        }

        if (health <= 0)
        {
            deadCounter++;
            GetComponent<PolygonCollider2D>().enabled = false;
        }

        if (deadCounter > 120)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            collideWithRock = true;
            wellPlaced = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            collideWithRock = false;
        }
    }

    public void GetDamaged()
    {
        health--;
    }

    public int GetDamageResource()
    {
        return resourceAmount / 10 + Random.Range(1, 5);
    }

    public int GetResourceAmount()
    {
        return resourceAmount;
    }
}
