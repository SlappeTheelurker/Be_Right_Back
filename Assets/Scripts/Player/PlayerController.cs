using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public Camera cam;
    public float baseMoveSpeed, dodgerollSpeed, dodgerollCooldownLength, dodgerollBreakSpeed, knockbackLength, knockbackBrake, diveKnockback;
    public int resourceAmount;
    public Text resourceAmountDisplay;
    public bool dodgerolling, isDead;
    public Animator pPAnimator;
    public AudioSource source;
    public AudioClip diveSound;
    public Weapon weapon;

    private Rigidbody2D myRigidbody;
    private bool moveVelocityActive;
    private float dodgerollCooldownCounter, knockbackCounter, collisionCounter, moveSpeed;
    private Animator animator;
    private Vector2 moveInput, lastInput, moveVelocity, mousePosition;

    //if a player already exists, destroy self
    private void Awake()
    {
        int playerCounter = 0;
        foreach (GameObject gameObj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObj.name == "Player")
            {
                playerCounter++;
            }
        }

        if (playerCounter >=2)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
        moveVelocityActive = false;
        DontDestroyOnLoad(transform.gameObject);
        animator = transform.GetComponent<Animator>();
        collisionCounter = 0;
        source = GetComponent<AudioSource>();
        isDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!dodgerolling)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            lastInput = moveInput;
        }
        else
        {
            moveInput = lastInput;
        }

        resourceAmountDisplay.text = "resources: " + resourceAmount;

        if (Input.GetKey(KeyCode.LeftShift) && moveInput != Vector2.zero)
        {
            dodgerolling = true;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            Destroy(GameObject.Find("UICanvas").gameObject);
            Destroy(GameObject.Find("SafeHouse").gameObject);
            Destroy(GameObject.Find("Music").gameObject);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        moveVelocity = moveInput.normalized * moveSpeed;

        knockbackCounter -= Time.deltaTime;
        if (knockbackCounter <= 0)
        {
            moveVelocityActive = true;
        }
        else
        {
            myRigidbody.velocity *= knockbackBrake;
        }

        if (moveVelocityActive && isDead == false)
        {
            myRigidbody.velocity = moveVelocity;
        }

        if (dodgerolling && dodgerollCooldownCounter <= 0)
        {
            moveSpeed = dodgerollSpeed;
            dodgerollCooldownCounter = dodgerollCooldownLength;
            animator.SetBool("Diving", true);
            source.PlayOneShot(diveSound);
        }

        if (dodgerollCooldownCounter > 0)
        {
            dodgerollCooldownCounter -= Time.deltaTime;
            if (dodgerollCooldownCounter < 0)
            {
                dodgerolling = false;
                animator.SetBool("Diving", false);
            }
        }

        if (moveSpeed > baseMoveSpeed)
        {
            moveSpeed -= dodgerollBreakSpeed;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }

        //Walking Forward
        if (moveInput.y < 0)
        {
            if (!animator.GetBool("Diving"))
            {
                animator.SetBool("WalkForward", true);
            }
        }
        else
        {
            animator.SetBool("WalkForward", false);
        }

        //Walking Backward
        if (moveInput.y > 0)
        {
            if (!animator.GetBool("WalkBack"))
            {
                animator.SetBool("WalkBack", true);
            }
        }
        else
        {
            animator.SetBool("WalkBack", false);
        }

        //Walking Right
        if (moveInput.x > 0 && moveInput.y == 0)
        {
            if (!animator.GetBool("WalkRight"))
            {
                animator.SetBool("WalkRight", true);
            }
        }
        else
        {
            animator.SetBool("WalkRight", false);
        }

        //Walking Left
        if (moveInput.x < 0 && moveInput.y == 0)
        {
            if (!animator.GetBool("WalkLeft"))
            {
                animator.SetBool("WalkLeft", true);
            }
        }
        else
        {
            animator.SetBool("WalkLeft", false);
        }

        pPAnimator.SetBool("GetItem", false);

        if (isDead)
        {
            pPAnimator.SetBool("Dead", true);
            animator.SetBool("WalkLeft", false);
            animator.SetBool("WalkForward", false);
            animator.SetBool("WalkRight", false);
            animator.SetBool("WalkBack", false);
            animator.SetBool("Diving", false);
            knockbackCounter = 5f;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (dodgerollCooldownCounter <= 0)
        {
            collisionCounter = 0;
        }

        //Diving against a rock
        if (other.gameObject.tag == "Rock" && other.gameObject.GetComponent<SpaceRockController>().wellPlaced
            && dodgerollCooldownCounter > 0 && collisionCounter <= 0)
        {
            other.gameObject.GetComponent<SpaceRockController>().GetDamaged();

            GetKnockedBack((transform.position - other.transform.position).normalized * diveKnockback);

            //get resources from rock
            int amountGotten = 0;
            if (other.gameObject.GetComponent<SpaceRockController>().health <= 0)
            {
                amountGotten = other.gameObject.GetComponent<SpaceRockController>().GetResourceAmount();
            }
            else
            {
                amountGotten = other.gameObject.GetComponent<SpaceRockController>().GetDamageResource();
                pPAnimator.SetBool("GetItem", true);
            }
            resourceAmount += amountGotten;

            //make text appear above resourceAmount
            GameObject amountChanged = Instantiate(Resources.Load("Prefabs/AmountChangeText"), GameObject.Find("Resource_Amount").transform) as GameObject;
            amountChanged.GetComponent<Text>().text = "+" + amountGotten;
        }
        collisionCounter++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionCounter = 0;
    }

    public void GetKnockedBack(Vector2 knockback)
    {
        myRigidbody.AddForce(knockback);
        knockbackCounter = knockbackLength;
        moveVelocityActive = false;
    }
}
