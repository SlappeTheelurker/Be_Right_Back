using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlateScript : MonoBehaviour {
    public Sprite happyPlate;
    public Sprite angryPlate;
    public string state;
    public GameObject turret0, turret1, turret2, turret3;

    private List<GameObject> turrets;

    private Sprite neutralPlate;
    private SpriteRenderer thisSpriteRenderer;

	// Use this for initialization
	void Start () {
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        neutralPlate = thisSpriteRenderer.sprite;
        turrets = new List<GameObject>();
        turrets.Add(turret0);
        turrets.Add(turret1);
        turrets.Add(turret2);
        turrets.Add(turret3);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            thisSpriteRenderer.sprite = happyPlate;
            state = "happy";
        }

        if (collision.CompareTag("Enemy"))
        {
            thisSpriteRenderer.sprite = angryPlate;
            state = "angry";
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            foreach (GameObject turret in turrets)
            {
                turret.GetComponent<TurretController>().TargetAt(other.transform.position);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            thisSpriteRenderer.sprite = neutralPlate;
            state = "neutral";
        }

        if (collision.CompareTag("Enemy"))
        {
            thisSpriteRenderer.sprite = neutralPlate;
            state = "neutral";
        }
    }
}
