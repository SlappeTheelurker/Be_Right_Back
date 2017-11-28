using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    public float speed, followRadius, knockbackForce;
    public int attackPowerAmount;

    private float currentDistance;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
    }

    void Update()
    {
        currentDistance = Vector2.Distance(transform.position, target.GetComponent<Rigidbody2D>().transform.position);

        if (GameObject.Find("Player").gameObject.GetComponent<PlayerController>().isDead)
        {
            followRadius = 0;
        }

        if (currentDistance < followRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.GetComponent<Rigidbody2D>().transform.position, speed * Time.deltaTime);
            transform.GetComponent<Animator>().SetBool("InPursuit", true);
        }
        else
        {
            transform.GetComponent<Animator>().SetBool("InPursuit", false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("IK GA SLAAN");

            Vector2 knockback = (other.transform.position - transform.position).normalized * knockbackForce;
            other.GetComponent<HealthController>().GetHit(attackPowerAmount, knockback);
        }
    }
}
