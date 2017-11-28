using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
    public GameObject healthBar;
    public int startingHealth, currentHealth;
    public bool hit, isDead;
    public float invulnerabilityAlpha, invulnerabilityLength;
    public Color hitColor;
    public AudioClip hitSound;
    public GameObject dieText;

    private Color defaultColor;
    private bool invulnerability;
    private float invulnerabilityCounter;
    private int gottenDamage;

    // Use this for initialization
    void Start () {
        currentHealth = startingHealth;
        hit = false;
        invulnerability = true;
        isDead = false;
        gottenDamage = 0;
        defaultColor = GetComponent<Renderer>().material.color;
    }
	
    private void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            GetComponent<Animator>().SetBool("Dead", true);
            GetComponent<PlayerController>().isDead = true;
            dieText.SetActive(true);
        }

        if (hit && !invulnerability)
        {
            invulnerability = true;
            invulnerabilityCounter = invulnerabilityLength;

            GetComponent<PlayerController>().pPAnimator.SetBool("Hit", true);
            GetComponent<PlayerController>().source.PlayOneShot(hitSound);

            currentHealth -= gottenDamage;
            healthBar.GetComponent<HeartController>().ChangeHeartAmount(-gottenDamage);
            gottenDamage = 0;
            hit = false;
        }

        if (invulnerability)
        {
            Debug.Log("invulnerability active");

            if (invulnerabilityCounter <= 0)
            {
                Debug.Log("invulnerability DE-active");
                invulnerability = false;
                GetComponent<PlayerController>().pPAnimator.SetBool("Hit", false);
                gottenDamage = 0;
            }
        }
        invulnerabilityCounter -= Time.deltaTime;
    }

    public void GetHit(int damage, Vector2 knockback)
    {
        hit = true;
        gottenDamage = damage;
        StartCoroutine(HitFlasher());
        GetComponent<PlayerController>().GetKnockedBack(knockback);
    }

    public IEnumerator HitFlasher()
    {
        for (int i = 0; i < 5; i++)
        {
            GetComponent<Renderer>().material.color = hitColor;
            yield return new WaitForSeconds(.1f);
            GetComponent<Renderer>().material.color = defaultColor;
            yield return new WaitForSeconds(.1f);
        }
    }
}
