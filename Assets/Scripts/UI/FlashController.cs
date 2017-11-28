using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashController : MonoBehaviour {
    private float opacity, dissapearSpeed;

	// Use this for initialization
	void Start () {
        opacity = 1.1f;
        dissapearSpeed = 0.02f;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, opacity);
        
        opacity -= dissapearSpeed;

        if (opacity <= 0)
        {
            Destroy(gameObject);
        }
    }
}
